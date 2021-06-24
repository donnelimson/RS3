using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Repository;
using Codebiz.Domain.Common.Model;
using Domain.Context;
using System.Data.Entity;
using PagedList;
using Codebiz.Domain.Common.Model.Filter;
using Infrastructure.Utilities.QueryHelper;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.ERP.Model.Data.ERP;
using Codebiz.Domain.Common.Model.DTOs.Sitio;

namespace Infrastructure.Repository
{
    class SitioRepository : RepositoryBase<Sitio>, ISitioRepository
    {
        public SitioRepository(AppCommonContext context) : base(context)
        {

        }

        public Sitio GetAllById(int sitioId)
        {
            return GetAll.Where(a => a.SitioId == sitioId).FirstOrDefault();
        }

        public IEnumerable<Sitio> GetAllSitios()
        {
            return GetAll.AsEnumerable();
        }

        public Sitio GetById(int id)
        {
            return GetAll.Where(a => a.SitioId == id).FirstOrDefault();

        }

        public override void InsertOrUpdate(Sitio entity)
        {
            if (entity.SitioId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public IEnumerable<Sitio> GetAllByBarangayId(int id)
        {
            return GetAll.Where(a => a.BarangayId == id).AsEnumerable();
        }

        public bool IsSitioNameExists(string name, int? purokId, int id)
        {
            return GetAll.Any(a => !a.IsDeleted && a.Name == name && a.PurokId == purokId && a.SitioId != id);
        }


        public bool IsSitioCodeExists(string code, int id)
        {
            return GetAll.Any(a => !a.IsDeleted && a.Code == code && a.SitioId != id);
        }

        public IPagedList<SitioDTO> SearchSitio(SitioFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new SitioDTO
            {
                SitioId = a.SitioId,
                Code = a.Code,
                Name = a.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        public IEnumerable<SitioDTO> GetDataForExportingToExcel(SitioFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new SitioDTO
            {
                Code = a.Code,
                Name = a.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        private IQueryable<Sitio> GetData(SitioFilter filter)
        {
            var data = GetAll.Where(a => !a.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter.Code))
            {
                filter.Code = filter.Code.Trim();
                data = data.Where(a => a.Code.Trim().Contains(filter.Code));
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filter.Name = filter.Name.Trim();
                data = data.Where(a => a.Name.Trim().Contains(filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Purok))
            {
                filter.Purok = filter.Purok.Trim();
                data = data.Where(a => a.Purok.Name.Trim().Contains(filter.Purok));
            }

            if (filter.CreatedOnFrom != null && filter.CreatedOnTo != null)
            {
                data = data.Where(a => DbFunctions.TruncateTime(a.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(a.CreatedOn) <= filter.CreatedOnTo);
            }

            filter.FilteredRecordCount = data.Count();

            return data;
        }
    }
}
