using Codebiz.Domain.ERP.Model.Data.Inventory;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Codebiz.Domain.ERP.Context
{
    public class ItemDataContext : DbContext
    {
        public ItemDataContext()
            : base("CodebizERP")
        {
        }

        #region DbSet Fields

        public DbSet<ItemMaster> MasterItems { get; set; }

        public DbSet<ItemGroup> ItemGroups { get; set; }

        public DbSet<PriceList> PriceList { get; set; }

        public DbSet<Warehouse> Warehouse { get; set; }

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
