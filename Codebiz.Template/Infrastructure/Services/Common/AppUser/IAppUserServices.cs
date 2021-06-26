using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Constants;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.AppUser;
using Codebiz.Domain.Common.Model.DTOs.BillingUnits;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using Domain.Repository;
using Infrastructure.Models;
using Infrastructure.Models.ViewModels;
using Infrastructure.Repository.Common;
using Infrastructure.Utilities;
using Infrastructure.Utilities.QueryHelper;
using Logging;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Infrastructure.Services
{
    public interface IAppUserServices
    {
        void InsertOrUpdate(AppUser entity, int appUserId);
        void Delete(AppUser entity, int appUserId);
        AppUser AddOrUpdateAppUser(AppUserAddOrUpdateViewModel model, int currentAppUserId);

        AppUser GetById(int id);
        AppUser GetActiveById(int id);
        List<AppUser> GetAllByIds(List<int> ids);
        List<AppUser> GetAllByEmployeeIds(List<int> ids);
        AppUser GetByUsername(string username);
        string GetUserNameById(int id);
        string GetCurrentOfficeById(int id);
        AppUser GetByEmail(string email);
        AppUser GetByEmailOrUsername(string emailOrUsername, Expression<Func<AppUser, object>>[] includeProperties = null);

        IPagedList<AppUserDTO> SearchAppUser(AppUserFilter filter);
        string ExportDataToExcelFile(AppUserFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);

        bool IsUsernameExists(string username, int appUserId);
        bool IsEmailExists(string username, int appUserId);
        bool CheckUsernameIfExist(string username);
        bool CheckIfEmployeeExist(int appUserId, int employeeId);
        bool CheckEmailIfExist(string email);
        bool CheckIfExistAsEmailOrUsername(string emailOrUsername, int id = 0);
        bool ValidatePassword(AppUser entity, string password);
        string HashPassword(string password, string salt);

        string GenerateActivateUserLink(UrlHelper urlHelper);
        string GeneratePasswordResetLink(string code, UrlHelper urlHelper);
        string GenerateUnlockUserLink(string code, UrlHelper urlHelper);

        bool SendActivationEmail(AppUser entity, string tempPassword, string activationUrl, string mailTemplatePath);
        void ResendActivationLink(int id, int currentAppUserId, UrlHelper urlHelper, HttpContextBase httpContextBase);
        bool SendResetPasswordEmail(AppUser entity, string resetPasswordUrl, string mailTemplatePath);
        void SendResetPasswordLink(int id, int currentAppUserId, UrlHelper urlHelper, HttpContextBase httpContext);
        void SendUnlockingAccountLink(int id, int currentAppUserId, UrlHelper urlHelper, HttpContextBase httpContext);
        bool SendUnlockAccountEmail(AppUser entity, string unlockUserUrl, string verificationCode, string mailTemplatePath);
        bool SendActivateInactiveAccountEmail(AppUser entity, string unlockUserUrl, string verificationCode, string mailTemplatePath);

        IPagedList<AppUserSearchDTO> SearchAppUserForLookup(LookUpFilter filter, UrlHelper Url, bool isDriver);
        AppUserProfileDTO GetAppUserProfileById(int appUserId);
        AppUserAddOrUpdateViewModel GetAppUsersDetailsById(int id);
        IEnumerable<AppUser> GetAllByOfficeAndPosition(int? officeId, string position);
        List<AppUser> GetAllUserByDepartmentAndDivision(int appUserId, int? officeId, int? departmentId, int? divisionId, int? divisionCategoryId);
        AppUser GetGeneralManager();
        string RandomPassword();
        int RandomNumber(int min, int max);
        string RandomString(int size, bool lowerCase);

    }

    public class AppUserServices : IAppUserServices
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IHashHelper _hashHelper;
        private readonly IEmailHelper _emailHelper;
        private readonly IConfigSettingService _configSettingService;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserGroupServices _userGroupServices;
        private readonly IContentFileService _contentFileService;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly string _mailTemplatePath;

        public AppUserServices(
            IAppUserRepository appUserRepository,
            IHashHelper hashHelper,
            IEmailHelper emailHelper,
            IConfigSettingService configSettingService,
            IPasswordHelper passwordHelper,
            IUserGroupServices userGroupServices,
            IContentFileService contentFileService,
            IUserGroupRepository userGroupRepository,
            IEmployeeRepository employeeRepository
        )
        {
            _appUserRepository = appUserRepository;
            _hashHelper = hashHelper;
            _emailHelper = emailHelper;
            _configSettingService = configSettingService;
            _passwordHelper = passwordHelper;
            _userGroupServices = userGroupServices;
            _mailTemplatePath = _configSettingService.GetStringValueById((int)ConfigurationSettings.MailTemplatePath);
            _contentFileService = contentFileService;
            _employeeRepository = employeeRepository;
        }

        public void InsertOrUpdate(AppUser entity, int appUserId)
        {
            if (entity.AppUserId.Equals(0))
            {
                var now = DateTime.Now;

                entity.CreatedByAppUserId = appUserId;
                entity.CreatedOn = now;
            }
            else
            {
                entity.ModifiedByAppUserId = appUserId;
                entity.ModifiedOn = DateTime.Now;
            }
            _appUserRepository.InsertOrUpdate(entity);
        }
        public void Delete(AppUser entity, int appUserId)
        {
            entity.ModifiedByAppUserId = appUserId;
            entity.ModifiedOn = DateTime.Now;
            entity.IsActive = false;

            _appUserRepository.InsertOrUpdate(entity);
        }
        public AppUser AddOrUpdateAppUser(AppUserAddOrUpdateViewModel model, int currentAppUserId)
        {
            var appUser = GetById(model.AppUserId) ?? new AppUser();

            appUser.Username = model.Username;
            appUser.RoleId = model.RoleId;
            appUser.IsActive = model.IsActive;
            appUser.FirstName = model.FirstName;
            appUser.MiddleName = model.MiddleName;
            appUser.LastName = model.LastName;
            appUser.Email = model.Email;
            if (appUser.UserGroups != null)
            {
                appUser.UserGroups.Clear();
            }

            appUser.UserGroups = _userGroupServices.GetByIds(model.SelectedUserGroups ?? new List<int>()).ToList();

            InsertOrUpdate(appUser, currentAppUserId);

            return appUser;
        }

        public AppUser GetById(int id)
        {
            return _appUserRepository.GetById(id);
        }
        public AppUser GetActiveById(int id)
        {
            return _appUserRepository.GetActiveById(id);
        }
        public List<AppUser> GetAllByIds(List<int> ids)
        {
            return _appUserRepository.GetAll.Where(p => ids.Contains(p.AppUserId)).ToList();
        }

        public List<AppUser> GetAllByEmployeeIds(List<int> ids)
        {
            return _appUserRepository.GetAll.Where(p => p.EmployeeId != null).Where(p => ids.Contains(p.EmployeeId.Value)).ToList();
        }
        public AppUser GetByUsername(string username)
        {
            return _appUserRepository.GetByUserName(username);
        }
        public string GetUserNameById(int id)
        {
            return _appUserRepository.GetById(id)?.Username;
        }
        public string GetCurrentOfficeById(int id)
        {
            var user = _appUserRepository.GetById(id);
            if (user != null)
                return user.Employee?.Office.Name;
            else
                return string.Empty;
        }
        public AppUser GetByEmail(string email)
        {
            return _appUserRepository.GetAll.Where(a => a.Employee.Email == email && a.IsActive).FirstOrDefault();
        }
        public AppUser GetByEmailOrUsername(string emailOrUsername, Expression<Func<AppUser, object>>[] includeProperties = null)
        {
            var query = _appUserRepository.GetAll.Where(x => x.IsActive);

            if (includeProperties != null)
            {
                query = _appUserRepository.GetAllIncluding(includeProperties);
            }

            return query.Where(a => a.Username == emailOrUsername || a.Employee.Email == emailOrUsername).FirstOrDefault();
        }

        public IPagedList<AppUserDTO> SearchAppUser(AppUserFilter filter)
        {
            return _appUserRepository.SearchAppUser(filter);
        }
        public string ExportDataToExcelFile(AppUserFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(AppUserToExcel)),
                _appUserRepository.GetDataForExportingToExcel(filter).ToList(),
                "Users_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Users List",
                currentAppUserId, currentOffice);

            return fileName;
        }

        public bool IsUsernameExists(string username, int appUserId)
        {
            return _appUserRepository.IsUsernameExists(username, appUserId);
        }
        public bool IsEmailExists(string username, int appUserId)
        {
            return _appUserRepository.IsEmailExists(username, appUserId);
        }
        public bool CheckUsernameIfExist(string username)
        {
            return _appUserRepository.GetAll.Any(a => a.Username == username && a.IsActive);
        }
        public bool CheckIfEmployeeExist(int appUserId, int employeeId)
        {
            return _appUserRepository.GetAll.Where(p => p.AppUserId != appUserId && p.EmployeeId == employeeId).Any();
        }
        public bool CheckEmailIfExist(string email)
        {
            return _appUserRepository.GetAll.Any(a => a.Employee.Email == email && a.IsActive);
        }
        public bool CheckIfExistAsEmailOrUsername(string emailOrUsername, int id = 0)
        {
            return _appUserRepository.GetAll.Any(a => a.AppUserId != id && (a.Username == emailOrUsername || a.Employee.Email == emailOrUsername));
        }

        public bool ValidatePassword(AppUser entity, string password)
        {
            if (entity == null)
                throw new ArgumentException("entity");

            var passDetails = entity.PasswordHash.Split(':');
            var hashedPassword = passDetails[0];
            var salt = passDetails[1];
            var Hashpass = this.HashPassword(password, salt).ToLower();
            var oldPassword = hashedPassword.ToLower();
            bool isSamePassword = Hashpass == oldPassword;
            if (isSamePassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string HashPassword(string password, string salt)
        {
            return _hashHelper.ComputeHash(password + salt + password);
        }

        public string GenerateActivateUserLink(UrlHelper urlHelper)
        {
            return _configSettingService.GetStringValueById((int)ConfigurationSettings.SitePublicBaseUrl).TrimEnd('/') + "/Account/Activate"; //Always use Site public base url for link
        }
        public string GeneratePasswordResetLink(string code, UrlHelper urlHelper)
        {
            return _configSettingService.GetStringValueById((int)ConfigurationSettings.SitePublicBaseUrl).TrimEnd('/') + "/Account/ResetPassword?code=" + code; //Always use Site public base url for link
        }
        public string GenerateUnlockUserLink(string code, UrlHelper urlHelper)
        {
            return _configSettingService.GetStringValueById((int)ConfigurationSettings.SitePublicBaseUrl).TrimEnd('/') + "/Account/Unlock?code=" + code; //Always use Site public base url for link
        }
        public bool SendActivationEmail(AppUser entity, string tempPassword, string activationUrl, string mailTemplatePath)
        {
            var employee = entity.EmployeeId.HasValue ? _employeeRepository.GetById(entity.EmployeeId.Value) : null;
            if (employee != null)
            {
                const string mailtemplate = "NewUserAccountActivation.html";
                string content = File.ReadAllText(Path.Combine(mailTemplatePath, mailtemplate));
                var records = new List<AccessRightDTO>();
                var data = entity.UserGroups.SelectMany(a => a.Permissions).OrderBy(x => x.PermissionGroupId);

                foreach (var item in data)
                {
                    var permissionGroup = records.FirstOrDefault(p => p.Name == item.PermissionGroup.Name);
                    if (permissionGroup == null)
                    {
                        permissionGroup = new AccessRightDTO
                        {
                            Name = item.PermissionGroup.Name,
                            Permissions = new List<string>()
                        };
                        records.Add(permissionGroup);
                    }
                }

                var recorddata = records.ToList().Select(p => new { p.Name, Permissions = string.Join(",", p.Permissions.ToList()) });
                var displayRecord = string.Join("<br />", recorddata.Select(p => p.Name + " - [" + p.Permissions + "]"));

                content = content.Replace("[Fullname]", employee.FirstName + " " + employee.LastName);
                content = content.Replace("[Link]", activationUrl);
                content = content.Replace("[Username]", entity.Username);
                content = content.Replace("[Password]", tempPassword);
                content = content.Replace("[Office]", employee.Office?.Name);
                content = content.Replace("[Department]", employee.Department?.Name);
                content = content.Replace("[Position]", employee.Position?.Name);
                content = content.Replace("[Division]", employee.Division?.Name);
                content = content.Replace("[Active]", entity.IsActive ? "YES" : "NO");
                content = content.Replace("[Access Right]", displayRecord);
                return _emailHelper.SendEmail(content, "User Account Activation - TARELCO 1", new List<MailAddress> { new MailAddress(employee.Email) });
            }

            return false;
        }
        public void ResendActivationLink(int id, int currentAppUserId, UrlHelper urlHelper, HttpContextBase httpContextBase)
        {
            var appUser = GetById(id);

            var tempPassword = _passwordHelper.GenerateRandomPassword();
            var passHash = _hashHelper.ComputeHash(Guid.NewGuid().ToString());
            appUser.PasswordHash = HashPassword(tempPassword, passHash) + ":" + passHash;

            InsertOrUpdate(appUser, currentAppUserId);

            SendActivationEmail(appUser, tempPassword, GenerateActivateUserLink(urlHelper), httpContextBase.Server.MapPath(_mailTemplatePath));
        }
        public bool SendResetPasswordEmail(AppUser entity, string resetPasswordUrl, string mailTemplatePath)
        {
            const string mailtemplate = "ResetPassword.html";

            string content = File.ReadAllText(Path.Combine(mailTemplatePath, mailtemplate));

            content = content.Replace("[Fullname]", entity.FullName);
            content = content.Replace("[Link]", resetPasswordUrl);
            content = content.Replace("[ExpirationDate]", entity.ForgotPasswordExpiryDate.Value.ToString());

            return _emailHelper.SendEmail(content, "Password Reset - TARELCO 1",
                new List<MailAddress> { new MailAddress(entity.Employee.Email) });
        }
        public void SendResetPasswordLink(int id, int currentAppUserId, UrlHelper urlHelper, HttpContextBase httpContext)
        {
            var appUser = GetById(id);

            appUser.ForgotPasswordUrlParam = Guid.NewGuid().ToString();
            appUser.ForgotPasswordExpiryDate = DateTime.Now.AddDays(1);

            SendResetPasswordEmail(appUser, GeneratePasswordResetLink(appUser.ForgotPasswordUrlParam, urlHelper), httpContext.Server.MapPath(_mailTemplatePath));
        }
        public void SendUnlockingAccountLink(int id, int currentAppUserId, UrlHelper urlHelper, HttpContextBase httpContext)
        {
            var appUser = GetById(id);

            appUser.UnlockUrlParam = Guid.NewGuid().ToString();
            appUser.VerificationCode = RandomPassword();

            var unlockUrlParam = GenerateUnlockUserLink(appUser.UnlockUrlParam, urlHelper);

            if (appUser.AppUserStatus == (int)UserStatuses.Dormant)
            {
                SendActivateInactiveAccountEmail(appUser, unlockUrlParam, appUser.VerificationCode, httpContext.Server.MapPath(_mailTemplatePath));
            }
            else if (appUser.AppUserStatus == (int)UserStatuses.Locked)
            {
                SendUnlockAccountEmail(appUser, unlockUrlParam, appUser.VerificationCode, httpContext.Server.MapPath(_mailTemplatePath));
            }
        }
        public bool SendUnlockAccountEmail(AppUser entity, string unlockUserUrl, string verificationCode, string mailTemplatePath)
        {
            const string mailtemplate = "UnlockAccount.html";

            string content = File.ReadAllText(Path.Combine(mailTemplatePath, mailtemplate));

            content = content.Replace("[Fullname]", entity.FullName);
            content = content.Replace("[Link]", unlockUserUrl);
            content = content.Replace("[VerificationCode]", verificationCode);
            //content = content.Replace("[ExpirationDate]", entity.ForgotPasswordExpiryDate.Value.ToString());

            return _emailHelper.SendEmail(content, "Unlock Account - TARELCO 1",
                new List<MailAddress> { new MailAddress(entity.Employee.Email) });
        }
        public bool SendActivateInactiveAccountEmail(AppUser entity, string unlockUserUrl, string verificationCode, string mailTemplatePath)
        {
            const string mailtemplate = "ActivateDormantAccount.html";

            string content = File.ReadAllText(Path.Combine(mailTemplatePath, mailtemplate));

            content = content.Replace("[Fullname]", entity.FullName);
            content = content.Replace("[Link]", unlockUserUrl);
            content = content.Replace("[VerificationCode]", verificationCode);
            //content = content.Replace("[ExpirationDate]", entity.ForgotPasswordExpiryDate.Value.ToString());

            return _emailHelper.SendEmail(content, "Activate Account - TARELCO 1",
                new List<MailAddress> { new MailAddress(entity.Employee.Email) });
        }

        public IPagedList<AppUserSearchDTO> SearchAppUserForLookup(LookUpFilter filter, UrlHelper Url, bool isDriver)
        {
            var data = _appUserRepository.SearchAppUserForLookup(filter, isDriver);

            foreach (var item in data)
            {
                var currentCroppedAppUserPhoto = _contentFileService.GetCurrentCroppedEmployeePhotoById(item.AppUserId);
                item.AppUserPhotoThumbnailUrl = Url.Action("CheckSource", "FileUpload", new
                {
                    area = "",
                    type = currentCroppedAppUserPhoto.FileMimeType,
                    fileName = currentCroppedAppUserPhoto.FileName,
                    folder = currentCroppedAppUserPhoto.Folder
                });
            }

            return data;
        }
        public AppUserProfileDTO GetAppUserProfileById(int appUserId)
        {
            var appUser = GetById(appUserId);

            if (appUser != null)
            {
                var data = new AppUserProfileDTO
                {
                    AppUserId = appUser.AppUserId,
                    FirstName = appUser.Employee != null ? appUser.Employee.FirstName : "",
                    MiddleName = appUser.Employee != null ? appUser.Employee.MiddleName : "",
                    LastName = appUser.Employee != null ? appUser.Employee.LastName : "",
                    Position = appUser.Employee != null ? appUser.Employee.Position?.Name : "",
                    Email = appUser.Employee != null ? appUser.Employee.Email : "",
                    Office = appUser.Employee != null ? appUser.Employee.Office?.Name : "",
                    UserGroup = appUser.UserGroups.Select(a => a.Name).FirstOrDefault(),
                };

                return data;
            }
            else
            {
                return new AppUserProfileDTO();
            }
        }

        public AppUserAddOrUpdateViewModel GetAppUsersDetailsById(int id)
        {
            var appUser = GetById(id);
            if (appUser != null)
            {
                var data = new AppUserAddOrUpdateViewModel
                {
                    AppUserId = appUser.AppUserId,
                    EmployeeId = appUser.EmployeeId,
                    EmployeeNo = appUser.Employee != null ? appUser.Employee.EmployeeNo : "",
                    Username = appUser.Username,
                    IsActive = appUser.IsActive,
                    SelectedUserGroups = appUser.UserGroups.Select(a => a.UserGroupId).ToList(),
                    RoleId = appUser.RoleId,
                    FirstName = appUser.FirstName,
                    MiddleName = appUser.MiddleName,
                    LastName = appUser.LastName,
                    Email = appUser.Email
                };

                return data;
            }
            else
            {
                return new AppUserAddOrUpdateViewModel();
            }
        }

        public IEnumerable<AppUser> GetAllByOfficeAndPosition(int? officeId, string position)
        {
            var data = _appUserRepository.GetAll.Where(p => p.IsActive && p.EmployeeId != null);

            if (officeId != null)
            {
                data = data.Where(p => p.Employee.OfficeId == officeId);
            }

            if (!string.IsNullOrWhiteSpace(position))
            {
                data = data.Where(p => p.Employee.Position.Name.ToUpper() == position);
            }

            return data.AsEnumerable();
        }

        public List<AppUser> GetAllUserByDepartmentAndDivision(int appUserId, int? officeId, int? departmentId, int? divisionId, int? divisionCategoryId)
        {
            return _appUserRepository.GetAllUserByDepartmentAndDivision(appUserId, officeId, departmentId, divisionId, divisionCategoryId);
        }
        public AppUser GetGeneralManager()
        {
            var data = _appUserRepository.GetAll
                .FirstOrDefault(a => a.Employee.Position.Name.ToUpper() == PositionName.GeneralManager && a.IsActive);

            return data;
        }

        public string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
