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
    public class SalesQoutationService : IDisposable
    {
        private SalesQoutationRepository _squRepository;

    
        private DocumentDataContext _docCtx;

        public SalesQoutationService(DocumentDataContext docCtx)
        {
            _docCtx = docCtx;
           
            _squRepository = new SalesQoutationRepository(_docCtx);
        }

        public SalesQoutation Add(SalesQoutation p1) => _squRepository.Add(p1);

        public SalesQoutation SaveOrUpdate(SalesQoutation p1) => _squRepository.SaveOrUpdate(p1);

        public IQueryable<SalesQoutation> GetList(Expression<Func<SalesQoutation, bool>> q) => _squRepository.GetList(q);

        public IQueryable<SalesQoutation> GetListEx(Expression<Func<SalesQoutation, bool>> q, params string[] _includes) => _squRepository.GetListEx(q, _includes);

        public int Commit() => _squRepository.Commit();


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
