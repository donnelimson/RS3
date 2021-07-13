using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;

namespace Infrastructure.Services
{
    public interface IUserGroupServices
    {
        IEnumerable<UserGroup> GetAll();
        IEnumerable<UserGroup> GetAllActive();
        IEnumerable<UserGroup> GetByIds(IEnumerable<int> ids);
        UserGroup GetById(int id);
        UserGroup GetByName(string name);
        void InsertOrUpdate(UserGroup entity, int appUserId);
        void Delete(UserGroup entity, int appUserId);
        bool CheckIfNameExist(string name, int id = 0);
        IPagedList<UserGroup> Search(string searchTerm, string sortBy, int maxNumberOfRows, int pageNumber);
        IPagedList<UserGroupDTO> SearchUserGroup(UserGroupFilter filter);
        List<int> GetAllUserGroupIdsByAppUserId(int id);
        List<string> GetAllPermissionsByUserGroupId(List<int> appUserUserGroupIds);
        List<string> GetAllPermissionsByPermissionGroupNames(List<string> permissionGroupNames, int id);

    }

    public class UserGroupServices : IUserGroupServices
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public void InsertOrUpdate(UserGroup entity, int appUserId)
        {
            if (entity.UserGroupId.Equals(0))
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

            _userGroupRepository.InsertOrUpdate(entity);
        }
        public void Delete(UserGroup entity, int appUserId)
        {
            entity.ModifiedByAppUserId = appUserId;
            entity.ModifiedOn = DateTime.Now;
            entity.IsActive = false; 
            _userGroupRepository.InsertOrUpdate(entity);
        }
        public UserGroupServices(IUserGroupRepository permissionRepository)
        {
            _userGroupRepository = permissionRepository;
        }
        public bool CheckIfNameExist(string name, int id = 0)
        {
            return _userGroupRepository.GetAll.Where(a => !a.IsDeleted).Any(o => o.UserGroupId != id && o.Name == name);
        }
        public IEnumerable<UserGroup> GetAll()
        {
            return _userGroupRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }
        public IEnumerable<UserGroup> GetAllActive()
        {
            return _userGroupRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }
        public UserGroup GetById(int id)
        {
            return _userGroupRepository.GetAll.FirstOrDefault(a => a.UserGroupId == id);
        }
        public IEnumerable<UserGroup> GetByIds(IEnumerable<int> ids)
        {
            return _userGroupRepository.GetAll.Where(a => ids.Contains(a.UserGroupId) && a.IsActive).AsEnumerable();
        }
        public UserGroup GetByName(string name)
        {
            return _userGroupRepository.GetAll.Where(a => a.Name == name && a.IsActive).FirstOrDefault();
        }
        public List<int> GetAllUserGroupIdsByAppUserId(int id)
        {
            return _userGroupRepository.GetAllUserGroupIdsByAppUserId(id);
        }

        public List<string> GetAllPermissionsByUserGroupId(List<int> appUserUserGroupIds)
        {
            return _userGroupRepository.GetAllPermissionsByUserGroupId(appUserUserGroupIds);
        }

        public List<string> GetAllPermissionsByPermissionGroupNames(List<string> permissionGroupNames, int id)
        {
            return _userGroupRepository.GetAllPermissionsByPermissionGroupNames(permissionGroupNames, id);
        }
        public IPagedList<UserGroupDTO> SearchUserGroup(UserGroupFilter filter)
        {
            return _userGroupRepository.SearchUserGroup(filter);
        }

        #region Search
        public IPagedList<UserGroup> Search(string searchTerm, string sortBy, int maxNumberOfRows, int pageNumber)
        {
            var result = _userGroupRepository.GetAll.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                result = result.Where(m => m.Name.Contains(searchTerm) || m.Description.Contains(searchTerm));
            }

            IOrderedQueryable<UserGroup> orderedResult;

            switch (sortBy)
            {
                case "Name":
                    orderedResult = result.OrderBy(a=>a.Name);
                    break;
                case "Name_Desc":
                    orderedResult = result.OrderByDescending(a => a.Name);
                    break;
                case "Description":
                    orderedResult = result.OrderBy(a => a.Description);
                    break;
                case "Description_Desc":
                    orderedResult = result.OrderByDescending(a => a.Description);
                    break;
                case "IsActive":
                    orderedResult = result.OrderBy(a => a.IsActive);
                    break;
                case "IsActive_Desc":
                    orderedResult = result.OrderByDescending(a => a.IsActive);
                    break;
                case "CreatedOn":
                    orderedResult = result.OrderBy(a => a.CreatedOn);
                    break;
                case "CreatedOn_Desc":
                    orderedResult = result.OrderByDescending(a => a.CreatedOn);
                    break;
                case "CreatedByAppUser":
                    orderedResult = result.OrderBy(a => a.CreatedByAppUser.Employee.LastName);
                    break;
                case "CreatedByAppUser_Desc":
                    orderedResult = result.OrderByDescending(a => a.CreatedByAppUser.Employee.LastName);
                    break;
                case "ModifiedOn":
                    orderedResult = result.OrderBy(a => a.ModifiedOn);
                    break;
                case "ModifiedOn_Desc":
                    orderedResult = result.OrderByDescending(a => a.ModifiedOn);
                    break;
                case "ModifiedByAppUser":
                    orderedResult = result.OrderBy(a => a.ModifiedByAppUser.Employee.LastName);
                    break;
                case "ModifiedByAppUser_Desc":
                    orderedResult = result.OrderByDescending(a => a.ModifiedByAppUser.Employee.LastName);
                    break;
                default:
                    orderedResult = result.OrderBy(a => a.UserGroupId);
                    break;
            }

            return orderedResult.ThenBy(m => m.UserGroupId).ToPagedList(pageNumber, maxNumberOfRows);
        }


        #endregion
    }
}
