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
    public class NavLinkRepository : RepositoryBase<NavLink>, INavLinkRepository
    {
        public NavLinkRepository(AppCommonContext context) : base(context)
        {
        }

        public IEnumerable<NavigationLinksToExcel> GetDataForExportingToExcel(NavLinkFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.ToList().Select(a => new NavigationLinksToExcel
            {
                Name = a.Name,
                IconClass = a.IconClass,
                Controller = a.Controller,
                Action = a.Action,
                Parameters = a.Parameters,
                IsParent = a.IsParent,
                NavigationGroup = a.ParentNavLink!= null ? a.ParentNavLink.Name : "",
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO.AsQueryable(), filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        private IQueryable<NavLink> GetData(NavLinkFilter filter)
        {
            var data = GetAll.Where(a => a.IsActive);

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.Name.Contains(filter.SearchTerm.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.ParentNavLink.Name.Contains(filter.SearchTerm.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.Controller.Contains(filter.SearchTerm.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.Action.Contains(filter.SearchTerm.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.IconClass.Contains(filter.SearchTerm.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.Parameters.Contains(filter.SearchTerm.Trim()));
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

        public override void InsertOrUpdate(NavLink entity)
        {
            if (entity.NavLinkId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
