using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs;
using Infrastructure.Utilities.QueryHelper;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.ERP.Repository;
using Codebiz.Domain.Common.Model.Enums;

namespace Infrastructure.Services
{
    public interface INavLinkServices
    {
        IEnumerable<NavLink> GetAll();
        IEnumerable<NavLink> GetAllActive();
        IEnumerable<NavLink> GetAllActiveParent();
        IEnumerable<NavLink> GetAllMainNavLink();
        GetUserNavLinksResult GetUserNavLinks(string usernameOrEmail);
        UserNavLinkAccessResult GetUserNavLinkAccess(string username);
        IEnumerable<NavLink> GetByIds(IEnumerable<int> ids);
        NavLink GetById(int id);
        NavLink GetByName(string name);
        void InsertOrUpdate(NavLink entity, int appUserId);
        void Delete(NavLink entity, int appUserId);
        bool CheckIfNameExist(string name, int id = 0);
        IPagedList<NavLinkDTO> Search(NavLinkFilter navLinkFilter);
     
    }

    public class NavLinkServices : INavLinkServices
    {
        private readonly INavLinkRepository _navLinkRepository;
        private readonly IAppUserRepository _appUserRepository;

        public NavLinkServices(
            INavLinkRepository navLinkRepository,
            IAppUserRepository appUserRepository
            )
        {
            _navLinkRepository = navLinkRepository;
            _appUserRepository = appUserRepository;
        }

        public bool CheckIfNameExist(string name, int id = 0)
        {
            return _navLinkRepository.GetAll.Any(a=>a.NavLinkId != id && a.Name == name);
        }

        public void Delete(NavLink entity, int appUserId)
        {
            entity.ModifiedByAppUserId = appUserId;
            entity.ModifiedOn = DateTime.Now;
            entity.IsActive = false;

            _navLinkRepository.InsertOrUpdate(entity);
        }

        public IEnumerable<NavLink> GetAll()
        {
            return _navLinkRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }

        public IEnumerable<NavLink> GetAllActive()
        {
            return _navLinkRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }

        public IEnumerable<NavLink> GetAllActiveParent()
        {
            return _navLinkRepository.GetAll.Where(a => a.IsActive && a.IsParent).AsEnumerable();
        }

        public NavLink GetById(int id)
        {
            return _navLinkRepository.GetAll.Where(a => a.NavLinkId == id && a.IsActive).FirstOrDefault();
        }

        public IEnumerable<NavLink> GetByIds(IEnumerable<int> ids)
        {
            return _navLinkRepository.GetAll.Where(a => ids.Contains(a.NavLinkId) && a.IsActive).AsEnumerable();
        }

        public NavLink GetByName(string name)
        {
            return _navLinkRepository.GetAll.Where(a => a.Name == name && a.IsActive).FirstOrDefault();
        }

        public UserNavLinkAccessResult GetUserNavLinkAccess(string username)
        {
            var result = new UserNavLinkAccessResult();

            var navLinkQuery = (from pnl in _navLinkRepository.GetAll
                                join nl in _navLinkRepository.GetAll on pnl.NavLinkId equals nl.ParentNavLinkId into nlx
                                from nl in nlx.DefaultIfEmpty()
                                select new
                                {
                                    ParentNavLink = pnl,
                                    MainNavlinkId = pnl.NavLinkId,
                                    NavLink = nl
                                }).ToList();

            var currentUser = _appUserRepository.GetAll
                .Where(a => (a.Username == username || a.Employee.Email == username) && a.IsActive)
                .Include(a=>a.UserGroups)
                .Include("UserGroups.Permissions")
                .Include("UserGroups.Permissions.NavLink")
                .FirstOrDefault();

            var userNavLinks = currentUser.UserGroups.SelectMany(a => a.Permissions)
                .Where(a => a.NavLinkId != null && a.IsActive)
                .Select(a => a.NavLink)
                .Where(a => a.IsActive)
                .Distinct()
                .Select(a=>a.NavLinkId);

            var parentNavlinkIds = navLinkQuery.Where(a => 
                                                        (a.NavLink != null && userNavLinks.Contains(a.NavLink.NavLinkId)) || 
                                                        (a.ParentNavLink != null && (userNavLinks.Contains(a.ParentNavLink.NavLinkId) || userNavLinks.Contains(a.MainNavlinkId))) 
                                                    )
                                                .Select(a => a.ParentNavLink.ParentNavLink != null ? a.ParentNavLink.ParentNavLink.NavLinkId : a.ParentNavLink.NavLinkId)
                                                .Distinct();
            result.NavLinkIds = userNavLinks.ToList();
            result.ParentNavLinkIds = parentNavlinkIds.ToList();
            return result;
        }

        public void InsertOrUpdate(NavLink entity, int appUserId)
        {
            if (entity.NavLinkId.Equals(0))
            {
                var now = DateTime.Now;

                entity.CreatedByAppUserId = appUserId;
                entity.CreatedOn = now;

                //entity.ModifiedByAppUserId = appUserId;
                //entity.ModifiedOn = now;
            }
            else
            {
                entity.ModifiedByAppUserId = appUserId;
                entity.ModifiedOn = DateTime.Now;
            }

            _navLinkRepository.InsertOrUpdate(entity);
        }

        public IPagedList<NavLinkDTO> Search(NavLinkFilter navLinkFilter)
        {
            var data = _navLinkRepository.GetAll.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(navLinkFilter.SearchTerm))
            {
                data = data.Where(m => m.Name.Contains(navLinkFilter.SearchTerm) 
                || m.IconClass.Contains(navLinkFilter.SearchTerm)
                || m.Controller.Contains(navLinkFilter.SearchTerm)
                || m.Action.Contains(navLinkFilter.SearchTerm)
                || m.Parameters.Contains(navLinkFilter.SearchTerm)
                );
            }

            navLinkFilter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new NavLinkDTO
            {
                NavLinkId = a.NavLinkId,
                Name = a.Name,
                IconClass = a.IconClass,
                Controller = a.Controller,
                Action = a.Action,
                Parameters = a.Parameters,
                IsParent = a.IsParent,
                ParentNavLink = a.ParentNavLink.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });
            dataDTO = QueryHelper.Ordering(dataDTO, navLinkFilter.SortColumn, navLinkFilter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(navLinkFilter.Page, navLinkFilter.PageSize);
        }

        public IEnumerable<NavLink> GetAllMainNavLink()
        {
            return _navLinkRepository.GetAllIncluding(a => a.ChildNavLinks)
                .Include("ChildNavLinks.ChildNavLinks")
                .Include("ChildNavLinks.ChildNavLinks.ChildNavLinks")
                .Where(a => a.IsActive && a.ParentNavLinkId == null);
        }

        public GetUserNavLinksResult GetUserNavLinks(string usernameOrEmail)
        {
            var result = new GetUserNavLinksResult() { NavLinks = new List<NavLinkModel>() };

            var userAccess = this.GetUserNavLinkAccess(usernameOrEmail);
            var mainNavLinks = this.GetAllMainNavLink().ToList();

            foreach (var navLink in mainNavLinks)
            {
                var mainNavLink = this.GetNavLinkIfUserHasAccess(navLink, userAccess, usernameOrEmail);
                if (mainNavLink != null)
                {
                    result.NavLinks.Add(mainNavLink);
                }
            }

            return result;
        }

        private NavLinkModel GetNavLinkIfUserHasAccess(NavLink navLink, UserNavLinkAccessResult userAccess, string usernameOrEmail)
        {
            if (navLink.IsParent)
            {
                if (userAccess.ParentNavLinkIds.Count() > 0 && userAccess.ParentNavLinkIds.Contains(navLink.NavLinkId))
                {
                    var parentNavLink = new NavLinkModel
                    {
                        Action = navLink.Action,
                        Area = navLink.Area,
                        Controller = navLink.Controller,
                        IconClass = navLink.IconClass,
                        IsParent = navLink.IsParent,
                        Name = navLink.Name,
                        NavLinkId = navLink.NavLinkId,
                        Parameters = navLink.Parameters,
                        Permission = navLink.Permissions.Any() ? navLink.Permissions.FirstOrDefault(p => p.Description.ToUpper().Contains("VIEW") && !p.Description.ToUpper().Contains("DETAILS")).Code : string.Empty,
                        Ordinal = navLink.Ordinal,
                    };

                    if (navLink.ChildNavLinks != null && navLink.ChildNavLinks.Count > 0)
                    {
                        var childNavLinks = navLink.ChildNavLinks.ToList();

                        foreach (var item in childNavLinks)
                        {
                            var childNavLinkWithAccess = this.GetNavLinkIfUserHasAccess(item, userAccess, usernameOrEmail);

                            if (childNavLinkWithAccess != null)
                            {
                                if (parentNavLink.ChildNavLinks == null)
                                    parentNavLink.ChildNavLinks = new List<NavLinkModel>();
                                parentNavLink.ChildNavLinks.Add(childNavLinkWithAccess);
                            }
                        }

                        if (parentNavLink.ChildNavLinks != null && parentNavLink.ChildNavLinks.Count > 0)
                        {
                            return parentNavLink;
                        }
                    }
                }
            }
            else
            {
                if (userAccess.NavLinkIds.Count() > 0 && userAccess.NavLinkIds.Contains(navLink.NavLinkId))
                {
                    var navLinkData = navLink.Permissions.FirstOrDefault(p => p.Description.ToUpper().Contains("VIEW"));
                    if (navLinkData == null)
                    {
                        navLinkData = navLink.Permissions.FirstOrDefault();
                    }

                    var newChildNavLink = new NavLinkModel
                    {
                        Action = navLink.Action,
                        Area = navLink.Area,
                        Controller = navLink.Controller,
                        IconClass = navLink.IconClass,
                        IsParent = navLink.IsParent,
                        Name = navLink.Name,
                        NavLinkId = navLink.NavLinkId,
                        Parameters = navLink.Parameters,
                        Permission = navLink.Permissions.Any() ? navLinkData.Code : string.Empty,
                        Ordinal = navLink.Ordinal,
                    };

                    return newChildNavLink;
                }
            }

            return null;
        }

    }
}
