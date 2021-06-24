using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Context;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Infrastructure.Utilities.QueryHelper;

namespace Infrastructure.Repository
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(AppCommonContext context) : base(context)
        {
        }

        public override void InsertOrUpdate(Permission entity)
        {
            if (entity.PermisssionId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public IEnumerable<Permission> GetAllPermission()
        {
            return GetAll.Where(a => !a.IsActive).AsEnumerable();
        }

        public IEnumerable<PermissionToExcel> GetDataForExportingToExcel(PermissionFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new PermissionToExcel
            {
                
                Description = a.Description,
                PermissionGroup = a.PermissionGroup.Description,
                NavigationLink = a.Description,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        private IQueryable<Permission> GetData(PermissionFilter filter)
        {
            var data = GetAll.Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                data = data.Where(m => m.Code.Contains(filter.SearchTerm) ||
                m.Description.Contains(filter.SearchTerm) ||
                m.PermissionGroup.Name.Contains(filter.SearchTerm) ||
                m.NavLink.Name.Contains(filter.SearchTerm));
            }

            return data;
        }
    }
}
