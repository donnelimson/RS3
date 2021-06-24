using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Codebiz.Domain.Common.Model;
using Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Helpers;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Logger.Configure("DebugLogger", "InfoLogger", "ErrorLogger", "DatabaseLogger");
            Logger.Debug("WEB APP STARTING....");
            
            // Autofac
            ConfigureIoc();
            DatabaseHelper.DbTrackerContextInitialize();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
       

            ModelBinders.Binders.Add(typeof(DateTime?), new NullableDateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());

            //Fix antiforgery issue
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        private void ConfigureIoc()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new SiteModule());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            /*webapi*/
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new AutofacWebApiDependencyResolver(container));
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            log4net.LogicalThreadContext.Properties["activityid"] = new ActivityIdHelper();

            if (Request.RawUrl.Contains(".aspx") || Request.RawUrl.Contains("Reserved.ReportViewerWebControl.axd"))
            {
                LongBeginRequestAsync();
            }
            else
            {
                LongBeginRequest();
            }
        }

        private async Task LongBeginRequestAsync()
        {
            await Task.Run(() =>
            {
                LongBeginRequest();
            });
        }

        private void LongBeginRequest()
        {
            try
            {
                Logger.Debug("Application_BeginRequest", logEventTitle: LogEventTitles.RequestStart);

                var containsPasswordData = Request.RawUrl.Contains("Account") &&
                (
                    Request.RawUrl.Contains("/Login") ||
                    Request.RawUrl.Contains("/Activate") ||
                    Request.RawUrl.Contains("/ChangePassword") ||
                    Request.RawUrl.Contains("/ResetPassword")
                ); // dont log if this is true

                var requestDetails = new
                {
                    RawUrl = Request.RawUrl,
                    QueryString = Request.QueryString,
                    HostAddress = Request.UserHostAddress,
                    UserAgent = Request.UserAgent,
                    FormValues = !containsPasswordData ? (object)Request.Form : "CONFIDENTIAL DATA"
                };

                Logger.Debug(requestDetails, logEventTitle: LogEventTitles.RequestDetails);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            };
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            log4net.LogicalThreadContext.Properties["user"] = User?.Identity?.Name;
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            Logger.Debug("Application_EndRequest");
        }

        private void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Logger.Error("ERROR STARTING APP....", exception);
        }
    }
}
