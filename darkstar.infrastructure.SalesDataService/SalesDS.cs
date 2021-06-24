using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Core.ERP.Data.Documents;
using Codebiz.Core.ERP.Data.Documents.Sales;

namespace Codebiz.Infrastructure.SalesDataService
{
    public class SalesDS : IUnitOfWork 
    {
        private SalesDataContext _salesContext;
        public Lazy<SalesOrderRepository> SalesOrderRepo { get; } 
        public Lazy<SalesDeliveryRepository> SalesDeliveryRepo { get; } 
        public Lazy<SalesInvoiceRepository> SalesInvoiceRepo{ get; } 
        public Lazy<SalesCreditNoteRepository> SalesCreditNoteRepository { get; } 
        public SalesDS()
        {
            _salesContext = new SalesDataContext();

            SalesOrderRepo= new Lazy<SalesOrderRepository>(() => new SalesOrderRepository(_salesContext));
            SalesDeliveryRepo = new Lazy<SalesDeliveryRepository>(() => new SalesDeliveryRepository(_salesContext));
            SalesInvoiceRepo = new Lazy<SalesInvoiceRepository>(() => new SalesInvoiceRepository(_salesContext));
            SalesCreditNoteRepository = new Lazy<SalesCreditNoteRepository>(() => new SalesCreditNoteRepository(_salesContext));
        }
        public int Commit()
        {
           return _salesContext.SaveChanges();
        }
    }
}
