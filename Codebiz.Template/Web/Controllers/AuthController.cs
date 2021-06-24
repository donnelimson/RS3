using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Utilities;
using Microsoft.AspNet.SignalR.Hubs;
//using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Web.Filters;

namespace Web.Controllers
{
    public class AuthController : ApiController
    {
        
        private readonly IAppUserServices _appUserServices;
        private readonly IPermissionServices _permissionServices;
        private readonly IConfigSettingService _configSettingService;
        private readonly INavLinkServices _navLinkServices;
        private readonly IHashHelper _hashHelper;
    //    private readonly ILoginHistoryServices _loginHistoryServices;

        private readonly IUnitOfWork _unitOfWork;

        public AuthController(
            IAppUserServices appUserServices,
            IPermissionServices permissionServices,
            IConfigSettingService configSettingService,
            INavLinkServices navLinkServices,
          //  ILoginHistoryServices loginHistoryServices,
            IHashHelper hashHelper,
            IUnitOfWork unitOfWork
            ) 
        {
            _appUserServices = appUserServices;
            _permissionServices = permissionServices;
            _configSettingService = configSettingService;
            _navLinkServices = navLinkServices;
            _hashHelper = hashHelper;
       //     _loginHistoryServices = loginHistoryServices;
            _unitOfWork = unitOfWork;
        }
        
        
        [Route("api/Auth/GetToken")]
        public IHttpActionResult GetToken()
        {
            //string key = "4Cz6rAnKUKQj4tn7XsSXe6EJhBh2isPp"; //Secret key which will be used later during validation    
            //var issuer = "JWT.Codebiz.ERP";  //normally this will be your site URL    

            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            ////Create a List of Claims, Keep claims name short    
            //var permClaims = new List<Claim>();
            //permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //permClaims.Add(new Claim("Name", "smallkid"));

            ////Create Security Token object by giving required parameters    
            //var token = new JwtSecurityToken(issuer, //Issure    
            //                issuer,  //Audience    
            //                permClaims,
            //                expires: DateTime.Now.AddDays(1),
            //                signingCredentials: credentials);
            //var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            //return Ok(new { data = jwt_token });

            var token = JwtTokenManager.GenerateToken("smallkid");

            return Ok(new { data = token });
        }



        [JwtAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditFinancial)]
        [Route("api/Auth/GetData")]
        public IHttpActionResult GetData()
        {
            return Ok("test");
        }
    }
}
