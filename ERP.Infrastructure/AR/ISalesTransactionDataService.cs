using Codebiz.Domain.ERP.Infrastructure.Documents.AR;

namespace Codebiz.Domain.ERP.Infrastructure.AR
{
    public interface ISalesTransactionDataService
    {
        SalesCreditNoteService SalesCreditNoteService { get; }

        SalesDeliveryService SalesDeliveryService { get; }

        SalesInvoiceService SalesInvoiceService { get; }

        SalesOrderService SalesOrderService { get; }

        SalesQoutationService SalesQoutationService { get; }

        SalesReturnService SalesReturnService { get; }
    }
}