using System.Web.Mvc;

namespace Web.Areas.PY
{
    public class PYAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PY";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PY_default",
                "PY/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Web.Areas.PY.Controllers" }
            );
        }
    }
}