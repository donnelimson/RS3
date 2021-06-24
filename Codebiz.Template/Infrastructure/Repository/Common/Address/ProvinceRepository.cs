using System;
using System.Data.Entity;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Domain.Context;
using System.Linq;
using System.Collections.Generic;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using Infrastructure.Utilities.QueryHelper;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;

namespace Infrastructure.Repository
{
    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(AppCommonContext context) : base(context)
        {
        }

        public bool CheckAbbreviationIfExists(string abbreviation, int? regionId, int id = 0)
        {
            return GetAll.Any(a => a.Abbreviation == abbreviation && a.RegionId == regionId && a.ProvinceId != id);

        }

        public bool CheckNameIfExists(string name, int? regionId, int id)
        {
            return GetAll.Any(a => a.Name == name && a.RegionId == regionId && a.ProvinceId != id);
        }

        public IEnumerable<Province> GetAllProvince()
        {
            return GetAll.Where(a => a.IsActive).AsEnumerable();
        }

        public Province GetById(int id)
        {
            return GetAll.FirstOrDefault(o => o.ProvinceId == id);
        }
        public override void InsertOrUpdate(Province entity)
        {
            if (entity.ProvinceId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public IPagedList<ProvinceDTO> SearchProvinces(ProvinceFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new ProvinceDTO()
            {
                ProvinceId = a.ProvinceId,
                Name = a.Name,
                Abbreviation = a.Abbreviation,
                Region = a.Region.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        public IEnumerable<Province> GetAllProvinceByRegionId(int regionId)
        {
            return GetAll.Where(x => x.RegionId == regionId).AsEnumerable();
        }

        public IEnumerable<ProvinceToExcel> GetDataForExportingToExcel(ProvinceFilter filter)
        {
            var data = GetData(filter);
            var dataDTO = data.Select(a => new ProvinceToExcel
            {
                Name = a.Name,
                Abbreviation = a.Abbreviation,
                Region = a.Region.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        private IQueryable<Province> GetData(ProvinceFilter filter)
        {
            var data = GetAll;

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                filter.SearchTerm = filter.SearchTerm.Trim();
                data = data.Where(a => (a.Name + ", " + a.Abbreviation + ", "
                                        + ((a.RegionId != null) ? a.Region.Name + "- " : "")
                                       + a.CreatedByAppUser.Employee.FirstName
                                        + a.CreatedByAppUser.Employee.LastName)
                    .Trim().Contains(filter.SearchTerm));
            }

            return data;
        }
        public int GetTarlacProvinceId()
        {
            return GetAll.FirstOrDefault(x => x.Name == "TARLAC").ProvinceId;
        }
    }
}
