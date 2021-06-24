using Codebiz.Domain.ERP.Model.Data.Accounts;
using Codebiz.Domain.ERP.Model.Data.DocNumbering;
using Codebiz.Domain.ERP.Model.DataModel.BusinessPartners;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Codebiz.Domain.ERP.Context
{
    public class AccountDataContext : DbContext
    {
        public AccountDataContext()
            : base("CodebizERP")
        {
            Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = true;

        }

        #region DbSet Fields

        public DbSet<BusinessPartner> BusinessPartners { get; set; }

        public DbSet<BusinessPartnerAddress> BusinessPartnerAddresses { get; set; }

        public DbSet<BusinessPartnerContactPerson> BusinessPartnerContactPersons { get; set; }

        public DbSet<PaymentTerm> PaymentTerms { get; set; }

        public DbSet<PaymentTermDetail> PaymentTermDetails { get; set; }

        public DbSet<SalesPerson> SalesPersons { get; set; }

        public DbSet<ShipType> ShipTypes { get; set; }

        public DbSet<BusinessPartnerGroup> BusinessPartnerGroups { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<DocNumbering> DocNumbers { get; set; }

        public DbSet<DocNumberingDetail> DocNumberingDetails { get; set; }



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
