using Infrastructure.Models;
using Infrastructure.Services;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers
{
    public class PurchaseOrderController : FixedAssetsMaterialsManagementSystemControllerBaseController
    {
        private readonly IPurchaseOrderServices _purchaseOrderService;
        public PurchaseOrderController(
            IPurchaseOrderServices purchaseOrderService,
            IAppUserServices appUserServices
        ) : base(appUserServices)
        {
            _purchaseOrderService = purchaseOrderService;
        }


        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Search(int page, int pageSize, string sortColumn, string sortOrder, string purchaseOrderCode="", string status="", string supplier="")
        {
            var filter = new DataSearch
            {
                Page = page,
                PageSize = pageSize,
                SortColumn = string.IsNullOrEmpty(sortColumn) ? "SupplierId" : sortColumn.Replace(" ", string.Empty),
                SortDirection = sortOrder
            };
            var getData = _purchaseOrderService.Search(filter,purchaseOrderCode,status,supplier);
            return Json(new
            {
                data = getData.ToList(),
                totalRecordCount = filter.FilteredRecordCount
            },JsonRequestBehavior.AllowGet);
        }
    }
}