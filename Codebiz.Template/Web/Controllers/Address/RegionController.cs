using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.ViewModel.Region;
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

namespace Web.Controllers
{
    public class RegionController : BaseController
    {
        private readonly IRegionService _regionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserServices _appUserService;
        readonly AjaxResult ajaxResult = new AjaxResult();

        public RegionController(IRegionService regionService,
            IAppUserServices appUserServices,
            IUnitOfWork unitOfWork)
            : base(appUserServices)
        {
            _regionService = regionService;
            _unitOfWork = unitOfWork;
            _appUserService = appUserServices;
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewRegion)]
        public ActionResult Index()
        {
            return View();
        }

        #region Add or Update

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddRegion)]
        public JsonResult AddRegion(RegionViewModel model)
        {
            var result = AddUpdateRegion(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditRegion)]
        public JsonResult UpdateRegion(RegionViewModel model)
        {
            var result = AddUpdateRegion(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        private AjaxResult AddUpdateRegion(RegionViewModel model)
        {
            ajaxResult.LogEventTitle = model.RegionId == 0 ? LogEventTitles.RegionActivated : LogEventTitles.RegionUpdated;
            string proccessType = model.RegionId == 0 ? "create" : "update";

            try
            {

                if (_regionService.CheckNameIfExists(model.Name, model.RegionId))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Name already exists!";
                }
                if (_regionService.CheckAbbreviationIfExists(model.Abbreviation, model.RegionId))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Abbreviation already exists!";
                }

                if (ajaxResult.Success)
                {
                    _regionService.AddUpdateRegion(model, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    ajaxResult.Message = $"{model.Name} has been successfully {proccessType}d!";
                    Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(model), ajaxResult.LogEventTitle, "", "", JsonConvert.SerializeObject(model));
                }
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {proccessType} {model.Name}! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }

            catch (Exception ex)
            {
                ajaxResult.Success = false;
                Logger.Error($"Error! {ex.InnerException.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            return ajaxResult;
        }

        #endregion

        #region Reactivate / Deactivate

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanDeActivateReActivateRegion)]
        public JsonResult ToggleRegionActiveStatus(int id, bool isActive, string name)
        {
            ajaxResult.LogEventTitle = isActive ? LogEventTitles.RegionActivated : LogEventTitles.RegionDeactivated;
            string proccessType = isActive ? "reactivate" : "deactivate";

            try
            {
                if (_regionService.CheckRegionIfInUse(id))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = $"Unable to deactivate {name}. {name} is in used";
                }
                else
                {
                    _regionService.ToggleRegionActiveStatus(id, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    ajaxResult.Message = $"{name} has been successfully {proccessType}d.";
                    Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", ajaxResult.LogEventTitle);
                }

            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {proccessType} {name}! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }

            catch (Exception ex)
            {
                ajaxResult.Message = ex.Message;
                ajaxResult.Success = false;
                Logger.Error($"{ex.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Export Data To Excel File

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportRegions)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(RegionFilter filter)
        {
         
            return Json(new
            {
                data = new
                {
                    FileName = _regionService.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, CurrentUser.CurrentOffice)
        }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get Functions

        [HttpGet]
        public JsonResult GetRegions(RegionFilter filter)
        {
            return Json(new
            {
                data = _regionService.SearchRegions(filter).ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetRegionDetails(int regionId)
        {
            return Json(new
            {
                data = _regionService.GetRegionDetails(regionId)

            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Look Up

        [HttpGet]
        public JsonResult GetRegionLookUp()
        {
            var result = _regionService.GetAll();

            return Json(new
            {
                data = result.Select(a => new
                {
                    a.RegionId,
                    a.Name,
                    a.Abbreviation,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}