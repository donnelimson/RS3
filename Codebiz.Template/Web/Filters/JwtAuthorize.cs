using Autofac.Integration.Mvc;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Web.Filters
{
    public class JwtAuthorize : AuthorizationFilterAttribute
    {

        private string _claimType { get; set; }
        private string[] _claimValues { get; set; }

        public JwtAuthorize()
        {
        }

        //public JwtAuthorize(string claimType, string claimValue)
        //{
        //    _claimType = claimType;
        //}


        public JwtAuthorize(string claimType, params string[] claimValues)
        {
            _claimType = claimType;
            _claimValues = claimValues;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {

            base.OnAuthorization(actionContext);


            HttpRequestMessage request = actionContext.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            var _permisionService = AutofacDependencyResolver.Current.GetService<IPermissionServices>();

            if (authorization != null)
            {
                var res = JwtTokenManager.ValidateToken(authorization.Parameter);


                if (res == null)
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                else
                {

                    var principal = JwtTokenManager.GetPrincipal(authorization.Parameter);
                    var identity = (ClaimsIdentity)principal.Identity;
                    var usrGroups = identity.Claims.ToList().Where(x => x.Type == ClaimTypes.Role).Select(a => Convert.ToInt32(a.Value));
                    var permissionClaims = _permisionService.GetByUserGroupIds(usrGroups);
                    bool IsAutorize = false;


                    if (_claimValues != null)
                    {
                        for (int i = 0; i < _claimValues.Count(); i++)
                        {
                            if (permissionClaims.Where(x => x.Code == _claimValues[i]).Count() > 0)
                            {
                                IsAutorize = true;
                                break;
                            }
                        }
                    }

                    if (!IsAutorize)
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}