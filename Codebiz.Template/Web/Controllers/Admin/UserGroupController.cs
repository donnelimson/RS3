using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;
using Web.Models.ViewModels.UserGroup;

namespace Web.Controllers
{
    public class UserGroupController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IPermissionServices _permissionServices;
        private readonly IPermissionGroupServices _permissionGroupServices;
        private readonly IUserGroupServices _userGroupServices;
        private readonly INavLinkServices _navLinkServices;

        private readonly IUnitOfWork _unitOfWork;

        public UserGroupController(
            IAppUserServices appUserServices,
            IPermissionServices permissionServices,
            IPermissionGroupServices permissionGroupServices,
            IUserGroupServices userGroupServices,
            INavLinkServices navLinkServices,

            IUnitOfWork unitOfWork
            ) : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _permissionServices = permissionServices;
            _permissionGroupServices = permissionGroupServices;
            _userGroupServices = userGroupServices;
            _navLinkServices = navLinkServices;

            _unitOfWork = unitOfWork;
        }

        // GET: UserGroup
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewUserGroup)]
        public ActionResult Index()
        {
            return View();
        }

        //UserGroup Index
        [HttpGet]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewUserGroup)]
        public JsonResult UserGroupIndex(string name, string description, string createdby,
            int page, int pageSize, string sortColumn, string sortOrder)
        {
            var userGroupFilter = new UserGroupFilter
            {
                Name = name,
                Description = description,
                CreatedBy = createdby,
                Page = page,
                PageSize = pageSize,
                SortColumn = string.IsNullOrEmpty(sortColumn) ? "UserGroupId" : sortColumn.Replace(" ", string.Empty),
                SortDirection = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder.Replace(" ", string.Empty)
            };
            var result = _userGroupServices.SearchUserGroup(userGroupFilter);
            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = userGroupFilter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddUserGroup)]
        public ActionResult Create()
        {
            var permissionsLookUp = this.GetPermissionsLookUp();
            //var navlinksLookUp = this.GetNavlinksLookUp();

            var viewModel = new UserGroupCreateViewModel
            {
                //PermissionsLookUp = permissionsLookUp,
                //NavlinksLookUp = navlinksLookUp,
                PermissionGroupsLookUp = this.GetPermissionGroupLookUp(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddUserGroup)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserGroupCreateViewModel model)
        {
            if (_userGroupServices.CheckIfNameExist(model.Name))
                ModelState.AddModelError("Name", "Name already exists.");

            if (!ModelState.IsValid)
            {
                //model.NavlinksLookUp = this.GetNavlinksLookUp();
                //model.PermissionsLookUp = this.GetPermissionsLookUp();
                model.PermissionGroupsLookUp = this.GetPermissionGroupLookUp();
                return View(model);
            }

            var userGroup = new UserGroup();
            userGroup.Name = model.Name;
            userGroup.Description = model.Description;
            userGroup.IsActive = model.IsActive;

            userGroup.Permissions = _permissionServices.GetByIds(model.SelectedPermissions ?? new List<int>()).ToList();
            //userGroup.Navlinks = _navLinkServices.GetByIds(model.SelectedNavlinks ?? new List<int>()).ToList();

            _userGroupServices.InsertOrUpdate(userGroup, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("User group successfully created.");
           // Logger.Info("User group successfully created. CreatedBy : [{0}] | NewValues : [{1}]", CurrentUser.Username, JsonConvert.SerializeObject(model));
            Logger.Info($"User group successfully created. UserId : [{CurrentUser.AppUserId}]" + JsonConvert.SerializeObject(model), LogEventTitles.UserGroupCreated, "", "", JsonConvert.SerializeObject(model));

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditUserGroup)]
        public ActionResult Edit(int id)
        {
            var userGroup = _userGroupServices.GetById(id);

            var viewModel = new UserGroupEditViewModel
            {
                UserGroupId = userGroup.UserGroupId,
                Name = userGroup.Name,
                Description = userGroup.Description,
                IsActive = userGroup.IsActive,
                SelectedPermissions = userGroup.Permissions.Select(a => a.PermisssionId).ToList(),
                //PermissionsLookUp = this.GetPermissionsLookUp(),
                PermissionGroupsLookUp = this.GetPermissionGroupLookUp(),
                SelectedPermissionGroups = userGroup.Permissions.Select(a=>a.PermissionGroupId).Distinct().ToList(),
                //SelectedNavlinks = userGroup.Navlinks.Select(a => a.NavLinkId).ToList(),
                //NavlinksLookUp = this.GetNavlinksLookUp(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditUserGroup)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserGroupEditViewModel model)
        {
            var userGroup = _userGroupServices.GetById(model.UserGroupId);

            if (userGroup == null)
                ModelState.AddModelError("", "UserGroupId not exists.");

            if (_userGroupServices.CheckIfNameExist(model.Name, model.UserGroupId))
                ModelState.AddModelError("Name", "Name already exists.");

            if (!ModelState.IsValid)
            {
                //model.PermissionsLookUp = this.GetPermissionsLookUp();
                model.PermissionGroupsLookUp = this.GetPermissionGroupLookUp();
                //model.NavlinksLookUp = this.GetNavlinksLookUp();
                return View(model);
            }

            userGroup.Name = model.Name;
            userGroup.Description = model.Description;
            userGroup.IsActive = model.IsActive;

            if (userGroup.Permissions != null)
                userGroup.Permissions.Clear();
            userGroup.Permissions = _permissionServices.GetByIds(model.SelectedPermissions ?? new List<int>()).ToList();

            //if (userGroup.Navlinks != null)
            //    userGroup.Navlinks.Clear();
            //userGroup.Navlinks = _navLinkServices.GetByIds(model.SelectedNavlinks ?? new List<int>()).ToList();

            _userGroupServices.InsertOrUpdate(userGroup, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("User group successfully updated.");
            Logger.Info($"User group successfully created. UserId : [{CurrentUser.AppUserId}]" + JsonConvert.SerializeObject(model), LogEventTitles.UserGroupUpdated, "", "", JsonConvert.SerializeObject(model));

            return RedirectToAction("Index");
        }

        //Delete UserGroup
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanDeleteUserGroup)]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var data = _userGroupServices.GetById(id);
            if (data != null)
            {
                data.IsDeleted = true;
                _unitOfWork.SaveChanges();
                result = true;
                Logger.Info($"User group successfully deleted. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.UserGroupDeleted, "", "", "");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Activate Or Deactivate UserGroup
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanDeactivateReactivateUserGroup)]
        public JsonResult DeactivateActivate(int id)
        {
            bool result = false;
            var data = _userGroupServices.GetById(id);

            if (data != null && data.IsActive == true)
            {
                data.IsActive = false;
                _unitOfWork.SaveChanges();
                result = false;
                Logger.Info($"User group successfully deactivated. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.UserGroupReactivateDeactivate, "", "", "");
            }
            else
            {
                data.IsActive = true;
                _unitOfWork.SaveChanges();
                result = true;
                Logger.Info($"User group successfully reactivated. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.UserGroupReactivateDeactivate, "", "", "");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
        private string GenerateActivateUserLink()
        {
            UrlHelper Url = new UrlHelper(this.ControllerContext.RequestContext);
            string url = Url.Action("Activate", "Account");

            return BaseSiteUrlWithoutAppPath.TrimEnd('/') + url; ;
        }

        private List<KeyValuePair<int, string>> GetPermissionsLookUp()
        {
            return _permissionServices.GetAllActive()
                .Select(a => new { a.PermisssionId, a.Description })
                .ToList()
                .Select(a => new KeyValuePair<int, string>(a.PermisssionId, a.Description))
                .ToList();
        }

        private List<KeyValuePair<int, string>> GetNavlinksLookUp()
        {
            return _navLinkServices.GetAllActive()
                .Select(a => new { a.NavLinkId, a.Name, ParentName = a.ParentNavLink?.Name })
                .ToList()
                .Select(a => new KeyValuePair<int, string>(a.NavLinkId, !string.IsNullOrEmpty(a.ParentName) ? $"{a.ParentName} - {a.Name}" : a.Name))
                .ToList();
        }

        private List<PermissionGroup> GetPermissionGroupLookUp()
        {
            return _permissionGroupServices.GetAllActive()
                    .Select(a => new PermissionGroup
                    {
                        PermissionGroupId = a.PermissionGroupId,
                        Name = a.Name,
                        Description = a.Description,
                        IsActive = a.IsActive,
                        Permissions = a.Permissions.Where(p => p.IsActive).Select(b => new Permission
                        {
                            PermisssionId = b.PermisssionId,
                            Code = b.Code,
                            Description = b.Description,
                            IsActive = a.IsActive
                        }).ToList(),
                    })
                    .ToList();
        }
        #endregion

        #region Export To Excel
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanExportUserGroupViewList)]
        [HttpGet]
        public JsonResult ExportDataToExcelFile(UserGroupFilter filter)
        {
            return Json(new
            {
                data = new
                {
                    FileName = _userGroupServices.ExportDataToExcelFile(
                        filter,
                        Server,
                        CurrentUser.AppUserId,
                        CurrentUser.CurrentOffice)
                }
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}