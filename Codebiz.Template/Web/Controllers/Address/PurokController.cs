using System;
using System.Linq;
using System.Web.Mvc;
using Codebiz.Domain.Common.Model;
using Infrastructure.Services;
using Web.Filters;
using Infrastructure;
using Logging;
using System.Data.Entity.Infrastructure;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.ViewModel.Purok;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class PurokController : BaseController
    {
        private readonly IPurokServices _purokService;
        private readonly IAppUserServices _appUserService;
        private readonly IUnitOfWork _unitOfWork;

        public PurokController(
            IPurokServices purokService,
            IAppUserServices appUserService,
            IUnitOfWork unitOfWork
        ) : base(appUserService)
        {
            _purokService = purokService;
            _appUserService = appUserService;
            _unitOfWork = unitOfWork;
        }

        // GET: Purok
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewPurok)]
        public ActionResult Index()
        {
            return View();
        }

        #region Add or Update

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddPurok)]
        public JsonResult AddPurok(PurokViewModel model)
        {
            var result = AddUpdatePurok(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditPurok)]
        public JsonResult UpdatePurok(PurokViewModel model)
        {
            var result = AddUpdatePurok(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        private AjaxResult AddUpdatePurok(PurokViewModel model)
        {
            var ajaxResult = new AjaxResult
            {
                LogEventTitle = model.PurokId == 0 ? LogEventTitles.PurokCreated : LogEventTitles.PurokUpdated
            };
            string proccessType = model.PurokId == 0 ? "create" : "update";

            try
            {
                if (_purokService.IsCodeExists(model.Code, model.PurokId))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Code already exists!";
                }
                if (_purokService.IsNameExists(model.Name, model.BarangayId, model.PurokId))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Name already exists!";
                }

                if (ajaxResult.Success)
                {
                    _purokService.AddUpdatePurok(model, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    ajaxResult.Message = $"Purok has been successfully {proccessType}d!";
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


        #endregion

        #region Delete

   
        #endregion

        #region Export data to Exel

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportPuroks)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(PurokFilter filter)
        {

            return Json(new
            {
                data = new
                {
                    FileName = _purokService.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, CurrentUser.CurrentOffice)
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region JSON Get

        [HttpGet]
        public JsonResult GetPuroks(PurokFilter filter)
        {
            return Json(new
            {
                data = _purokService.SearchPuroks(filter).ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPurokDetails(int purokId)
        {
            return Json(new
            {
                data = _purokService.GetPurokDetails(purokId)
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurokLookUp(int barangayId)
        {
            var result = _purokService.GetAllByBarangayId(barangayId).Where(a=> a.IsActive);

            return Json(new
            {
                data = result.Select(a => new
                {
                    a.PurokId,
                    a.Name,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}