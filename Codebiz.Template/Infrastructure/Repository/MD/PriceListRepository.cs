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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.MD
{
    public interface IPriceListRepository:IERepositoryBase<PriceList, int>
    {
        PriceList GetById(int id);
        IPagedList<PriceListIndexDTO> Search(PriceListFilter filter);
        List<BrandPriceListForItemMasterDTO> GetPriceListForItemMaster(int itemMasterId);
    }
    public class PriceListRepository:ERepositoryBase<PriceList,int>,IPriceListRepository,IDisposable
    {
        private readonly ERPDataContext _context;
        public PriceListRepository(ERPDataContext context) : base(context)
        {
            _context = context;
        }
        public override void InsertOrUpdate(PriceList entity)
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
        public PriceList GetById(int id)
        {
            return GetAll.Where(x => x.Id == id).FirstOrDefault();
        }
        public IQueryable<PriceList> GetData(PriceListFilter filter)
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
        public IPagedList<PriceListIndexDTO> Search(PriceListFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new PriceListIndexDTO
            {
                CreatedBy = a.CreatedByAppUser.LastName + " "
                + a.CreatedByAppUser.FirstName + " "
                + (a.CreatedByAppUser.MiddleName == null ? "" : a.CreatedByAppUser.MiddleName + " "),
                CreatedOn = a.CreatedOn,
                Id = a.Id,
                Code = a.Code,
                Name = a.Name,
                Status = a.IsActive ? "YES":"NO"
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        public List<BrandPriceListForItemMasterDTO> GetPriceListForItemMaster(int itemMasterId)
        {
            var newData = itemMasterId == 0 ? true : false;
            if (!newData)
            {
                return GetAll.Where(x => !x.IsDeleted).SelectMany(r => r.PriceListItemMasters).Where(r => r.ItemMasterId == itemMasterId).Select(a => new BrandPriceListForItemMasterDTO
                {
                    Code = a.PriceList.Code,
                    Id = a.Id,
                    ItemCost = a.ItemCost,
                    Name = a.PriceList.Name,
                    PriceListId = a.PriceListId
                }).ToList();
            }
            else
            {
                return GetAll.Where(x => !x.IsDeleted).Select(a => new BrandPriceListForItemMasterDTO
                {
                    Code = a.Code,
                    Id = 0,
                    ItemCost = 0,
                    Name = a.Name,
                    PriceListId = a.Id
                }).ToList();
            }

        }
    }
}
