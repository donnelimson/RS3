using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Logging;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;
using Web.Models.ViewModels.Province;

namespace Web.Controllers
{
    public class ProvinceController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProvinceService _provinceService;
        private readonly IAppUserServices _appUserService;

        public ProvinceController(
            IProvinceService provinceService,
            IAppUserServices appUserServices, 
            IUnitOfWork unitOfWork)
            : base(appUserServices)
        {
            _unitOfWork = unitOfWork;
            _provinceService = provinceService;
            _appUserService = appUserServices;
        }

        // GET: Province
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewProvince)]
        public ActionResult Index()
        {
            return View();
        }

        #region JSON

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddProvince)]
        public JsonResult AddProvince(ProvinceViewModel model)
        {
            return AddUpdateProvince(model);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditProvince)]
        public JsonResult UpdateProvince(ProvinceViewModel model)
        {
            return AddUpdateProvince(model);
        }

        private JsonResult AddUpdateProvince(ProvinceViewModel model)
        {
            string message = "";
            bool success = true;

            try
            {
                var isAbbreviationExists = _provinceService.CheckAbbreviationIfExists(model.Abbreviation, model.RegionId, model.ProvinceId);
                var isNameExists = _provinceService.CheckNameIfExists(model.Abbreviation, model.RegionId, model.ProvinceId);

                if (isAbbreviationExists)
                {
                    success = false;
                    message = "Abbreviation already exists!";
                }

                if (isNameExists)
                {
                    success = false;
                    message = "Name already exists!";
                }

                if (success)
                {
                    var province = _provinceService.GetById(model.ProvinceId) ?? new Province();


                    province.Name = model.Name;
                    province.Abbreviation = model.Abbreviation;
                    province.RegionId = model.RegionId;

                    _provinceService.InsertOrUpdate(province, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();
                    success = true;

                    if (model.ProvinceId == 0)
                    {
                        message = "Province has been successfully created!";
                    }
                    else
                    {
                        message = "Province has been successfully updated!";
                    }
                    Logger.Info($"{message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(model), model.ProvinceId == 0 ? LogEventTitles.ProvinceCreated : LogEventTitles.ProvinceUpdated, "", "", JsonConvert.SerializeObject(model));
                }
            }
            catch (DbUpdateException dbEx)
            {
                success = false;
                Logger.Error(
                    $"Failed to save Province! {dbEx.InnerException.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            catch (Exception ex)
            {
                success = false;
                Logger.Error($"Error! {ex.InnerException.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            return Json(new
            {
                success,
                message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanDeActivateReActivateProvince)]
        public JsonResult ToggleProvinceActiveStatus(int id)
        {
            var success = true;
            var isActive = true;

            string message;
            try
            {
                var province = _provinceService.GetById(id);

                province.IsActive = !province.IsActive;
                isActive = province.IsActive;
                _provinceService.InsertOrUpdate(province, CurrentUser.AppUserId);
                _unitOfWork.SaveChanges();

                if (isActive)
                {
                    message = "Province has been successfully reactivated.";
                    Logger.Info($"{message}. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.ProvinceActivated);
                }
                else
                {
                    message = "Province has been successfully deactivated.";
                    Logger.Info($"{message}. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.ProvinceDeactivated);
                }

            }
            catch (DbUpdateException dbEx)
            {
                success = false;

                if (isActive)
                {
                    message = "Failed to reactivate province!";
                }
                else
                {
                    message = "Failed to deactivate province!";
                }

                Logger.Error($"{message} {dbEx.InnerException.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            catch (Exception ex)
            {
                message = ex.Message;
                success = false;
                Logger.Error($"{ex.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            return Json(new
            {
                success,
                message
            }, JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportProvinces)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(ProvinceFilter filter)
        {
            var exportResult = _provinceService.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, CurrentUser.CurrentOffice);

            return Json(new
            {
                data = new
                {
                    FileName = exportResult
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region JSON Get

        [HttpGet]
        public JsonResult GetProvinces(ProvinceFilter filter)
        {
            var result = _provinceService.SearchProvinces(filter);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProvinceDetails(int provinceId)
        {
            var result = _provinceService.GetById(provinceId);
            return Json(new
            {
                data = new
                {
                    result.ProvinceId,
                    result.Name,
                    result.Abbreviation,
                    result.RegionId,

                }

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProvinceLookUp()
        {
            var result = _provinceService.GetAll().Where(a=> a.IsActive);

            return Json(new
            {
                data = result.Select(a => new
                {
                    a.ProvinceId,
                    a.Name,
                    a.Abbreviation,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvinceByRegion(int regionId)
        {
            var result = _provinceService.GetAllProvinceByRegionId(regionId);

            return Json(new
            {
                data = result.Select(a => new
                {
                    a.ProvinceId,
                    a.Name,
                    a.Abbreviation,
                })
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTarlacProvinceId()
        {
            return Json(new { provinceId = _provinceService.GetTarlacProvinceId() }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
   
    