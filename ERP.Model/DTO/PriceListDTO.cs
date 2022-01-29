using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.DTO
{
    #region DTOs
    public class PriceListIndexDTO
    {
        [DisplayName("NAME")]
        public string Name { get; set; }
        [DisplayName("CODE")]
        public string Code { get; set; }
        [DisplayName("STATUS")]
        public string Status { get; set; }
        [DisplayName("CREATED ON")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        public int Id { get; set; }
    }
    public class PriceListForItemMasterDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? ItemCost { get; set; } = 0;
    }
    #endregion
    #region ViewModels
    public class PriceListAddOrUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public List<ItemMasterPriceListDTO> Items { get; set; } = new List<ItemMasterPriceListDTO>();
    }
    #endregion
}
