using ERP.Model.Filter;
using Infrastructure.Services;
using Infrastructure.Services.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.PY.Controllers
{
    public class CashieringController : BaseController
    {
        private readonly ISaleTransactionService _saleTransactionService;
        public CashieringController(IAppUserServices appUserServices,
             ISaleTransactionService saleTransactionService) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _saleTransactionService = saleTransactionService;
        }
        // GET: PY/Cashiering
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form()
        {
            return View();
        }
        #region JSON
        public JsonResult Search(SaleTransactionFilter filter)
        {
            return Json(new { result = _saleTransactionService.Search(filter), totalRecordCount = filter.FilteredRecordCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}