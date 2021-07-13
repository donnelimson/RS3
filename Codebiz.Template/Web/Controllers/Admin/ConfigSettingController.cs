using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;
using Web.Models.ViewModels.ConfigSetting;

namespace Web.Controllers
{
    public class ConfigSettingController : BaseController
    {
        private readonly IConfigSettingDataTypeService _configSettingDataTypeService;
        private readonly IConfigSettingGroupService _configSettingGroupService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigSettingService _configSettingService;
        private readonly IAppUserServices _appUserServices;
        public ConfigSettingController(IConfigSettingService configSettingService
                                       , IConfigSettingDataTypeService configSettingDataTypeService
                                       , IConfigSettingGroupService configSettingGroupService
                                       , IAppUserServices appUserServices
                                       , IUnitOfWork unitOfWork) :base(appUserServices) 
        {
            _configSettingService = configSettingService;
            _configSettingDataTypeService = configSettingDataTypeService;
            _configSettingGroupService = configSettingGroupService;
            _appUserServices = appUserServices;
            _unitOfWork = unitOfWork;
        }

        #region View and Search
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewConfigSettingData)]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchConfigSettings(string searchTerm , int page, int pageSize, string sortColumn, string sortOrder,
           string createdBy, DateTime? createdOnFrom, DateTime? createdOnTo)
        {
            var configSettingsFilter = new ConfigSettingsFilter
            {
                SearchTerm = searchTerm,
                CreatedBy = createdBy,
                CreatedOnFrom = createdOnFrom,
                CreatedOnTo = createdOnTo,
                Page = page,
                PageSize = pageSize,
                SortColumn = string.IsNullOrEmpty(sortColumn) ? "CreatedDate" : sortColumn.Replace(" ", string.Empty),
                SortDirection = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder.Replace(" ", string.Empty)

            };

            var result = _configSettingService.Search(configSettingsFilter);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = configSettingsFilter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Edit
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditConfigSettingData)]
        public ActionResult Edit(int id)
        {
            var configSetting = _configSettingService.GetById(id);

            var vM = new ConfigSettingViewModel
            {
                ConfigSettingId = configSetting.ConfigSettingId,
                Name = configSetting.Name,
                Description = configSetting.Description,
                Value = configSetting.Value,
                ConfigSettingDataTypeId = configSetting.ConfigSettingDataTypeId ?? 0,
                ConfigSettingGroupId = configSetting.ConfigSettingGroupId ?? 0,
                ConfigSettingDataTypeLookUp = GetConfigSettingDataTypeLookUp(),
                ConfigSettingGroupLookUp = GetConfigSettingGroupLookUp()

            };

            return View(vM);
        }
        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditConfigSettingData)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConfigSettingViewModel model)
        {
            if (_configSettingService.CheckNameIfExists(model.Name, model.ConfigSettingDataTypeId,model.ConfigSettingGroupId, model.ConfigSettingId))
                ModelState.AddModelError("Name", "Name already exists.");

            if (!ModelState.IsValid)
            {
                model.ConfigSettingDataTypeLookUp = GetConfigSettingDataTypeLookUp();
                model.ConfigSettingGroupLookUp = GetConfigSettingGroupLookUp();

                return View(model);
            }

            var configSetting = _configSettingService.GetById(model.ConfigSettingId);

            configSetting.Name = model.Name;
            configSetting.Description = model.Description;
            configSetting.Value = model.Value;
            configSetting.ConfigSettingDataTypeId = model.ConfigSettingDataTypeId;
            configSetting.ConfigSettingGroupId = model.ConfigSettingGroupId;

            _configSettingService.Update(configSetting, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("Configuration setting successfully updated.");
            Logger.Info($"Configuration setting successfully updated. UserId : [{CurrentUser.AppUserId}]" + JsonConvert.SerializeObject(model), LogEventTitles.ConfigurationSettingsUpdated, "", "", JsonConvert.SerializeObject(model));

            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetConfigSettingDataTypeLookUp()
        {
            return _configSettingDataTypeService.GetAll()
                .ToList()
                .Select(a => new SelectListItem() { Text = a.CodeName, Value = a.ConfigSettingDataTypeId.ToString() })
                .ToList();
        }
        private List<SelectListItem> GetConfigSettingGroupLookUp()
        {
            return _configSettingGroupService.GetAll()
                .ToList()
                .Select(a => new SelectListItem() { Text = a.CodeName, Value = a.ConfigSettingGroupId.ToString() })
                .ToList();
        }

        #endregion


    }
}