using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context.SeedData;
using Infrastructure;
using Infrastructure.Services;
using Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;
using Web.Models.ViewModels.Permission;

namespace Web.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly IPermissionServices _permissionService;
        private readonly INavLinkServices _navLinkService;
        private readonly IPermissionGroupServices _permissionGroupService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserServices _appUserService;


        public PermissionController(
            IAppUserServices appUserServices,
            IPermissionServices permissionService,
            INavLinkServices navLinkService,
            IPermissionGroupServices permissionGroupService,
            IUnitOfWork unitOfWork) : base(appUserServices)
        {
            _permissionService = permissionService;
            _navLinkService = navLinkService;
            _permissionGroupService = permissionGroupService;
            _unitOfWork = unitOfWork;
            _appUserService = appUserServices;

        }

        //Lookup
        private List<KeyValuePair<int, string>> GetPermissionGroupLookUp()
        {
            return _permissionGroupService.GetAll().Select(a => new { a.PermissionGroupId, a.Description }).ToList()
                .Select(a => new KeyValuePair<int, string>(a.PermissionGroupId, a.Description)).ToList();
        }

        private List<KeyValuePair<int, string>> GetNavLinkLookUp()
        {
            return _navLinkService.GetAll().Select(a => new { a.NavLinkId, a.Name }).ToList()
                .Select(a => new KeyValuePair<int, string>(a.NavLinkId, a.Name)).ToList();
        }

        // GET: Permission
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewPermissionData)]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchPermission(string searchTerm, int page, int pageSize, string sortColumn, string sortOrder,
          string createdBy, DateTime? createdOnFrom, DateTime? createdOnTo)
        {
            var permissionFilter = new PermissionFilter
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

            var result = _permissionService.Search(permissionFilter);

            return Json(new
            {
                data = result.ToList(),
                totalRecordCount = permissionFilter.FilteredRecordCount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditPermissionData)]

        public ActionResult Edit(int id)
        {
            var permission = _permissionService.GetById(id);

            if (permission == null)
            {
                return RedirectToAction("Index");
            }

            var vm = new PermissionEditViewModel
            {
                PermissionId = permission.PermisssionId,
                Description = permission.Description,
                SelectedPermissionGroup = permission.PermissionGroupId,
                SelectedNavLink = permission.NavLinkId

            };

            vm.PermissionGroupLookUp = GetPermissionGroupLookUp();
            vm.NavLinkLookUp = GetNavLinkLookUp();

            return View(vm);
        }

        [HttpPost]
        [ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditPermissionData)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PermissionEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PermissionGroupLookUp = GetPermissionGroupLookUp();
                model.NavLinkLookUp = GetNavLinkLookUp();
                return View(model);
            }


            var entity = _permissionService.GetById(model.PermissionId);

            entity.Description = model.Description;
            entity.PermissionGroupId = model.SelectedPermissionGroup;
            entity.NavLinkId = (model.SelectedNavLink == null) ? null : model.SelectedNavLink;

            _permissionService.InsertOrUpdate(entity, CurrentUser.AppUserId);
            _unitOfWork.SaveChanges();

            CreateSuccessMessage("Permission successfully updated.");
            Logger.Info($"Permission successfully updated. UserId : [{CurrentUser.AppUserId}]" + JsonConvert.SerializeObject(model), LogEventTitles.PermissionUpdated, "", "", JsonConvert.SerializeObject(model));

            return RedirectToAction("Index");
        }


    }
}