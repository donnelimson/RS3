using Newtonsoft.Json;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{ 
    public static class HtmlHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null, string cssClass = null, string area = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            var currentArea =
              (html.ViewContext.RequestContext.RouteData.Values["area"] ?? string.Empty).ToString();
            var currentController =
               (html.ViewContext.RequestContext.RouteData.Values["controller"] ?? string.Empty).ToString();
            var currentAction =
                (html.ViewContext.RequestContext.RouteData.Values["action"] ?? string.Empty).ToString();

            var hasArea = !String.IsNullOrEmpty(area) ? area.Equals(currentArea, StringComparison.InvariantCultureIgnoreCase) : false;
            var hasController = !String.IsNullOrEmpty(controller) ? controller.Equals(currentController, StringComparison.InvariantCultureIgnoreCase) : false;
            var hasAction = !String.IsNullOrEmpty(action) ? action.Equals(currentAction, StringComparison.InvariantCultureIgnoreCase) : false;

            if (!string.IsNullOrEmpty(currentAction) && hasAction && hasController)
            {
                return cssClass;
            }
            else if (!string.IsNullOrEmpty(currentAction) && !string.IsNullOrEmpty(currentArea) && hasAction && hasController && hasArea)
            {
                return cssClass;
            }
            else if (((string.IsNullOrEmpty(currentAction) && string.IsNullOrEmpty(currentArea)) ||
                       (currentAction.ToUpper().Contains("CREATE") ||
                        currentAction.ToUpper().Contains("PROCESS") ||
                        currentAction.ToUpper().Contains("EDIT") || 
                        currentAction.ToUpper().Contains("ADD") ||
                        currentAction.ToUpper().Contains("UPDATE") ||
                        currentAction.ToUpper().Contains("DETAIL") ||
                        (currentAction.ToUpper().Contains("FORM") && !currentAction.ToUpper().Contains("TOBE")) ||
                        currentAction.ToUpper().Contains("MODIFY") ||
                        currentAction.ToUpper().Contains("APPROVE"))) && hasController)
            {
                return cssClass;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string IsSelectedParent(this HtmlHelper html, string controller = null, string action = null, string cssClass = null, string area = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";
            string currentArea = (string)html.ViewContext.ParentActionViewContext.RouteData.Values["area"];
            string currentAction = (string)html.ViewContext.ParentActionViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.ParentActionViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(area))
                area = currentArea;

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && area == currentArea && action == currentAction ? cssClass : String.Empty;
        }

        public static IHtmlString IsSubMenuSelected(this HtmlHelper html, List<NavLinkModel> childNavLinks, string cssClass = null)
        {
            var currentArea =
              (html.ViewContext.RequestContext.RouteData.Values["area"] ?? string.Empty).ToString();
            var currentController =
               (html.ViewContext.RequestContext.RouteData.Values["controller"] ?? string.Empty).ToString();
            var currentAction =
                (html.ViewContext.RequestContext.RouteData.Values["action"] ?? string.Empty).ToString();

            var submenuSelected = false;

            childNavLinks = childNavLinks.Where(p => !p.Permission.ToUpper().Contains("DETAIL")).ToList();
            if (childNavLinks != null && childNavLinks.Count > 0)
            {
                foreach (var item in childNavLinks)
                {
                    var hasArea = !String.IsNullOrEmpty(item.Area) ? item.Area.Equals(currentArea, StringComparison.InvariantCultureIgnoreCase) : false;
                    var hasController = !String.IsNullOrEmpty(item.Controller) ? item.Controller.Equals(currentController, StringComparison.InvariantCultureIgnoreCase) : false;
                    var hasAction = !String.IsNullOrEmpty(item.Action) ? item.Action.Equals(currentAction, StringComparison.InvariantCultureIgnoreCase) : false;

                    submenuSelected = (hasAction && hasController && hasArea) || hasController;

                    if (submenuSelected)
                        break;
                }
            }

            return submenuSelected ? new HtmlString(cssClass) : new HtmlString(string.Empty);
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static MvcHtmlString TruncateAndDisplayInBootstrapPopover(this HtmlHelper htmlHelper, string longText, int maxLenght = 50, bool allowHtml = false, string _class = "", bool getLastChar = false)
        {
            string innerhtml = "";

            if (!string.IsNullOrEmpty(longText) && longText.Length > maxLenght)
            {
                var truncatedText = string.Empty;

                if (getLastChar)
                {
                    truncatedText = "..." + longText.Substring(Math.Max(0, longText.Length - maxLenght));
                }
                else
                {
                    truncatedText = longText.Substring(0, maxLenght) + "...";
                }

                if (allowHtml)
                {
                    innerhtml = string.Format(@"<span tabindex='0' data-toggle='popover' data-placement='auto top' data-trigger='focus hover' data-html='true' 
                                                data-content='<pre>{0}</pre>' data-original-title title style='cursor: pointer;' class='{1}'>{2}</span>", htmlHelper.Encode(longText), _class, truncatedText);
                }
                else
                {
                    innerhtml = string.Format(@"<span tabindex='0' data-toggle='popover' data-placement='auto top' data-trigger='focus hover' 
                                                data-content='{0}' data-original-title title style='cursor: pointer;' class='{1}'>{2}</span>", longText, _class, truncatedText);
                }
            }
            else
            {
                innerhtml = longText;
            }

            return new MvcHtmlString(innerhtml);
        }

        public static List<Codebiz.Domain.Common.Model.NavLink> GetMainNavigations(this HtmlHelper html)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            var _navLinkServices = DependencyResolver.Current.GetService<INavLinkServices>();

            return _navLinkServices.GetAllMainNavLink().ToList();
        }

        public static List<Codebiz.Domain.Common.Model.NavLink> GetNavLinksWithAccess(this HtmlHelper html)
        {
            //TODO: use DI
            global::Domain.Context.AppCommonContext context = new global::Domain.Context.AppCommonContext();


            var user = HttpContext.Current.User as ClaimsPrincipal;

            //var links = from au in context.AppUsers
            //            join aug in context.AppUserUserGroup on au.AppUserId equals aug.

            var x = context.AppUsers.FirstOrDefault(u => u.Username == user.Identity.Name);

            var y = x.UserGroups.SelectMany(ug => ug.Permissions);

            var z = y.Where(p => p.NavLink != null).Select(p => p.NavLink);


            var allParent = context.NavLinks.Where(nl => nl.IsParent && nl.IsActive);


            var links = z.ToList();

            links.AddRange(allParent.ToList());

            var finalLinks = links.Distinct().ToList();

            return finalLinks;
        }

        public static UserNavLinkAccessResult GetUserNavLinkAccess(this HtmlHelper html)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            var _navLinkServices = DependencyResolver.Current.GetService<INavLinkServices>();

            return _navLinkServices.GetUserNavLinkAccess(user.Identity.Name);
        }

        public static GetUserNavLinksResult GetUserNavLink(this HtmlHelper html)
        {
            var user = HttpContext.Current.User as ClaimsPrincipal;
            var userNavLinkClaims = user.Claims.Where(a => a.Type == ClaimCustomTypes.UserNavLink).FirstOrDefault();
            var deserializedUserNavLinks = JsonConvert.DeserializeObject<GetUserNavLinksResult>(userNavLinkClaims.Value);

            return deserializedUserNavLinks;
        }

        public static IHtmlString ActionLinkWithClaims(this HtmlHelper htmlHelper, string linkText, string action, string controller, string area, object routeValues, object htmlAttributes, string userPermission, string faIconClass = "")
        {
            if (!string.IsNullOrEmpty(userPermission))
            {
                var user = HttpContext.Current.User as ClaimsPrincipal;
                if (user != null && user.Identity.IsAuthenticated && user.HasClaim(ClaimCustomTypes.UserPermissions, userPermission))
                {
                }
                else
                {
                    return MvcHtmlString.Create(string.Empty);
                }
            }
            
            //continue user has permission
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var anchor = new TagBuilder("a") { InnerHtml = string.IsNullOrEmpty(faIconClass) ? linkText :  $"<i class='fa {faIconClass}'></i> {linkText}"  };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            // Create li
            var liTagBuilder = new TagBuilder("li") { InnerHtml = anchor.ToString() };

            return MvcHtmlString.Create(liTagBuilder.ToString());
        }

        public static IHtmlString UserHasPermission(this HtmlHelper htmlHelper, List<NavLinkModel> childNavLinks = null, string userPermission = "")
        {
            var hasPermission = false;
            var user = HttpContext.Current.User as ClaimsPrincipal;
            var _permissionServices = DependencyResolver.Current.GetService<IPermissionServices>();

            if (!string.IsNullOrEmpty(userPermission))
            {
                //var permission = _permissionServices.GetByName(userPermission);
                //if (permission != null && user != null)
                //{
                //    hasPermission = user.Identity.IsAuthenticated && user.HasClaim(ClaimCustomTypes.UserPermissions, permission.PermisssionId.ToString());
                //}

                hasPermission = user.Identity.IsAuthenticated && user.HasClaim(ClaimCustomTypes.UserPermissions, userPermission);
            }
            else
            {
                childNavLinks = childNavLinks.Where(p => !p.Permission.ToUpper().Contains("DETAIL")).ToList();
                if (childNavLinks != null && childNavLinks.Count > 0)
                {
                    foreach (var item in childNavLinks)
                    {
                        hasPermission = user != null && user.Identity.IsAuthenticated && 
                            user.HasClaim(ClaimCustomTypes.UserPermissions, item.Permission == null ? string.Empty : item.Permission);

                        if (hasPermission)
                            break;
                        if (item.ChildNavLinks != null)
                        {
                            foreach (var item2 in item.ChildNavLinks)
                            {
                                hasPermission = user != null && user.Identity.IsAuthenticated &&
                                    user.HasClaim(ClaimCustomTypes.UserPermissions, item2.Permission == null ? string.Empty : item2.Permission);

                                if (hasPermission)
                                    break;
                            }
                        }
                    }
                }
            }

            return hasPermission ? new HtmlString(String.Empty) : new HtmlString("style='display:none'");
        }

        public static IHtmlString UserHasPermissionIndex(this HtmlHelper htmlHelper, List<NavLinkModel> childNavLinks = null, string userPermission = "")
        {
            var hasPermission = false;
            var user = HttpContext.Current.User as ClaimsPrincipal;

            if (!string.IsNullOrEmpty(userPermission))
            {
                hasPermission = user != null && user.Identity.IsAuthenticated && user.HasClaim(ClaimCustomTypes.UserPermissions, userPermission);
            }
            else
            {
                if (childNavLinks != null && childNavLinks.Count > 0)
                {
                    foreach (var item in childNavLinks)
                    {
                        hasPermission = user != null && user.Identity.IsAuthenticated &&
                            user.HasClaim(ClaimCustomTypes.UserPermissions, item.Permission == null ? string.Empty : item.Permission);

                        if (hasPermission)
                            break;

                        foreach (var item2 in item.ChildNavLinks)
                        {
                            hasPermission = user != null && user.Identity.IsAuthenticated &&
                                user.HasClaim(ClaimCustomTypes.UserPermissions, item2.Permission == null ? string.Empty : item2.Permission);

                            if (hasPermission)
                                break;
                        }
                    }
                }
            }

            return hasPermission ? new HtmlString("true") : new HtmlString("false");
        }

        public static IHtmlString UserHasPermission(this HtmlHelper htmlHelper, List<NavLinkModel> childNavLinks = null, List<string> userPermission = null)
        {
            var hasPermission = false;
            var user = HttpContext.Current.User as ClaimsPrincipal;

            if (userPermission!=null)
            {
                foreach(var item in userPermission)
                {
                    hasPermission = user != null && user.Identity.IsAuthenticated && user.HasClaim(ClaimCustomTypes.UserPermissions, item);
                    if (hasPermission)
                    {
                        break;
                    }
                }
            }
            else
            {
                if (childNavLinks != null && childNavLinks.Count > 0)
                {
                    foreach (var item in childNavLinks)
                    {
                        hasPermission = user != null && user.Identity.IsAuthenticated &&
                            user.HasClaim(ClaimCustomTypes.UserPermissions, item.Permission == null ? string.Empty : item.Permission);

                        if (hasPermission)
                            break;
                    }
                }
            }

            return hasPermission ? new HtmlString(String.Empty) : new HtmlString("viewonly");
        }
    }
}