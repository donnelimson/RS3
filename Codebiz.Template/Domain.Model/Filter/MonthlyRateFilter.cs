using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
   public class MonthlyRateFilter: FilterBase
    {
        public int MonthlyRateId { get; set; }
        public string BillingPeriod { get; set; }
        public int? ConsumerClassId { get; set; }
    }
}
