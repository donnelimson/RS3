using Codebiz.Domain.Common.Model.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Filter
{
    public class ItemMasterFilter:FilterBase
    {
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ItemCode { get; set; }
    }
    public class ItemMasterPriceListFilter : FilterBase
    {
        public string LongDescription { get; set; }
    }
}
