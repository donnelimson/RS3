using Codebiz.Domain.Common.Model.DTOs;
using ERP.Model.DTO;
using ERP.Model.Filter;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.MD;
using Logging;
using Newtonsoft.Json;
using Codebiz.Domain.Common.Model;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.MD.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IBrandService _brandService;
        public BrandController(IAppUserServices appUserServices, IUnitOfWork unitOfwork,
            IBrandService brandService) :base(appUserServices)
        {
            _unitOfWork = unitOfwork;
            _brandService = brandService;
        }
        // GET: MD/Brand
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form(int? id)
        {
            return View();
        }
        #region JSON
        [HttpPost]
        public JsonResult Search(BrandFilter filter)
        {
            return Json(new { result = _brandService.Search(filter), totalRecordCount = filter.FilteredRecordCount },JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddOrUpdate(BrandAddOrUpdateViewModel model)
        {
            var dataDTO = model;
            var ajaxResult = new AjaxResult();
            ajaxResult.Action = model.Id == 0 ? "create" : "update";
            ajaxResult.LogEventTitle = model.Id == 0 ? LogEventTitles.BrandCreated : LogEventTitles.BrandUpdated;
            ajaxResult.Module = "Brand";
            RunMethod(() =>
            {
                _brandService.AddOrUpdate(model, CurrentUser.AppUserId);
                _unitOfWork.SaveChanges();
                ajaxResult.Success = true;
                ajaxResult.Message = "Brand successfully " + ajaxResult.Action + "d";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(dataDTO), ajaxResult.LogEventTitle, "", "", JsonConvert.SerializeObject(model));
            }, ajaxResult);
            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetBrandForItemMaster(int itemMasterId)
        {
            return Json(new { result = _brandService.GetBrandForItemMaster(itemMasterId) }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllBrands()
        {
            return Json(new { result = _brandService.GetAllBrands() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetDetailsById(int id)
        {
            return Json(new { result = _brandService.GetDetailsById(id) }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}