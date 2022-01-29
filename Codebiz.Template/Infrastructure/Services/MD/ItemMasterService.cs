using Codebiz.Domain.ERP.Model;
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
    }
    public class ItemMasterService: IItemMasterService
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
            itemMaster.ShortDescription = model.ShortDescription;
            itemMaster.LongDescription = model.LongDescription;
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
            };
        }
    }
}
