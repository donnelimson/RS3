using Infrastructure.Services;
using Infrastructure.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IRoleService _roleService;
        public RoleController(IAppUserServices appUserService,
             IRoleService roleService) : base(appUserService)
        {
            _appUserServices = appUserService;
            _roleService = roleService;
        }
        #region Get Lookup
        public JsonResult GetRolesForSelect2LookUp()
        {
            return Json(new { result = _roleService.GetRolesForSelect2LookUp() }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }
    }
}