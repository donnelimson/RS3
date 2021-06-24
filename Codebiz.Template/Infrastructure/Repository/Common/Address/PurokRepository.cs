using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Domain.Context;
using Infrastructure.Utilities.QueryHelper;
using PagedList;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.DTOs.Purok;
using Codebiz.Domain.ERP.Model.Data.ERP;

namespace Infrastructure.Repository
{
    public class PurokRepository : RepositoryBase<Purok>, IPurokRepository
    {

        public PurokRepository(AppCommonContext context) : base(context)
        {

        }

        public override void InsertOrUpdate(Purok entity)
        {
            if (entity.PurokId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }

        #region Get Functions

        public IEnumerable<Purok> GetAllPuroks()
        {
            return GetAll.Where(a => !a.IsDeleted && a.IsActive);
        }
        public Purok GetById(int id)
        {
            return GetAll.Where(a => a.PurokId == id && !a.IsDeleted).FirstOrDefault();
        }

        public IEnumerable<Purok> GetAllByBarangayId(int BarangayId)
        {
            return GetAll.Where(a => a.BarangayId == BarangayId && !a.IsDeleted && a.IsActive).AsEnumerable();
        }

        public Purok GetAllById(int? purokId)
        {
            return GetAll.Where(a => a.PurokId == purokId && !a.IsDeleted && a.IsActive).FirstOrDefault();
        }

        #endregion

        #region Search

        public IPagedList<PurokDTO> SearchPurok(PurokFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();

            var dataDTO = data.Select(a => new PurokDTO
            {
                PurokId = a.PurokId,
                Code = a.Code,
                Name = a.Name,
                Barangay = a.Barangay.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }

        #endregion

        #region Get Data For Export in Excel

        public IEnumerable<PurokDTO> GetDataForExportingToExcel(PurokFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.Select(a => new PurokDTO
            {
                Code = a.Code,
                Name = a.Name,
                Barangay = a.Barangay.Name,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        #endregion

        #region Get Data

        private IQueryable<Purok> GetData(PurokFilter filter)
        {
            var data = GetAll.Where(a => !a.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter.Code))
            {
                filter.Code = filter.Code.Trim();
                data = data.Where(a => a.Code.Contains(filter.Code));
            }

            if (filter.Name != null)
            {
                filter.Name = filter.Name.Trim();
                data = data.Where(a => a.Name.Contains(filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Barangay))
            {
                filter.Barangay = filter.Barangay.Trim();
                data = data.Where(a => a.Barangay.Name.Contains(filter.Barangay));
            }

            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                filter.CreatedBy = filter.CreatedBy.Trim();
                data = data.Where(a => (a.CreatedByAppUser.Employee.FirstName
                                        + a.CreatedByAppUser.Employee.LastName)
                    .Trim().Contains(filter.CreatedBy));
            }

            if (filter.CreatedOnFrom != null && filter.CreatedOnTo != null)
            {
                data = data.Where(a => DbFunctions.TruncateTime(a.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(a.CreatedOn) <= filter.CreatedOnTo);
            }

            return data;
        }

        #endregion

        #region Validations



        public bool IsNameExists(string name,int? barangayId, int id)
        {
            return GetAll.Any(a => !a.IsDeleted && a.Name == name && a.BarangayId == barangayId && a.PurokId != id);
        }

        public bool IsCodeExists(string code, int id)
        {
            return GetAll.Any(a => !a.IsDeleted && a.Code == code && a.PurokId != id);

        }

        #endregion
    }
}

