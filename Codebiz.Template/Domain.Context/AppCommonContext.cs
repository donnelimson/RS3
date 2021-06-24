using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel;
using Codebiz.Domain.Common.Model.DataModel.Approval.ApprovalStage;
using Codebiz.Domain.Common.Model.DataModel.Collection;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DataModel.CSA.Billing.UnbundledTransaction;
using Codebiz.Domain.Common.Model.DataModel.CSA.SupportingDocument;
using Codebiz.Domain.Common.Model.DataModel.Financial.Currency;
using Codebiz.Domain.Common.Model.DataModel.Financials;
using Codebiz.Domain.Common.Model.DataModel.Inventory;
using Codebiz.Domain.Common.Model.DataModel.Notification;
using Codebiz.Domain.Common.Model.DataModel.Transaction;
using Codebiz.Domain.Common.Model.EnumBaseModels;
using Codebiz.Domain.Common.Model.Financial;
using Codebiz.Domain.ERP.Model;
using Codebiz.Domain.ERP.Model.Data.ERP;
using Codebiz.Domain.ERP.Model.EnumBaseModels.FAMMS;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Domain.Context
{
    public class AppCommonContext : DbContext
    {
        #region Fields

        private const string ConnectionString = "CodebizERP";

        #endregion

        #region Constructors

        public AppCommonContext()
            : this(ConnectionString)
        {
        }

        public AppCommonContext(string connectionString)
            : base(connectionString)
        {
            //Configuration.ProxyCreationEnabled = false;          

            //var objectContext = (this as IObjectContextAdapter).ObjectContext;
            //objectContext.CommandTimeout = 1200;
            //Database.Log = sql => Debug.Write(sql);
        }

        #endregion

        #region DbSet Fields
        #region rs3
        #region user related
        public IDbSet<Role> Roles { get; set; }

        #endregion
        #region ticket
        public IDbSet<Ticket> Tickets { get; set; }
        public IDbSet<Subticket> Subtickets { get; set; }
        public IDbSet<TicketComment> TicketComments { get; set; }
        public IDbSet<TicketAttachment> TicketAttachments { get; set; }
        #endregion
        #endregion

        public IDbSet<Log> Logs { get; set; }
        public IDbSet<Purpose> Purposes { get; set; }
        public IDbSet<PurposeDetail> PurposeDetails { get; set; }
        public IDbSet<TransactionNumbering> TransactionNumberings { get; set; }
        public IDbSet<AppUser> AppUsers { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<UserGroup> UserGroups { get; set; }
        public IDbSet<Permission> Permissions { get; set; }
        public IDbSet<PermissionGroup> PermissionGroups { get; set; }
        public IDbSet<NavLink> NavLinks { get; set; }
        public IDbSet<Region> Regions { get; set; }
        public IDbSet<Province> Provinces { get; set; }
        public IDbSet<CityTown> CityTowns { get; set; }
        public IDbSet<Pole> Poles { get; set; }
        public IDbSet<PoleNumberSeed> PoleNumberSeeds { get; set; }
        public IDbSet<Office> Offices { get; set; }
        public IDbSet<Position> Positions { get; set; }
        public IDbSet<Sitio> Sitios { get; set; }
        public IDbSet<Route> Routes { get; set; }
        public IDbSet<ApprovalStage> ApprovalStages { get; set; }
        public IDbSet<ApprovalTemplate> ApprovalTemplates { get; set; }
        public IDbSet<ApprovalTemplateOriginator> ApprovalTemplateOriginators { get; set; }
        public IDbSet<ApprovalTemplateTransaction> ApprovalTemplateTransactionsData { get; set; }
        public IDbSet<ApprovalTemplateStages> ApprovalTemplateStageData { get; set; }
        public IDbSet<ApprovalStageApprover> ApprovalStageApprovers { get; set; }
        public IDbSet<ConfigSettingDataType> ConfigSettingDataTypes { get; set; }
        public IDbSet<ConfigSettingGroup> ConfigSettingGroups { get; set; }
        public IDbSet<ConfigSetting> ConfigSettings { get; set; }
        public IDbSet<AccessLevel> AccessLevels { get; set; }
        public IDbSet<TransactionType> ApprovalTemplateTransactions {get;set;}
        public IDbSet<TransactionTypeCategory> TransactionTypeCategories { get; set; }
        public IDbSet<LogEventTitle> LogEventTitles { get; set; }
        public IDbSet<ApprovalStatus> ApprovalStatuses { get; set; }
        public IDbSet<LeaveApprovalStatus> LeaveApprovalStatuses { get; set; }
        public IDbSet<Approval> Approvals { get; set; }
        public IDbSet<ApprovalApproverStage> ApprovalApproverStages { get; set; }
        public IDbSet<ApprovalApprover> ApprovalApprovers { get; set; }
        public IDbSet<ApproverLabel> ApproverLabels { get; set; }

        public IDbSet<ContentFileType> ContentFileType { get; set; }
        public IDbSet<FileType> FileType { get; set; }
        public IDbSet<ContentFile> ContentFile { get; set; }

        public IDbSet<Barangay> Barangays { get; set; }

        public IDbSet<Purok> Puroks { get; set; }


        public IDbSet<LeaveCreditMonetizationApprovalStatus> LeaveCreditMonetizationApprovalStatuses { get; set; }


        public IDbSet<BrandType> BrandTypes { get; set; }
        public IDbSet<LoginHistory> LoginHistories { get; set; }
        public IDbSet<ComplaintType> ComplaintTypes { get; set; }

        public IDbSet<ComplaintSubType> ComplaintSubTypes { get; set; }
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<DepartmentDetail> DepartmentDetails { get; set; }
        public IDbSet<DivisionType> DivisionTypes { get; set; }
        public IDbSet<Division> Divisions { get; set; }
        public IDbSet<DivisionCategory> DivisionCategories { get; set; }

        public IDbSet<NoOfUnitsAndKvaRating> NoOfUnitsAndKvaRatings { get; set; }
        public IDbSet<EmployeePhoto> EmployeePhotos { get; set; }
        public IDbSet<BillingUnit> BillingUnits { get; set; }
        public IDbSet<BillingMonthlyRate> BillingMonthlyRates { get; set; }
        public IDbSet<BillingMonthlyRateUnbundledTransaction> BillingMonthlyRateUnbundledTransactions { get; set; }

        public IDbSet<BillingUnbundledTransaction> BillingUnbundledTransactions { get; set; }
        public IDbSet<SupportingDocument> SupportingDocuments { get; set; }
        public IDbSet<TransactionSubType> TransactionSubTypes { get; set; }
        public IDbSet<SupportingDocumentDetail> SupportingDocumentDetails { get; set; }
        public IDbSet<ConsumerClass> ConsumerClasses { get; set; }
        public IDbSet<Surcharge> Surcharges { get; set; }
        public IDbSet<WorkOrder> WorkOrders { get; set; }
        public IDbSet<SubStation> SubStations { get; set; }
        public IDbSet<Feeder> Feeders { get; set; }
        public IDbSet<OfficeDepartment> OfficeDepartments { get; set; }
        public IDbSet<MeterReadingRemark> MeterReadingRemarks { get; set; }
        public IDbSet<TaxType> TaxTypes { get; set; }
        public IDbSet<Volume> Volumes { get; set; }
        public IDbSet<PackagingType> PackagingTypes { get; set; }
        //public IDbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public IDbSet<Manufacturer> Manufacturers { get; set; }
        public IDbSet<FinancialProject> FinancialProjects { get; set; }

        //Financial
        public IDbSet<Currency> Currencies { get; set; }

        //Currency
        public IDbSet<ISOCurrencyCode> ISOCurrencyCodes { get; set; }
        public IDbSet<Rounding> Roundings { get; set; }
        public IDbSet<Decimals> Decimals { get; set; }

        //Job Order
        public IDbSet<JobOrderTaskToBePerform> JobOrderTaskToBePerforms { get; set; }
        public IDbSet<JobOrderTaskToBePerFormDetail> JobOrderTaskToBePerFormDetails { get; set; }

        public IDbSet<JobOrderNatureType> JobOrderNatureTypes { get; set; }



        //LifelineSubsidy
        public IDbSet<LifelineSubsidy> LifelineSubsidies { get; set; }



        //Sales Employee
        public IDbSet<SalesEmployee> SalesEmployees { get; set; }

        public IDbSet<DocumentHistory> DocumentHistories { get; set; }

 

        //Notification
        public IDbSet<NotificationDetail> NotificationDetails { get; set; }
        public IDbSet<Notification> Notifications { get; set; }

        //Report Signatory
        public IDbSet<ReportSignatory> ReportSignatories { get; set; }
        public IDbSet<ReportSignatoryDetail> ReportSignatoryDetails { get; set; }
        public IDbSet<ReportCategory> ReportCategories { get; set; }
        public IDbSet<Report> Reports { get; set; }

        #endregion

        #region DbContext Events

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<Member>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<AppUser>()
                .HasMany<UserGroup>(s => s.UserGroups)
                .WithMany(c => c.AppUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("AppUserId");
                    cs.MapRightKey("UserGroupId");
                    cs.ToTable("AppUserUserGroup");
                });

            //modelBuilder.Entity<UserGroup>()
            //     .HasMany<NavLink>(s => s.Navlinks)
            //     .WithMany(c => c.UserGroups)
            //     .Map(cs =>
            //     {
            //         cs.MapLeftKey("UserGroupId");
            //         cs.MapRightKey("NavLinkId");
            //         cs.ToTable("UserGroupNavLink");
            //     });

            modelBuilder.Entity<EmployeePhoto>()
                            .HasRequired<Employee>(a => a.Employee)
                            .WithMany(a => a.EmployeePhotos);


            modelBuilder.Entity<UserGroup>()
                .HasMany<Permission>(s => s.Permissions)
                .WithMany(c => c.UserGroups)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserGroupId");
                    cs.MapRightKey("PermissionId");
                    cs.ToTable("UserGroupPermission");
                });

            //modelBuilder.Entity<ApprovalStage>()
            //    .HasMany<AppUser>(s => s.AppUsers)
            //    .WithMany(c => c.ApprovalStages)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("ApprovalStageId");
            //        cs.MapRightKey("AppUserId");
            //        cs.ToTable("ApprovalStageAppUser");
            //    });

            modelBuilder.Entity<AppUser>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<AppUser>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);

            modelBuilder.Entity<BillingUnbundledTransactionForVatDetails>().HasRequired(a => a.BillingUnbundledTransactionForVat).WithMany().HasForeignKey(a => a.BillingUnbundledTransactionForVatId);
            modelBuilder.Entity<BillingUnbundledTransactionForDiscountDetails>().HasRequired(a => a.BillingUnbundledTransactionForDiscount).WithMany().HasForeignKey(a => a.BillingUnbundledTransactionForDiscountId);
            #region Unit Mapping
            #endregion

            #region SubUnit Mapping
            #endregion

            #region Region Mapping
            modelBuilder.Entity<Region>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<Region>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);
            #endregion

            #region Province Mapping
            modelBuilder.Entity<Province>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<Province>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);
            modelBuilder.Entity<Province>().HasOptional(g => g.Region).WithMany().HasForeignKey(g => g.RegionId);
            #endregion

            #region CityTown Mapping
            modelBuilder.Entity<CityTown>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<CityTown>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);
            modelBuilder.Entity<CityTown>().HasOptional(g => g.Province).WithMany().HasForeignKey(g => g.ProvinceId);
            #endregion

            #region ConfigSetting Mapping
            modelBuilder.Entity<ConfigSetting>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<ConfigSetting>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);
            modelBuilder.Entity<ConfigSetting>().HasOptional(g => g.ConfigSettingDataType).WithMany().HasForeignKey(g => g.ConfigSettingDataTypeId);
            modelBuilder.Entity<ConfigSetting>().HasOptional(g => g.ConfigSettingGroup).WithMany().HasForeignKey(g => g.ConfigSettingGroupId);
            #endregion

            #region DesignationGroup Mapping
            #endregion

            #region Content File
            modelBuilder.Entity<ContentFile>().HasOptional(a => a.ContentFileType).WithMany().HasForeignKey(a => a.ContentFileTypeId);
            modelBuilder.Entity<ContentFile>().HasOptional(a => a.FileType).WithMany().HasForeignKey(a => a.FileTypeId);
            modelBuilder.Entity<ContentFile>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<ContentFile>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);
            #endregion

        }

        #endregion

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var errors = new List<string>();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        var error = string.Format("Class: {0}, Property: {1}, Error: {2}.",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                        errors.Add(error);
                        Trace.TraceInformation(error);
                    }
                }

                throw new System.Exception(string.Join(System.Environment.NewLine, errors), dbEx);
            }
            catch (DbUpdateException dbx)
            {
                throw;
            }
        }
    }
}