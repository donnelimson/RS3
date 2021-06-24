using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IPermissionGroupServices
    {
        IEnumerable<PermissionGroup> GetAll();
        IEnumerable<PermissionGroup> GetAllActive();
        IEnumerable<PermissionGroup> GetByIds(IEnumerable<int> Ids);
        PermissionGroup GetById(int id);
        PermissionGroup GetByName(string name);
        void InsertOrUpdate(PermissionGroup entity, int appUserId);
        IPagedList<PermissionGroup> Search(string searchStr, string sortOrder, int maxNumberOfRows, int pageNumber);
    }

    public class PermissionGroupServices : IPermissionGroupServices
    {
        private readonly IPermissionGroupRepository _permissionGroupRepository;
        public PermissionGroupServices(IPermissionGroupRepository permissionGroupRepository)
        {
            _permissionGroupRepository = permissionGroupRepository;
        }

        public IEnumerable<PermissionGroup> GetAll()
        {
            return _permissionGroupRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }

        public IEnumerable<PermissionGroup> GetAllActive()
        {
            return _permissionGroupRepository.GetAll.Where(a=>a.IsActive).AsEnumerable();
        }

        public PermissionGroup GetById(int id)
        {
            return _permissionGroupRepository.GetAll.Where(a => a.PermissionGroupId == id && a.IsActive).FirstOrDefault();
        }

        public IEnumerable<PermissionGroup> GetByIds(IEnumerable<int> Ids)
        {
            return _permissionGroupRepository.GetAll.Where(a => Ids.Contains(a.PermissionGroupId) && a.IsActive).AsEnumerable();
        }

        public PermissionGroup GetByName(string name)
        {
            return _permissionGroupRepository.GetAll.Where(a => a.Name == name && a.IsActive).FirstOrDefault();
        }
        
        public void InsertOrUpdate(PermissionGroup entity, int appUserId)
        {
            if (entity.PermissionGroupId.Equals(0))
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

            _permissionGroupRepository.InsertOrUpdate(entity);
        }

        public IPagedList<PermissionGroup> Search(string searchStr, string sortOrder, int maxNumberOfRows, int pageNumber)
        {
            var result = _permissionGroupRepository.GetAll.Where(a => a.IsActive);

            if (!string.IsNullOrEmpty(searchStr))
            {
                result = result.Where(m => m.Name.Contains(searchStr) || m.Description.Contains(searchStr));
            }

            switch (sortOrder)
            {
                case "Name":
                    result = result.OrderBy(a=>a.Name);
                    break;
                case "Name_Desc":
                    result = result.OrderByDescending(a => a.Name);
                    break;
                case "Description":
                    result = result.OrderBy(a => a.Description);
                    break;
                case "Description_Desc":
                    result = result.OrderByDescending(a => a.Description);
                    break;
                case "IsActive":
                    result = result.OrderBy(a => a.IsActive);
                    break;
                case "IsActive_Desc":
                    result = result.OrderByDescending(a => a.IsActive);
                    break;
                case "CreatedOn":
                    result = result.OrderBy(a => a.CreatedOn);
                    break;
                case "CreatedOn_Desc":
                    result = result.OrderByDescending(a => a.CreatedOn);
                    break;
                case "CreatedByAppUser":
                    result = result.OrderBy(a => a.CreatedByAppUser.Employee.LastName);
                    break;
                case "CreatedByAppUser_Desc":
                    result = result.OrderByDescending(a => a.CreatedByAppUser.Employee.LastName);
                    break;
                case "ModifiedOn":
                    result = result.OrderBy(a => a.ModifiedOn);
                    break;
                case "ModifiedOn_Desc":
                    result = result.OrderByDescending(a => a.ModifiedOn);
                    break;
                case "ModifiedByAppUser":
                    result = result.OrderBy(a => a.ModifiedByAppUser.Employee.LastName);
                    break;
                case "ModifiedByAppUser_Desc":
                    result = result.OrderByDescending(a => a.ModifiedByAppUser.Employee.LastName);
                    break;
                default:
                    result = result.OrderBy(a => a.PermissionGroupId);
                    break;
            }

            return result.OrderByDescending(m => m.PermissionGroupId).ToPagedList(pageNumber, maxNumberOfRows);
        }
    }
}
