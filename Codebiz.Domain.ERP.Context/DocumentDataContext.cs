using Codebiz.Domain.ERP.Model.Data.Documents.Purchases;
using Codebiz.Domain.ERP.Model.Data.Documents.Sales;
using Codebiz.Domain.ERP.Model.Data.Payments.IncommingPayments;
using Codebiz.Domain.ERP.Model.Data.Payments.OutgoingPayments;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Codebiz.Domain.ERP.Context
{
    public class DocumentDataContext : DbContext
    {
        public DocumentDataContext()
            : base("CodebizERP")
        {
        }

        #region Purchases Fields

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrder_Line> PurchaseOrder_Lines { get; set; }

        public DbSet<PurchaseDelivery> PurchaseDeliveries { get; set; }
        public DbSet<PurchaseDelivery_Line> PurchaseDelivery_Lines { get; set; }

        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoice_Line> PurchaseInvoice_Lines { get; set; }

        public DbSet<PurchaseQuotation> PurchaseQoutations { get; set; }
        public DbSet<PurchaseQuotation_Line> PurchaseQoutation_Lines { get; set; }

        public DbSet<PurchaseCreditNote> PurchaseCreditNotes { get; set; }
        public DbSet<PurchaseCreditNote_Line> PurchaseCreditNote_Lines { get; set; }

        #endregion

        #region Sales Fields
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrder_Line> SalesOrder_Lines { get; set; }

        public DbSet<SalesDelivery> SalesDeliveries { get; set; }
        public DbSet<SalesDelivery_Line> SalesDelivery_Lines { get; set; }

        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoice_Line> SalesInvoice_Lines { get; set; }

        public DbSet<SalesQoutation> SalesQoutations { get; set; }
        public DbSet<SalesQoutation_Line> SalesQoutation_Lines { get; set; }

        public DbSet<SalesCreditNote> SalesCreditNotes { get; set; }
        public DbSet<SalesCreditNote_Line> SalesCreditNote_Lines { get; set; }
        #endregion

        #region Payment Fields
        public DbSet<IncomingPayment> IncomingPayment { get; set; }
        public DbSet<OutgoingPayment> OutgoingPayment { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<double>().Configure(x => x.HasPrecision(19, 6));
            modelBuilder.Properties<decimal>().Configure(x => x.HasPrecision(19, 6));
        }
    }
}
