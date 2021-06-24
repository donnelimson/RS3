using Codebiz.Domain.Common.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Web
{
    public class JwtTokenManager
    {
        //private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        private static string secretkey = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        private static string issuer = "JWT.Codebiz.ERP";  //normally this will be your site URL    
        private static string audience = "07a87a20-564e-4ae0-a4f3-b7e1204b422c";

        public static string GenerateToken(string userName)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(secretkey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim(ClaimTypes.Name, userName));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(
                            issuer, //Issure    
                            audience,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }

        public static string GenerateToken(AppUser appUser)
        {

            //var issuer = "JWT.Codebiz.ERP";  //normally this will be your site URL    
            //var audience = "07a87a20-564e-4ae0-a4f3-b7e1204b422c";

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(secretkey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim(ClaimTypes.Name, appUser.Username));
            appUser.UserGroups.ToList().ForEach(x => permClaims.Add(new Claim(ClaimTypes.Role, $"{x.UserGroupId}")));
            
            
            //usrPermissions.ForEach(x => permClaims.Add(new Claim(claimType, $"{x}")));
            //claims.ForEach(x => permClaims.Add(new Claim(claimType, x.Value)));
            //Create Security Token object by giving required parameters    

            var token = new JwtSecurityToken(
                            issuer, //Issure    
                            audience,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(secretkey);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    
                    ValidateAudience = true,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;
            return username;
        }
    }
}