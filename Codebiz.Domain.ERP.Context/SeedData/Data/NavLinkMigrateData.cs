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




            #endregion

            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Users, Controller = "AppUsers", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 16 });
           // context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Offices, Controller = "Office", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 17 });
         //   context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Departments, Controller = "Department", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 18 });
         //   context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Divisions, Controller = "Division", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 19 });


            context.NavLinks.AddOrUpdate(a => a.Name, new NavLink { ParentNavLinkId = managementParentNavLink.NavLinkId, Name = NavLinkData.Tickets, Controller = "Ticket", Action = "Index", IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId, Ordinal = 29 });


            context.SaveChanges();

   
      
        }
    }
}