using System.Web.Mvc;

namespace Web.Areas.FixedAssetsMaterialsManagementSystem
{
    public class FAMMSAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FixedAssetsMaterialsManagementSystem";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FixedAssetsMaterialsManagementSystem_default",
                "FixedAssetsMaterialsManagementSystem/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers" }
            );
        }
    }
}