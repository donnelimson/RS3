using System.Web.Mvc;

namespace Web.Filters
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private string claimType;
        private string claimValue;
        private bool isPartialView;

        public ClaimsAuthorizeAttribute(string claimType, string claimValue, bool isPartialView = false)
        {
            this.claimType = claimType;
            this.claimValue = claimValue;
            this.isPartialView = isPartialView;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as System.Security.Claims.ClaimsPrincipal;
            if (user != null && user.Identity.IsAuthenticated && user.HasClaim(claimType, claimValue))
            {
                base.OnAuthorization(filterContext);
            }
            else if (user != null && user.Identity.IsAuthenticated && !user.HasClaim(claimType, claimValue))
            {
                if (isPartialView)
                {
                    filterContext.Result = new PartialViewResult
                    {
                        ViewName = "Unauthorized",
                        ViewData = filterContext.Controller.ViewData,
                        TempData = filterContext.Controller.TempData
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "Unauthorized",
                        ViewData = filterContext.Controller.ViewData,
                        TempData = filterContext.Controller.TempData
                    };
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}