using Codebiz.Domain.Common.Model.EnumBaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class BillingMonthlyRate : ModelBase
    {
        public BillingMonthlyRate()
        {
            MonthlyRateUnbundledTransactions = new HashSet<BillingMonthlyRateUnbundledTransaction>();
        }

        public int BillingMonthlyRateId { get; set; }
        public string Description { get; set; }
        public string BillingPeriod { get; set; }

        [ForeignKey("ConsumerClass")]
        public int ConsumerClassId { get; set; }
        public virtual ConsumerClass ConsumerClass { get; set; }

        public virtual ICollection<BillingMonthlyRateUnbundledTransaction> MonthlyRateUnbundledTransactions { get; set; }
    }
}
