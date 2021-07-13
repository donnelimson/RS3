using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;
using Web.Models.ViewModels.NavLink;

namespace Web.Controllers
{
    public class NavLinkController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IPermissionServices _permissionServices;
        private readonly IPermissionGroupServices _permissionGroupServices;
        private readonly IUserGroupServices _userGroupServices;
        private readonly INavLinkServices _navLinkServices;

        private readonly IUnitOfWork _unitOfWork;

        public NavLinkController(
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

        #region View
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewNavLink)]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchNavLink(string searchTerm, int page, int pageSize, string sortColumn, string sortOrder,
           string createdBy, DateTime? createdOnFrom, DateTime? createdOnTo)
        {
            var navLinkFilter = new NavLinkFilter
            {
                SearchTerm = searchTerm,
                CreatedBy = createdBy,
                CreatedOnFrom = createdOnFrom,
                CreatedOnTo = createdOnTo,
                Page = page,
                PageSize = pageSize,
                SortColumn = string.IsNullOrEmpty(sortColumn) ? "CreatedDate" : sortColumn.Replace(" ", string.Empty),
                SortDirection = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder.Replace(" ", string.Empty)

            };

            var result = _navLinkServices.Search(navLinkFilter);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = navLinkFilter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }


        #endregion


        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddNavLink)]
        public ActionResult Create()
        {
            var viewModel = new NavLinkCreateViewModel
            {
                ParentNavLinkLookUp = this.GetNavLinkParentLookUp()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanAddNavLink)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NavLinkCreateViewModel model)
        {
            if (_navLinkServices.CheckIfNameExist(model.Name))
                ModelState.AddModelError("Name", "Name already exists.");

            if (!model.IsParent && string.IsNullOrEmpty(model.Controller))
                ModelState.AddModelError("Controller", "Required if not parent.");

            if (!model.IsParent && string.IsNullOrEmpty(model.Action))
                ModelState.AddModelError("Action", "Required if not parent.");

            if (!ModelState.IsValid)
            {
                model.ParentNavLinkLookUp = this.GetNavLinkParentLookUp();
                return View(model);
            }

            var navLink = new NavLink();
            navLink.Name = model.Name;
            navLink.IconClass = model.IconClass;
            navLink.Controller = model.Controller;
            navLink.Action = model.Action;
            navLink.Parameters = model.Parameters;
            navLink.IsActive = model.IsActive;
            navLink.IsParent = model.IsParent;
            navLink.ParentNavLinkId = model.SelectedParentNavLinkId;

            _navLinkServices.InsertOrUpdate(navLink, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("Navigation Link successfully created.");
            Logger.Info($"Navigation Link successfully created. UserId : [{CurrentUser.AppUserId}]" + JsonConvert.SerializeObject(model), LogEventTitles.NavlinkCreated, "", "", JsonConvert.SerializeObject(model));
            return RedirectToAction("Index");
        }

        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditNavLink)]
        public ActionResult Edit(int id)
        {
            var navLink = _navLinkServices.GetById(id);
            if (navLink == null)
                return HttpNotFound($"NavLinkId [{id}] not found.");

            var viewModel = new NavLinkEditViewModel
            {
                NavLinkId = navLink.NavLinkId,
                Name = navLink.Name,
                IconClass = navLink.IconClass,
                Controller = navLink.Controller,
                Action = navLink.Action,
                Parameters = navLink.Parameters,
                IsActive = navLink.IsActive,
                IsParent = navLink.IsParent,
                SelectedParentNavLinkId = navLink.ParentNavLinkId,
                ParentNavLinkLookUp = this.GetNavLinkParentLookUp(),
            };

            return View(viewModel);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditNavLink)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NavLinkEditViewModel model)
        {
            var navLink = _navLinkServices.GetById(model.NavLinkId);

            if (navLink == null)
                ModelState.AddModelError("", "NavLinkId not exists.");

            if (_navLinkServices.CheckIfNameExist(model.Name, model.NavLinkId))
                ModelState.AddModelError("Name", "Name already exists.");

            if (!model.IsParent && string.IsNullOrEmpty(model.Controller))
                ModelState.AddModelError("Controller", "Required if not parent.");

            if (!model.IsParent && string.IsNullOrEmpty(model.Action))
                ModelState.AddModelError("Action", "Required if not parent.");

            if (!ModelState.IsValid)
            {
                model.ParentNavLinkLookUp = this.GetNavLinkParentLookUp();
                return View(model);
            }

            navLink.Name = model.Name;
            navLink.IconClass = model.IconClass;
            navLink.Controller = model.Controller;
            navLink.Action = model.Action;
            navLink.Parameters = model.Parameters;
            navLink.IsActive = model.IsActive;
            navLink.IsParent = model.IsParent;
            navLink.ParentNavLinkId = model.SelectedParentNavLinkId;

            _navLinkServices.InsertOrUpdate(navLink, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("Navigation Link successfully updated.");
            Logger.Info($"Navigation Link successfully updated. UserId : [{CurrentUser.AppUserId}]" + JsonConvert.SerializeObject(model), LogEventTitles.NavlinkUpdated, "", "", JsonConvert.SerializeObject(model));

            return RedirectToAction("Index");
        }


        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanDeleteNavLink)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            string message = string.Empty;
            var result = false;

            try
            {
                var navLink = _navLinkServices.GetById(id);
                if (navLink == null)
                {
                    message = "NavLink not found!";
                }
                else
                {
                    navLink.IsActive = false;
                    _unitOfWork.SaveChanges();

                    result = true;
                    message = "NavLink has been deleted!";
                    Logger.Info($"Navigation Link successfully deleted. UserId : [{CurrentUser.AppUserId}]", LogEventTitles.NavLinkDeleted, "", "", "");
                }
            }
            catch (DbUpdateException dbEx)
            {
                Logger.Error($"Failed to delete NavLink! {dbEx.InnerException.Message}. UserId : [{CurrentUser.AppUserId}]");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error! {ex.InnerException.Message}. UserId : [{CurrentUser.AppUserId}]");
            }

            return Json(new
            {
                result = result,
                message = message
            }, JsonRequestBehavior.AllowGet);
        }


        #region Helpers
        private List<KeyValuePair<int, string>> GetNavLinkParentLookUp()
        {
            return _navLinkServices.GetAllActiveParent()
                .Select(a => new { a.NavLinkId, a.Name, ParentNavLinkName = a.ParentNavLink?.Name })
                .ToList()
                .Select(a => new KeyValuePair<int, string>(a.NavLinkId, !string.IsNullOrEmpty(a.ParentNavLinkName) ? $"{a.ParentNavLinkName} - {a.Name}" : a.Name))
                .ToList();
        }

        #endregion

    }
}