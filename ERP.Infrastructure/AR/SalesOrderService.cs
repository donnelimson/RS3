using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data.Documents.Sales;
using Codebiz.Domain.ERP.Repository.Documents;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Codebiz.Domain.ERP.Infrastructure.Documents.AR
{
    public class SalesOrderService : IDisposable
    {
        private SalesOrderRepository _sorRepository;
         

        private DocumentDataContext _docCtx;

        public SalesOrderService(DocumentDataContext docCtx)
        {
            _docCtx = docCtx;

            _sorRepository = new SalesOrderRepository(_docCtx);
        }

        public SalesOrder Add(SalesOrder p1) => _sorRepository.Add(p1);

        public SalesOrder SaveOrUpdate(SalesOrder p1) => _sorRepository.SaveOrUpdate(p1);

        public IQueryable<SalesOrder> GetList(Expression<Func<SalesOrder, bool>> q) => _sorRepository.GetList(q);

        public IQueryable<SalesOrder> GetListEx(Expression<Func<SalesOrder, bool>> q, params string[] _includes) => _sorRepository.GetListEx(q, _includes);

        public int Commit() => _sorRepository.Commit();

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
