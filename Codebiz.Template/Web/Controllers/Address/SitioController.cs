using Infrastructure;
using System;
using System.Linq;
using System.Web.Mvc;
using Codebiz.Domain.Common.Model;
using Web.Filters;
using System.Data.Entity.Infrastructure;
using Logging;
using Infrastructure.Services;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.ViewModel.Sitio;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class SitioController : BaseController
    {
        private readonly ISitioServices _sitioServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserServices _appUserService;

        public SitioController(
            ISitioServices sitioServices,
            IAppUserServices appUserService,
            IUnitOfWork unitOfWork
        ) : base(appUserService)
        {
            _sitioServices = sitioServices;
            _unitOfWork = unitOfWork;
            _appUserService = appUserService;
        }

        // GET: Sitio
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewSitio)]
        public ActionResult Index()
        {
            return View();
        }

        #region Add or Update

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddSitio)]
        public JsonResult AddSitio(SitioViewModel model)
        {
            var result = AddOrUpdateSitio(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditSitio)]
        public JsonResult UpdateSitio(SitioViewModel model)
        {
            var result = AddOrUpdateSitio(model);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        private AjaxResult AddOrUpdateSitio(SitioViewModel model)
        {
            var ajaxResult = new AjaxResult
            {
                LogEventTitle = model.SitioId == 0 ? LogEventTitles.SitioCreated : LogEventTitles.SitioUpdated
            };
            string proccessType = model.SitioId == 0 ? "create" : "update";

            try
            {
                if (_sitioServices.IsSitioCodeExists(model.Code, model.SitioId))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Code already exists!";
                }
                if (_sitioServices.IsSitioNameExists(model.Name, model.PurokId, model.SitioId))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Name already exists!";
                }

                if (ajaxResult.Success)
                {
                    _sitioServices.AddUpdateSitio(model, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    ajaxResult.Message = $"Sitio has been successfully {proccessType}d!";
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

        #region Deactivate / Reactivate

   
        #endregion

        #region Delete

  

        #endregion

        #region Export data to Excel

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportSitios)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(SitioFilter filter)
        {
               return Json(new
            {
                data = new
                {
                    FileName = _sitioServices.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, CurrentUser.CurrentOffice)
                }
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region JSON Get

        [HttpPost]
        public JsonResult GetSitioDetails(int sitioId)
        {
            var result = _sitioServices.GetById(sitioId);

            return Json(new
            {
                data = new
                {
                    result.SitioId,
                    result.Code,
                    result.Name,
                    result.ProvinceId,
                    result.CityTownId,
                    result.BarangayId,
                    result.PurokId
                }
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSitio(SitioFilter filter)
        {
            var result = _sitioServices.SearchSitio(filter);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllSitioCode()
        {
            var getSitioCode = _sitioServices.GetAllSitioCode();
            return Json(new
            {
                data = getSitioCode
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSitioLookUp(int id)
        {
            var result = _sitioServices.GetAllByBarangayId(id).Where(a=> a.IsActive);

            return Json(new
            {
                data = result.Select(a => new
                {
                    a.SitioId,
                    a.Name,
                })
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}