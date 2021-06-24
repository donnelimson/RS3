using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class SurchargeFilter : FilterBase
    {
        public int? ConsumerClass { get; set; }
        public int? YearOfDelinquency { get; set; }
        public double? RatePerMonth { get; set; }
    }
}
