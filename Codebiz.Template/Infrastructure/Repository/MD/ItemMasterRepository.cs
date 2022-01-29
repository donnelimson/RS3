using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model;
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
    public interface IItemMasterRepository:IERepositoryBase<ItemMaster,int>
    {
        IPagedList<ItemMasterIndexDTO> Search(ItemMasterFilter filter);
        ItemMaster GetById(int id);
        List<ItemMasterPriceListDTO> GetAllItemsForPriceList(LookUpFilter filter);
        PriceListItemMaster GetPriceListItemMaster(int itemId, int priceListId);
    }
    public class ItemMasterRepository:ERepositoryBase<ItemMaster,int>,IItemMasterRepository
    {
        public ItemMasterRepository(ERPDataContext context) : base(context)
        {
        }
        public override void InsertOrUpdate(ItemMaster entity)
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
        public PriceListItemMaster GetPriceListItemMaster(int itemId, int priceListId)
        {
            return this.Context.Set<PriceListItemMaster>().Where(x => x.ItemMasterId == itemId && x.PriceListId == priceListId).FirstOrDefault();
        }
        public ItemMaster GetById(int id)
        {
            return GetAll.Where(x => x.Id == id).FirstOrDefault();
        }
        public List<ItemMasterPriceListDTO> GetAllItemsForPriceList(LookUpFilter filter)
        {
            var data = GetAll.Where(x => !x.IsDeleted);
            if (!string.IsNullOrEmpty(filter.Searcher))
                data = data.Where(x => x.LongDescription.Contains(filter.Searcher) || x.ItemCode.Contains(filter.Searcher));
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new ItemMasterPriceListDTO
            {
                Id = a.Id,
                Category ="",
                ItemCode = a.ItemCode,
                ItemCost = 0,
                LongDescription = a.LongDescription,
                IsActive = a.IsActive ? "YES":"NO"
            }).ToList();
            return dataDTO;
        }
        public IQueryable<ItemMaster> GetData(ItemMasterFilter filter)
        {
            var data = GetAll.Where(x=> !x.IsDeleted);
            if (!string.IsNullOrEmpty(filter.LongDescription))
                data = data.Where(x => x.LongDescription.Contains(filter.LongDescription));
            if (!string.IsNullOrEmpty(filter.ShortDescription))
                data = data.Where(x => x.ShortDescription.Contains(filter.ShortDescription));
            if (!string.IsNullOrEmpty(filter.ItemCode))
                data = data.Where(x => x.ItemCode.Contains(filter.ItemCode));
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
        public IPagedList<ItemMasterIndexDTO> Search(ItemMasterFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new ItemMasterIndexDTO
            {
                Category = "",
                CreatedBy = a.CreatedByAppUser.LastName + " "
                + a.CreatedByAppUser.FirstName + " "
                + (a.CreatedByAppUser.MiddleName == null ? "" : a.CreatedByAppUser.MiddleName + " "),
                CreatedOn = a.CreatedOn,
                Id = a.Id,
                ItemCode = a.ItemCode,
                LongDescription = a.LongDescription,
                ShortDescription = a.ShortDescription
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
    }
}
