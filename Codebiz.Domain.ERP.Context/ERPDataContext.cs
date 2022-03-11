using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA.Billing.UnbundledTransaction;
using Codebiz.Domain.ERP.Model;
using ERP.Model.DataModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Codebiz.Domain.ERP.Context
{
    public class ERPDataContext : DbContext
    {
        public ERPDataContext() : base("CodebizERP") { }

        #region AppCommon

        public IDbSet<AppUser> AppUsers { get; set; }
        public IDbSet<UserGroup> UserGroups { get; set; }
        public IDbSet<Permission> Permissions { get; set; }
        public IDbSet<PermissionGroup> PermissionGroups { get; set; }
        public IDbSet<ApprovalStage> ApprovalStages { get; set; }
        public IDbSet<Region> Regions { get; set; }
        public IDbSet<AccessLevel> AccessLevels { get; set; }
        public IDbSet<Position> Positions { get; set; }
        public IDbSet<Office> Offices { get; set; }
        public IDbSet<Codebiz.Domain.Common.Model.Department> Departments { get; set; }

        #endregion
        public IDbSet<ItemMaster> ItemMasters { get; set; }
        public IDbSet<PriceList> PriceLists { get; set; }
        public IDbSet<Brand> Brands { get; set; }
        public IDbSet<PriceListItemMaster> PriceListItemMasters { get; set; }
        public IDbSet<BrandItemMaster> BrandItemMasters { get; set; }
        public IDbSet<SaleTransaction> SaleTransactions { get; set; }
        public IDbSet<SaleTransactionDetail> SaleTransactionDetails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<decimal>().Configure(x => x.HasPrecision(19, 6));

            modelBuilder.Entity<AppUser>()
             .HasMany<UserGroup>(s => s.UserGroups)
             .WithMany(c => c.AppUsers)
             .Map(cs =>
             {
                 cs.MapLeftKey("AppUserId");
                 cs.MapRightKey("UserGroupId");
                 cs.ToTable("AppUserUserGroup");
             });

            modelBuilder.Entity<UserGroup>()
                .HasMany<Permission>(s => s.Permissions)
                .WithMany(c => c.UserGroups)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserGroupId");
                    cs.MapRightKey("PermissionId");
                    cs.ToTable("UserGroupPermission");
                });

            modelBuilder.Entity<AppUser>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<AppUser>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);

            modelBuilder.Entity<BillingUnbundledTransactionForVatDetails>().HasRequired(a => a.BillingUnbundledTransactionForVat).WithMany().HasForeignKey(a => a.BillingUnbundledTransactionForVatId);
            modelBuilder.Entity<BillingUnbundledTransactionForDiscountDetails>().HasRequired(a => a.BillingUnbundledTransactionForDiscount).WithMany().HasForeignKey(a => a.BillingUnbundledTransactionForDiscountId);
        }
    }
}
