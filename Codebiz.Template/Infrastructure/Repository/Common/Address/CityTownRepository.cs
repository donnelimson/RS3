using System.Data.Entity;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Domain.Context;
using System.Linq;
using System.Collections.Generic;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using Infrastructure.Utilities.QueryHelper;
using Codebiz.Domain.Common.Model.DTOs.CityTown;

namespace Infrastructure.Repository
{
    public class CityTownRepository : RepositoryBase<CityTown>, ICityTownRepository
    {
        public CityTownRepository(AppCommonContext context) : base(context)
        {
        }
        public CityTown GetById(int id)
        {
            return Context.Set<CityTown>().FirstOrDefault(o => o.CityTownId == id);
        }
        public bool CheckNameIfExists(string name, int provinceId, int id = 0)
        {
            return Context.Set<CityTown>().Where(o => o.CityTownId != id)
                                        .Where(o => o.ProvinceId == provinceId)
                                        .Any(o => o.Name.ToUpper() == name.ToUpper());
        }
        public IEnumerable<CityTown> GetAllCityTown()
        {
            return Context.Set<CityTown>().AsEnumerable();
        }
        public IEnumerable<CityTown> GetByProvinceId(int provinceId)
        {
            return Context.Set<CityTown>().Where(a => a.ProvinceId == provinceId).AsEnumerable();
        }
        public override void InsertOrUpdate(CityTown entity)
        {
            if (entity.CityTownId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public bool CheckIfCityTownCodeExist(string cityTownCode, int cityTownId)
        {
            return GetAll.Any(a => a.CityTownCode.ToUpper() == cityTownCode.ToUpper() && a.CityTownId != cityTownId);
        }
        public GetDetatilsCityTownDTO GetDetails(int cityTownId)
        {
            var result = GetAll.Where(x => x.CityTownId == cityTownId).Select(a => new GetDetatilsCityTownDTO
            {
                RegionId = a.Province.RegionId,
                CityTownId = a.CityTownId,
                CityTownCode = a.CityTownCode,
                Name = a.Name,
                Longitude = a.Longitude,
                Latitude = a.Latitude,
                ProvinceId = a.ProvinceId,
                ZipCode=a.ZipCode,
                abbv=a.Abbreviation
            }).FirstOrDefault();
            return result;
        }
        public IPagedList<CityTownIndexDTO> SearchCityTown(CityTownFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new CityTownIndexDTO
            {
                CityTownID = a.CityTownId,
                CityTownCode = a.CityTownCode,
                CityTown = a.Name,
                FullName = a.CreatedByAppUser.Employee.LastName + " " + a.CreatedByAppUser.Employee.FirstName,
                CreatedOn = a.CreatedOn,
                Province = a.Province.Name,
                Status = a.IsActive
            });
            //Sort
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        public IEnumerable<CityTownIndexDTO> GetDataForExportingToExcel(CityTownFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new CityTownIndexDTO
            {
                CityTownCode = a.CityTownCode,
                CityTown = a.Name,
                FullName = a.CreatedByAppUser.Employee.LastName + " " + a.CreatedByAppUser.Employee.FirstName,
                CreatedOn = a.CreatedOn,
                Province = a.Province.Name,
                Status = a.IsActive
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.AsEnumerable();
        }
        private IQueryable<CityTown> GetData(CityTownFilter filter)
        {
            var data = GetAll;

            if (!string.IsNullOrWhiteSpace(filter.CityTown))
            {
                filter.CityTown = filter.CityTown.Trim();
                data = data.Where(a => a.Name.Contains(filter.CityTown));
            }
            if (!string.IsNullOrWhiteSpace(filter.Province))
            {
                filter.Province = filter.Province.Trim();
                data = data.Where(a => a.Province.Name.Contains(filter.Province));
            }
            return data;
        }
    }
}
