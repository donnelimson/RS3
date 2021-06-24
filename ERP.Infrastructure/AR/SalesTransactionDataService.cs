using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Infrastructure.Documents.AR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.AR
{
    public class SalesTransactionDataService : ISalesTransactionDataService, IDisposable
    {
        private DocumentDataContext _docCtx;

        private FinanceDataContext _finCtx;
     
        private Lazy<SalesQoutationService> _lazy_SalesQoutationService;

        private Lazy<SalesOrderService> _lazy_SalesOrderService;

        private Lazy<SalesDeliveryService> _lazy_SalesDeliveryService;

        private Lazy<SalesReturnService> _lazy_SalesReturnService;

        private Lazy<SalesInvoiceService> _lazy_SalesInvoiceService;

        private Lazy<SalesCreditNoteService> _lazy_SalesCreditNoteService;

        public SalesTransactionDataService()
        {
            _docCtx = new DocumentDataContext();

            _finCtx = new FinanceDataContext();

            _lazy_SalesQoutationService = new Lazy<SalesQoutationService>(() => new SalesQoutationService(_docCtx));

            _lazy_SalesOrderService = new Lazy<SalesOrderService>(() => new SalesOrderService(_docCtx));

            _lazy_SalesDeliveryService = new Lazy<SalesDeliveryService>(() => new SalesDeliveryService(_docCtx, _finCtx));

            _lazy_SalesReturnService = new Lazy<SalesReturnService>(() => new SalesReturnService(_docCtx, _finCtx));

            _lazy_SalesInvoiceService = new Lazy<SalesInvoiceService>(() => new SalesInvoiceService(_docCtx, _finCtx));

            _lazy_SalesCreditNoteService = new Lazy<SalesCreditNoteService>(() => new SalesCreditNoteService(_docCtx, _finCtx));
        }
        
        public SalesQoutationService SalesQoutationService { get { return _lazy_SalesQoutationService.Value ; } }

        public SalesOrderService SalesOrderService { get { return _lazy_SalesOrderService.Value ; } }

        public SalesDeliveryService SalesDeliveryService { get { return _lazy_SalesDeliveryService.Value; } }

        public SalesReturnService SalesReturnService { get { return _lazy_SalesReturnService.Value; } }

        public SalesInvoiceService SalesInvoiceService { get { return _lazy_SalesInvoiceService.Value; } }

        public SalesCreditNoteService SalesCreditNoteService { get { return _lazy_SalesCreditNoteService.Value ; } }

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
