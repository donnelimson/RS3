using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class BillingUnbundledTransactionFilter : FilterBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int? CategoryId { get; set; }
    }
}
