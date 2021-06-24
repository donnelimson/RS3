using Codebiz.Domain.ERP.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Inventory
{
    public class InventoryDataService : IDisposable, IInventoryDataService
    {

        private Lazy<ItemGroupService> _lazy_ItemGroupService;
        private Lazy<WarehouseService> _lazy_WarehouseService;
        private Lazy<ItemMasterService> _lazy_ItemMasterService;
        private Lazy<PriceListService> _lazy_PriceListService;

        public ItemGroupService ItemGroupService { get => _lazy_ItemGroupService.Value; }
        public WarehouseService WarehouseService { get => _lazy_WarehouseService.Value; }
        public ItemMasterService ItemMasterService { get => _lazy_ItemMasterService.Value; }
        public PriceListService PriceListService { get => _lazy_PriceListService.Value; }

        private ItemDataContext ctx;

        public InventoryDataService()
        {
            ctx = new ItemDataContext();

            _lazy_ItemGroupService = new Lazy<ItemGroupService>(() => new ItemGroupService(ctx));

            _lazy_WarehouseService = new Lazy<WarehouseService>(() => new WarehouseService(ctx));

            _lazy_ItemMasterService = new Lazy<ItemMasterService>(() => new ItemMasterService(ctx));

            _lazy_PriceListService = new Lazy<PriceListService>(() => new PriceListService(ctx));

        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
