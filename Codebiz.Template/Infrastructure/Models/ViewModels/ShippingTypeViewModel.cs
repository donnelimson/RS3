using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.ViewModels
{
    public class ShippingTypeViewModel
    {
        public int ShippingTypeId { get; set; }
        public List<ShippingCodeNameViewModel> ShippingTypes { get; set; }
    }
    public class ShippingCodeNameViewModel
    {
        public int ShippingTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
