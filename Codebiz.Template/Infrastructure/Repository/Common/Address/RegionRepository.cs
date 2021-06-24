using System.Collections.Generic;
using System.Data.Entity;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using Domain.Context;
using PagedList;
using System.Linq;
using Infrastructure.Utilities.QueryHelper;

namespace Infrastructure.Repository
{
    public class RegionRepository : RepositoryBase<Region>, IRegionRepository
    {
        public RegionRepository(AppCommonContext context) : base(context)
        {
        }

        #region Validations

        public bool CheckNameIfExists(string name, int id)
        {
            return GetAll.Any(a => a.Name == name && a.RegionId != id);
        }

        public bool CheckAbbreviationIfExists(string abbreviation, int id)
        {
            return GetAll.Any(a => a.Abbreviation == abbreviation && a.RegionId != id);
        }

        public bool CheckRegionIfInUse(int id)
        {
            return Context.Set<Province>().Any(a => a.RegionId == id);
        }

        #endregion

        #region Get Functions

        public IEnumerable<Region> GetAllRegion()
        {
            return GetAll;
        }

        public Region GetById(int id)
        {
            return GetAll.Where(a => a.RegionId == id).FirstOrDefault();

        }

        #endregion

        #region Insert Or Update

        public override void InsertOrUpdate(Region entity)
        {
            if (entity.RegionId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }

        #endregion

        #region Search

        public IPagedList<RegionDTO> SearchRegions(RegionFilter filter)
        {
            var data = GetData(filter);

            filter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new RegionDTO()
            {
                RegionId = a.RegionId,
                Name = a.Name,
                Abbreviation = a.Abbreviation,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);


            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        #endregion

        #region Get Data / For Exporting to Excel

        public IEnumerable<RegionDTO> GetDataForExportingToExcel(RegionFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new RegionDTO
            {
                Name = a.Name,
                Abbreviation = a.Abbreviation,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        private IQueryable<Region> GetData(RegionFilter filter)
        {
            var data = GetAll;

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                filter.SearchTerm = filter.SearchTerm.Trim();
                data = data.Where(a => (a.Name + "- " + a.Abbreviation + " " + a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName)
                                .Trim().Contains(filter.SearchTerm));
            }

            return data;
        }

        #endregion  
    }
}
