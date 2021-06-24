using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Controllers;
using Infrastructure.Services;


namespace Web.Areas.FixedAssetsMaterialsManagementSystem.Controllers
{
    public class FixedAssetsMaterialsManagementSystemControllerBaseController : BaseController
    {

        private readonly IAppUserServices _appUserServices;

        public FixedAssetsMaterialsManagementSystemControllerBaseController(IAppUserServices appUserServices) : base(appUserServices)
        {
            _appUserServices = appUserServices;

            //InitializeCurrentUser();
        }


        public override string BaseSiteUrl
        {
            get
            {
                var currentContext = ControllerContext.HttpContext;
                string baseUrl = currentContext.Request.Url.Scheme + "://" + currentContext.Request.Url.Authority + currentContext.Request.ApplicationPath.TrimEnd('/') + '/';
                return baseUrl + "/" + "FixedAssetsMaterialsManagementSystem/";
            }
        }

    }
}