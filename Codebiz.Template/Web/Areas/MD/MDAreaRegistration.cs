using System.Web.Mvc;

namespace Web.Areas.MD
{
    public class MDAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MD_default",
                "MD/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Web.Areas.MD.Controllers" }
            );
        }
    }
}