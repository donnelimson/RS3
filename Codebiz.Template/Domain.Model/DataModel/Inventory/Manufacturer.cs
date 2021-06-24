using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model.DataModel.Inventory
{
    public class Manufacturer : ModelBase
    {
        [Key]
        public int ManufacturerId { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }
        
        [MaxLength(300)]
        [DisplayName("NAME")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
      
    }
}
