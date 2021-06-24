using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Models.ViewModels;
using Infrastructure.Services;
using Infrastructure.Utilities;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;
using Web.Models;
using Web.Models.ViewModels.AppUsers;

namespace Web.Controllers
{
    public class AppUsersController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IUserGroupServices _userGroupServices;
        private readonly IConfigSettingService _configSettingService;
        private readonly IHashHelper _hashHelper;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _mailTemplatePath;

        public AppUsersController(
            IAppUserServices appUserServices,
            IUserGroupServices userGroupServices,
            IConfigSettingService configSettingService,
            IHashHelper hashHelper,
            IPasswordHelper passwordHelper,
            IUnitOfWork unitOfWork)
            : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _userGroupServices = userGroupServices;
            _configSettingService = configSettingService;
            _hashHelper = hashHelper;
            _passwordHelper = passwordHelper;
            _unitOfWork = unitOfWork;
            _mailTemplatePath = _configSettingService.GetStringValueById((int)ConfigurationSettings.MailTemplatePath);

        }

        #region View

        public ActionResult MyProfile()
        {
            var viewModel = new ChangePasswordViewModel
            {
                AppUserId = CurrentUser.AppUserId,
                ForFrofile = true
            };

            return View("MyProfile", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(ChangePasswordViewModel model)
        {
            var appUser = _appUserServices.GetById(CurrentUser.AppUserId);
            if (!_appUserServices.ValidatePassword(CurrentUser, model.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "The current password was incorrect");

                return View("MyProfile", model);
            }
            else
            {
                try
                {
                    var passHash = _hashHelper.ComputeHash(Guid.NewGuid().ToString());
                    appUser.PasswordHash = _appUserServices.HashPassword(model.NewPassword, passHash) + ":" + passHash;

                    _appUserServices.InsertOrUpdate(appUser, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    CreateSuccessMessage("Password successfully changed");
                    Logger.Info($"Password successfully changed. Username : {CurrentUser.Username}. User : [{CurrentUser.Username}]." + JsonConvert.SerializeObject(model), LogEventTitles.ChangePassword, "", "", JsonConvert.SerializeObject(model));

                }
                catch (DbUpdateException dbEx)
                {
                    CreateErrorMessage($"Failed to change password! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]");
                    Logger.Error($"Failed to change password! UserId : [{CurrentUser.Username}] | NewValues : [{JsonConvert.SerializeObject(model)}]",
                        (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message),
                        LogEventTitles.ChangePassword);
                }
                catch (Exception ex)
                {
                    CreateErrorMessage($"Failed to change password! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]");
                    Logger.Error($"Failed to change password! UserId : [{CurrentUser.Username}] | NewValues : [{JsonConvert.SerializeObject(model)}]",
                        (ex.InnerException == null ? ex.Message : ex.InnerException.Message),
                        LogEventTitles.ChangePassword);
                }
            }

            return View(new ChangePasswordViewModel());
        }

        [HttpGet, Authorize]
        [AllowAnonymous]
        public JsonResult CheckIfCurrentPassIsCorrect(string currentPassword, string newPassword, string confirmPass)
        {
            bool valid = newPassword == confirmPass;
            bool proceed = false;

            if (valid)
            {
                proceed = _appUserServices.ValidatePassword(CurrentUser, currentPassword);
            }

            return Json(new
            {
                proceed,
                valid
            }, JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewAppUser)]
        public ActionResult Index()
        {
            return View();
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddAppUser)]
        public ActionResult Create()
        {
            var userGroupsLookUp = this.GetUserGroupLookUp();

            var viewModel = new AppUsersCreateViewModel
            {
                IsActive = true,
                UserGroupsLookUp = userGroupsLookUp,
            };

            return View("Form", viewModel);
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditAppUser)]
        public ActionResult Edit(int id)
        {
            var appUser = _appUserServices.GetById(id);

            if (appUser == null)
                ModelState.AddModelError("", "Account not exists.");

            if (!ModelState.IsValid)
            {
                var allErrors = string.Join("<br>", ModelState.Values.SelectMany(v => v.Errors).Select(a => a.ErrorMessage));
                CreateErrorMessage(allErrors);
                return RedirectToAction("Index");
            }

            var userGroupsLookUp = this.GetUserGroupLookUp();


            var viewModel = new AppUsersCreateViewModel
            {
                AppUserId = appUser.AppUserId,
                ForFrofile = false,
               // Email = appUser.Employee.Email,
                Username = appUser.Username,
                FirstName = appUser.FirstName,
                IsLocalNetworkUser = appUser.IsLocalNetworkUser,
                IsActive = appUser.IsActive,
                LastName = appUser.LastName,
                MiddleName = appUser.MiddleName,
                SelectedUserGroups = appUser.UserGroups.Select(a => a.UserGroupId).ToList(),
                UserGroupsLookUp = userGroupsLookUp,
                RoleId = appUser.RoleId.Value
            };

            return View("Form", viewModel);
        }

        #endregion

        #region Search

        [HttpGet]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewAppUser)]
        public JsonResult SearchAppUser(AppUserFilter filter)
        {
            return Json(new
            {
                data = _appUserServices.SearchAppUser(filter).ToList(),
                totalRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportUsers)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(AppUserFilter filter)
        {
            var currentOffice = CurrentUser.Employee?.Office?.Name;
            var result = _appUserServices.ExportDataToExcelFile(filter, Server, CurrentUser.AppUserId, currentOffice);

            return Json(new
            {
                data = new { FileName = result }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppusersLookUp(LookUpFilter filter, bool isDriver = false)

        {
            var result = _appUserServices.SearchAppUserForLookup(filter, Url, isDriver);
            return Json(new
            {
                data = result,
                totalRecordCount = filter.TotalRecordCount,
                filteredRecordCount = filter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Action

        public JsonResult SaveAppUser(AppUserAddOrUpdateViewModel model)
        {
            var ajaxResult = new AjaxResult
            {
                LogEventTitle = model.AppUserId == 0 ? LogEventTitles.AppUserUpdated : LogEventTitles.AppUserCreated
            };
            var proccessType = model.AppUserId > 0 ? "update" : "create";

            try
            {
                if (_appUserServices.CheckIfExistAsEmailOrUsername(model.Username, model.AppUserId))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = $"Username already exists!";
                }
                
                if (_appUserServices.CheckIfEmployeeExist(model.AppUserId, model.EmployeeId.HasValue ? model.EmployeeId.Value : 0))
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = $"Employee already has user!";
                }

                if (ajaxResult.Success)
                {
                    var appUser = _appUserServices.AddOrUpdateAppUser(model, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();
                    ajaxResult.Success = true;

                    #region Send Activation Link

                    if (model.AppUserId == 0)
                    {
                        var tempPassword = _passwordHelper.GenerateRandomPassword();
                        appUser.PasswordHash = _appUserServices.HashPassword(tempPassword, _hashHelper.ComputeHash(Guid.NewGuid().ToString())) +
                                               ":" + _hashHelper.ComputeHash(Guid.NewGuid().ToString());
                        _appUserServices.SendActivationEmail(appUser, tempPassword, _appUserServices.GenerateActivateUserLink(Url), HttpContext.Server.MapPath(_mailTemplatePath));
                    }

                    #endregion

                    ajaxResult.Message = $"{model.Username} has been successfully {proccessType}d!";
                    Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]." + JsonConvert.SerializeObject(model), ajaxResult.LogEventTitle, "", "", JsonConvert.SerializeObject(model));
                }
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {proccessType} {model.Username}! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message), ajaxResult.LogEventTitle);
            }

            catch (Exception ex)
            {
                ajaxResult.Success = false;
                Logger.Error($"Error! {ex.InnerException.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanDeleteAppUser)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            var appUser = _appUserServices.GetById(id);

            try
            {
                if (appUser != null)
                {
                    _appUserServices.Delete(appUser, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    CreateSuccessMessage("User successfully deleted.");
                    Logger.Info($"User successfully deleted. Deleted By : [{CurrentUser.Username}] | Id : [{id}]");
                }
                else
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = $"USer not found!";
                    Logger.Error($"USer not found!");
                }

            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"User cannot be deleted, there are record attached to this record! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message));
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"User cannot be deleted, there are record attached to this record! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanActivateDeactivateAppUser)]
        public JsonResult ToggleAppUserActiveStatus(int id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            var isActive = true;

            try
            {
                var appUser = _appUserServices.GetById(id);
                if (appUser != null)
                {
                    appUser.IsActive = !appUser.IsActive;
                    _appUserServices.InsertOrUpdate(appUser, CurrentUser.AppUserId);
                    _unitOfWork.SaveChanges();

                    var activeMessage = appUser.IsActive ? "reactivated" : "deactivated";
                    ajaxResult.Message = $"App User has been successfully {activeMessage}";
                    Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.PositionActivated);
                }
                else
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = $"App User not found!";
                    Logger.Info($"App User not found!", LogEventTitles.PositionActivated);
                }
            }
            catch (DbUpdateException dbEx)
            {
                var activeMessage = isActive ? "reactivate" : "deactivate";
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {activeMessage} appUser! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message));
            }
            catch (Exception ex)
            {
                var activeMessage = isActive ? "reactivate" : "deactivate";
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to {activeMessage} appUser! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }

            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message
            }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Resend Activation Link 

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanActivateAppUserAccount)]
        public JsonResult ResendActivationLink(int id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            try
            {
                _appUserServices.ResendActivationLink(id, CurrentUser.AppUserId, Url, HttpContext);
                _unitOfWork.SaveChanges();
                ajaxResult.Message = "Activation link sent!";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.ActivationLinkSent);
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to resend activation link! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message));
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to resend activation link! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message
            },
         JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Send Reset/Unlock password link

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanResetPasswordOfAppUser)]
        public JsonResult SendResetPasswordLink(int id)
        {
            AjaxResult ajaxResult = new AjaxResult();

            try
            {
                _appUserServices.SendResetPasswordLink(id, CurrentUser.AppUserId, Url, HttpContext);
                _unitOfWork.SaveChanges();
                ajaxResult.Message = "Reset password link sent!";
                Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.ResetPasswordLinkSent);
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to send password link!! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message));
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to send password link!! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }

            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message
            },
         JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanUnlockAppUserAccount)]
        public JsonResult SendUnlockingAccountLink(int id)
        {
            AjaxResult ajaxResult = new AjaxResult();

            try
            {
                if (_appUserServices.GetById(id) == null)
                {
                    ajaxResult.Message = "Account does not exists!";
                    ajaxResult.Success = false;
                }
                else
                {
                    _appUserServices.SendUnlockingAccountLink(id, CurrentUser.AppUserId, Url, HttpContext);
                    _unitOfWork.SaveChanges();
                    ajaxResult.Message = "Account unlocking link sent!";
                    Logger.Info($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.ActivationLinkSent);
                }
            }
            catch (DbUpdateException dbEx)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to send account unlocking link! [{(dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (dbEx.InnerException == null ? dbEx.Message : dbEx.InnerException.Message));
            }
            catch (Exception ex)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = $"Failed to send account unlocking link! [{(ex.InnerException == null ? ex.Message : ex.InnerException.Message)}]";
                Logger.Error($"{ajaxResult.Message}. UserId : [{CurrentUser.AppUserId}]", (ex.InnerException == null ? ex.Message : ex.InnerException.Message));
            }
            return Json(new
            {
                success = ajaxResult.Success,
                message = ajaxResult.Message
            },
         JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GET

        public JsonResult GetAllUserGroup()
        {
            return Json(new
            {
                data = _userGroupServices.GetAllActive().Where(a => !a.IsDeleted).Select(a => new
                {
                    Id = a.UserGroupId,
                    a.Description,
                    IsSelected = false
                })
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get AppUsers Details 

        public JsonResult GetAppUsersDetails(int? appUserId)
        {
            int ex = appUserId == null ? CurrentUser.AppUserId : appUserId.Value;
            return Json(new
            {
                data = _appUserServices.GetAppUserProfileById(ex),
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAppUsersDetailsById(int appUserId)
        {
            return Json(new
            {
                data = _appUserServices.GetAppUsersDetailsById(appUserId),
            },
            JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Helpers
        private List<KeyValuePair<int, string>> GetUserGroupLookUp()
        {
            return _userGroupServices.GetAllActive().Where(a => !a.IsDeleted)
                .Select(a => new { a.UserGroupId, a.Description }).ToList()
                .Select(a => new KeyValuePair<int, string>(a.UserGroupId, a.Description)).ToList();
        }

        #endregion
        [HttpGet]
        public JsonResult GetAppUserStatuses()
        {
            return Json(new
            {
                data = Enum.GetValues(typeof(UserStatuses)).Cast<UserStatuses>().Select(a => new
                {
                    Id = (int)a,
                    Description = a.GetEnumDescription()
                })
            },
         JsonRequestBehavior.AllowGet); ;
        }
    }
}