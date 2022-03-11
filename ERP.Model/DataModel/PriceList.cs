using Codebiz.Domain.Common.Model;
using Codebiz.Domain.ERP.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.DataModel
{
    public class PriceList:ModelBase
    {
        public PriceList()
        {
            this.PriceListItemMasters = new HashSet<PriceListItemMaster>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<PriceListItemMaster> PriceListItemMasters { get; set; }
    }
    public class PriceListItemMaster
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ItemMaster")] 
        public int ItemMasterId { get; set; }
        public virtual ItemMaster ItemMaster { get; set; }
        [ForeignKey("PriceList")]
        public int PriceListId { get; set; }
        public virtual PriceList PriceList { get; set; }
        public decimal? ItemCost { get; set; } = 0;
        public bool IsDefault { get; set; }
    }
    public class BrandItemMaster
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ItemMaster")]
        public int ItemMasterId { get; set; }
        public virtual ItemMaster ItemMaster { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public decimal? ItemCost { get; set; } = 0;
        public bool IsDefault { get; set; }
    }
}
