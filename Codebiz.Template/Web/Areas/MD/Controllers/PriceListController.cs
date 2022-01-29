using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using ERP.Model.DTO;
using ERP.Model.Filter;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.MD;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;
using Web.Filters;

namespace Web.Areas.MD.Controllers
{
    public class PriceListController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPriceListService _priceListService;
        public PriceListController(IAppUserServices appUserService,
        IUnitOfWork unitOfWork,
        IPriceListService priceListService) :base(appUserService)
        {
            _appUserServices = appUserService;
            _unitOfWork = unitOfWork;
            _priceListService = priceListService;
        }
        #region Views
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewPriceList)]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
        #endregion
        #region Actions
        [HttpPost]
        public JsonResult Search(PriceListFilter filter)
        {
            return Json(new { result = _priceListService.Search(filter), totalRecordCount = filter.FilteredRecordCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetAllItemsForPriceList(LookUpFilter filter)
        {
            return Json(new { result = _priceListService.GetAllItemsForPriceList(filter), totalRecordCount = filter.FilteredRecordCount }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult AddOrUpdate(PriceListAddOrUpdateViewModel model)
        {
            var dataDTO = model;
            var ajaxResult = new AjaxResult();
            ajaxResult.Action = model.Id == 0 ? "create" : "update";
            ajaxResult.Module = "Price List";
            RunMethod(() =>
            {
                _priceListService.AddOrUpdate(model, CurrentUser.AppUserId);
                _unitOfWork.SaveChanges();
                ajaxResult.Success = true;
                ajaxResult.Message = "Price List successfully " + ajaxResult.Action + "d";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(dataDTO), ajaxResult.LogEventTitle, "", "", JsonConvert.SerializeObject(model));
            }, ajaxResult);
            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetPriceListForItemMaster(int itemMasterId)
        {
            return Json(new { result = _priceListService.GetPriceListForItemMaster(itemMasterId) }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}