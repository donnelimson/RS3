using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.ViewModel.CityTown;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class CityTownController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityTownService _cityTownService;
        private readonly IProvinceService _provinceService;
        private readonly IAppUserServices _appUserService;
        public CityTownController(ICityTownService cityTownService,
            IProvinceService provinceService,
            IRegionService regionService,
            IAppUserServices appUserServices,
            IUnitOfWork unitOfWork) : base(appUserServices)
        {
            _regionService = regionService;
            _unitOfWork = unitOfWork;
            _cityTownService = cityTownService;
            _provinceService = provinceService;
            _appUserService = appUserServices;

        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewCityTownData)]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllCityTown()
        {
            var result = _cityTownService.GetAll();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewCityTownData)]
        public JsonResult Search(CityTownFilter filter)
        {
            var result = _cityTownService.SearchCityTown(filter);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportCityTownViewList)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(CityTownFilter filter)
        {
            var currentOffice = CurrentUser.CurrentOffice;
            var exportResult = _cityTownService.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, currentOffice);
            return Json(new
            {
                data = new
                {
                    FileName = exportResult
                }
            }, JsonRequestBehavior.AllowGet);
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddCityTownData)]
        public ActionResult Create()
        {
            var vm = new CityTownViewModel
            {
                RegionLookup = GetRegionLookUp(),
                ProvinceLookup = new List<SelectListItem>()
            };

            return View(vm);
        }
        public JsonResult InsertOrUpdate(CityTownAddUpdateViewModel model)
        {
            var result = new AjaxResult();
            string action = "";
            try
            {
                var checkIfCityTownCodeExist = _cityTownService.CheckIfCityTownCodeExist(model.CityTownCode, model.CityTownId);
                var checkIfCityTownNameExist = _cityTownService.CheckNameIfExists(model.Name, model.ProvinceId.Value, model.CityTownId);

                if (checkIfCityTownCodeExist)
                {
                    result.Message = "Code already exist";
                    result.Success = false;
                }

                if (checkIfCityTownNameExist)
                {
                    result.Message = "Name already exist";
                    result.Success = false;
                }

                if (result.Success)
                {
                    _cityTownService.AddOrUpdate(model, CurrentUser.AppUserId);
                    action = model.CityTownId == 0 ? "add" : "update";
                    result.Message = model.CityTownId == 0 ? "Successfully " + action + "ed" + " city town" : "Successfully " + action + "d" + " city town";
                    result.LogEventTitle = model.CityTownId == 0 ? LogEventTitles.CityTownAdded : LogEventTitles.CityTownUpdated;
                    result.Success = true;
                    _unitOfWork.SaveChanges();
                    result.Success = true;
                    Logger.Info($"{result.Message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(model), result.LogEventTitle, "", "", JsonConvert.SerializeObject(model));
                }

            }
            catch (DbUpdateException dbEx)
            {
                result.Success = false;
                result.Message = $"Failed to {action} city town! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{result.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), result.LogEventTitle);
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = $"Failed to {action} city town! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{result.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message), result.LogEventTitle);
            }
            return Json(new { succeed = result.Success, message = result.Message }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCityTownDetails(int cityTownId)
        {
            var result = _cityTownService.GetDetails(cityTownId);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem> GetRegionLookUp()
        {
            return _regionService.GetAll()
                .ToList()
                .Select(a => new SelectListItem() {Text = a.Name, Value = a.RegionId.ToString()})
                .ToList();
        }

        private List<SelectListItem> GetProvinceLookUp(int regionId)
        {
            return _provinceService.GetAll().Where(o=>o.RegionId==regionId)
                .ToList()
                .Select(a => new SelectListItem() { Text = a.Name, Value = a.ProvinceId.ToString() })
                .ToList();
        }

        public ActionResult GetProvince(int regionId)
        {
            var provinceLookup = GetProvinceLookUp(regionId);
            return Json(provinceLookup, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddCityTownData)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityTownViewModel model)
        {
            if (_cityTownService.CheckNameIfExists(model.Name, model.ProvinceId))
                ModelState.AddModelError("Name", "Name already exists.");

            if (!ModelState.IsValid)
            {
                model.RegionLookup = GetRegionLookUp();
                model.ProvinceLookup = GetProvinceLookUp(model.RegionId);

                return View(model);
            }

            var cityTown = new CityTown
            {
                Name = model.Name,
                Abbreviation = model.Abbreviation,
                ZipCode = model.ZipCode,
                ProvinceId = model.ProvinceId,
                IsActive = true,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };

            _cityTownService.Insert(cityTown, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("City/Town successfully created.");
            Logger.Info("City/Town successfully created. CreatedBy : [{0}] | NewValues : [{1}]", CurrentUser.Username, JsonConvert.SerializeObject(model));

            return RedirectToAction("Index");
        }

    
        public JsonResult GetCityTownLookUp(int provinceId)
        {
            var result = _cityTownService.GetByProvinceId(provinceId).Where(a=> a.IsActive);

            return Json(new
            {
                data = result.Select(a => new
                {
                    CityTownId = a.CityTownId,
                    Name = a.Name,
                    Abbreviation = a.Abbreviation,
                    CityCode =  a.CityTownCode
                })
            }, JsonRequestBehavior.AllowGet);
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions,PermissionData.UserCanReactivateDeactivateCityTownData)]
        public JsonResult ReactivateDeactivate(int cityTownId,string name,bool status)
        {
            var ajaxResult = new AjaxResult();
            string action="";
            action = status ? "deactivate" : "reactivate";
            ajaxResult.LogEventTitle = status ? LogEventTitles.CityTownDeactivated : LogEventTitles.CityTownActivated;
            try
            {
                _cityTownService.ReactivateDeactivate(cityTownId, CurrentUser.AppUserId);
                ajaxResult.Message = "City Town with name " + name + " was successfully " + action+"d";
                _unitOfWork.SaveChanges();
                ajaxResult.Success = true;
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]. " +
                                                      $"UserId : [{CurrentUser.AppUserId}]", ajaxResult.LogEventTitle);
            }
            catch (DbEntityValidationException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} city town! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", dbEx.InnerException.Message, ajaxResult.LogEventTitle);
                return Json(new { succeed = ajaxResult.Success, message = ajaxResult.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} city town ! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", ex.InnerException.Message, ajaxResult.LogEventTitle);
                return Json(new { succeed = ajaxResult.Success, message = ajaxResult.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { succeed=ajaxResult.Success, message=ajaxResult.Message}, JsonRequestBehavior.AllowGet);

        }
    
    }
}