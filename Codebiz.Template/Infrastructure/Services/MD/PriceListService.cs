using Codebiz.Domain.Common.Model.Filter;
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
    public interface IPriceListService
    {
        IPagedList<PriceListIndexDTO> Search(PriceListFilter filter);
        List<ItemMasterPriceListDTO> GetAllItemsForPriceList(LookUpFilter filter);
        PriceList AddOrUpdate(PriceListAddOrUpdateViewModel model, int currentUserId);
        List<BrandPriceListForItemMasterDTO> GetPriceListForItemMaster(int itemMasterId);
        PriceListAddOrUpdateViewModel GetDetailsById(int id);
    }
    public class PriceListService: IPriceListService
    {
        private readonly IPriceListRepository _priceListRepository;
        private readonly IItemMasterRepository _itemMasterRepository;
        public PriceListService(IPriceListRepository priceListRepository,
             IItemMasterRepository itemMasterRepository)
        {
            _priceListRepository = priceListRepository;
            _itemMasterRepository = itemMasterRepository;
        }
        public IPagedList<PriceListIndexDTO> Search(PriceListFilter filter)
        {
            return _priceListRepository.Search(filter);
        }
        public List<ItemMasterPriceListDTO> GetAllItemsForPriceList(LookUpFilter filter)
        {
            return _itemMasterRepository.GetAllItemsForPriceList(filter);
        }
        public PriceList AddOrUpdate(PriceListAddOrUpdateViewModel model, int currentUserId)
        {
            var priceList = _priceListRepository.GetById(model.Id) ?? new PriceList();
            if (model.Id == 0)
            {
                priceList.CreatedByAppUserId = currentUserId;
                priceList.CreatedOn = DateTime.Now;
                priceList.Code = model.Code;
            }
            else
            {
                priceList.ModifiedOn = DateTime.Now;
                priceList.ModifiedByAppUserId = currentUserId;
            }
            priceList.Name = model.Name;
            priceList.IsActive = model.IsActive;
            foreach (var item in model.Items)
            {
                var data = _itemMasterRepository.GetPriceListItemMaster(item.Id, model.Id);
                if (data == null)
                {
                    priceList.PriceListItemMasters.Add(new PriceListItemMaster { 
                    ItemCost =item.ItemCost,
                    ItemMasterId = item.Id,
                    });
                }
                else
                {
                    data.ItemCost = item.ItemCost;
                }
            }
            _priceListRepository.InsertOrUpdate(priceList);
            return priceList;
        }
        public List<BrandPriceListForItemMasterDTO> GetPriceListForItemMaster(int itemMasterId)
        {
            return _priceListRepository.GetPriceListForItemMaster(itemMasterId);
        }
        public PriceListAddOrUpdateViewModel GetDetailsById(int id)
        {
            var data = _priceListRepository.GetById(id);
            return new PriceListAddOrUpdateViewModel
            {
                Code = data.Code,
                IsActive = data.IsActive,
                Id = data.Id,
                Name = data.Name,
            };
        }
    }
}
