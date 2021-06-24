using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Domain.Context;
using Codebiz.Domain.ERP.Model.Data.CSA.EnumBaseModels;
using PagedList;
using Infrastructure.Utilities.QueryHelper;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.Route;
using Codebiz.Domain.Common.Model.DTOs.Barangay;

namespace Infrastructure.Repository
{
    public class BarangayRepository : RepositoryBase<Barangay>, IBarangayRepository
    {
        public BarangayRepository(AppCommonContext context) : base(context)
        {
        }
        public Barangay GetById(int id)
        {
            return Context.Set<Barangay>().FirstOrDefault(a => a.BarangayId == id);
        }
        public IEnumerable<Barangay> GetAllBarangay()
        {
            return Context.Set<Barangay>().AsEnumerable();
        }
        public IEnumerable<Barangay> GetAllByCityTownId(int CityTownId)
        {
            return Context.Set<Barangay>().Where(a => a.CityTownId == CityTownId).AsEnumerable();
        }
        public override void InsertOrUpdate(Barangay entity)
        {
            if (entity.BarangayId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public bool CheckIfBarangayCodeExist(string barangayCode, int barangayId, int cityTownId)
        {
            return GetAll.Any(a => (a.BarangayCode == barangayCode && a.BarangayId!=barangayId) && a.CityTownId == cityTownId);
        }
        public bool CheckIfBarangayNameExist(string barangayName, int barangayId, int cityTownId)
        {
            return GetAll.Any(a => (a.Name == barangayName && a.BarangayId != barangayId) && a.CityTownId == cityTownId);
        }
        public GetDetailsBarangayDTO GetDetails (int barangayId)
        {
            var result = GetAll.Where(x => x.BarangayId == barangayId).Select(a=>new GetDetailsBarangayDTO
            {
                BarangayId=a.BarangayId,
                BarangayCode=a.BarangayCode,
                Name=a.Name,
                Longitude=a.Longitude,
                Latitude=a.Latitude,
                CityTownId=a.CityTownId,
                ProvinceId=a.CityTown.ProvinceId
            }).FirstOrDefault();
            return result;
        }

        public IPagedList<BarangayIndexDTO> SearchBarangay(BarangayFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new BarangayIndexDTO
            {
                BarangayId = a.BarangayId,
                BarangayName = a.Name,
                CityTown = a.CityTown.Name,
            
                CreatedBy = a.CreatedByAppUser.Employee.LastName + ", " + a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.MiddleName,
                CreatedOn = a.CreatedOn,
                Province = a.CityTown.Province.Name,
                Status = a.IsActive,
                BarangayCode = a.BarangayCode
            });
            //Sort
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        public IEnumerable<BarangayToExcel> GetDataForExportingToExcel(BarangayFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new BarangayToExcel
            {
                BarangayName = a.Name,
                CityTown = a.CityTown.Name,
                CreatedBy = a.CreatedByAppUser.Employee.LastName + ", " + a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.MiddleName,
                CreatedOn = a.CreatedOn,
                Province = a.CityTown.Province.Name,
                Status = a.IsActive,
                BarangayCode = a.BarangayCode
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.AsEnumerable();
        }
        private IQueryable<Barangay> GetData(BarangayFilter filter)
        {
            var data = GetAll;

            if (!string.IsNullOrWhiteSpace(filter.Barangay))
            {
                filter.Barangay = filter.Barangay.Trim();
                data = data.Where(a => a.Name.Contains(filter.Barangay));
            }
            return data;
        }
    }

    public class DocumentTypeRepository : RepositoryBase<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(AppCommonContext context) : base(context)
        {

        }

        public override void InsertOrUpdate(DocumentType entity)
        {
            //if (entity.DocumentTypeId.Equals(0))
            //{
            //    this.Context.Entry(entity).State = EntityState.Added;
            //}
            //else
            //{
            //    this.Context.Entry(entity).State = EntityState.Modified;
            //}
        }
    }
}
