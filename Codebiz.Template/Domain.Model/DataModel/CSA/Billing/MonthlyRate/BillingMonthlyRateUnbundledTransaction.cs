using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class BillingMonthlyRateUnbundledTransaction : ModelBase
    {
        [Key]
        public int BillingMonthlyRateUnbundledTransactionId { get; set; }

        [ForeignKey("BillingMonthlyRate")]
        public int BillingMonthlyRateId { get; set; }
        public virtual BillingMonthlyRate BillingMonthlyRate { get; set; }

        [ForeignKey("BillingUnbundledTransaction")]
        public int BillingUnbundledTransactionId { get; set; }
        public virtual BillingUnbundledTransaction BillingUnbundledTransaction { get; set; }

        public bool IsAmmount { get; set; }
        public float Rate { get; set; } = 0;
        public float Amount { get; set; } = 0;
        public int Ordinal { get; set; }
    }
}
