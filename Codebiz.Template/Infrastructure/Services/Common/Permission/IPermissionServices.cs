using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs;
using Infrastructure.Utilities.QueryHelper;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;

namespace Infrastructure.Services
{
    public interface IPermissionServices
    {
        IEnumerable<Permission> GetAll();
        IEnumerable<Permission> GetAllActive();
        IEnumerable<Permission> GetByUserGroupIds(IEnumerable<int> userGroupIds);
        IEnumerable<Permission> GetByIds(IEnumerable<int> Ids);
        Permission GetById(int id);
        Permission GetByCode(string code);
        void InsertOrUpdate(Permission entity, int appUserId);
        IPagedList<PermissionDTO> Search(PermissionFilter permissionFilter);
        string ExportDataToExcelFile(PermissionFilter filter, HttpServerUtilityBase server, int currentAppUserId,string currentOffice);

    }

    public class PermissionServices : IPermissionServices
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionServices(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public IEnumerable<Permission> GetAll()
        {
            return _permissionRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }

        public IEnumerable<Permission> GetAllActive()
        {
            return _permissionRepository.GetAll.Where(a=>a.IsActive).AsEnumerable();
        }

        public Permission GetById(int id)
        {
            return _permissionRepository.GetAll.Where(a => a.PermisssionId == id && a.IsActive).FirstOrDefault();
        }

        public IEnumerable<Permission> GetByIds(IEnumerable<int> Ids)
        {
            return _permissionRepository.GetAll.Where(a => Ids.Contains(a.PermisssionId) && a.IsActive).AsEnumerable();
        }

        public Permission GetByCode(string code)
        {
            return _permissionRepository.GetAll.Where(a => a.Code == code && a.IsActive).FirstOrDefault();
        }

        public IEnumerable<Permission> GetByUserGroupIds(IEnumerable<int> userGroupIds)
        {
            return _permissionRepository.GetAll.Where(a => a.UserGroups.Any(b => userGroupIds.Contains(b.UserGroupId)) && a.IsActive).Distinct();
        }

        public void InsertOrUpdate(Permission entity, int appUserId)
        {
            if (entity.PermisssionId.Equals(0))
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

            _permissionRepository.InsertOrUpdate(entity);
        }

        public IPagedList<PermissionDTO> Search(PermissionFilter permissionFilter)
        {
            var data = _permissionRepository.GetAll.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(permissionFilter.SearchTerm))
            {
                data = data.Where(m => m.Description.Contains(permissionFilter.SearchTerm) ||
                m.Description.Contains(permissionFilter.SearchTerm) ||
                m.PermissionGroup.Name.Contains(permissionFilter.SearchTerm) ||
                m.NavLink.Name.Contains(permissionFilter.SearchTerm));
            }
            permissionFilter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new PermissionDTO
            {
                PermisssionId = a.PermisssionId,
                Code = a.Code,
                Description = a.Description,
                PermissionGroup = a.PermissionGroup.Name,
                NavLink = a.NavLink.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, permissionFilter.SortColumn, permissionFilter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(permissionFilter.Page, permissionFilter.PageSize);
        }

        public string ExportDataToExcelFile(PermissionFilter filter, HttpServerUtilityBase server, int currentAppUserId,string currentOffice)
        {
            var data = _permissionRepository.GetDataForExportingToExcel(filter).ToList();
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(PermissionToExcel)),
                data,
                "Permission" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Permission List",
                currentAppUserId, currentOffice);

            return fileName;
        }
    }
}
