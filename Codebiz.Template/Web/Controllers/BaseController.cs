using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA;
using Codebiz.Domain.Common.Model.DTOs;
using Infrastructure.Services;
using Infrastructure.Services.Common;
using Logging;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public  IAppUserServices _appUserServices;
        private AppUser _currentAppUser = null;


        public BaseController(IAppUserServices appUserServices)
        {
            _appUserServices = appUserServices;
        }     
        private void InitializeCurrentUser()
        {
            var currentContext = System.Web.HttpContext.Current;

            if (currentContext == null) 
                return;

            if (!currentContext.Request.IsAuthenticated) 
                return;

            var userSession = Session["AppUser"] as AppUser;
            if (userSession != null && userSession.Username == currentContext.User.Identity.Name)
            {
                _currentAppUser = userSession;
            }
            else
            {
                _currentAppUser = _appUserServices.GetByEmailOrUsername(currentContext.User.Identity.Name, 
                    includeProperties: new System.Linq.Expressions.Expression<Func<AppUser, object>>[] 
                    {
                        a => a.Employee.Region,
                        a => a.AccessLevel,
                        a => a.UserGroups,
                        a => a.Employee.Office,
                        a => a.Employee.Department,
                        a => a.Employee.Division,
                        a => a.Employee.Office,
                        a => a.Employee.Office.Province,
                        a => a.Employee.Office.CityTown,
                        a => a.Employee.Office.Barangay,
                        a => a.Employee.Position,
                        a => a.Employee.Position.Divisions.Select(b => b.DivisionCategory),
                        a => a.Employee.Office,
                        a => a.Employee.Office.Barangay,
                        a => a.Employee.Office.CityTown,
                        a => a.Employee.Office.Province,
                    } 
                );

                Session["AppUser"] = _currentAppUser;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            InitializeCurrentUser();
        }
        public void RunMethod(Action method, AjaxResult ajaxResult)
        {
            try
            {
                method();
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {ajaxResult.Action} {ajaxResult.Module}! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {ajaxResult.Action} {ajaxResult.Module}! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message), ajaxResult.LogEventTitle);
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            Logger.Error(string.Format("Unhandled Exception : {0} - {1}", filterContext.Exception.GetType().ToString(), filterContext.Exception.Message), filterContext.Exception);
        }

        protected void CreateSuccessMessage(string message)
        {
            Session["SuccessMessage"] = message;
        }

        protected void CreateErrorMessage(string message)
        {
            Session["ErrorMessage"] = message;
        }

        protected void CreateWarningMessage(string message)
        {
            Session["WarningMessage"] = message;
        }

        protected AppUser CurrentUser
        {
            get
            {
                InitializeCurrentUser();
                return _currentAppUser;
            }
        }

        protected bool IsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }

        public virtual string BaseSiteUrl
        {
            get
            {
                var currentContext = ControllerContext.HttpContext;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public string BaseSiteUrlWithoutAppPath
        {
            get
            {
                var currentContext = ControllerContext.HttpContext;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority.TrimEnd('/') + '/';
                return baseUrl;
            }
        }

        public string GetCurrentUri
        {
            get { return Request.Url.PathAndQuery; }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonDotNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        protected ViewResult NotAuthorizedResult()
        {
            return new ViewResult
            {
                ViewName = "Unauthorized",
                ViewData = ViewData,
                TempData = TempData
            };
        }

        public DivisionType GetEmployeeDivisionCategory(Employee employee)
        {
            var division = employee.Position != null
                     ? employee.Position.Divisions.FirstOrDefault(p => p.DepartmentId == employee.DepartmentId && p.DivisionId == employee.DivisionId)
                     : null;
            return division == null ? null : division.DivisionCategory;
        }
    }
}