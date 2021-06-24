using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Context;
using Codebiz.Domain.Common.Model.DTOs;
using PagedList;
using Codebiz.Domain.Common.Model.Filter;
using Infrastructure.Utilities.QueryHelper;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;

namespace Infrastructure.Repository
{
    public class UserGroupRepository : RepositoryBase<UserGroup>, IUserGroupRepository
    {
        private readonly AppCommonContext db;

        public UserGroupRepository(AppCommonContext context) : base(context)
        {
            db = context;
        }

        public override void InsertOrUpdate(UserGroup entity)
        {
            if (entity.UserGroupId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public List<int> GetAllUserGroupIdsByAppUserId(int id)
        {
            return db.AppUsers.Where(a => a.IsActive && a.AppUserId == id)
                                .SelectMany(x => x.UserGroups.Select(b => b.UserGroupId)).ToList();
        }

        public List<string> GetAllPermissionsByUserGroupId(List<int> appUserUserGroupIds)
        {
            var appUserPermissions = new List<string>();

            foreach (var userGroupId in appUserUserGroupIds)
            {
                var userGroupPermissions = db.UserGroups.Where(a => a.IsActive && a.UserGroupId == userGroupId)
                                .SelectMany(x => x.Permissions.Select(b => b.Code)).ToList();

                appUserPermissions.AddRange(userGroupPermissions);
            }

            return appUserPermissions.Distinct().ToList();
        }

        public List<string> GetAllPermissionsByPermissionGroupNames(List<string> permissionGroupNames, int id)
        {
            var appUserPermissions = new List<string>();
            var appUserUserGroupIds = GetAllUserGroupIdsByAppUserId(id);

            foreach (var permissionGroupName in permissionGroupNames)
            {
                foreach (var userGroupId in appUserUserGroupIds)
                {
                    var userGroupPermissions = db.UserGroups.Where(a => a.IsActive && a.UserGroupId == userGroupId)
                                    .SelectMany(x => x.Permissions.Where(b => b.PermissionGroup.Name == permissionGroupName).Select(c => c.Code)).ToList();

                    appUserPermissions.AddRange(userGroupPermissions);
                }
            }

            return appUserPermissions.Distinct().ToList();
        }
        public IPagedList<UserGroupDTO> SearchUserGroup(UserGroupFilter filter)
        {
            var data = GetAll.Where(a =>a.IsDeleted == false);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filter.Name = filter.Name.Trim();
                data = data.Where(a => a.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                filter.Description = filter.Description.Trim();
                data = data.Where(a => a.Description.Contains(filter.Description));
            }

            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                filter.CreatedBy = filter.CreatedBy.Trim();
                data = data.Where(a => (a.CreatedByAppUser.Employee.FirstName + " "
                + a.CreatedByAppUser.Employee.LastName)
                .Trim().Contains(filter.CreatedBy));
            }

            filter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new UserGroupDTO
            {
                UserGroupId = a.UserGroupId,
                Name = a.Name,
                Description = a.Description,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedOn = a.CreatedOn
            });
            //Sort
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        public IEnumerable<UserGroupDTO> GetDataForExportingToExcel(UserGroupFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.ToList().Select(a => new UserGroupDTO
            {
                Name = a.Name,
                Description = a.Description,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedOn = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO.AsQueryable(), filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        private IQueryable<UserGroup> GetData(UserGroupFilter filter)
        {
            var data = GetAll.Where(a => a.IsActive);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                data = data.Where(a => a.Name.Contains(filter.Name.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                data = data.Where(a => a.Description.Contains(filter.Description.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                data = data.Where(a => (a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName)
                    .Trim().Contains(filter.CreatedBy.Trim()));
            }

            if (filter.CreatedOnFrom != null && filter.CreatedOnTo != null)
            {
                data = data.Where(a => DbFunctions.TruncateTime(a.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(a.CreatedOn) <= filter.CreatedOnTo);
            }

            return data;
        }
    }
}
