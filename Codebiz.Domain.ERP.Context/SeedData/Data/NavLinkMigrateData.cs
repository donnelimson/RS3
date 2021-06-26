using Codebiz.Domain.Common.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class NavLinkMigrateData
    {

        public static void CreateNavLinks(DbTrackerContext context, DateTime now, AppUser AdminUser)
        {
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { Name = NavLinkData.Dashboard, IconClass = "icon-home", Controller = "Home", Action = "Index", Parameters = "", IsActive = true, IsParent = false, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 1 });
            context.SaveChanges();

            #region Admin

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { Name = NavLinkData.Admin, IconClass = "icon-settings", Parameters = "Admin", IsActive = true, IsParent = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 2 });
            context.SaveChanges();

            var adminParentNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Admin);
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = adminParentNavLink.NavLinkId, Name = NavLinkData.UserGroups, Controller = "UserGroup", Action = "Index", Parameters = "", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 3 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = adminParentNavLink.NavLinkId, Name = NavLinkData.ConfigSetting, Controller = "ConfigSetting", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 4 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = adminParentNavLink.NavLinkId, Name = NavLinkData.NavLinks, Controller = "NavLink", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 5 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = adminParentNavLink.NavLinkId, Name = NavLinkData.Permissions, Controller = "Permission", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 6 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = adminParentNavLink.NavLinkId, Name = NavLinkData.AuditLogs, Controller = "Log", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 7 });

            #region Company Setup

            context.NavLinks.AddOrUpdate(a => new { a.Parameters, a.Name }, new NavLink { ParentNavLinkId = adminParentNavLink.NavLinkId, Name = NavLinkData.CompanySetup, IconClass = "", Parameters = "Company Setup", IsActive = true, IsParent = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 8 });
            context.SaveChanges();

            var companyManagementParentNavlink = context.NavLinks
                .FirstOrDefault(x => x.Name == NavLinkData.CompanySetup && x.ParentNavLinkId == adminParentNavLink.NavLinkId);

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = companyManagementParentNavlink.NavLinkId, Name = NavLinkData.GeneralSetting, Controller = "AdminSetting", Action = "GeneralSettings", Area = "ADM", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 9 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = companyManagementParentNavlink.NavLinkId, Name = NavLinkData.CompanyDetail, Controller = "AdminSetting", Action = "CompanyDetails", Area = "ADM", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 10 });

            #endregion

            context.SaveChanges();

            #endregion

            #region Approval

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { Name = NavLinkData.Approval, IconClass = "glyphicon glyphicon-forward", Controller = "Approval", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 11 });
            context.SaveChanges();

            #endregion

            #region Management

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { Name = NavLinkData.Management, IconClass = "icon-settings", Parameters = "Management", IsActive = true, IsParent = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 12 });
            context.SaveChanges();
            var managementParentNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Management);

            #region Company Setup

            context.NavLinks.AddOrUpdate(a => new { a.Parameters, a.Name }, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.CompanySetup, IconClass = "", Parameters = "Company Setup", IsActive = true, IsParent = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 13 });
            context.SaveChanges();

            var companySetupManagementParentNavlink = context.NavLinks
                .FirstOrDefault(x => x.Name == NavLinkData.CompanySetup && x.ParentNavLinkId == managementParentNavLink.NavLinkId);

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = companySetupManagementParentNavlink.NavLinkId, Name = NavLinkData.GeneralSetting, Controller = "AdminSetting", Action = "GeneralSettings", Area = "ADM", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 14 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = companySetupManagementParentNavlink.NavLinkId, Name = NavLinkData.CompanyDetail, Controller = "AdminSetting", Action = "CompanyDetails", Area = "ADM", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 15 });

            #endregion

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Users, Controller = "AppUsers", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 16 });
           // context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Offices, Controller = "Office", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 17 });
         //   context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Departments, Controller = "Department", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 18 });
         //   context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Divisions, Controller = "Division", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 19 });
           context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Positions, Controller = "Position", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 20 });
          //  context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Purpose, Controller = "Purpose", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 20 });

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.ConsumerTypes, Controller = "ConsumerType", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 21 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.NoOfUnitsandKVARating, Controller = "NoOfUnitsAndKvaRating", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 23 });
            //context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.WorkOrders, Controller = "WorkOrder", Action = "Index", IsActive = false, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 24 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.SupportingDocuments, Controller = "SupportingDocuments", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 25 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.DocumentNumberings, Controller = "DocumentNumbering", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 26 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Substations, Controller = "SubStation", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 27 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.LifelineSubsidy, Controller = "LifelineSubsidy", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 28 });

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Poles, Controller = "Pole", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 29 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Tickets, Controller = "Ticket", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 29 });

            #region Address

            context.NavLinks.AddOrUpdate(a => new { a.Parameters, a.Name }, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Address, IconClass = "", Parameters = "Address", IsActive = true, IsParent = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 30 });
            context.SaveChanges();

            var addressManagementParentNavlink = context.NavLinks
                .FirstOrDefault(x => x.Name == NavLinkData.Address && x.ParentNavLinkId == managementParentNavLink.NavLinkId);

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = addressManagementParentNavlink.NavLinkId, Name = NavLinkData.Regions, Controller = "Region", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 31 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = addressManagementParentNavlink.NavLinkId, Name = NavLinkData.Provinces, Controller = "Province", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 32 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = addressManagementParentNavlink.NavLinkId, Name = NavLinkData.CitiesTowns, Controller = "CityTown", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 33 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = addressManagementParentNavlink.NavLinkId, Name = NavLinkData.Barangays, Controller = "Barangay", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 34 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = addressManagementParentNavlink.NavLinkId, Name = NavLinkData.Puroks, Controller = "Purok", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 35 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = addressManagementParentNavlink.NavLinkId, Name = NavLinkData.Sitios, Controller = "Sitio", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 36 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = addressManagementParentNavlink.NavLinkId, Name = NavLinkData.Routes, Controller = "Route", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 37 });

            #endregion

            #region Approval Procedure

            context.NavLinks.AddOrUpdate(a => new { a.Parameters, a.Name }, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.ApprovalProcedures, IconClass = "", Parameters = "Approval Procedures", IsActive = true, IsParent = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 38 });
            context.SaveChanges();

            var approvalProceduresManagementParentNavlink = context.NavLinks
                .FirstOrDefault(x => x.Name == NavLinkData.ApprovalProcedures && x.ParentNavLinkId == managementParentNavLink.NavLinkId);

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = approvalProceduresManagementParentNavlink.NavLinkId, Name = NavLinkData.ApprovalStages, Controller = "ApprovalStage", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 39 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = approvalProceduresManagementParentNavlink.NavLinkId, Name = NavLinkData.ApprovalTemplates, Controller = "ApprovalTemplate", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 40 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = approvalProceduresManagementParentNavlink.NavLinkId, Name = NavLinkData.ApproverLabels, Controller = "ApproverLabel", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 40 });

            #endregion

            #region Banking

            context.NavLinks.AddOrUpdate(a => new { a.Parameters, a.Name }, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Banking, IconClass = "", Parameters = "Banking", IsActive = true, IsParent = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 41 });
            context.SaveChanges();

            var bankingpManagementParentNavlink = context.NavLinks
                .FirstOrDefault(x => x.Name == NavLinkData.Banking && x.ParentNavLinkId == managementParentNavLink.NavLinkId);

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = bankingpManagementParentNavlink.NavLinkId, Name = NavLinkData.Banks, Controller = "Bank", Action = "Index", Area = "", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 42 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = bankingpManagementParentNavlink.NavLinkId, Name = NavLinkData.CreditCards, Controller = "CreditCard", Action = "Index", Area = "", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 43 });
            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = bankingpManagementParentNavlink.NavLinkId, Name = NavLinkData.HouseBankAccounts, Controller = "HouseBankAccount", Action = "Index", Area = "", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 44 });

            #endregion

            context.SaveChanges();

            #endregion

      
        }
    }
}