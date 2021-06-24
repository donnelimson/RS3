using AutoMapper;
using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Infrastructure.AR.Model;
using Codebiz.Domain.ERP.Model.Data.Documents.Sales;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Model.Data.Payments.IncommingPayments;
using Codebiz.Domain.ERP.Model.Data.Payments.OutgoingPayments;
using Codebiz.Domain.ERP.Repository.Documents;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Documents.AR
{
    public class SalesCreditNoteService : IDisposable
    {
        private SalesCreditNoteRepository _scrRepository;

        private OutgoingPaymentRepository _ppyRepository;

        private JournalEntryRepository _jenRepository;

        private DocumentDataContext _docCtx;
        private FinanceDataContext _finCtx;

        public SalesCreditNoteService(DocumentDataContext docCtx, FinanceDataContext finCtx)
        {
            _docCtx = docCtx;
            _finCtx = finCtx;


            _scrRepository = new SalesCreditNoteRepository(_docCtx);

            _ppyRepository = new OutgoingPaymentRepository(_docCtx);

            _jenRepository = new JournalEntryRepository(_finCtx);

        }

        public SalesCreditNote Add(SalesCreditNote p1) => _scrRepository.Add(p1);

        public SalesCreditNote SaveOrUpdate(SalesCreditNote p1) => _scrRepository.SaveOrUpdate(p1);

        public IQueryable<SalesCreditNote> GetList(Expression<Func<SalesCreditNote, bool>> q) => _scrRepository.GetList(q);

        public IQueryable<SalesCreditNote> GetListEx(Expression<Func<SalesCreditNote, bool>> q, params string[] _includes) => _scrRepository.GetListEx(q, _includes);

        public int Commit() => _scrRepository.Commit();


        /*Custom functions*/

        public int AddCreditNotePayment(ARCreditNotePaymentDTO m)
        {
            try
            {

                //var arInvoice = new SalesInvoice();
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<ARCreditNotePaymentDTO, SalesCreditNote>();
                    c.CreateMap<ARCreditNotePaymentDTO, List<OutgoingPayment>>();
                });

                var mapper = cfg.CreateMapper();

                var arCreditNote = mapper.Map<SalesCreditNote>(m);

                var payments = mapper.Map<List<OutgoingPayment>>(m.Payments);

                var invoice = _scrRepository.Add(arCreditNote);

                _docCtx.SaveChanges();

                if (invoice.DocID > 0)
                {
                    var inv = _scrRepository.GetListEx(x => x.DocID == invoice.DocID).FirstOrDefault();
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
                                payment_line.InvType = "SCR";
                                payment_line.InstId = 1;
                            }

                            var payment = _ppyRepository.Add(x);
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
