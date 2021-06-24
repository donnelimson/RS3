using Codebiz.Domain.Common.Model.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class BillingUnbundledTransactionCategory : ModelBase
    {
        [Key]
        public int BillingUnbundledTransactionCategoryId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public bool IsBill { get; set; }

        public bool IsDeleted { get; set; }

        public int Ordinal { get; set; }

        public virtual ICollection<BillingUnbundledTransaction> BillingUnbundledTransactions { get; set; }
    }
}
