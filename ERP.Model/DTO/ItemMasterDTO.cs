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
    public class ItemMasterPriceListDTO
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string LongDescription { get; set; }
        public decimal ItemCost { get; set; }
        public string Category { get; set; }
        public string IsActive { get; set; }
    }
    #endregion
    #region ViewModel
    public class ItemMasterViewModel
    {
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public List<PriceListForItemMasterDTO> ItemPriceLists { get; set; } = new List<PriceListForItemMasterDTO>();
    }
    #endregion

}
