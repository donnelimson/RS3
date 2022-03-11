using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.EnumBaseModels;
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
        IPagedList<ItemMasterLookUpDTO> GetItemMasterLookUp(LookUpFilter filter);
        BrandItemMaster GetBrandItemMaster(int itemId, int brandId);
        BrandItemMaster GetBrandItemMasterById(int id);
        PriceListItemMaster GetPriceListItemMasterById(int id);
        List<LookUpDTO> GetAllUoM();
        ItemMasterInventoryUOM GetInventoryUomById(int id);
        List<LookUpDTO> GetAllItems();
        ItemMasterLookUpDTO GetItemMasterLookUpById(int itemMasterId);
        int? GetMaxQtyByItemMasterAndBrandId(int itemMasterId, int brandId);
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
        public PriceListItemMaster GetPriceListItemMasterById(int id)
        {
            return this.Context.Set<PriceListItemMaster>().Where(x => x.Id == id).FirstOrDefault();
        }
        public BrandItemMaster GetBrandItemMasterById(int id)
        {
            return this.Context.Set<BrandItemMaster>().Where(x => x.Id == id).FirstOrDefault();
        }
        public ItemMasterInventoryUOM GetInventoryUomById(int id)
        {
            return this.Context.Set<ItemMasterInventoryUOM>().Where(x => x.Id == id).FirstOrDefault();
        }
        public PriceListItemMaster GetPriceListItemMaster(int itemId, int priceListId)
        {
            return this.Context.Set<PriceListItemMaster>().Where(x => x.ItemMasterId == itemId && x.PriceListId == priceListId).FirstOrDefault();
        }
        public BrandItemMaster GetBrandItemMaster(int itemId, int brandId)
        {
            return this.Context.Set<BrandItemMaster>().Where(x => x.ItemMasterId == itemId && x.BrandId == brandId).FirstOrDefault();
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
            if (filter.Id == null)
            {
                var dataDTO = data.Select(a => new ItemMasterPriceListDTO
                {
                    Id = a.Id,
                    Category = "",
                    ItemCode = a.ItemCode,
                    ItemCost = 0,
                    LongDescription = a.LongDescription,
                    IsActive = a.IsActive ? "YES" : "NO"
                }).ToList();
                return dataDTO;
            }
            else
            {
                var dataDTO = data.SelectMany(r=>r.PriceListItemMasters).Where(x=>x.PriceListId==filter.Id).Select(a => new ItemMasterPriceListDTO
                {
                    Id = a.ItemMaster.Id,
                    Category = "",
                    ItemCode = a.ItemMaster.ItemCode,
                    ItemCost = a.ItemCost,
                    LongDescription = a.ItemMaster.LongDescription,
                    IsActive = a.ItemMaster.IsActive ? "YES" : "NO"
                }).ToList();
                return dataDTO;
            }
         
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
        public IPagedList<ItemMasterLookUpDTO> GetItemMasterLookUp(LookUpFilter filter)
        {
            var data = GetAll.Where(x=>!x.IsDeleted);
            if (!string.IsNullOrEmpty(filter.Searcher))
                data = data.Where(x => x.LongDescription.Contains(filter.Searcher) || x.ItemCode.Contains(filter.Searcher));

            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new ItemMasterLookUpDTO
            {
                Id= a.Id,
                ItemCode = a.ItemCode,
                LongDescription = a.LongDescription,
                IsActive = a.IsActive ? "YES":"NO",
                BasePrice = a.BasePrice,
                Brands = a.BrandItemMasters.Select(x=> new BrandDescAndIdDTO { Id =x.Id , Name = x.Brand.Name, ItemCost = x.ItemCost, IsDefault = x.IsDefault}).ToList(),
                PriceLists = a.PriceListItemMasters.Select(x=>new PriceListDescAndIdDTO { Id=x.Id, Name=x.PriceList.Name, ItemCost = x.ItemCost, IsDefault = x.IsDefault}).ToList()
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        public List<LookUpDTO> GetAllUoM()
        {
            return Context.Set<UnitOfMeasurement>().Select(a => new LookUpDTO { Description = a.Description, Id = a.Id, Name = a.Name }).ToList();
        }
        public List<LookUpDTO> GetAllItems()
        {
            return GetAll.Where(x=>!x.IsDeleted).Select(a => new LookUpDTO { Description = a.LongDescription, Id = a.Id, Name = a.ShortDescription }).ToList();
        }
        public ItemMasterLookUpDTO GetItemMasterLookUpById(int itemMasterId)
        {
            var data = GetAll.Where(x => x.Id == itemMasterId).FirstOrDefault();
            return new ItemMasterLookUpDTO
            {
                Id = data.Id,
                ItemCode = data.ItemCode,
                LongDescription = data.LongDescription,
                IsActive = data.IsActive ? "YES" : "NO",
                BasePrice = data.BasePrice,
                ItemCost = data.BasePrice,
                Brands = data.BrandItemMasters.Select(x => new BrandDescAndIdDTO { Id = x.Id, Name = x.Brand.Name, ItemCost = x.ItemCost, IsDefault = x.IsDefault }).ToList(),
                PriceLists = data.PriceListItemMasters.Select(x => new PriceListDescAndIdDTO { Id = x.Id, Name = x.PriceList.Name, ItemCost = x.ItemCost, IsDefault = x.IsDefault }).ToList()
            };
        }
        public int? GetMaxQtyByItemMasterAndBrandId(int itemMasterId, int brandId)
        {
            var data = Context.Set<ItemMasterInventoryUOM>().Where(x => x.ItemMasterId == itemMasterId && x.BrandId == brandId);
            if(data!=null)
               return data.Sum(x => x.PackagingUoMQuantity);
            return 0;
        }
    }
}
