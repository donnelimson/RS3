namespace Codebiz.Domain.Common.Model.Filter.FAMMS.Inventory.ItemMasterData
{
    public class ItemMasterDataFilter: FilterBase
    {
        public string ItemNo { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public string ItemTypeId { get; set; }
        public int? ItemGroupId { get; set; }
        public bool IsInventoryItem { get; set; }
        public bool IsSalesItem { get; set; }
        public bool IsPurchaseItem { get; set; }
        public bool IsFixedAssets { get; set; }

    }
}
