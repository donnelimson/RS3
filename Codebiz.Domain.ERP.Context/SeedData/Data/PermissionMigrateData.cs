using Codebiz.Domain.Common.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Codebiz.Domain.ERP.Context.SeedData
{
    public static class PermissionMigrateData
    {
        public static void CreatePermissions(DbTrackerContext context, DateTime now, AppUser AdminUser)
        {
            #region Dashboard

            var dashBoardPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Dashboard).Select(a => a.PermissionGroupId).FirstOrDefault();
            var dashBoardNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Dashboard);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = dashBoardNavLink.NavLinkId, PermissionGroupId = dashBoardPermissionGroupId, Code = PermissionData.UserCanViewDashboard, Description = PermissionDataDescription.UserCanViewDashboard.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.SaveChanges();

            #endregion

            #region Admin

            #region User Group

            var userGroupsPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.UserGroups).Select(a => a.PermissionGroupId).FirstOrDefault();
            var userGroupsIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.UserGroups);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = userGroupsIndexNavLink.NavLinkId, PermissionGroupId = userGroupsPermissionGroupId, Code = PermissionData.UserCanViewUserGroup, Description = PermissionDataDescription.UserCanViewUserGroup.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = userGroupsIndexNavLink.NavLinkId, PermissionGroupId = userGroupsPermissionGroupId, Code = PermissionData.UserCanExportUserGroupViewList, Description = PermissionDataDescription.UserCanExportUserGroupViewList.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = userGroupsIndexNavLink.NavLinkId, PermissionGroupId = userGroupsPermissionGroupId, Code = PermissionData.UserCanAddUserGroup, Description = PermissionDataDescription.UserCanAddUserGroup.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = userGroupsIndexNavLink.NavLinkId, PermissionGroupId = userGroupsPermissionGroupId, Code = PermissionData.UserCanEditUserGroup, Description = PermissionDataDescription.UserCanEditUserGroup.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = userGroupsIndexNavLink.NavLinkId, PermissionGroupId = userGroupsPermissionGroupId, Code = PermissionData.UserCanDeleteUserGroup, Description = PermissionDataDescription.UserCanDeleteUserGroup.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = userGroupsIndexNavLink.NavLinkId, PermissionGroupId = userGroupsPermissionGroupId, Code = PermissionData.UserCanDeactivateReactivateUserGroup, Description = PermissionDataDescription.UserCanDeactivateReactivateUserGroup.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            context.SaveChanges();

            #endregion

            #region Configuration Setting

            var configSettingPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.ConfigSetting).Select(a => a.PermissionGroupId).FirstOrDefault();
            var configSettingsIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.ConfigSetting);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = configSettingsIndexNavLink.NavLinkId, PermissionGroupId = configSettingPermissionGroupId, Code = PermissionData.UserCanViewConfigSettingData, Description = PermissionDataDescription.UserCanViewConfigSettingData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = configSettingsIndexNavLink.NavLinkId, PermissionGroupId = configSettingPermissionGroupId, Code = PermissionData.UserCanEditConfigSettingData, Description = PermissionDataDescription.UserCanEditConfigSettingData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = configSettingsIndexNavLink.NavLinkId, PermissionGroupId = configSettingPermissionGroupId, Code = PermissionData.UserCanExportConfigurationSettingViewList, Description = PermissionDataDescription.UserCanExportConfigurationSettingViewList.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #region Nav Links

            var navLinksPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.NavLinks).Select(a => a.PermissionGroupId).FirstOrDefault();
            var navLinksIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.NavLinks);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = navLinksIndexNavLink.NavLinkId, PermissionGroupId = navLinksPermissionGroupId, Code = PermissionData.UserCanViewNavLink, Description = PermissionDataDescription.UserCanViewNavLink.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = navLinksIndexNavLink.NavLinkId, PermissionGroupId = navLinksPermissionGroupId, Code = PermissionData.UserCanExportNavigationLinkViewList, Description = PermissionDataDescription.UserCanExportNavigationLinkViewList.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = navLinksIndexNavLink.NavLinkId, PermissionGroupId = navLinksPermissionGroupId, Code = PermissionData.UserCanAddNavLink, Description = PermissionDataDescription.UserCanAddNavLink.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = navLinksIndexNavLink.NavLinkId, PermissionGroupId = navLinksPermissionGroupId, Code = PermissionData.UserCanEditNavLink, Description = PermissionDataDescription.UserCanEditNavLink.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = navLinksIndexNavLink.NavLinkId, PermissionGroupId = navLinksPermissionGroupId, Code = PermissionData.UserCanDeleteNavLink, Description = PermissionDataDescription.UserCanDeleteNavLink.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

      

            #region Log 

            var auditLogPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.AuditLogs).Select(a => a.PermissionGroupId).FirstOrDefault();
            var auditLogNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.AuditLogs);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = auditLogNavLink.NavLinkId, PermissionGroupId = auditLogPermissionGroupId, Code = PermissionData.UserCanAuditViewLogs, Description = PermissionDataDescription.UserCanAuditViewLogs.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #endregion

        
            #region Management

        

            #region User

            var usersPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Users).Select(a => a.PermissionGroupId).FirstOrDefault();
            var usersIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Users);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanViewAppUser, Description = PermissionDataDescription.UserCanViewAppUser.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanExportUsers, Description = PermissionDataDescription.UserCanExportUsers.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanAddAppUser, Description = PermissionDataDescription.UserCanAddAppUser.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanEditAppUser, Description = PermissionDataDescription.UserCanEditAppUser.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanActivateDeactivateAppUser, Description = PermissionDataDescription.UserCanActivateDeactivateAppUser.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanResetPasswordOfAppUser, Description = PermissionDataDescription.UserCanResetPasswordOfAppUser.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanActivateAppUserAccount, Description = PermissionDataDescription.UserCanActivateAppUserAccount.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanUnlockAppUserAccount, Description = PermissionDataDescription.UserCanUnlockAppUserAccount.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = usersIndexNavLink.NavLinkId, PermissionGroupId = usersPermissionGroupId, Code = PermissionData.UserCanViewAppUserProfile, Description = PermissionDataDescription.UserCanViewAppUserProfile.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

   


            #region rs3
            var ticketPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Ticket).Select(a => a.PermissionGroupId).FirstOrDefault();
            var ticketIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Tickets);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanCreateTicket, Description = PermissionDataDescription.UserCanCreateTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanEditTicket, Description = PermissionDataDescription.UserCanEditTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanDeleteTicket, Description = PermissionDataDescription.UserCanDeleteTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanAssignTicket, Description = PermissionDataDescription.UserCanAssignTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanMoveTicket, Description = PermissionDataDescription.UserCanMoveTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanTakeTicket, Description = PermissionDataDescription.UserCanTakeTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanViewTicketList, Description = PermissionDataDescription.UserCanViewTicketList.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanViewTicket, Description = PermissionDataDescription.UserCanViewTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanSetTicketPriority, Description = PermissionDataDescription.UserCanSetTicketPriority.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = ticketIndexNavLink.NavLinkId, PermissionGroupId = ticketPermissionGroupId, Code = PermissionData.UserCanResolveOrReopenTicket, Description = PermissionDataDescription.UserCanResolveOrReopenTicket.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            
            #endregion


            context.SaveChanges();

            #endregion
  
        }

        public static void CreatePermissionGroups(DbTrackerContext context, DateTime now, AppUser AdminUser)
        {
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Dashboard, Description = PermissionGroupData.Dashboard, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #region Admin

            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.UserGroups, Description = PermissionGroupData.UserGroups, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.ConfigSetting, Description = PermissionGroupData.ConfigSetting, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.NavLinks, Description = PermissionGroupData.NavLinks, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Permissions, Description = PermissionGroupData.Permissions, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.PermissionGroups, Description = PermissionGroupData.PermissionGroups, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.AuditLogs, Description = PermissionGroupData.AuditLogs, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion


            #region Management


            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Users, Description = PermissionGroupData.Users, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });       
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Office, Description = PermissionGroupData.Office, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });


            #endregion

            #region RS3
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Ticket, Description = PermissionGroupData.Ticket, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion


            context.SaveChanges();
        }
    }
}