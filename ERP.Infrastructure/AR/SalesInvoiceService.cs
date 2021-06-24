using AutoMapper;
using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Infrastructure.AR.Model;
using Codebiz.Domain.ERP.Model.Data.Documents.Sales;
using Codebiz.Domain.ERP.Model.Data.Payments.IncommingPayments;
using Codebiz.Domain.ERP.Model.Data.Payments.OutgoingPayments;
using Codebiz.Domain.ERP.Repository.Documents;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Codebiz.Domain.ERP.Infrastructure.Documents.AR
{
    public class SalesInvoiceService : IDisposable
    {
        private SalesInvoiceRepository _sinRepository;
        
        private SalesCreditNoteRepository _scrRepository;

        private IncomingPaymentRepository _spyRepository;
        
        private OutgoingPaymentRepository _ppyRepository;

        private JournalEntryRepository _jenRepository;

        private DocumentDataContext _docCtx;

        private FinanceDataContext _finCtx;

        public SalesInvoiceService(DocumentDataContext docCtx, FinanceDataContext finCtx)
        {
            _docCtx = docCtx;

            _finCtx = finCtx;

            _sinRepository = new SalesInvoiceRepository(_docCtx);
            _scrRepository = new SalesCreditNoteRepository(_docCtx);

            _spyRepository = new IncomingPaymentRepository(_docCtx);
            _ppyRepository = new OutgoingPaymentRepository(_docCtx);

            _jenRepository = new JournalEntryRepository(_finCtx);

        }

        public SalesInvoice Add(SalesInvoice p1) => _sinRepository.Add(p1);

        public SalesInvoice SaveOrUpdate(SalesInvoice p1) => _sinRepository.SaveOrUpdate(p1);

        public IQueryable<SalesInvoice> GetList(Expression<Func<SalesInvoice, bool>> q) => _sinRepository.GetList(q);

        public IQueryable<SalesInvoice> GetListEx(Expression<Func<SalesInvoice, bool>> q, params string[] _includes) => _sinRepository.GetListEx(q, _includes);

        public int Commit() => _sinRepository.Commit();


        /*Custom functions*/
        public int AddInvoicePayment(ARInvoicePaymentDTO m)
        {
            try
            {

                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<ARInvoicePaymentDTO, SalesInvoice>();
                    c.CreateMap<ARInvoicePaymentDTO, List<IncomingPayment>>();
                });

                var mapper = cfg.CreateMapper();

                var arInvoice = mapper.Map<SalesInvoice>(m);

                var payments = mapper.Map<List<IncomingPayment>>(m.Payments);

                var invoice = _sinRepository.Add(arInvoice);

                _docCtx.SaveChanges();

                if (invoice.DocID  > 0)
                {
                    var inv = _sinRepository.GetListEx(x => x.DocID == invoice.DocID ).FirstOrDefault();
                    if (null != inv)
                    {

                        // todo add je for invoice
                        //var sin_je = new JournalEntry();
                        //var sin_je_lines = new List<JournalEntry_Line>();

                        payments.ForEach(x =>
                        {
                            x.CardCode = inv.CardCode;
                            x.CardName = inv.CardName;
                            x.DocTotal = x.CashSum + x.CheckSum + x.TrsfrSum;

                            var payment_line = x.PaymentLines.FirstOrDefault();

                            if (payment_line != null)
                            {
                                payment_line.DocID = invoice.DocID;
                                payment_line.InvType = "SIN";
                                payment_line.InstId = 1;
                            }

                            var payment = _spyRepository.Add(x);
                            if (payment.PaymentID > 0)
                            {
                                //todo add add JE for Payment
                                //todo add recon doc for invoice and payment
                            }
                        });
                         
                        //commit changes
                        _docCtx.SaveChanges();
                        _finCtx.SaveChanges();
                    }
                }

                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public ARInvoicePaymentDTO ARInvoicePayment(string numAtCard)
        {
            try
            {
                var invoice = _sinRepository.GetListEx(x => x.NumAtCard == numAtCard).FirstOrDefault();

                if(invoice != null)
                {

                }

                return null;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
