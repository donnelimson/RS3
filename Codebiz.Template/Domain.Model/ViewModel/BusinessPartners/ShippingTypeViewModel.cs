using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel
{
    public class ShippingTypeViewModel
    {
        public int ShippingTypesId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
    }
}
