using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Utilities;
using Logging;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IPermissionServices _permissionServices;
        private readonly IConfigSettingService _configSettingService;
        private readonly INavLinkServices _navLinkServices;
        private readonly IHashHelper _hashHelper;
        private readonly ILoginHistoryServices _loginHistoryServices;

        private readonly IUnitOfWork _unitOfWork;
        private readonly string _mailTemplatePath;

        public AccountController(
            IAppUserServices appUserServices,
            IPermissionServices permissionServices,
            IConfigSettingService configSettingService,
            INavLinkServices navLinkServices,
            ILoginHistoryServices loginHistoryServices,

            IHashHelper hashHelper,

            IUnitOfWork unitOfWork
            ) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _permissionServices = permissionServices;
            _configSettingService = configSettingService;
            _navLinkServices = navLinkServices;
            _hashHelper = hashHelper;
            _loginHistoryServices = loginHistoryServices;
            _unitOfWork = unitOfWork;
            _mailTemplatePath = _configSettingService.GetStringValueById((int)ConfigurationSettings.MailTemplatePath);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            Session["TBA_ACCESS_TOKEN"] = "";
            Response.Cookies.Remove("TBA_ACCESS_TOKEN");


            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return RedirectToLocal(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            var dashboardUrl = Url.Action("Index", "Home");
            if (returnUrl == dashboardUrl)
            {
                ViewBag.ReturnUrl = Url.Action("Index", "Home");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
            }

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AuthenticationManager.SignOut();
            Session["TBA_ACCESS_TOKEN"] = "";
            Response.Cookies.Remove("TBA_ACCESS_TOKEN");

            ClaimsIdentity identity;

            var appUser = _appUserServices.GetByEmailOrUsername(model.Username);
            var dateNow = DateTime.Now;
            var resetloginAttempt = 0;

            if (appUser != null)
            {
                //Account Inactive
                if (!appUser.IsActive)
                {
                    ModelState.AddModelError("Username", "Account is inactive.");
                    return View(model);
                }


         
                //Success Login
                if (_appUserServices.ValidatePassword(appUser, model.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, appUser.Username),
                        new Claim(ClaimTypes.Name, appUser.Username)
                    };

                    #region Access Rights

                    var userGroupIds = appUser.UserGroups.Select(a => a.UserGroupId).ToList();
                    var permissions = _permissionServices.GetByUserGroupIds(userGroupIds)
                        .Select(a => a.Code)
                        .Distinct()
                        .ToList()
                        .Select(a => new Claim(ClaimCustomTypes.UserPermissions, a));
                    string Logtype = ((LogEventTitles)4).ToString();
                    var userNavLinks = _navLinkServices.GetUserNavLinks(appUser.Username);
                    var userNavLinksJson = JsonConvert.SerializeObject(userNavLinks);

                    claims.Add(new Claim(ClaimCustomTypes.UserNavLink, userNavLinksJson));
                    claims.AddRange(permissions);

                    #endregion

                    identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(identity);

                    var appuserNullLogouts = _loginHistoryServices.GetAllNullLogoutsByAppUserId(appUser.AppUserId);
                    foreach (var item in appuserNullLogouts)
                    {
                        item.LogoutDateTime = dateNow;
                    }

                    appUser.LastLoggedIn = dateNow;

                    Session["AppUser"] = System.Web.HttpContext.Current.Session.SessionID;

                    //var token = JwtTokenManager.GenerateToken(appUser.Username, ClaimCustomTypes.UserPermissions, new List<Claim>());
                    //var token = JwtTokenManager.GenerateToken(appUser);

                    // temporary removed by smallkid
                    //Session["TBA_ACCESS_TOKEN"] = token;


                    appUser.FailedLoggedInAttempt = resetloginAttempt;
     
                    #region Login history

                    var loginHistory = new LoginHistory();
                    loginHistory.LoginDateTime = dateNow;
                    loginHistory.SessionID = System.Web.HttpContext.Current.Session.SessionID;
                    loginHistory.AppUserId = appUser.AppUserId;
                    loginHistory.UserIpAddress = GetLocalIPAddress();
                    _loginHistoryServices.InsertOrUpdate(loginHistory, loginHistory.LoginHistoryId);
                    _unitOfWork.SaveChanges();
                    Logger.Info($"Successful login. Username : [{model.Username}]",
                        logEventTitle: LogEventTitles.Login);

                    #endregion
                    //var token = JwtTokenManager.GenerateToken(appUser.Username, ClaimCustomTypes.UserPermissions, new List<Claim>());
                    //var token = JwtTokenManager.GenerateToken(appUser);
                    //Session["TBA_ACCESS_TOKEN"] = token;
                    //Session["AppUser"] = loginHistory.SessionID;
                    //Response.Cookies.Add(new HttpCookie("TBA_ACCESS_TOKEN", token));
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid password");
                    //ModelState.AddModelError("Failed Logged In Attempt);
                    appUser.FailedLoggedInAttempt += 1; // If the Pasword is Invalid it adds FailedLoggedInAttempt = +1
                    _appUserServices.InsertOrUpdate(appUser, appUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    if (appUser.FailedLoggedInAttempt >=
                        _configSettingService.GetInt32ValueById((int)ConfigurationSettings.MaxLoginAttempt)
                    ) // If the Max Login attempts is reached by the User the Account will be locked
                    {
                        ModelState.AddModelError("Username",
                            "This account has been locked! Please Check your email to unlock your Account.");
                        appUser.IsActive = false;
                        appUser.FailedLoggedInAttempt = resetloginAttempt;
                        var guid = Guid.NewGuid().ToString();

                        //var expiryDate = DateTime.Now.AddDays(1);

                        appUser.UnlockUrlParam = guid;

                        appUser.VerificationCode = _appUserServices.RandomPassword();
                        var unlockUrlParam = _appUserServices.GenerateUnlockUserLink(appUser.UnlockUrlParam, Url);
                        //_appUserServices.SendUnlockAccountEmail(appUser, unlockUrlParam, appUser.VerificationCode,
                        //    HttpContext.Server.MapPath(_mailTemplatePath));
                        _appUserServices.InsertOrUpdate(appUser, appUser.AppUserId);
                        _unitOfWork.SaveChanges();
                    }

                    return View(model);
                }
            }
            else
            {
                var deactivatedUser = _appUserServices.GetByUsername(model.Username);
                string modelError = deactivatedUser != null ? "is deactivated, Please contact your system administrator." : "does not exist";

                ModelState.AddModelError("Username", "Username " + modelError);
                return View(model);
            }

            if (!string.IsNullOrEmpty(returnUrl))
                return RedirectToLocal(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            var loginhistory = _loginHistoryServices.GetByAppUserIdAndSessionId(CurrentUser.AppUserId, Session.SessionID);

            loginhistory.LogoutDateTime = DateTime.Now;
            _loginHistoryServices.InsertOrUpdate(loginhistory, loginhistory.LoginHistoryId);
            _unitOfWork.SaveChanges();

            Session["AppUser"] = null;
            Session.Clear();

            Logger.Info($"Successful logout. Username : [{CurrentUser.Username}]", logEventTitle: LogEventTitles.Logout);

            return RedirectToAction("Index", "Home");
        }

        //[AllowAnonymous]
        public ActionResult ErrorResult()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Activate()
        {
            return View();
        }
      


        // GET: /Account/ConfirmEmail
        //[AllowAnonymous]
        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await UserManager.ConfirmEmailAsync(userId, code);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

     

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return HttpNotFound("Empty code");
            }

            return View();
        }

  

        //[HttpGet,Authorize]
        public ActionResult _ProfileSidebar(string username)
        {
            return PartialView(_appUserServices.GetByEmailOrUsername(username));
        }


        //[HttpGet,Authorize]
        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var appUser = _appUserServices.GetById(CurrentUser.AppUserId);
            if (!_appUserServices.ValidatePassword(CurrentUser, model.OldPassword))
            {
                CreateErrorMessage("The current password was Incorrect");
                return RedirectToAction("MyProfile", "AppUsers");
            }

            var passHash = _hashHelper.ComputeHash(Guid.NewGuid().ToString());
            appUser.PasswordHash = _appUserServices.HashPassword(model.NewPassword, passHash) + ":" + passHash;

            _appUserServices.InsertOrUpdate(appUser, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("Password successfully changed");
            Logger.Info("Change password successful. Username : [{0}]", CurrentUser.Username);

            return RedirectToAction("MyProfile", "AppUsers");
        }

        [AllowAnonymous]
        public ActionResult Unlock()
        {
            var model = new UnlockingViewModel();
            return View(model);
        }


        private string GenerateUnlockUserLink(string code)
        {
            UrlHelper Url = new UrlHelper(this.ControllerContext.RequestContext);
            string url = Url.Action("Unlock", "Account", new { code });

            return _configSettingService.GetStringValueById((int)ConfigurationSettings.SitePublicBaseUrl).TrimEnd('/') + url;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        [HttpGet, Authorize]
        public bool IsUserLoggedOnElsewhere()
        {
            Session["AppUser"] = System.Web.HttpContext.Current.Session.SessionID;
            //var appUser = _appUserServices.GetByEmailOrUsername(CurrentUser.Username);
            var loginHistory = _loginHistoryServices.GetByAppUserIdAndSessionId(CurrentUser.AppUserId, Session.SessionID);

            if (loginHistory != null)
            {
                if (loginHistory.LogoutDateTime != null && loginHistory.AppUserId == CurrentUser.AppUserId)
                {
                    return true;
                }
            }

            return false;

        }

        public bool IsUserSessionIfExpired()
        {
            for (int i = 1; i < 10; i++)
            {
                System.Threading.Thread.Sleep(1000);
            }
            return true;
        }

        [HttpGet, Authorize]
        [AllowAnonymous]
        public bool CheckIfUserHasPendingLogin(string username, LoginViewModel model)
        {
            var appUser = _appUserServices.GetByEmailOrUsername(username);

            if (appUser != null && _appUserServices.ValidatePassword(appUser, model.Password))
            {
                if (appUser == null) return false;
                var loginHistory = _loginHistoryServices.GetByAppUserId(appUser.AppUserId);

                if (loginHistory == null) return false;
                return (loginHistory.LogoutDateTime == null && loginHistory.AppUserId == appUser.AppUserId);
            }

            return false;
        }


        ////
        //// GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);

        ////
        //// GET: /Account/ResetPasswordConfirmation
        //[AllowAnonymous]
        //public ActionResult ResetPasswordConfirmation()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        ////
        //// GET: /Account/SendCode
        //[AllowAnonymous]
        //public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        //{
        //    var userId = await SignInManager.GetVerifiedUserIdAsync();
        //    if (userId == null)
        //    {
        //        return View("Error");
        //    }
        //    var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
        //    var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
        //    return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        ////
        //// POST: /Account/SendCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendCode(SendCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    // Generate the token and send it
        //    if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
        //    {
        //        return View("Error");
        //    }
        //    return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        //}

        ////
        //// GET: /Account/ExternalLoginCallback
        //[AllowAnonymous]
        //public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    // Sign in the user with this external login provider if the user already has a login
        //    var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
        //        case SignInStatus.Failure:
        //        default:
        //            // If the user does not have an account, then prompt the user to create an account
        //            ViewBag.ReturnUrl = returnUrl;
        //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
        //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
        //    }
        //}

        ////
        //// POST: /Account/ExternalLoginConfirmation
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        ////
        //// GET: /Account/ExternalLoginFailure
        //[AllowAnonymous]
        //public ActionResult ExternalLoginFailure()
        //{
        //    return View();
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}