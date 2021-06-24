namespace Codebiz.Domain.ERP.Infrastructure.Inventory
{
    public interface IInventoryDataService
    {
        ItemGroupService ItemGroupService { get; }
        WarehouseService WarehouseService { get; }
        ItemMasterService ItemMasterService { get; }
        PriceListService PriceListService { get; }
    }
}