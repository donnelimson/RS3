using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.ERP.Context;
using ERP.Model.DataModel;
using ERP.Model.DTO;
using ERP.Model.Filter;
using Infrastructure.Utilities.QueryHelper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.MD
{
    public interface IBrandRepository : IERepositoryBase<Brand, int>
    {
        IPagedList<BrandIndexDTO> Search(BrandFilter filter);
        Brand GetById(int id);
        List<BrandPriceListForItemMasterDTO> GetBrandForItemMaster(int itemMasterId);
        List<LookUpDTO> GetAllBrands();
    }
    public class BrandRepository : ERepositoryBase<Brand,int>,IDisposable, IBrandRepository
    {
        public BrandRepository(ERPDataContext context) : base(context)
        {
        }
        public override void InsertOrUpdate(Brand entity)
        {
            if (entity.Id.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public Brand GetById(int id)
        {
            return GetAll.Where(x => x.Id == id).FirstOrDefault();
        }
        public IPagedList<BrandIndexDTO> Search(BrandFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new BrandIndexDTO
            {
                CreatedBy = a.CreatedByAppUser.LastName + " "
                + a.CreatedByAppUser.FirstName + " "
                + (a.CreatedByAppUser.MiddleName == null ? "" : a.CreatedByAppUser.MiddleName + " "),
                CreatedOn = a.CreatedOn,
                Id = a.Id,
                Code = a.Code,
                Name = a.Name,
                Status = a.IsActive ? "YES" : "NO"
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        public IQueryable<Brand> GetData(BrandFilter filter)
        {
            var data = GetAll;
            if (!string.IsNullOrEmpty(filter.Name))
                data = data.Where(x => x.Name.Contains(filter.Name));
            if (!string.IsNullOrEmpty(filter.Code))
                data = data.Where(x => x.Code.Contains(filter.Code));
            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                filter.CreatedBy = filter.CreatedBy.Replace(",", "").Trim();
                data = data.Where(a => (a.CreatedByAppUser.LastName + " "
                + a.CreatedByAppUser.FirstName + " "
                + (a.CreatedByAppUser.MiddleName == null ? "" : a.CreatedByAppUser.MiddleName + " "))
                .Trim().Contains(filter.CreatedBy));
            }
            if (filter.CreatedOnFrom != null)
                data = data.Where(x => DbFunctions.TruncateTime(x.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(x.CreatedOn) <= filter.CreatedOnTo);
            return data;
        }
        public List<BrandPriceListForItemMasterDTO> GetBrandForItemMaster(int itemMasterId)
        {
            var data = Context.Set<BrandItemMaster>();
            var newData = itemMasterId == 0 ? true : false;
            var dataDTO = data.Where(x=>x.ItemMasterId == itemMasterId).Select(a => new BrandPriceListForItemMasterDTO
            {
                Code = a.Brand.Code,
                Id = a.Id,
                ItemCost = newData ? 0 : a.ItemCost,
                Name = a.Brand.Name,
                IsDefault = a.IsDefault,
                BrandId = a.BrandId
            }).ToList();
            return dataDTO;
        }
        public List<LookUpDTO> GetAllBrands()
        {
            return GetAll.Select(a => new LookUpDTO { Description = a.Name, Id = a.Id, Name = a.Name }).ToList();
        }
    }
}
