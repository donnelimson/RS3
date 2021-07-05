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

   


     

            #region Position

            var positionPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Position).Select(a => a.PermissionGroupId).FirstOrDefault();
            var positionIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Positions);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = positionIndexNavLink.NavLinkId, PermissionGroupId = positionPermissionGroupId, Code = PermissionData.UserCanViewPosition, Description = PermissionDataDescription.UserCanViewPosition.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = positionIndexNavLink.NavLinkId, PermissionGroupId = positionPermissionGroupId, Code = PermissionData.UserCanAddPosition, Description = PermissionDataDescription.UserCanAddPosition.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = positionIndexNavLink.NavLinkId, PermissionGroupId = positionPermissionGroupId, Code = PermissionData.UserCanEditPosition, Description = PermissionDataDescription.UserCanEditPosition.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = positionIndexNavLink.NavLinkId, PermissionGroupId = positionPermissionGroupId, Code = PermissionData.UserCanDeletePosition, Description = PermissionDataDescription.UserCanDeletePosition.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = positionIndexNavLink.NavLinkId, PermissionGroupId = positionPermissionGroupId, Code = PermissionData.UserCanDeActivateReActivatePosition, Description = PermissionDataDescription.UserCanDeActivateReActivatePosition.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = positionIndexNavLink.NavLinkId, PermissionGroupId = positionPermissionGroupId, Code = PermissionData.UserCanExportPositions, Description = PermissionDataDescription.UserCanExportPositions.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

     

            #region Address

            var managementParentNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Management);

            #region Region

            var regionPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Region).Select(a => a.PermissionGroupId).FirstOrDefault();
            var regionIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Regions);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = regionIndexNavLink.NavLinkId, PermissionGroupId = regionPermissionGroupId, Code = PermissionData.UserCanViewRegion, Description = PermissionDataDescription.UserCanViewRegion.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = regionIndexNavLink.NavLinkId, PermissionGroupId = regionPermissionGroupId, Code = PermissionData.UserCanAddRegion, Description = PermissionDataDescription.UserCanAddRegion.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = regionIndexNavLink.NavLinkId, PermissionGroupId = regionPermissionGroupId, Code = PermissionData.UserCanEditRegion, Description = PermissionDataDescription.UserCanEditRegion.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = regionIndexNavLink.NavLinkId, PermissionGroupId = regionPermissionGroupId, Code = PermissionData.UserCanDeActivateReActivateRegion, Description = PermissionDataDescription.UserCanDeActivateReActivateRegion.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = regionIndexNavLink.NavLinkId, PermissionGroupId = regionPermissionGroupId, Code = PermissionData.UserCanExportRegions, Description = PermissionDataDescription.UserCanExportRegions.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #region Province

            var provincePermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Province).Select(a => a.PermissionGroupId).FirstOrDefault();
            var provinceIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Provinces);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = provinceIndexNavLink.NavLinkId, PermissionGroupId = provincePermissionGroupId, Code = PermissionData.UserCanViewProvince, Description = PermissionDataDescription.UserCanViewProvince.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = provinceIndexNavLink.NavLinkId, PermissionGroupId = provincePermissionGroupId, Code = PermissionData.UserCanAddProvince, Description = PermissionDataDescription.UserCanAddProvince.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = provinceIndexNavLink.NavLinkId, PermissionGroupId = provincePermissionGroupId, Code = PermissionData.UserCanEditProvince, Description = PermissionDataDescription.UserCanEditProvince.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = provinceIndexNavLink.NavLinkId, PermissionGroupId = provincePermissionGroupId, Code = PermissionData.UserCanDeActivateReActivateProvince, Description = PermissionDataDescription.UserCanDeActivateReActivateProvince.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = provinceIndexNavLink.NavLinkId, PermissionGroupId = provincePermissionGroupId, Code = PermissionData.UserCanExportProvinces, Description = PermissionDataDescription.UserCanExportProvinces.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = provinceIndexNavLink.NavLinkId, PermissionGroupId = provincePermissionGroupId, Code = PermissionData.UserCanDeleteProvince, Description = PermissionDataDescription.UserCanDeleteProvince.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #region City/Town

            var citytownPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.CityTown).Select(a => a.PermissionGroupId).FirstOrDefault();
            var citytownIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.CitiesTowns);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = citytownIndexNavLink.NavLinkId, PermissionGroupId = citytownPermissionGroupId, Code = PermissionData.UserCanViewCityTownData, Description = PermissionDataDescription.UserCanViewCityTownData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = citytownIndexNavLink.NavLinkId, PermissionGroupId = citytownPermissionGroupId, Code = PermissionData.UserCanAddCityTownData, Description = PermissionDataDescription.UserCanAddCityTownData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = citytownIndexNavLink.NavLinkId, PermissionGroupId = citytownPermissionGroupId, Code = PermissionData.UserCanEditCityTownData, Description = PermissionDataDescription.UserCanEditCityTownData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = citytownIndexNavLink.NavLinkId, PermissionGroupId = citytownPermissionGroupId, Code = PermissionData.UserCanDeleteCityTownData, Description = PermissionDataDescription.UserCanDeleteCityTownData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = citytownIndexNavLink.NavLinkId, PermissionGroupId = citytownPermissionGroupId, Code = PermissionData.UserCanReactivateDeactivateCityTownData, Description = PermissionDataDescription.UserCanReactivateDeactivateCityTownData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = citytownIndexNavLink.NavLinkId, PermissionGroupId = citytownPermissionGroupId, Code = PermissionData.UserCanExportCityTownViewList, Description = PermissionDataDescription.UserCanExportCityTownViewList.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #region Barangay

            var barangayPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Barangay).Select(a => a.PermissionGroupId).FirstOrDefault();
            var barangayIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.Barangays);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = barangayIndexNavLink.NavLinkId, PermissionGroupId = barangayPermissionGroupId, Code = PermissionData.UserCanViewBarangayData, Description = PermissionDataDescription.UserCanViewBarangayData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = barangayIndexNavLink.NavLinkId, PermissionGroupId = barangayPermissionGroupId, Code = PermissionData.UserCanAddBarangayData, Description = PermissionDataDescription.UserCanAddBarangayData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = barangayIndexNavLink.NavLinkId, PermissionGroupId = barangayPermissionGroupId, Code = PermissionData.UserCanEditBarangayData, Description = PermissionDataDescription.UserCanEditBarangayData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = barangayIndexNavLink.NavLinkId, PermissionGroupId = barangayPermissionGroupId, Code = PermissionData.UserCanReactivateDeactivateBarangayData, Description = PermissionDataDescription.UserCanReactivateDeactivateBarangayData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = barangayIndexNavLink.NavLinkId, PermissionGroupId = barangayPermissionGroupId, Code = PermissionData.UserCanExportBarangayViewList, Description = PermissionDataDescription.UserCanExportBarangayViewList.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = barangayIndexNavLink.NavLinkId, PermissionGroupId = barangayPermissionGroupId, Code = PermissionData.UserCanDeleteBarangayData, Description = PermissionDataDescription.UserCanDeleteBarangayData.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #region Purok

            var purokPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Purok).Select(a => a.PermissionGroupId).FirstOrDefault();
            var purokIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == "Puroks");
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = purokIndexNavLink.NavLinkId, PermissionGroupId = purokPermissionGroupId, Code = PermissionData.UserCanViewPurok, Description = PermissionDataDescription.UserCanViewPurok.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = purokIndexNavLink.NavLinkId, PermissionGroupId = purokPermissionGroupId, Code = PermissionData.UserCanAddPurok, Description = PermissionDataDescription.UserCanAddPurok.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = purokIndexNavLink.NavLinkId, PermissionGroupId = purokPermissionGroupId, Code = PermissionData.UserCanEditPurok, Description = PermissionDataDescription.UserCanEditPurok.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = purokIndexNavLink.NavLinkId, PermissionGroupId = purokPermissionGroupId, Code = PermissionData.UserCanDeletePurok, Description = PermissionDataDescription.UserCanDeletePurok.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = purokIndexNavLink.NavLinkId, PermissionGroupId = purokPermissionGroupId, Code = PermissionData.UserCanDeActivateReActivatePurok, Description = PermissionDataDescription.UserCanDeActivateReActivatePurok.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = purokIndexNavLink.NavLinkId, PermissionGroupId = purokPermissionGroupId, Code = PermissionData.UserCanExportPuroks, Description = PermissionDataDescription.UserCanExportPuroks.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #region Sitio

            var sitioPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Sitio).Select(a => a.PermissionGroupId).FirstOrDefault();
            var sitioIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == "Sitios");
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = sitioIndexNavLink.NavLinkId, PermissionGroupId = sitioPermissionGroupId, Code = PermissionData.UserCanViewSitio, Description = PermissionDataDescription.UserCanViewSitio.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = sitioIndexNavLink.NavLinkId, PermissionGroupId = sitioPermissionGroupId, Code = PermissionData.UserCanAddSitio, Description = PermissionDataDescription.UserCanAddSitio.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = sitioIndexNavLink.NavLinkId, PermissionGroupId = sitioPermissionGroupId, Code = PermissionData.UserCanEditSitio, Description = PermissionDataDescription.UserCanEditSitio.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = sitioIndexNavLink.NavLinkId, PermissionGroupId = sitioPermissionGroupId, Code = PermissionData.UserCanDeleteSitio, Description = PermissionDataDescription.UserCanDeleteSitio.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = sitioIndexNavLink.NavLinkId, PermissionGroupId = sitioPermissionGroupId, Code = PermissionData.UserCanDeActivateReActivateSitio, Description = PermissionDataDescription.UserCanDeActivateReActivateSitio.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = sitioIndexNavLink.NavLinkId, PermissionGroupId = sitioPermissionGroupId, Code = PermissionData.UserCanExportSitios, Description = PermissionDataDescription.UserCanExportSitios.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion

            #region Route
            var routePermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.Route).Select(a => a.PermissionGroupId).FirstOrDefault();
            var routeIndexNavLink = context.NavLinks.FirstOrDefault(a => a.Name == "Routes");
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = routeIndexNavLink.NavLinkId, PermissionGroupId = routePermissionGroupId, Code = PermissionData.UserCanViewRoute, Description = PermissionDataDescription.UserCanViewRoute.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = routeIndexNavLink.NavLinkId, PermissionGroupId = routePermissionGroupId, Code = PermissionData.UserCanAddRoute, Description = PermissionDataDescription.UserCanAddRoute.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = routeIndexNavLink.NavLinkId, PermissionGroupId = routePermissionGroupId, Code = PermissionData.UserCanEditRoute, Description = PermissionDataDescription.UserCanEditRoute.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = routeIndexNavLink.NavLinkId, PermissionGroupId = routePermissionGroupId, Code = PermissionData.UserCanDeleteRoute, Description = PermissionDataDescription.UserCanDeleteRoute.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = routeIndexNavLink.NavLinkId, PermissionGroupId = routePermissionGroupId, Code = PermissionData.UserCanDeactivateReactivateRoute, Description = PermissionDataDescription.UserCanDeactivateReactivateRoute.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = routeIndexNavLink.NavLinkId, PermissionGroupId = routePermissionGroupId, Code = PermissionData.UserCanExportRoute, Description = PermissionDataDescription.UserCanExportRoute.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            #endregion

            #endregion



            #region Supporting Documents

            var supportingDocumentsPermissionGroupId = context.PermissionGroups.Where(a => a.Name == PermissionGroupData.SupportingDocuments).Select(a => a.PermissionGroupId).FirstOrDefault();
            var supportingDocumentsNavlink = context.NavLinks.FirstOrDefault(a => a.Name == NavLinkData.SupportingDocuments);
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = supportingDocumentsNavlink.NavLinkId, PermissionGroupId = supportingDocumentsPermissionGroupId, Code = PermissionData.UserCanViewSupportingDocuments, Description = PermissionDataDescription.UserCanViewSupportingDocuments.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = supportingDocumentsNavlink.NavLinkId, PermissionGroupId = supportingDocumentsPermissionGroupId, Code = PermissionData.UserCanAddSupportingDocuments, Description = PermissionDataDescription.UserCanAddSupportingDocuments.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = supportingDocumentsNavlink.NavLinkId, PermissionGroupId = supportingDocumentsPermissionGroupId, Code = PermissionData.UserCanEditSupportingDocuments, Description = PermissionDataDescription.UserCanEditSupportingDocuments.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = supportingDocumentsNavlink.NavLinkId, PermissionGroupId = supportingDocumentsPermissionGroupId, Code = PermissionData.UserCanDeleteSupportingDocuments, Description = PermissionDataDescription.UserCanDeleteSupportingDocuments.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.Permissions.AddOrUpdate(a => a.Code, new Permission { NavLinkId = supportingDocumentsNavlink.NavLinkId, PermissionGroupId = supportingDocumentsPermissionGroupId, Code = PermissionData.UserCanExportSupportingDocuments, Description = PermissionDataDescription.UserCanExportSupportingDocuments.ToSentenceCase(), IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

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
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Department, Description = PermissionGroupData.Department, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Division, Description = PermissionGroupData.Division, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Position, Description = PermissionGroupData.Position, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Address, Description = PermissionGroupData.Address, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Region, Description = PermissionGroupData.Region, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Province, Description = PermissionGroupData.Province, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.CityTown, Description = PermissionGroupData.CityTown, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Barangay, Description = PermissionGroupData.Barangay, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Purok, Description = PermissionGroupData.Purok, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Sitio, Description = PermissionGroupData.Sitio, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Route, Description = PermissionGroupData.Route, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            //context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.WorkOrder, Description = PermissionGroupData.WorkOrder, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Purpose, Description = PermissionGroupData.Purpose, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.SupportingDocuments, Description = PermissionGroupData.SupportingDocuments, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.DocumentNumbering, Description = PermissionGroupData.DocumentNumbering, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });


            #endregion

            #region RS3
            context.PermissionGroups.AddOrUpdate(a => a.Name, new PermissionGroup { Name = PermissionGroupData.Ticket, Description = PermissionGroupData.Ticket, IsActive = true, CreatedOn = now, CreatedByAppUserId = AdminUser.AppUserId });

            #endregion


            context.SaveChanges();
        }
    }
}