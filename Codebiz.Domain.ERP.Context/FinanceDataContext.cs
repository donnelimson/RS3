using Codebiz.Domain.ERP.Model.Data.Reconcilations;
using Codebiz.Domain.ERP.Model.Data.Financials;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.ERP.Model.DataModel.Financials;

namespace Codebiz.Domain.ERP.Context
{
    public class FinanceDataContext : DbContext
    {
        public FinanceDataContext()
            : base("CodebizERP")
        {
        }

        #region DbSet Fields

        public DbSet<Project> Projects { get; set; }

        public DbSet<GLAccount> GLAccounts { get; set; }

        public DbSet<GLAccountSegment> GLAccountSegments { get; set; }

        public DbSet<GLAccountSegmentDetail> GLAccountSegmentDetails { get; set; }

        public DbSet<FinancialPeriod> FinancialPeriods { get; set; }

        public DbSet<PeriodIndicator> PeriodIndicators { get; set; }

        public DbSet<TaxGroup> TaxGroups { get; set; }

        public DbSet<JournalEntry> JournalEntries { get; set; }

        public DbSet<JournalEntry_Line> JournalEntry_Lines { get; set; }

        public DbSet<JournalVoucher> JournalVoucherEntries { get; set; }

        public DbSet<JournalVoucher_Line> JournalVoucher_Lines { get; set; }

        public DbSet<InternalReconcilation> InternalReconcilations { get; set; }

        public DbSet<InternalReconcilation_Line> InternalReconcilation_Lines { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Properties<double>().Configure(x => x.HasPrecision(19, 6));

            //modelBuilder.Properties<decimal>().Configure(x => x.HasPrecision(19, 6));
            modelBuilder.Entity<AppUser>()
            .HasMany<UserGroup>(s => s.UserGroups)
            .WithMany(c => c.AppUsers)
            .Map(cs =>
            {
                cs.MapLeftKey("AppUserId");
                cs.MapRightKey("UserGroupId");
                cs.ToTable("AppUserUserGroup");
            });
            modelBuilder.Entity<AppUserPhoto>()
                .HasRequired<AppUser>(a => a.AppUser)
                .WithMany(a => a.AppUserPhotos);


            modelBuilder.Entity<AppUser>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<AppUser>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);
        }
    }
}
