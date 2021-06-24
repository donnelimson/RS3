using Newtonsoft.Json;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers
{

    public class FixedAssetsMaterialsManagementSystemControllerHomeController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IUnitOfWork _unitOfWork;

        public FixedAssetsMaterialsManagementSystemControllerHomeController(
            IAppUserServices appUserServices,
            IUnitOfWork unitOfWork
            ) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult SidebarDynamic(string username)
        {
            var ctx = HttpContext.GetOwinContext();
            ClaimsPrincipal user = ctx.Authentication.User;
            var userNavLinkClaims = user.Claims.Where(a => a.Type == ClaimCustomTypes.UserNavLink).FirstOrDefault();
            var deserializedUserNavLinks = JsonConvert.DeserializeObject<GetUserNavLinksResult>(userNavLinkClaims.Value);

            return PartialView("_SidebarDynamicOptimized", deserializedUserNavLinks);
        }
    }
}