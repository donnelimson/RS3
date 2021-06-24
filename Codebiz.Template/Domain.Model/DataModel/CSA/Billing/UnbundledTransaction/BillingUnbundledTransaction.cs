using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Codebiz.Domain.Common.Model.DataModel.CSA.Billing.UnbundledTransaction;
using System.Collections.Generic;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model
{
    public class BillingUnbundledTransaction : ModelBase
    {
        public BillingUnbundledTransaction()
        {
            this.BillingUnbundledTransactionDiscountDetails = new HashSet<BillingUnbundledTransactionForDiscountDetails>();
            this.BillingUnbundledTransactionVatDetails = new HashSet<BillingUnbundledTransactionForVatDetails>();
        }
        
        [Key]
        public int BillingUnbundledTransactionId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [MaxLength(300)]
        [DisplayName("NAME")]
        public string DisplayedName { get; set; }

        [ForeignKey("BillingUnbundledTransactionCategory")]
        public int BillingUnbundledTransactionCategoryId { get; set; }
        public virtual BillingUnbundledTransactionCategory BillingUnbundledTransactionCategory { get; set; }

        public string IncludedTo { get; set; }
        public string Classification { get; set; }
        public string ComputedBy { get; set; }

        public bool ZeroWhenThereIsDemand { get; set; }
        public bool ZeroWhenThereIsNoDemand { get; set; }
        public bool IsICERA { get; set; }
        public bool IsGRAM { get; set; }
        public bool IsLifelineSubsidy { get; set; }
        public bool IsSeniorCitizenDiscount { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<BillingUnbundledTransactionForDiscountDetails> BillingUnbundledTransactionDiscountDetails { get; set; }
        public virtual ICollection<BillingUnbundledTransactionForVatDetails> BillingUnbundledTransactionVatDetails { get; set; }

    }
}
