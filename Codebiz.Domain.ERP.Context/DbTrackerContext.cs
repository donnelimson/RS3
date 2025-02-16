﻿using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel;
using Codebiz.Domain.Common.Model.DataModel.Approval.ApprovalStage;
using Codebiz.Domain.Common.Model.DataModel.Collection;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DataModel.CSA.Billing.UnbundledTransaction;
using Codebiz.Domain.Common.Model.DataModel.CSA.SupportingDocument;
using Codebiz.Domain.Common.Model.DataModel.Financial.Currency;
using Codebiz.Domain.Common.Model.DataModel.Financials;
using Codebiz.Domain.Common.Model.DataModel.Inventory;
using Codebiz.Domain.Common.Model.DataModel.Log;
using Codebiz.Domain.Common.Model.DataModel.Notification;
using Codebiz.Domain.Common.Model.DataModel.SalesEmployee;
using Codebiz.Domain.Common.Model.DataModel.Transaction;
using Codebiz.Domain.Common.Model.EnumBaseModels;
using Codebiz.Domain.Common.Model.Financial;
using Codebiz.Domain.ERP.Model;
using Codebiz.Domain.ERP.Model.Data.ERP;
using Codebiz.Domain.ERP.Model.Data.ERP.EnumBaseModels;
using Codebiz.Domain.ERP.Model.EnumBaseModels;
using Codebiz.Domain.ERP.Model.EnumBaseModels.CSA.Request;
using Codebiz.Domain.ERP.Model.EnumBaseModels.CSA.Transformer;
using Codebiz.Domain.ERP.Model.EnumBaseModels.FAMMS;
using Codebiz.Domain.ERP.Model.EnumBaseModels.Inventory.PriceList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Codebiz.Domain.ERP.Context
{
    public class DbTrackerContext : DbContext
    {
        #region Constructor

        public DbTrackerContext()
            : base("CodebizERP")
        {
        }

        #endregion

        #region DbSet Fields    

        #region Common

        public IDbSet<Log> Logs { get; set; }
        public IDbSet<LogAttachment> LogAttachments { get; set; }
        public IDbSet<AppUser> AppUsers { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<UserGroup> UserGroups { get; set; }
        public IDbSet<Permission> Permissions { get; set; }
        public IDbSet<PermissionGroup> PermissionGroups { get; set; }
        public IDbSet<NavLink> NavLinks { get; set; }
        public IDbSet<Region> Regions { get; set; }
        public IDbSet<Province> Provinces { get; set; }
        public IDbSet<CityTown> CityTowns { get; set; }
        public IDbSet<Barangay> Barangays { get; set; }
        public IDbSet<Purok> Puroks { get; set; }
        public IDbSet<Office> Offices { get; set; }
        public IDbSet<OfficeDepartment> OfficeDepartments { get; set; }
        public IDbSet<DivisionCategory> DivisionCategories { get; set; }
        public IDbSet<DivisionType> DivisionTypes { get; set; }
        public IDbSet<Division> Divisions { get; set; }
        public IDbSet<Position> Positions { get; set; }
        public IDbSet<Sitio> Sitios { get; set; }
        public IDbSet<Pole> Poles { get; set; }
        public IDbSet<Route> Routes { get; set; }
        public IDbSet<PoleNumberSeed> PoleNumberSeeds { get; set; }
        public IDbSet<Approval> Approvals { get; set; }
        public IDbSet<ApprovalApprover> ApprovalApprovers { get; set; }
        public IDbSet<ApprovalApproverStage> ApprovalApproverStages { get; set; }
        public IDbSet<ApprovalStageApprover> ApprovalStageApprovers { get; set; }
        public IDbSet<ApprovalStage> ApprovalStages { get; set; }
        public IDbSet<ApprovalTemplate> ApprovalTemplates { get; set; }
        public IDbSet<ApprovalTemplateOriginator> ApprovalTemplateOriginators { get; set; }

        public IDbSet<ApproverLabel> ApproverLabels { get; set; }
        public IDbSet<AccessLevel> AccessLevels { get; set; }
        public IDbSet<LogEventTitle> LogEventTitles { get; set; }
        public IDbSet<ConfigSettingDataType> ConfigSettingDataTypes { get; set; }
        public IDbSet<ConfigSettingGroup> ConfigSettingGroups { get; set; }
        public IDbSet<ConfigSetting> ConfigSettings { get; set; }
        public IDbSet<UserStatus> UserStatuses { get; set; }
        public IDbSet<Purpose> Purposes { get; set; }
        public IDbSet<PurposeDetail> PurposeDetails { get; set; }
        public IDbSet<BillingUnbundledTransactionCategory> UnbundledTransactionCategories { get; set; }
        public IDbSet<LoginHistory> LoginHistories { get; set; }

        public IDbSet<ComplaintType> ComplaintTypes { get; set; }
        public IDbSet<ComplaintSubType> ComplaintSubTypes { get; set; }
        public IDbSet<ApprovalTemplateTransaction> ApprovalTemplateTransactionsData { get; set; }
        public IDbSet<ApprovalTemplateStages> ApprovalTemplateStageData { get; set; }
        public IDbSet<Codebiz.Domain.Common.Model.Department> Departments { get; set; }
        public IDbSet<DepartmentDetail> DepartmentDetails { get; set; }
        public IDbSet<NoOfUnitsAndKvaRating> NoOfUnitsAndKvaRatings { get; set; }
        public IDbSet<EmployeePhoto> EmployeePhotos { get; set; }
        public IDbSet<BillingUnit> Units { get; set; }
        public IDbSet<BillingMonthlyRate> MonthlyRates { get; set; }
        public IDbSet<BillingMonthlyRateUnbundledTransaction> MonthlyRateUnbundledTransactions { get; set; }
        public IDbSet<BillingUnbundledTransaction> BillingUnbundledTransactions { get; set; }
        public IDbSet<BillingUnbundledTransactionForDiscountDetails> BillingUnbundledTransactionForDiscountDetails { get; set; }
        public IDbSet<BillingUnbundledTransactionForVatDetails> BillingUnbundledTransactionForVatDetails { get; set; }
        public IDbSet<SupportingDocument> SupportingDocuments { get; set; }
        public IDbSet<Surcharge> Surcharges { get; set; }
        public IDbSet<WorkOrder> WorkOrders { get; set; }
        public IDbSet<SubStation> SubStations { get; set; }
        public IDbSet<Feeder> Feeders { get; set; }
        public IDbSet<MeterReadingRemark> MeterReadingRemarks { get; set; }
        public IDbSet<PackagingType> PackagingTypes { get; set; }
  
        public IDbSet<Manufacturer> Manufacturers { get; set; }

        #region Financial
        public IDbSet<FinancialProject> FinancialProjects { get; set; }
        //Currency
        public IDbSet<Currency> Currencies { get; set; }
        public IDbSet<ISOCurrencyCode> ISOCurrencyCodes { get; set; }
        public IDbSet<Rounding> Roundings { get; set; }
        public IDbSet<Decimals> Decimals { get; set; }

        #endregion

        //Job Order
        public IDbSet<JobOrderTaskToBePerform> JobOrderTaskToBePerforms { get; set; }
        public IDbSet<JobOrderTaskToBePerFormDetail> JobOrderTaskToBePerFormDetails { get; set; }

        public IDbSet<JobOrderNatureType> JobOrderNatureTypes { get; set; }

        //Country


        //LifelineSubsidy
        public IDbSet<LifelineSubsidy> LifelineSubsidies { get; set; }

        //Shipping Types


        public IDbSet<SalesEmployee> SalesEmployees { get; set; }
        public IDbSet<CommisionGroups> CommisionGroups { get; set; }


        //Notification
        public IDbSet<NotificationDetail> NotificationDetails { get; set; }
        public IDbSet<Notification> Notifications { get; set; }
        
        //Reports
        
        public IDbSet<ReportSignatory> ReportSignatories { get; set; }
        public IDbSet<ReportSignatoryDetail> ReportSignatoryDetails { get; set; }
        public IDbSet<ReportCategory> ReportCategories { get; set; }
        public IDbSet<Report> Reports { get; set; }


        #endregion


        public IDbSet<ApprovalStatus> ApprovalStatuses { get; set; }
        public IDbSet<LeaveApprovalStatus> LeaveApprovalStatuses { get; set; }
        public IDbSet<ContentFileType> ContentFileType { get; set; }
        public IDbSet<FileType> FileType { get; set; }
        public IDbSet<ContentFile> ContentFile { get; set; }
        public IDbSet<Model.Data.CSA.EnumBaseModels.DocumentType> DocumentTypes { get; set; }
        public IDbSet<RelationshipType> RelationshipTypes { get; set; }
        public IDbSet<ConsumerClass> ConsumerClasses { get; set; }

        public IDbSet<RequestPhase> RequestPhases { get; set; }

        public IDbSet<LeaveCreditMonetizationApprovalStatus> LeaveCreditMonetizationApprovalStatuses { get; set; }

        public IDbSet<ItemSales> ItemSales { get; set; }
        //public IDbSet<Common.Model.SalesOrder> SalesOrdersList { get; set; }
        public IDbSet<BrandType> BrandTypes { get; set; }
        public IDbSet<TransactionType> TransactionTypes { get; set; }
        public IDbSet<TransactionTypeCategory> TransactionTypeCategories { get; set; }
        public IDbSet<TransactionSubType> TransactionSubTypes { get; set; }
        public IDbSet<SupportingDocumentDetail> SupportingDocumentDetails { get; set; }
        public IDbSet<TransformerManagementType> TransformerManagementTypes { get; set; }
        public IDbSet<TaxType> TaxTypes { get; set; }
        public IDbSet<Volume> Volumes { get; set; }
        public IDbSet<CountryCode> CountryCodes { get; set; }
        //public IDbSet<Country> Countries { get; set; }
        public IDbSet<TemplateName> TemplateNames { get; set; }
        public IDbSet<PaperType> PaperTypes { get; set; }
        public IDbSet<DocumentHistory> DocumentHistories { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<decimal>().Configure(x => x.HasPrecision(19, 6));

            #region App User

            modelBuilder.Entity<AppUser>()
                .HasMany<UserGroup>(s => s.UserGroups)
                .WithMany(c => c.AppUsers)
                .Map(cs =>
                {
                    cs.MapLeftKey("AppUserId");
                    cs.MapRightKey("UserGroupId");
                    cs.ToTable("AppUserUserGroup");
                });

            #endregion

            #region User Group

            modelBuilder.Entity<UserGroup>()
                .HasMany<Permission>(s => s.Permissions)
                .WithMany(c => c.UserGroups)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserGroupId");
                    cs.MapRightKey("PermissionId");
                    cs.ToTable("UserGroupPermission");
                });

            #endregion

            #region App user Mapping

            modelBuilder.Entity<AppUser>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<AppUser>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);

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

            #region Content File

            modelBuilder.Entity<ContentFile>().HasOptional(a => a.ContentFileType).WithMany().HasForeignKey(a => a.ContentFileTypeId);
            modelBuilder.Entity<ContentFile>().HasOptional(a => a.FileType).WithMany().HasForeignKey(a => a.FileTypeId);
            modelBuilder.Entity<ContentFile>().HasOptional(a => a.CreatedByAppUser).WithMany().HasForeignKey(a => a.CreatedByAppUserId);
            modelBuilder.Entity<ContentFile>().HasOptional(a => a.ModifiedByAppUser).WithMany().HasForeignKey(a => a.ModifiedByAppUserId);

            #endregion

            #region Unbundled transaction
            modelBuilder.Entity<BillingUnbundledTransactionForVatDetails>().HasOptional(a => a.BillingUnbundledTransactionForVat).WithMany().HasForeignKey(a => a.BillingUnbundledTransactionForVatId);
            modelBuilder.Entity<BillingUnbundledTransactionForDiscountDetails>().HasOptional(a => a.BillingUnbundledTransactionForDiscount).WithMany().HasForeignKey(a => a.BillingUnbundledTransactionForDiscountId);
            #endregion

     

        }

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
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
