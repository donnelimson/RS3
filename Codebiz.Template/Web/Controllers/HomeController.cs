using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(
            IAppUserServices appUserServices,
            IUnitOfWork unitOfWork
            ) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _unitOfWork = unitOfWork;
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewDashboard)]
        public ActionResult Index()
        {
            //removed temporarily /smallkid
            //ViewData["TBA_ACCESS_TOKEN"] = Session["TBA_ACCESS_TOKEN"];
            return View();
        }

        //[ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.AboutCanView)]
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //[ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.ContactCanView)]
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        [ChildActionOnly]
        public PartialViewResult SidebarDynamic(string username)
        {
            var ctx = HttpContext.GetOwinContext();
            ClaimsPrincipal user = ctx.Authentication.User;
            var userNavLinkClaims = user.Claims.Where(a=>a.Type == ClaimCustomTypes.UserNavLink).FirstOrDefault();
            var deserializedUserNavLinks = JsonConvert.DeserializeObject<GetUserNavLinksResult>(userNavLinkClaims.Value);

            return PartialView("_SidebarDynamicOptimized", deserializedUserNavLinks);
        }
    }
}