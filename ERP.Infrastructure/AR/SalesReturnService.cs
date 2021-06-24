using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data.Documents.Sales;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Model.Data.Payments.IncommingPayments;
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
    public class SalesReturnService : IDisposable
    {
        private SalesReturnRepository _salRepository;

        private IncomingPaymentRepository _rctRepository;

        private JournalEntryRepository _jenRepository;

        private DocumentDataContext _docCtx;
        private FinanceDataContext _finCtx;

        public SalesReturnService(DocumentDataContext docCtx, FinanceDataContext finCtx)
        {
            _docCtx = docCtx;
            _finCtx = finCtx;


            _salRepository = new SalesReturnRepository(_docCtx);

            _rctRepository = new IncomingPaymentRepository(_docCtx);

            _jenRepository = new JournalEntryRepository(_finCtx);

        }

        public SalesReturn Add(SalesReturn p1) => _salRepository.Add(p1);

        public SalesReturn SaveOrUpdate(SalesReturn p1) => _salRepository.SaveOrUpdate(p1);

        public IQueryable<SalesReturn> GetList(Expression<Func<SalesReturn, bool>> q) => _salRepository.GetList(q);

        public IQueryable<SalesReturn> GetListEx(Expression<Func<SalesReturn, bool>> q, params string[] _includes) => _salRepository.GetListEx(q, _includes);

        public int Commit() => _salRepository.Commit();


        /*Custom functions*/

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
