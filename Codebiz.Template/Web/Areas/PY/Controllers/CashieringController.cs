using Infrastructure.Services;
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
        public CashieringController(IAppUserServices appUserServices) : base(appUserServices)
        {
            _appUserServices = appUserServices;
        }
        // GET: PY/Cashiering
        public ActionResult Index()
        {
            return View();
        }
    }
}