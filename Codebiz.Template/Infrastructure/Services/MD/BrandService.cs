using Codebiz.Domain.Common.Model.DTOs;
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
    public interface IBrandService
    {
        IPagedList<BrandIndexDTO> Search(BrandFilter filter);
        Brand AddOrUpdate(BrandAddOrUpdateViewModel model, int currentUserId);
        List<BrandPriceListForItemMasterDTO> GetBrandForItemMaster(int itemMasterId);
        List<LookUpDTO> GetAllBrands();
        BrandAddOrUpdateViewModel GetDetailsById(int id);
    }
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IItemMasterRepository _itemMasterRepository;
        public BrandService(IBrandRepository brandRepository,
            IItemMasterRepository itemMasterRepository)
        {
            _brandRepository = brandRepository;
            _itemMasterRepository = itemMasterRepository;
        }
        public Brand GetById(int id)
        {
            return _brandRepository.GetById(id);
        }
        public IPagedList<BrandIndexDTO> Search(BrandFilter filter)
        {
            return _brandRepository.Search(filter);
        }
        public List<BrandPriceListForItemMasterDTO> GetBrandForItemMaster(int itemMasterId)
        {
            return _brandRepository.GetBrandForItemMaster(itemMasterId);
        }
        public Brand AddOrUpdate(BrandAddOrUpdateViewModel model, int currentUserId)
        {
            var brand = _brandRepository.GetById(model.Id) ?? new Brand();
            if (model.Id == 0)
            {
                brand.CreatedByAppUserId = currentUserId;
                brand.CreatedOn = DateTime.Now;
                brand.Code = model.Code;
            }
            else
            {
                brand.ModifiedOn = DateTime.Now;
                brand.ModifiedByAppUserId = currentUserId;
            }
            brand.Name = model.Name;
            brand.IsActive = model.IsActive;
            foreach (var item in model.Items)
            {
                var data = _itemMasterRepository.GetBrandItemMaster(item.Id, model.Id);
                if (data == null)
                {
                    brand.BrandItemMasters.Add(new BrandItemMaster
                    {
                        ItemCost = item.ItemCost,
                        ItemMasterId = item.Id,
                    });
                }
                else
                {
                    data.ItemCost = item.ItemCost;
                }
            }
            _brandRepository.InsertOrUpdate(brand);
            return brand;
        }
        public List<LookUpDTO> GetAllBrands()
        {
            return _brandRepository.GetAllBrands();
        }
        public BrandAddOrUpdateViewModel GetDetailsById(int id)
        {
            var data = _brandRepository.GetById(id);
            return new BrandAddOrUpdateViewModel
            {
                Code = data.Code,
                Id = data.Id,
                IsActive = data.IsActive,
                Name = data.Name,
                Items = data.BrandItemMasters.Select(a=>new ItemMasterPriceListDTO
                {
                    IsActive = a.ItemMaster.IsActive ? "YES":"NO",
                    Id = a.Id,
                    ItemCode = a.ItemMaster.ItemCode,
                    ItemCost = a.ItemCost,
                    LongDescription =a.ItemMaster.LongDescription
                }).ToList()
            };
        }
    }
 
}
