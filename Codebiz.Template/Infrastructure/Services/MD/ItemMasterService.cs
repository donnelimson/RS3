using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.ERP.Model;
using ERP.Model.DataModel;
using ERP.Model.DTO;
using ERP.Model.Filter;
using Infrastructure.Repository.MD;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.MD
{
    public interface IItemMasterService
    {
        IPagedList<ItemMasterIndexDTO> Search(ItemMasterFilter filter);
        ItemMaster AddOrUpdate(ItemMasterViewModel model, int currentUserId);
        ItemMasterViewModel GetDetailsById(int id);
        IPagedList<ItemMasterLookUpDTO> GetItemMasterLookUp(LookUpFilter filter);
        List<LookUpDTO> GetAllUoM();
        List<LookUpDTO> GetAllItems();
        ItemMasterLookUpDTO GetItemMasterLookUpById(int itemMasterId);
        int? GetMaxQtyByItemMasterAndBrandId(int itemMasterId, int brandId);
    }
    public class ItemMasterService : IItemMasterService
    {
        private readonly IItemMasterRepository _itemMasterRepository;
        public ItemMasterService(IItemMasterRepository itemMasterRepository)
        {
            _itemMasterRepository = itemMasterRepository;
        }
        public IPagedList<ItemMasterIndexDTO> Search(ItemMasterFilter filter)
        {
            return _itemMasterRepository.Search(filter);
        }
        public IPagedList<ItemMasterLookUpDTO> GetItemMasterLookUp(LookUpFilter filter)
        {
            return _itemMasterRepository.GetItemMasterLookUp(filter);
        }
        public ItemMaster AddOrUpdate(ItemMasterViewModel model, int currentUserId)
        {
            var itemMaster = _itemMasterRepository.GetById(model.Id) ?? new ItemMaster();
            if (model.Id == 0)
            {
                itemMaster.CreatedOn = DateTime.Now;
                itemMaster.CreatedByAppUserId = currentUserId;
                itemMaster.ItemCode = model.ItemCode;
            }
            else
            {
                itemMaster.ModifiedOn = DateTime.Now;
                itemMaster.CreatedByAppUserId = currentUserId;
            }
            itemMaster.BasePrice = model.BasePrice;
            itemMaster.ShortDescription = model.ShortDescription;
            itemMaster.LongDescription = model.LongDescription;
            foreach (var item in model.Brands)
            {
                var itemBrand = _itemMasterRepository.GetBrandItemMasterById(item.Id) ?? new BrandItemMaster();
                itemBrand.ItemCost = item.ItemCost;
                itemBrand.BrandId = item.BrandId.Value;
                itemBrand.IsDefault = item.IsDefault;
                if (item.Id == 0)
                    itemMaster.BrandItemMasters.Add(itemBrand);
            }
            foreach (var item in model.PriceLists)
            {
                var itemPriceList = _itemMasterRepository.GetPriceListItemMasterById(item.Id) ?? new PriceListItemMaster();
                itemPriceList.ItemCost = item.ItemCost;
                itemPriceList.PriceListId = item.PriceListId.Value;
                itemPriceList.IsDefault = item.IsDefault;
                if (item.Id == 0)
                    itemMaster.PriceListItemMasters.Add(itemPriceList);
            }
            foreach (var item in model.InventoryItems)
            {
                var inventoryUoM = _itemMasterRepository.GetInventoryUomById(item.Id) ?? new ItemMasterInventoryUOM();
                inventoryUoM.BrandId = item.BrandId;
                inventoryUoM.PackagingUoMId = item.PackagingUoMId;
                inventoryUoM.PackagingUoMQuantity = item.PackagingUoMQuantity;
                inventoryUoM.BaseUoMId = item.BaseUoMId;
                inventoryUoM.BaseUoMQuantity = item.BaseUoMQuantity;
                if (item.Id == 0)
                    itemMaster.ItemMasterInventoryUOMs.Add(inventoryUoM);
            }

            _itemMasterRepository.InsertOrUpdate(itemMaster);
            return itemMaster;
        }
        public ItemMasterViewModel GetDetailsById(int id)
        {
            var itemMaster = _itemMasterRepository.GetById(id);
            if (itemMaster == null)
                return null;
            return new ItemMasterViewModel
            {
                Id = id,
                ItemCode = itemMaster.ItemCode,
                LongDescription = itemMaster.LongDescription,
                ShortDescription = itemMaster.ShortDescription,
                BasePrice = itemMaster.BasePrice,
                InventoryItems = itemMaster.ItemMasterInventoryUOMs.Select(a => new ItemMasterInventoryUoM
                {
                    Id = a.Id,
                    BaseUoMId = a.BaseUoMId,
                    BaseUoMQuantity = a.BaseUoMQuantity,
                    ItemCost = a.ItemCost,
                    PackagingUoMId = a.PackagingUoMId,
                    PackagingUoMQuantity = a.PackagingUoMQuantity,
                    CreatedOn = a.CreatedOn,
                    BrandId = a.BrandId
                }).ToList()
            };
        }
        public List<LookUpDTO> GetAllUoM()
        {
            return _itemMasterRepository.GetAllUoM();
        }
        public List<LookUpDTO> GetAllItems()
        {
            return _itemMasterRepository.GetAllItems();
        }
        public ItemMasterLookUpDTO GetItemMasterLookUpById(int itemMasterId)
        {
            return _itemMasterRepository.GetItemMasterLookUpById(itemMasterId);
        }
        public int? GetMaxQtyByItemMasterAndBrandId(int itemMasterId, int brandId)
        {
            return _itemMasterRepository.GetMaxQtyByItemMasterAndBrandId(itemMasterId, brandId);
        }
    }
}
