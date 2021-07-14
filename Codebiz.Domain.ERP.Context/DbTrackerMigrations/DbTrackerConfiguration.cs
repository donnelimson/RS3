using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DataModel.Financial.Currency;
using Codebiz.Domain.Common.Model.DataModel.Inventory;
using Codebiz.Domain.Common.Model.DataModel.SalesEmployee;
using Codebiz.Domain.Common.Model.EnumBaseModels;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model.Enums.BusinessPartners;
using Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication;
using Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication.Request;
using Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication.Transformer;
using Codebiz.Domain.Common.Model.Enums.Currency;
using Codebiz.Domain.ERP.Context.SeedData;
using Codebiz.Domain.ERP.Model;
using CsvHelper;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Codebiz.Domain.Common.Model.DataModel;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Codebiz.Domain.ERP.Context.DbTrackerMigrations
{
    public class DbTrackerConfiguration : DbMigrationsConfiguration<DbTrackerContext>
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        public AppUser AdminUser { get; set; }


        public DbTrackerConfiguration()
        {
            //AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DbTrackerMigrations";
        }

        protected override void Seed(DbTrackerContext context)
        {
            //System.Diagnostics.Debugger.Launch();
            var now = DateTime.Now;

            #region Seed Enum Data

            Helpers.SeedEnumData<AccessLevel, AccessLevels>(context.AccessLevels);
            Helpers.SeedEnumData<LogEventTitle, LogEventTitles>(context.LogEventTitles);
            Helpers.SeedEnumData<ApprovalStatus, ApprovalStatuses>(context.ApprovalStatuses);
            Helpers.SeedEnumData<UserStatus, UserStatuses>(context.UserStatuses);
            Helpers.SeedEnumData<CountryCode, CountryCodes>(context.CountryCodes);
            Helpers.SeedEnumData<PaperType, PaperTypes>(context.PaperTypes);
            Helpers.SeedEnumData<TemplateName, TemplateNames>(context.TemplateNames);
            Helpers.SeedEnumData<ISOCurrencyCode, ISOCurrencyCodes>(context.ISOCurrencyCodes);
            Helpers.SeedEnumData<Rounding, Roundings>(context.Roundings);
            Helpers.SeedEnumData<Decimals, DecimalEnums>(context.Decimals);
    
            Helpers.SeedEnumData<CommisionGroups, CommisionGroupEnums>(context.CommisionGroups);
            Helpers.SeedEnumData<DivisionType, DivisionTypeEnums>(context.DivisionTypes);
            Helpers.SeedEnumData<ConsumerClass, ConsumerClasses>(context.ConsumerClasses);


            //Report Category
            Helpers.SeedEnumData<ReportCategory, ReportCategoryEnums>(context.ReportCategories);

            context.SaveChanges();

            #endregion

            #region Default Users

            context.AppUsers.AddOrUpdate(x => x.Username, new AppUser
            {
                Username = "admin",
                PasswordHash = "07d8cada9b0b50464914625cb1a28a47e7e95afb:2b37f7a149ae82552504732b5df3201c263eb45e",
                CreatedByAppUserId = 1,
                CreatedOn = DateTime.Now,
                AccessFailedCount = 0,
                IsActive = true
            });

            context.Employees.AddOrUpdate(x => x.EmployeeNo, new Employee
            {
                EmployeeNo = "E999999",
                LastName = "Admin",
                FirstName = "Admin",
                MiddleName = "",
                Email = "admin@gmail.com",
                CreatedByAppUserId = 1,
                CreatedOn = DateTime.Now,
                IsActive = true
            });

            context.SaveChanges();

            var employee = context.Employees.FirstOrDefault(a => a.EmployeeNo == "E999999");
            var user = context.AppUsers.FirstOrDefault(a => a.Username == "admin");
            if (employee != null && user != null)
            {
                user.EmployeeId = employee.EmployeeId;
            }

            AdminUser = context.AppUsers.FirstOrDefault(a => a.Username == "admin");

            #endregion

            #region Permissions/Nav/Config

            NavLinkMigrateData.CreateNavLinks(context, now, AdminUser);
            PermissionMigrateData.CreatePermissionGroups(context, now, AdminUser);
            PermissionMigrateData.CreatePermissions(context, now, AdminUser);
            CreateConfigSettings(context, now);

            context.SaveChanges();

            #endregion

            #region Common

            SeedFileType(context);
            SeedContentFileTypes(context);

            //Address
            ManagementMigrateData.Seed_Region(context);
            ManagementMigrateData.Seed_Routes(context);

            //Management
            ManagementMigrateData.Seed_Office(context);
            //ManagementMigrateData.Seed_Position_Division_By_Department(context);
            //UserMigrateData.Seed_Update_Users(context);



            //UserMigrateData.Seed_Users(context);
          //  UserMigrateData.Seed_UserGroups(context);
            //UserMigrateData.Seed_User_PermissionGroup(context);

         //   UserMigrateData.Seed_Employees(context);

         //   UserMigrateData.Seed_Users(context);
            UserMigrateData.Seed_UserGroups(context);
            UserMigrateData.Seed_User_PermissionGroup(context);


            SeedSubStation(context);
            SeedFeeder(context);

            //Transactions
            //ManagementMigrateData.Seed_Approval_Transactions(context);
            //ManagementMigrateData.Seed_CSA_Transactions(context);
            //ManagementMigrateData.Seed_Inventory_Transactions(context);
            //ManagementMigrateData.Seed_BusinessPartner_Transactions(context);
            //ManagementMigrateData.Seed_Vehicle_Transactions(context);
            //ManagementMigrateData.Seed_Purchase_Transactions(context);
            //ManagementMigrateData.Seed_Sale_Transactions(context);
            //ManagementMigrateData.Seed_Production_Transactions(context);
            //ManagementMigrateData.Seed_JEN_Transactions(context);
            //ManagementMigrateData.Seed_PY_Transactions(context);
            //ManagementMigrateData.Seed_Material_Transaction(context);

            //ManagementMigrateData.Seed_SupportingDocument(context);
        //    ManagementMigrateData.Seed_Approval_Setup(context);
         //   ManagementMigrateData.SeedApproverLabel(context);

          //  SeedComplaintTypes(context);
           // SeedComplaintSubType(context);

            //Billing
            BillingMigrateData.Seed_BillingUnbundledTransactionCategory(context);
            //SeedMonthlyRate(context);
         //   SeedLifeSubsidy(context);

            context.SaveChanges();

            #endregion

            /*exec only once /smallkid */
            //CleanTablesForModelBaseMigration(context);
            /*exec only once /donnel */ //for approval with no reference
                                        //UpdateApprovalReference(context);
            /*exec only once /donnel */ // Error : OutOfMemoryException
                                        //UpdateAccountTypes(context);

            //ExecuteCustomSQLForDefaultConstraint(context);
            // SeedTransactionNumbering(context);
            //   ExecCustomSQL(context);
            //Report Signatories
            // ReportSignatoryData.Seed_Reports(context);

            //Migrations
            //ConsumerAccountMigrateData.Seed_Consumers(context);
            //ConsumerAccountMigrateData.Seed_Accounts(context);
            //ConsumerAccountMigrateData.Seed_BPAccounts(context);

            //Vehicle
            //SeedCoopVehicles(context);
            #region RS3
            SeedRoles(context);
            #endregion
            context.SaveChanges();
        }
        #region RS3
        private void SeedRoles(DbTrackerContext context)
        {
            context.Roles.AddOrUpdate(a=>a.Code, new Role{Id=1, Code="ADM", Description="Administrator" });
            context.Roles.AddOrUpdate(a => a.Code, new Role { Id = 2, Code = "CC", Description = "Call Center" });
            context.Roles.AddOrUpdate(a => a.Code, new Role { Id = 3, Code = "TCH", Description = "Technician" });
            context.Roles.AddOrUpdate(a => a.Code, new Role { Id = 4, Code = "CLN", Description = "Client" });
            context.SaveChanges();
        }
        #endregion
        #region Configuration Setting

        private void SeedFileType(DbTrackerContext context)
        {
            context.FileType.AddOrUpdate(a => a.CodeName, new FileType { FileTypeId = 1, CodeName = "image_jpeg", MimeType = "image/jpeg", FileExtension = "jpg,jpeg" });
            context.FileType.AddOrUpdate(a => a.CodeName, new FileType { FileTypeId = 2, CodeName = "image_png", MimeType = "image/png", FileExtension = "png" });
            context.FileType.AddOrUpdate(a => a.CodeName, new FileType { FileTypeId = 3, CodeName = "application_pdf", MimeType = "application/pdf", FileExtension = "pdf" });
            context.FileType.AddOrUpdate(a => a.CodeName, new FileType { FileTypeId = 4, CodeName = "application_msword", MimeType = "application/msword", FileExtension = "doc" });
            context.FileType.AddOrUpdate(a => a.CodeName, new FileType { FileTypeId = 5, CodeName = "docx", MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document", FileExtension = "docx" });
            context.FileType.AddOrUpdate(a => a.CodeName, new FileType { FileTypeId = 6, CodeName = "mp4", MimeType = "video/mp4", FileExtension = "mp4" });
            context.FileType.AddOrUpdate(a => a.CodeName, new FileType { FileTypeId = 7, CodeName = "gif", MimeType = "image/gif", FileExtension = "gif" });

            context.SaveChanges();
        }
        private void SeedContentFileTypes(DbTrackerContext context)
        {
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.EmployeePhoto,
                    Name = "EmployeePhoto",
                    Description = "Employee Photo",
                    ConfigSettingFolderId = (int)ConfigurationSettings.EmployeePhotoFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.MemberSupportingDocument,
                    Name = "MemberSupportingDocument",
                    Description = "Member Supporting Document",
                    ConfigSettingFolderId = (int)ConfigurationSettings.MemberSupportingDocumentsFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.AccountSupportingDocument,
                    Name = "AccountSupportingDocument",
                    Description = "Account Supporting Document",
                    ConfigSettingFolderId = (int)ConfigurationSettings.AccountSupportingDocumentsFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.HouseWiringInspectionSupportingDocument,
                    Name = "HouseWiringInspectionSupportingDocument",
                    Description = "House Wiring Inspection Supporting Document",
                    ConfigSettingFolderId = (int)ConfigurationSettings.HouseWiringInspectionSupportingDocumentsFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.DiscountApplicationSupportingDocument,
                    Name = "DiscountApplicationSupportingDocument",
                    Description = "Discount Application Supporting Document",
                    ConfigSettingFolderId = (int)ConfigurationSettings.DiscountApplicationSupportingDocumentsFolder
                });


            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.MembershipBarcode,
                    Name = "MembershipBarcode",
                    Description = "Membership Barcode",
                    ConfigSettingFolderId = (int)ConfigurationSettings.MembershipBarcodeFolder
                });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.Photo,
                    Name = "Photo",
                    Description = "Photo",
                    ConfigSettingFolderId = (int)ConfigurationSettings.MemberPhotoFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.Signature,
                    Name = "Signature",
                    Description = "Signature",
                    ConfigSettingFolderId = (int)ConfigurationSettings.MemberSignatureFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.ConnectionOrderPdf,
                    Name = "ConnectionOrderPdf",
                    Description = "Connection Order PDF",
                    ConfigSettingFolderId = (int)ConfigurationSettings.ConnectionOrderPdfFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.MaterialChargeTicketPdf,
                    Name = "MaterialChargeTicketPdf",
                    Description = "Material Charge Ticket PDF",
                    ConfigSettingFolderId = (int)ConfigurationSettings.MaterialChargeTicketPdfFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.JobOrderPdf,
                    Name = "JobOrderPdf",
                    Description = "Job Order PDF",
                    ConfigSettingFolderId = (int)ConfigurationSettings.JobOrderPdfFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.PurchaseOrderPdf,
                    Name = "PurchaseOrderPdf",
                    Description = "Purchase Order PDF",
                    ConfigSettingFolderId = (int)ConfigurationSettings.PurchaseOrderPdfFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.BillingAddressPdf,
                    Name = "BillingAddressPdf",
                    Description = "Billing Address PDF",
                    ConfigSettingFolderId = (int)ConfigurationSettings.BillingAddressPdfFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.BurialAssistanceSupportingDocument,
                    Name = "BurialAssistanceSupportingDocument",
                    Description = "Burial Assistance Supporting Document",
                    ConfigSettingFolderId = (int)ConfigurationSettings.BurialAssistanceSupportingDocumentsFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
                new ContentFileType
                {
                    ContentFileTypeId = (int)ContentFileTypes.ChangeOfNameSupportingDocument,
                    Name = "ChangeOfNameSupportingDocument",
                    Description = "Change Of Name Supporting Document",
                    ConfigSettingFolderId = (int)ConfigurationSettings.ChangeOfNameSupportingDocumentsFolder
                });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.NetMeteringDocumentsFolder,
                Name = "NetMeteringDocumentsFolder",
                Description = "Net Metering Documents Folder",
                ConfigSettingFolderId = (int)ConfigurationSettings.NetMeteringDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.JobOrderComplaintSupportingDocument,
                Name = "JobOrderComplaintSupportingDocument",
                Description = "Job Order Complaint Supporting Document",
                ConfigSettingFolderId = (int)ConfigurationSettings.JobOrderComplaintSupportingDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.JobOrderRequestSupportingDocument,
                Name = "JobOrderRequestSupportingDocument",
                Description = "Job Order Request Supporting Document",
                ConfigSettingFolderId = (int)ConfigurationSettings.JobOrderRequestSupportingDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.ItemMasterDataAttachment,
                Name = "ItemMasterDataAttachment",
                Description = "Item Master Data Attachment Folder",
                ConfigSettingFolderId = (int)ConfigurationSettings.ItemMasterDataAttachment
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
          new ContentFileType
          {
              ContentFileTypeId = (int)ContentFileTypes.SalesInvoiceDataAttachment,
              Name = "SalesInvoiceDataAttachment",
              Description = "Sales Invoice Data Attachment Folder",
              ConfigSettingFolderId = (int)ConfigurationSettings.SalesInvoiceDataAttachment
          });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.SalesDeliveryDataAttachment,
                Name = "SalesDeliveryDataAttachment",
                Description = "Sales Delivery Data Attachment Folder",
                ConfigSettingFolderId = (int)ConfigurationSettings.SalesDeliveryDataAttachment
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.SalesQuotationDataAttachment,
                Name = "SalesQuotationDataAttachment",
                Description = "Sales Quotation Data Attachment Folder",
                ConfigSettingFolderId = (int)ConfigurationSettings.SalesQuotationDataAttachment
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.SalesOrderDataAttachment,
                Name = "SalesOrderDataAttachment",
                Description = "Sales Order Data Attachment Folder",
                ConfigSettingFolderId = (int)ConfigurationSettings.SalesOrderDataAttachment
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.BusinessPartnerSupportingDocumentsFolder,
                Name = "BusinessPartnerSupportingDocumentsFolder",
                Description = "Business Partner Supporting Documents Folder",
                ConfigSettingFolderId = (int)ConfigurationSettings.BusinessPartnerSupportingDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.InventoryTransferSupportingDocumentsFolder,
                Name = "BusinessPartnerSupportingDocumentsFolder",
                Description = "Business Partner Supporting Documents Folder",
                ConfigSettingFolderId = (int)ConfigurationSettings.InventoryTransferSupportingDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.EmployeeSignature,
                Name = "Employee Signature",
                Description = "EmployeeSignature",
                ConfigSettingFolderId = (int)ConfigurationSettings.EmployeeSignatureFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.InventoryReceivingSupportingDocumentsFolder,
                Name = "InventoryReceivingSupportingDocumentsFolder",
                Description = "InventoryReceivingSupportingDocumentsFolder",
                ConfigSettingFolderId = (int)ConfigurationSettings.InventoryReceivingSupportingDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.OtherRequestSupportingDocument,
                Name = "OtherRequestSupportingDocument",
                Description = "Other Request Supporting Document",
                ConfigSettingFolderId = (int)ConfigurationSettings.OtherRequestSupportingDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
        new ContentFileType
        {
            ContentFileTypeId = (int)ContentFileTypes.CounterSetupSupportingDocument,
            Name = "CounterSetupSupportingDocument",
            Description = "Counter Setup Supporting Document",
            ConfigSettingFolderId = (int)ConfigurationSettings.CounterSetupDocumentsFolder
        });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
        new ContentFileType
        {
            ContentFileTypeId = (int)ContentFileTypes.ProcessJobOrderDocumentsFolder,
            Name = "ProcessJobOrderDocumentsFolder",
            Description = "Process Job Order Folder",
            ConfigSettingFolderId = (int)ConfigurationSettings.ProcessJobOrderDocumentsFolder
        });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.MaterialRequestSupportingDocument,
                Name = ContentFileTypes.MaterialRequestSupportingDocument.GetEnumDescription(),
                Description = ContentFileTypes.MaterialRequestSupportingDocument.GetEnumDescription(),
                ConfigSettingFolderId = (int)ConfigurationSettings.MaterialRequestSupportingDocumentsFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.MaterialRequestFolder,
                Name = ContentFileTypes.MaterialRequestFolder.GetEnumDescription(),
                Description = ContentFileTypes.MaterialRequestFolder.GetEnumDescription(),
                ConfigSettingFolderId = (int)ConfigurationSettings.MaterialRequestFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.ContestableApplicationFolder,
                Name = ContentFileTypes.ContestableApplicationFolder.GetEnumDescription(),
                Description = ContentFileTypes.ContestableApplicationFolder.GetEnumDescription(),
                ConfigSettingFolderId = (int)ConfigurationSettings.ContestableApplicationFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
         new ContentFileType
         {
             ContentFileTypeId = (int)ContentFileTypes.RequisitionVoucherFolder,
             Name = ContentFileTypes.RequisitionVoucherFolder.GetEnumDescription(),
             Description = ContentFileTypes.RequisitionVoucherFolder.GetEnumDescription(),
             ConfigSettingFolderId = (int)ConfigurationSettings.RequisitionVoucherFolder
         });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
         new ContentFileType
         {
             ContentFileTypeId = (int)ContentFileTypes.RequisitionVoucherSupportingDocumentsFolder,
             Name = ContentFileTypes.RequisitionVoucherSupportingDocumentsFolder.GetEnumDescription(),
             Description = ContentFileTypes.RequisitionVoucherSupportingDocumentsFolder.GetEnumDescription(),
             ConfigSettingFolderId = (int)ConfigurationSettings.RequisitionVoucherSupportingDocumentsFolder
         });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
        new ContentFileType
        {
            ContentFileTypeId = (int)ContentFileTypes.TransformerRentalFolder,
            Name = ContentFileTypes.TransformerRentalFolder.GetEnumDescription(),
            Description = ContentFileTypes.TransformerRentalFolder.GetEnumDescription(),
            ConfigSettingFolderId = (int)ConfigurationSettings.TransformerRentalFolder
        });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
         new ContentFileType
         {
             ContentFileTypeId = (int)ContentFileTypes.TransformerRentalWitnessSignatureFolder,
             Name = ContentFileTypes.TransformerRentalWitnessSignatureFolder.GetEnumDescription(),
             Description = ContentFileTypes.TransformerRentalWitnessSignatureFolder.GetEnumDescription(),
             ConfigSettingFolderId = (int)ConfigurationSettings.TransformerRentalWitnessSignatureFolder
         });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
          new ContentFileType
          {
              ContentFileTypeId = (int)ContentFileTypes.ChangeOfMeterSupportingDocumentsFolder,
              Name = ContentFileTypes.ChangeOfMeterSupportingDocumentsFolder.GetEnumDescription(),
              Description = ContentFileTypes.ChangeOfMeterSupportingDocumentsFolder.GetEnumDescription(),
              ConfigSettingFolderId = (int)ConfigurationSettings.ChangeOfMeterSupportingDocumentsFolder
          });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
        new ContentFileType
        {
            ContentFileTypeId = (int)ContentFileTypes.SpecialLightingSupportingDocumentsFolder,
            Name = ContentFileTypes.SpecialLightingSupportingDocumentsFolder.GetEnumDescription(),
            Description = ContentFileTypes.SpecialLightingSupportingDocumentsFolder.GetEnumDescription(),
            ConfigSettingFolderId = (int)ConfigurationSettings.SpecialLightingSupportingDocumentsFolder
        });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.BillingAdjustmentAttachmentFolder,
                Name = ContentFileTypes.BillingAdjustmentAttachmentFolder.GetEnumDescription(),
                Description = ContentFileTypes.BillingAdjustmentAttachmentFolder.GetEnumDescription(),
                ConfigSettingFolderId = (int)ConfigurationSettings.BillingAdjustmentAttachmentFolder
            });
            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.StatementOfAccountFolder,
                Name = ContentFileTypes.StatementOfAccountFolder.GetEnumDescription(),
                Description = ContentFileTypes.StatementOfAccountFolder.GetEnumDescription(),
                ConfigSettingFolderId = (int)ConfigurationSettings.StatementOfAccountFolder
            });

            context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
            new ContentFileType
            {
                ContentFileTypeId = (int)ContentFileTypes.CoopVehicleSupportingDocumentsFolder,
                Name = ContentFileTypes.CoopVehicleSupportingDocumentsFolder.GetEnumDescription(),
                Description = ContentFileTypes.CoopVehicleSupportingDocumentsFolder.GetEnumDescription(),
                ConfigSettingFolderId = (int)ConfigurationSettings.CoopVehicleSupportingDocumentsFolder
            }); context.ContentFileType.AddOrUpdate(a => a.ContentFileTypeId,
             new ContentFileType
             {
                 ContentFileTypeId = (int)ContentFileTypes.TicketAttachmentFolder,
                 Name = ContentFileTypes.TicketAttachmentFolder.GetEnumDescription(),
                 Description = ContentFileTypes.TicketAttachmentFolder.GetEnumDescription(),
                 ConfigSettingFolderId = (int)ConfigurationSettings.TicketAttachmentFolder
             });
            context.SaveChanges();
        }
        private void ConfigurePermissionUserGroup(DbTrackerContext context)
        {
            var adminUserGroups = context.UserGroups.FirstOrDefault(a => a.Name == "Admin");

            // Permissiom
            if (adminUserGroups.Permissions != null)
                adminUserGroups.Permissions.Clear();

            adminUserGroups.Permissions = context.Permissions.ToList();

            context.SaveChanges();

            // User Group
            if (AdminUser.UserGroups != null)
                AdminUser.UserGroups.Clear();

            AdminUser.UserGroups = new List<UserGroup> { adminUserGroups };

            context.SaveChanges();
        }

        private void CreateConfigSettings(DbTrackerContext context, DateTime now)
        {
            // Config Setting Data Types
            context.ConfigSettingDataTypes.AddOrUpdate(a => a.CodeName, new ConfigSettingDataType { CodeName = "INTEGER", ConfigSettingDataTypeId = 1 });
            context.ConfigSettingDataTypes.AddOrUpdate(a => a.CodeName, new ConfigSettingDataType { CodeName = "STRING", ConfigSettingDataTypeId = 2 });
            context.ConfigSettingDataTypes.AddOrUpdate(a => a.CodeName, new ConfigSettingDataType { CodeName = "DECIMAL", ConfigSettingDataTypeId = 3 });
            context.ConfigSettingDataTypes.AddOrUpdate(a => a.CodeName, new ConfigSettingDataType { CodeName = "BOOLEAN", ConfigSettingDataTypeId = 4 });

            // Config Setting Groups
            context.ConfigSettingGroups.AddOrUpdate(a => a.CodeName, new ConfigSettingGroup { CodeName = "App Settings", ConfigSettingGroupId = 1 });
            context.ConfigSettingGroups.AddOrUpdate(a => a.CodeName, new ConfigSettingGroup { CodeName = "Policy", ConfigSettingGroupId = 2 });
            context.ConfigSettingGroups.AddOrUpdate(a => a.CodeName, new ConfigSettingGroup { CodeName = "Security", ConfigSettingGroupId = 3 });
            context.ConfigSettingGroups.AddOrUpdate(a => a.CodeName, new ConfigSettingGroup { CodeName = "Others", ConfigSettingGroupId = 4 });
            context.ConfigSettingGroups.AddOrUpdate(a => a.CodeName, new ConfigSettingGroup { CodeName = "File Upload Folder", ConfigSettingGroupId = 5 });

            // Config Settings
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.MailTemplatePath, Name = "MailTemplatePath", Description = "Mail Template Path", Value = @"~/content/mailtemplates", ConfigSettingGroupId = 4, IsActive = true, ConfigSettingDataTypeId = 2, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.SmtpDisplayName, Name = "SmtpDisplayName", Description = "SmtpDisplayName", Value = "noreply.communitech", ConfigSettingGroupId = 4, IsActive = true, ConfigSettingDataTypeId = 2, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.SmtpUsername, Name = "SmtpUsername", Description = "SmtpUsername", Value = "noreply.communitech@gmail.com", ConfigSettingGroupId = 4, IsActive = true, ConfigSettingDataTypeId = 2, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.SmtpPassword, Name = "SmtpPassword", Description = "SmtpPassword", Value = "Communit3k", ConfigSettingGroupId = 4, IsActive = true, ConfigSettingDataTypeId = 2, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.SmtpHost, Name = "SmtpHost", Description = "SmtpHost", Value = "smtp.gmail.com", ConfigSettingGroupId = 4, IsActive = true, ConfigSettingDataTypeId = 2, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.SmtpPort, Name = "SmtpPort", Description = "SmtpPort", Value = "587", ConfigSettingGroupId = 4, IsActive = true, ConfigSettingDataTypeId = 1, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.SiteLocalNetworkBaseUrl, Name = "SiteLocalNetworkBaseUrl", Description = "Site Local Network Base Url", Value = @"http://localhost:49801/", ConfigSettingGroupId = 4, IsActive = true, ConfigSettingDataTypeId = 2, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });
            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting { ConfigSettingId = (int)ConfigurationSettings.MaxLoginAttempt, Name = "MaxLoginAttempt", Description = "Maximum Login Attempts", Value = "3", ConfigSettingGroupId = 1, IsActive = true, ConfigSettingDataTypeId = 1, CreatedByAppUserId = AdminUser.AppUserId, CreatedOn = DateTime.Now });


            #region Folder path for files/attachments



            context.ConfigSettings.AddOrUpdate(a => a.Name, new ConfigSetting
            {
                ConfigSettingId = (int)ConfigurationSettings.TicketAttachmentFolder,
                Name = ConfigurationSettings.TicketAttachmentFolder.ToString(),
                Description = ConfigurationSettings.TicketAttachmentFolder.GetEnumDescription(),
                Value = @"C:\RS3\TicketAttachmentFolder",
                ConfigSettingGroupId = 5,
                IsActive = true,
                ConfigSettingDataTypeId = 2,
                CreatedByAppUserId = AdminUser.AppUserId,
                CreatedOn = DateTime.Now
            });
            #endregion

            context.SaveChanges();
        }

        #endregion


   

        private void insertUnbundledTransactionDetail(DbTrackerContext context, string code, string detailCode, bool forVat)
        {
            var billingunbundled = context.BillingUnbundledTransactions.Where(x => x.Code == detailCode).FirstOrDefault();

            if (billingunbundled != null)
            {
                if (forVat)
                {
                    context.BillingUnbundledTransactionForVatDetails.Add(
                        new Common.Model.DataModel.CSA.Billing.UnbundledTransaction.BillingUnbundledTransactionForVatDetails
                        {
                            BillingUnbundledTransactionId = context.BillingUnbundledTransactions.Where(x => x.Code == code).FirstOrDefault().BillingUnbundledTransactionId,
                            BillingUnbundledTransactionForVatId = billingunbundled.BillingUnbundledTransactionId
                        }); ;
                }
                else
                {
                    context.BillingUnbundledTransactionForDiscountDetails.Add(
                        new Common.Model.DataModel.CSA.Billing.UnbundledTransaction.BillingUnbundledTransactionForDiscountDetails
                        {
                            BillingUnbundledTransactionId = context.BillingUnbundledTransactions.Where(x => x.Code == code).FirstOrDefault().BillingUnbundledTransactionId,
                            BillingUnbundledTransactionForDiscountId = billingunbundled.BillingUnbundledTransactionId
                        });
                }
            }
        }
       
        private void SeedSubStation(DbTrackerContext context)
        {
            context.SubStations.AddOrUpdate(a => a.Description,
                new SubStation
                {
                    SubStationId = 1,
                    Description = "Gerona 10 MVA S/S",
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                });

            context.SubStations.AddOrUpdate(a => a.Description,
                new SubStation
                {
                    SubStationId = 2,
                    Description = "Gerona 20 MVA S/S",
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                });

            context.SubStations.AddOrUpdate(a => a.Description,
                new SubStation
                {
                    SubStationId = 3,
                    Description = "Camiling 10 MVA S/S",
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                });

            context.SubStations.AddOrUpdate(a => a.Description,
                 new SubStation
                 {
                     SubStationId = 4,
                     Description = "Sta. Ignacia 10 MVA S/S",
                     CreatedByAppUserId = 1,
                     IsActive = true,
                     CreatedOn = DateTime.Now,
                     IsDeleted = false
                 });

            context.SubStations.AddOrUpdate(a => a.Description,
                 new SubStation
                 {
                     SubStationId = 5,
                     Description = "Paniqui 10 MVA S/S",
                     CreatedByAppUserId = 1,
                     IsActive = true,
                     CreatedOn = DateTime.Now,
                     IsDeleted = false
                 });

            context.SubStations.AddOrUpdate(a => a.Description,
                 new SubStation
                 {
                     SubStationId = 6,
                     Description = "Paniqui 5 MVA S/S",
                     CreatedByAppUserId = 1,
                     IsActive = true,
                     CreatedOn = DateTime.Now,
                     IsDeleted = false
                 });

            context.SubStations.AddOrUpdate(a => a.Description,
                new SubStation
                {
                    SubStationId = 7,
                    Description = "Moncada 5 MVA S/S",
                    CreatedByAppUserId = 1,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    IsDeleted = false
                });

            context.SubStations.AddOrUpdate(a => a.Description,
               new SubStation
               {
                   SubStationId = 8,
                   Description = "Victoria 10 MVA S/S",
                   CreatedByAppUserId = 1,
                   IsActive = true,
                   CreatedOn = DateTime.Now,
                   IsDeleted = false
               });

            context.SubStations.AddOrUpdate(a => a.Description,
               new SubStation
               {
                   SubStationId = 9,
                   Description = "ANOA 10 MVA S/S",
                   CreatedByAppUserId = 1,
                   IsActive = true,
                   CreatedOn = DateTime.Now,
                   IsDeleted = false
               });

            context.SaveChanges();
        }
        private void SeedFeeder(DbTrackerContext context)
        {
            #region Gerona 10 MVA S/S = 1
            context.Feeders.AddOrUpdate(a => a.Name,
                new Feeder
                {
                    Name = "Amacalan",
                    SubStationId = 1,
                    IsDeleted = false
                });
            context.Feeders.AddOrUpdate(a => a.Name,
               new Feeder
               {
                   Name = "Amacalan Industrial",
                   SubStationId = 1,
                   IsDeleted = false
               });
            #endregion

            #region Gerona 20 MVA S/S = 2
            context.Feeders.AddOrUpdate(a => a.Name,
                new Feeder
                {
                    Name = "CP SOLE USED",
                    SubStationId = 2,
                    IsDeleted = false
                });
            context.Feeders.AddOrUpdate(a => a.Name,
               new Feeder
               {
                   Name = "GROBEST SOLE USED",
                   SubStationId = 2,
                   IsDeleted = false
               });
            context.Feeders.AddOrUpdate(a => a.Name,
             new Feeder
             {
                 Name = "Gerona",
                 SubStationId = 2,
                 IsDeleted = false
             });
            #endregion

            #region Camiling 10 MVA S/S = 3
            context.Feeders.AddOrUpdate(a => a.Name,
                new Feeder
                {
                    Name = "San Clemente",
                    SubStationId = 3,
                    IsDeleted = false
                });
            context.Feeders.AddOrUpdate(a => a.Name,
                new Feeder
                {
                    Name = "Camiling",
                    SubStationId = 3,
                    IsDeleted = false
                });
            context.Feeders.AddOrUpdate(a => a.Name,
               new Feeder
               {
                   Name = "Mayantoc",
                   SubStationId = 3,
                   IsDeleted = false
               }); context.Feeders.AddOrUpdate(a => a.Name,
                new Feeder
                {
                    Name = "Matubog",
                    SubStationId = 3,
                    IsDeleted = false
                });
            #endregion

            #region Ignacia 10 MVA S/S = 4
            context.Feeders.AddOrUpdate(a => a.Name,
               new Feeder
               {
                   Name = "San Jose",
                   SubStationId = 4,
                   IsDeleted = false
               });
            context.Feeders.AddOrUpdate(a => a.Name,
               new Feeder
               {
                   Name = "Sta. Ignacio",
                   SubStationId = 4,
                   IsDeleted = false
               });
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Calayaan, Tyson SOLE USED",
                  SubStationId = 4,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Tyson SOLE USED",
                  SubStationId = 4,
                  IsDeleted = false
              });
            #endregion

            #region Panique 10 MVA S/S = 5
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Apulid, Northern part of Paniqui",
                  SubStationId = 5,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
             new Feeder
             {
                 Name = "Ramos",
                 SubStationId = 5,
                 IsDeleted = false
             });
            context.Feeders.AddOrUpdate(a => a.Name,
             new Feeder
             {
                 Name = "Paniqui south",
                 SubStationId = 5,
                 IsDeleted = false
             });
            #endregion

            #region Panique 5 MVA S/S = 6
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Paniqui North",
                  SubStationId = 6,
                  IsDeleted = false
              });
            #endregion

            #region Moncada 5 MVA S/S = 7
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "San Manuel",
                  SubStationId = 7,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Moncada",
                  SubStationId = 7,
                  IsDeleted = false
              });
            #endregion

            #region Victoria 10 MVA S/S = 8
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Baculong",
                  SubStationId = 8,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "San Andres",
                  SubStationId = 8,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Pura",
                  SubStationId = 8,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Victoria",
                  SubStationId = 8,
                  IsDeleted = false
              });
            #endregion

            #region ANOA 10 MVA S/S = 9
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Cuyapo",
                  SubStationId = 9,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
              new Feeder
              {
                  Name = "Anao, Moncada",
                  SubStationId = 9,
                  IsDeleted = false
              });
            context.Feeders.AddOrUpdate(a => a.Name,
             new Feeder
             {
                 Name = "Nampicuan",
                 SubStationId = 9,
                 IsDeleted = false
             });
            #endregion

            context.SaveChanges();
        }
        private void SeedComplaintTypes(DbTrackerContext context)
        {
            if (!context.ComplaintTypes.Any())
            {
                var natureTypes = context.JobOrderNatureTypes.ToList();

                context.ComplaintTypes.AddOrUpdate(a => a.NatureTypeId,
                   new ComplaintType
                   {
                       NatureTypeId = natureTypes.FirstOrDefault(p => p.Description == Helpers.GetEnumDescription(JONatureTypeEnums.Pole)).JobOrderNatureTypeId,
                       IsDelete = false,
                       CreatedByAppUserId = 1,
                       CreatedOn = DateTime.Now,
                       IsActive = true
                   });

                context.ComplaintTypes.AddOrUpdate(a => a.NatureTypeId,
                    new ComplaintType
                    {
                        NatureTypeId = natureTypes.FirstOrDefault(p => p.Description == Helpers.GetEnumDescription(JONatureTypeEnums.KWHMeter)).JobOrderNatureTypeId,
                        IsDelete = false,
                        CreatedByAppUserId = 1,
                        CreatedOn = DateTime.Now,
                        IsActive = true
                    });

                context.ComplaintTypes.AddOrUpdate(a => a.NatureTypeId,
                   new ComplaintType
                   {
                       NatureTypeId = natureTypes.FirstOrDefault(p => p.Description == Helpers.GetEnumDescription(JONatureTypeEnums.Transformer)).JobOrderNatureTypeId,
                       IsDelete = false,
                       CreatedByAppUserId = 1,
                       CreatedOn = DateTime.Now,
                       IsActive = true
                   });

                context.ComplaintTypes.AddOrUpdate(a => a.NatureTypeId,
                   new ComplaintType
                   {
                       NatureTypeId = natureTypes.FirstOrDefault(p => p.Description == Helpers.GetEnumDescription(JONatureTypeEnums.Line)).JobOrderNatureTypeId,
                       IsDelete = false,
                       CreatedByAppUserId = 1,
                       CreatedOn = DateTime.Now,
                       IsActive = true
                   });

                context.ComplaintTypes.AddOrUpdate(a => a.NatureTypeId,
                   new ComplaintType
                   {
                       NatureTypeId = natureTypes.FirstOrDefault(p => p.Description == Helpers.GetEnumDescription(JONatureTypeEnums.ServiceDrop)).JobOrderNatureTypeId,
                       IsDelete = false,
                       CreatedByAppUserId = 1,
                       CreatedOn = DateTime.Now,
                       IsActive = true
                   });

                context.SaveChanges();
            }
        }
        private void SeedComplaintSubType(DbTrackerContext context)
        {
            if (!context.ComplaintSubTypes.Any())
            {
                var complaintTypes = context.ComplaintTypes.ToList();

                #region Pole

                var pole = complaintTypes.Where(a => a.NatureType.Description == Helpers.GetEnumDescription(JONatureTypeEnums.Pole)).FirstOrDefault();

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Rotten",
                        ComplaintTypeId = pole.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Leaning",
                        ComplaintTypeId = pole.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Relocation",
                        ComplaintTypeId = pole.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                   new ComplaintSubType
                   {
                       Complaint = "Bogged Down",
                       ComplaintTypeId = pole.ComplaintTypeId,
                       IsDeleted = false,
                       IsActive = true
                   });

                #endregion

                #region KWH Meter

                var kwhMeter = complaintTypes.Where(a => a.NatureType.Description == Helpers.GetEnumDescription(JONatureTypeEnums.KWHMeter)).FirstOrDefault();

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "High Consumption",
                        ComplaintTypeId = kwhMeter.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Creeping Meter",
                        ComplaintTypeId = kwhMeter.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Sparkling",
                        ComplaintTypeId = kwhMeter.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                   new ComplaintSubType
                   {
                       Complaint = "Stop Meter",
                       ComplaintTypeId = kwhMeter.ComplaintTypeId,
                       IsDeleted = false,
                       IsActive = true
                   });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                  new ComplaintSubType
                  {
                      Complaint = "Broken Glass Meter",
                      ComplaintTypeId = kwhMeter.ComplaintTypeId,
                      IsDeleted = false,
                      IsActive = true
                  });

                #endregion

                #region Transformer

                var transformer = complaintTypes.Where(a => a.NatureType.Description == Helpers.GetEnumDescription(JONatureTypeEnums.Transformer)).FirstOrDefault();

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Sparkling",
                        ComplaintTypeId = transformer.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Busted",
                        ComplaintTypeId = transformer.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                #endregion

                #region Line

                var line = complaintTypes.Where(a => a.NatureType.Description == Helpers.GetEnumDescription(JONatureTypeEnums.Line)).FirstOrDefault();

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                   new ComplaintSubType
                   {
                       Complaint = "Loose Connection",
                       ComplaintTypeId = line.ComplaintTypeId,
                       IsDeleted = false,
                       IsActive = true
                   });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Secondary Line Cut",
                        ComplaintTypeId = line.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Primary Line Cut",
                        ComplaintTypeId = line.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Sagging Lines",
                        ComplaintTypeId = line.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                #endregion

                #region Service Drop

                var serviceDrop = complaintTypes.Where(a => a.NatureType.Description == Helpers.GetEnumDescription(JONatureTypeEnums.ServiceDrop)).FirstOrDefault();

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                   new ComplaintSubType
                   {
                       Complaint = "Loose Connection",
                       ComplaintTypeId = serviceDrop.ComplaintTypeId,
                       IsDeleted = false,
                       IsActive = true
                   });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Sparkling",
                        ComplaintTypeId = serviceDrop.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Burned",
                        ComplaintTypeId = serviceDrop.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                context.ComplaintSubTypes.AddOrUpdate(a => new { a.Complaint, a.ComplaintTypeId },
                    new ComplaintSubType
                    {
                        Complaint = "Sagging",
                        ComplaintTypeId = serviceDrop.ComplaintTypeId,
                        IsDeleted = false,
                        IsActive = true
                    });

                #endregion

                context.SaveChanges();
            }
        }
    
    
 
        public List<TEntity> ReadAndMapFile<TEntity, TId>(string filePath, char separator = '\t')
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream s = assembly.GetManifestResourceStream(filePath))
            {
                using (var sr = new StreamReader(s))
                {

                    string line_data = string.Empty;
                    var line_data_fieldName = sr.ReadLine();
                    var line_data_fieldNameAlias = sr.ReadLine();
                    var field_names = line_data_fieldName.Split(separator);
                    var entity_list = new List<TEntity>();

                    while ((line_data = sr.ReadLine()) != null)
                    {
                        var myEntity = Activator.CreateInstance<TEntity>();
                        if (line_data != string.Empty)
                        {
                            var line_data_array = line_data.Split(separator);
                            Type _type = typeof(TEntity);

                            TId Id = default(TId);
                            if (!string.IsNullOrEmpty(line_data_array[0]))
                                Id = (TId)Convert.ChangeType(line_data_array[0], typeof(TId));


                            for (var x = 0; x < line_data_array.Length; x++)
                            {
                                try
                                {
                                    PropertyInfo propInfo = _type.GetProperty(field_names[x]);
                                    if (propInfo != null)
                                    {
                                        Type fieldType = propInfo.PropertyType;
                                        //bool
                                        if ((fieldType == typeof(bool) || fieldType == typeof(bool?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            switch (line_data_array[x])
                                            {
                                                case "1":
                                                    propInfo.SetValue(myEntity, true);
                                                    break;
                                                case "0":
                                                    propInfo.SetValue(myEntity, false);
                                                    break;
                                                case "True":
                                                    propInfo.SetValue(myEntity, bool.Parse(line_data_array[x]));
                                                    break;
                                                case "False":
                                                    propInfo.SetValue(myEntity, bool.Parse(line_data_array[x]));
                                                    break;
                                                case "Y":
                                                    propInfo.SetValue(myEntity, true);
                                                    break;
                                                case "N":
                                                    propInfo.SetValue(myEntity, false);
                                                    break;
                                            }
                                        }

                                        if ((fieldType == typeof(byte) || fieldType == typeof(byte?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            byte b = byte.TryParse(line_data_array[x], out b) ? b : (byte)0;
                                            propInfo.SetValue(myEntity, b);
                                        }

                                        if ((fieldType == typeof(sbyte) || fieldType == typeof(sbyte?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            sbyte b = sbyte.TryParse(line_data_array[x], out b) ? b : (sbyte)0;
                                            propInfo.SetValue(myEntity, b);
                                        }

                                        if ((fieldType == typeof(int) || fieldType == typeof(int?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            int i = int.TryParse(line_data_array[x], out i) ? i : 0;
                                            propInfo.SetValue(myEntity, i);
                                        }

                                        if ((fieldType == typeof(long) || fieldType == typeof(long?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            long i = long.TryParse(line_data_array[x], out i) ? i : 0;
                                            propInfo.SetValue(myEntity, i);
                                        }

                                        if ((fieldType == typeof(float) || fieldType == typeof(float?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            var str_data = line_data_array[x].Replace("\"", "").Replace(",", "").Replace("\'", "").Replace(" ", "");
                                            double tmp_val = double.TryParse(str_data, out tmp_val) ? tmp_val : 0;
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if ((fieldType == typeof(double) || fieldType == typeof(double?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            var str_data = line_data_array[x].Replace("\"", "").Replace(",", "").Replace("\'", "").Replace(" ", "");
                                            double tmp_val = double.TryParse(str_data, out tmp_val) ? tmp_val : 0;
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if ((fieldType == typeof(decimal) || fieldType == typeof(decimal?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            var str_data = line_data_array[x].Replace("\"", "").Replace(",", "").Replace("\'", "").Replace(" ", "");

                                            decimal tmp_val = decimal.TryParse(str_data, out tmp_val) ? tmp_val : 0;

                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if (fieldType == typeof(string))
                                            propInfo.SetValue(myEntity, line_data_array[x]);

                                        if (fieldType == typeof(char))
                                            propInfo.SetValue(myEntity, line_data_array[x]);

                                        if ((fieldType == typeof(DateTime) || fieldType == typeof(DateTime?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            DateTime tmp_val = DateTime.TryParse(line_data_array[x], out tmp_val) ? tmp_val : DateTime.Parse("1900-01-01");
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if ((fieldType == typeof(TimeSpan) || fieldType == typeof(TimeSpan?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            TimeSpan tmp_val = TimeSpan.TryParse(line_data_array[x], out tmp_val) ? tmp_val : TimeSpan.Parse("1900-01-01");
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    //Errors.Add($"{e.Data}");
                                }
                            }
                        }
                        entity_list.Add(myEntity);
                    }
                    return entity_list;
                }
            }
        }
     

   
        void SeedTransactionNumbering(DbTrackerContext ctx)
        {
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.DiscountApplication,
                DocSeriesName = TransactionTypeEnum.DiscountApplication.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.DiscountApplication);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.ChangeOfName,
                DocSeriesName = TransactionTypeEnum.ChangeOfName.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.ChangeOfName);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.BurialAssistance,
                DocSeriesName = TransactionTypeEnum.BurialAssistance.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.BurialAssistance);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.NetMetering,
                DocSeriesName = TransactionTypeEnum.NetMetering.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.NetMetering);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.TransformerRental,
                DocSeriesName = TransactionTypeEnum.TransformerRental.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.TransformerRental);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.JobOrderComplaint,
                DocSeriesName = TransactionTypeEnum.JobOrderComplaint.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.JobOrderComplaint);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.JobOrderComplaintDirect,
                DocSeriesName = TransactionTypeEnum.JobOrderComplaintDirect.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.JobOrderComplaintDirect);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.JobOrderComplaintIndirect,
                DocSeriesName = TransactionTypeEnum.JobOrderComplaintIndirect.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.JobOrderComplaintIndirect);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.JobOrderRequest,
                DocSeriesName = TransactionTypeEnum.JobOrderRequest.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.JobOrderRequest);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.JobOrderRequestDirect,
                DocSeriesName = TransactionTypeEnum.JobOrderRequestDirect.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.JobOrderRequestDirect);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.JobOrderRequestIndirect,
                DocSeriesName = TransactionTypeEnum.JobOrderRequestIndirect.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.JobOrderRequestIndirect);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.ContestableApplication,
                DocSeriesName = TransactionTypeEnum.ContestableApplication.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.ContestableApplication);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.ChangeOfMeter,
                DocSeriesName = TransactionTypeEnum.ChangeOfMeter.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.ChangeOfMeter);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.Approval,
                DocSeriesName = TransactionTypeEnum.Approval.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = true,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.Approval);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.SpecialLightingApplication,
                DocSeriesName = TransactionTypeEnum.SpecialLightingApplication.GetEnumDescription(),
                Suffix = "SP",
                IsYearPrefix = true,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 1,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.SpecialLightingApplication);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.MemberAccountApplication,
                DocSeriesName = TransactionTypeEnum.MemberAccountApplication.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 100,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.MemberAccountApplication);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.Applicant,
                DocSeriesName = TransactionTypeEnum.Applicant.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 100,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.Applicant);
            ctx.Set<TransactionNumbering>().AddIfNotExists(new TransactionNumbering
            {
                TransactionTypeId = (int)TransactionTypeEnum.BillingAdjustment,
                DocSeriesName = TransactionTypeEnum.BillingAdjustment.GetEnumDescription(),
                Suffix = "",
                IsYearPrefix = false,
                NoOfDigits = 7,
                NumberFormat = "0",
                Prefix = "",
                NextNum = 100,
            }, x => x.TransactionTypeId == (int)TransactionTypeEnum.BillingAdjustment);
            ctx.SaveChanges();

        }

        void ExecuteCustomSQLForDefaultConstraint(DbTrackerContext ctx)
        {

            //if not exists(select 1 from sys.sysobjects where [name] = 'DF_BusinessPartner_Active')
            //begin
            //    ALTER TABLE BusinessPartner ADD CONSTRAINT DF_BusinessPartner_Active DEFAULT N'Y' FOR Active
            //end

            //if not exists(select 1 from sys.sysobjects where [name] = 'DF_BusinessPartnerAddress_Active')
            //begin
            //    ALTER TABLE BusinessPartnerAddress ADD CONSTRAINT DF_BusinessPartnerAddress_Active DEFAULT N'Y' FOR Active
            //end

            //if not exists(select 1 from sys.sysobjects where [name] = 'DF_BusinessPartnerContactPerson_Active')
            //begin
            //    ALTER TABLE BusinessPartnerContactPerson ADD CONSTRAINT DF_BusinessPartnerContactPerson_Active DEFAULT N'Y' FOR Active
            //end

            ctx.Database
                .ExecuteSqlCommand(
                @"
                if not exists(select 1 from sys.sysobjects where [name] = 'DF_ItemMaster_Active')
                begin
                    ALTER TABLE ItemMaster ADD CONSTRAINT DF_ItemMaster_Active DEFAULT N'Y' FOR Active
                end

                if not exists(select 1 from sys.sysobjects where [name] = 'DF_ItemGroup_Active')
                begin
                    ALTER TABLE ItemGroup ADD CONSTRAINT DF_ItemGroup_Active DEFAULT N'Y' FOR Active
                end

                if not exists(select 1 from sys.sysobjects where [name] = 'DF_Warehouse_Active')
                begin
                    ALTER TABLE Warehouse ADD CONSTRAINT DF_Warehouse_Active DEFAULT N'Y' FOR Active
                end

                if not exists(select 1 from sys.sysobjects where [name] = 'DF_DocNumberingDetail_Active')
                begin
                    ALTER TABLE DocNumberingDetail ADD CONSTRAINT DF_DocNumberingDetail_Active DEFAULT N'Y' FOR Active
                end

                if not exists(select 1 from sys.sysobjects where [name] = 'DF_GLAccount_Active')
                begin
                    ALTER TABLE GLAccount ADD CONSTRAINT DF_GLAccount_Active DEFAULT N'Y' FOR Active
                end

                ");
        }
        void ExecCustomSQL(DbTrackerContext ctx)
        {

            //Server server = new Server(new ServerConnection(connection));
            //server.ConnectionContext.ExecuteNonQuery(script);

            DbConnection conn = ctx.Database.Connection;
            var smoServer = new Server(new ServerConnection(new SqlConnection(conn.ConnectionString)));
            var db = smoServer.Databases[conn.Database];

            //var sqlFile = "Codebiz.Domain.ERP.Context.CustomSQL.Custom_SP_Scripts.sql";

            using (Stream s = assembly.GetManifestResourceStream("Codebiz.Domain.ERP.Context.CustomSQL.Custom_SP_Scripts.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                var sqlScript = sr.ReadToEnd();
                smoServer.ConnectionContext.ExecuteNonQuery(sqlScript);
            }


            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_CancelIntnernalRecon.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetAvailableSerialNo.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetDocumentListByObjType.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }


            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetInternalReconLines.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }


            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetItemsForInventoryQtyPosting.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetOpenInvoice.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }
            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetOpenInvoiceEx.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }
            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetOpenInvoiceForRecon.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetPaymentLines.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetPostingPeriod.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_PostTransaction.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_ReconcileDoc.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_UpdateBaseOpenQty.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_UpdateInventoryOnHand.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_UpdateOrderQty.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }


            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_InventoryQtyCount.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }


            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetStockPrice.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetTransactionJournalReport.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_GetPNLReport.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }

            using (Stream s = assembly.GetManifestResourceStream($"Codebiz.Domain.ERP.Context.CustomSQL.SP_DocFormGridCustomCommands.sql"))
            using (StreamReader sr = new StreamReader(s))
            {
                smoServer.ConnectionContext.ExecuteNonQuery(sr.ReadToEnd());
            }
        }
    
  
    
        //void CleanTablesForModelBaseMigration(DbTrackerContext ctx)
        //{
        //    ctx.Database
        //     .ExecuteSqlCommand(@"
        //        delete from ItemMaster
        //        delete from ItemGroup
        //        delete from Warehouse
        //        delete from GLAccount 
        //        delete from FinancialPeriodDetail 
        //        delete from FinancialPeriod
        //        delete from TaxGroup 
        //        delete from AdminSetting 
        //        delete from DocNumberingDetail 
        //        delete from DocNumbering
        //        ");
        //}
    }
    public static class DbSetExtensions
    {
        public static T AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }
    }
}