using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel.CSA.Billing.UnbundledTransaction
{
    public class BillingUnbundledTransactionForVatDetails
    {
        [Key]
        public int BillingUnbundledTransactionDetailId { get; set; }

        [ForeignKey("BillingUnbundledTransaction")]
        public int BillingUnbundledTransactionId { get; set; }
        public virtual BillingUnbundledTransaction BillingUnbundledTransaction { get; set; }

        [ForeignKey("BillingUnbundledTransactionForVat")]
        public int? BillingUnbundledTransactionForVatId { get; set; }
        public virtual BillingUnbundledTransaction BillingUnbundledTransactionForVat { get; set; }

    }
    public class BillingUnbundledTransactionForDiscountDetails
    {
        [Key]
        public int BillingUnbundledTransactionDetailId { get; set; }

        [ForeignKey("BillingUnbundledTransaction")]
        public int BillingUnbundledTransactionId { get; set; }
        public virtual BillingUnbundledTransaction BillingUnbundledTransaction { get; set; }

        [ForeignKey("BillingUnbundledTransactionForDiscount")]
        public int? BillingUnbundledTransactionForDiscountId { get; set; }
        public virtual BillingUnbundledTransaction BillingUnbundledTransactionForDiscount { get; set; }

    }
}
