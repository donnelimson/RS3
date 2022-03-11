using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.DTO
{
    #region DTOs
    public class ItemMasterIndexDTO
    {
        [DisplayName("ITEM CODE")]
        public string ItemCode { get; set; }
        [DisplayName("LONG DESCRIPTION")]
        public string LongDescription { get; set; }
        [DisplayName("SHORT DESCRIPTION")]
        public string ShortDescription { get; set; }
        [DisplayName("CATEGORY")]
        public string Category { get; set; }
        [DisplayName("CREATED ON")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        public int Id { get; set; }
    }
    public class ItemMasterLookUpDTO
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string LongDescription { get; set; }
        public string IsActive { get; set; }
        public decimal? ItemCost { get; set; } = 0;
        public List<PriceListDescAndIdDTO> PriceLists { get; set; } = new List<PriceListDescAndIdDTO>();
        public List<BrandDescAndIdDTO> Brands { get; set; } = new List<BrandDescAndIdDTO>();
        public decimal? BasePrice { get; set; }= 0;
        public int? MaxQty { get; set; } = 0;

    }
    public class PriceListDescAndIdDTO : DescAndIdDTO
    {

    }
    public class BrandDescAndIdDTO : DescAndIdDTO
    {

    }
    public class DescAndIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? ItemCost { get; set; } = 0;
        public bool IsDefault { get; set; }
    }
    public class ItemMasterPriceListDTO
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string LongDescription { get; set; }
        public decimal? ItemCost { get; set; }
        public string Category { get; set; }
        public string IsActive { get; set; }

    }
    public class ItemMasterInventoryUoM
    {
        public int Id { get; set; }
        public int? PackagingUoMId { get; set; }
        public int? BaseUoMId { get; set; }
        public int? BaseUoMQuantity { get; set; }
        public int? PackagingUoMQuantity { get; set; }
        public decimal? ItemCost { get; set; }
        public int? BrandId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
    #endregion
    #region ViewModel
    public class ItemMasterViewModel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public decimal? BasePrice { get; set; } = 0;
        public List<BrandPriceListForItemMasterDTO> PriceLists { get; set; } = new List<BrandPriceListForItemMasterDTO>();
        public List<BrandPriceListForItemMasterDTO> Brands { get; set; } = new List<BrandPriceListForItemMasterDTO>();
        public List<ItemMasterInventoryUoM> InventoryItems { get; set; } = new List<ItemMasterInventoryUoM>();
    }
    #endregion

}
