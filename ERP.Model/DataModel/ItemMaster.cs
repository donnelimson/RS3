using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.EnumBaseModels;
using ERP.Model.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model
{
    public class ItemMaster:ModelBase
    {
        public ItemMaster()
        {
            BrandItemMasters = new HashSet<BrandItemMaster>();
            PriceListItemMasters = new HashSet<PriceListItemMaster>();
            ItemMasterInventoryUOMs = new HashSet<ItemMasterInventoryUOM>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(13)]
        public string ItemCode { get; set; }
        [MaxLength(100)]
        public string LongDescription { get; set; }
        [MaxLength(50)]
        public string ShortDescription { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? BasePrice { get; set; } = 0;

        public virtual ICollection<BrandItemMaster> BrandItemMasters { get; set; }
        public virtual ICollection<PriceListItemMaster> PriceListItemMasters { get; set; }
        public virtual ICollection<ItemMasterInventoryUOM> ItemMasterInventoryUOMs { get; set; }

    }
    public class ItemMasterInventoryUOM : ModelBase
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ItemMaster")]
        public int ItemMasterId { get; set; }
        public virtual ItemMaster ItemMaster { get; set; }
        [ForeignKey("PackagingUoM")]
        public int? PackagingUoMId { get; set; }
        public int? PackagingUoMQuantity { get; set; }
        public virtual UnitOfMeasurement PackagingUoM { get; set; }
        [ForeignKey("BaseUoM")]
        public int? BaseUoMId { get; set; }
        public virtual UnitOfMeasurement BaseUoM { get; set; }
        public int? BaseUoMQuantity { get; set; }
        public decimal? ItemCost { get; set; }
        [ForeignKey("Brand")]
        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
