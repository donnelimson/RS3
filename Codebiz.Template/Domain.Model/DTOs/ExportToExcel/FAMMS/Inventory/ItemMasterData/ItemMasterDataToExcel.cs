using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel.FAMMS.Inventory.ItemMasterData
{
   public class ItemMasterDataToExcel
    {
        [DisplayName("ITEM NO")]
        public string ItemNo { get; set; }
        [DisplayName("BAR CODE")]
        public string BarCode { get; set; }
        [DisplayName("DESCRIPTION")]
        public string Description { get; set; }
        [DisplayName("ITEM TYPE")]
        public string ItemType { get; set; }
        [DisplayName("ITEM GROUP")]
        public string ItemGroup { get; set; }
        [DisplayName("PRICE LIST")]
        public string PriceList { get; set; }
        [DisplayName("UNIT PRICE")]
        public Decimal UnitPrice { get; set; }
        [DisplayName("INVENTORY")]
        public bool IsInventoryItem { get; set; }
        [DisplayName("SALES")]
        public bool IsSalesItem { get; set; }
        [DisplayName("PURCHASE")]
        public bool IsPurchaseItem { get; set; }
        [DisplayName("FIXED ASSETS")]
        public bool IsFixedAssets { get; set; }
    }
}
