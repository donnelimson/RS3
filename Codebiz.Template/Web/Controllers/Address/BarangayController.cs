using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.ViewModel.Barangay;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Logging;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class BarangayController : BaseController
    {
        private readonly IBarangayService _barangayService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserServices _appUserService;


        public BarangayController(IAppUserServices appUserServices,
            IBarangayService barangayService,
            IUnitOfWork unitOfWork) : base(appUserServices)
        {
            _barangayService = barangayService;
            _unitOfWork = unitOfWork;
            _appUserService = appUserServices;
        }


        [HttpGet]
       [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewBarangayData)]
        public ActionResult Index(string barangayName, string sortBy, int pageSize = 10, int pageNumber = 1)
        {
          
            return View();
        }

        [HttpGet]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewBarangayData)]
        public JsonResult Search(BarangayFilter filter)
        {
            var result = _barangayService.SearchBarangay(filter);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportBarangayViewList)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(BarangayFilter filter)
        {
            var currentOffice = CurrentUser.CurrentOffice;
            var exportResult = _barangayService.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, currentOffice);
            return Json(new
            {
                data = new
                {
                    FileName = exportResult
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBarangayDetails(int barangayId)
        {
            var result = _barangayService.GetDetails(barangayId);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBarangayLookUp(int cityTownId)
        {
            var result = _barangayService.GetAllByCityTownId(cityTownId).Where(a=> a.IsActive);
            return Json(new
            {
                data = result.Select(a => new
                {
                    BarangayId = a.BarangayId,
                    Name = a.Name,
                    BarangayCode = a.BarangayCode,
                })
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllBarangay()
        {
            var result = _barangayService.GetAll();

            return Json(new
            {
                data = result.Select(a => new
                {
                    BarangayId = a.BarangayId,
                    Name = a.Name,
                })
            }, JsonRequestBehavior.AllowGet);
        }
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions,PermissionData.UserCanReactivateDeactivateBarangayData)]
        public JsonResult ReactivateDeactivate(int barangayId,string name,bool status)
        {
            var ajaxResult = new AjaxResult();
            string action = "";
            action = status ? "deactivate" : "reactivate";
            ajaxResult.LogEventTitle = status ? LogEventTitles.BarangayDeactivated : LogEventTitles.BarangayActivated;
            try
            {
                _barangayService.ReactivateDeactivate(barangayId, CurrentUser.AppUserId);
                ajaxResult.Message = "Barangay with name " + name + " was successfully " + action + "d";
                _unitOfWork.SaveChanges();
                ajaxResult.Success = true;
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]. " +
                                                      $"UserId : [{CurrentUser.AppUserId}]", ajaxResult.LogEventTitle);
            }
            catch (DbEntityValidationException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} barangay! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", dbEx.InnerException.Message, ajaxResult.LogEventTitle);
                return Json(new { succeed = ajaxResult.Success, message = ajaxResult.Message }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} baarangay! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", ex.InnerException.Message, ajaxResult.LogEventTitle);
                return Json(new { succeed = ajaxResult.Success, message = ajaxResult.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { succeed = ajaxResult.Success, message = ajaxResult.Message }, JsonRequestBehavior.AllowGet);

        }   

        public JsonResult AddOrUpdate(BarangayAddOrUpdateViewModel model)
        {
            var ajaxResult = new AjaxResult();
            string action = "";
            
            try
            {
                var codeExist = _barangayService.CheckIfBarangayCodeExist(model.BarangayCode, model.BarangayId , model.CityTownId);
                var nameExist = _barangayService.CheckIfBarangayNameExist(model.Name, model.BarangayId, model.CityTownId);
                
                if (codeExist)
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Barangay code already exist.";              
                }
                if (nameExist)
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Barangay Name already exist.";
                }

                if(ajaxResult.Success)
                {
                    _barangayService.AddOrUpdate(model, CurrentUser.AppUserId);
                    action = model.BarangayId == 0 ? "add" : "update";
                    ajaxResult.Message = model.CityTownId == 0 ? "Successfully " + action + "ed" + " barangay" : "Successfully " + action + "ed" + " barangay";
                    ajaxResult.LogEventTitle = model.CityTownId == 0 ? LogEventTitles.BarangayAdded : LogEventTitles.BarangayUpdated;
                    _unitOfWork.SaveChanges();
                    ajaxResult.Success = true;
                    Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(model), ajaxResult.LogEventTitle, "", "", JsonConvert.SerializeObject(model));
                }
             }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} barangay! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {action} barangay! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message), ajaxResult.LogEventTitle);
            }
            return Json(new { succeed=ajaxResult.Success, message=ajaxResult.Message }, JsonRequestBehavior.AllowGet);
     }

    }
}