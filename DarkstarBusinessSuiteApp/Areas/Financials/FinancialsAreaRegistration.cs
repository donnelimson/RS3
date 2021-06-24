using System.Web.Mvc;

namespace DarkstarBusinessSuiteApp.Areas.Financials
{
    public class FinancialsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Financials";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Financials_default",
                "Financials/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}