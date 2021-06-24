using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class BillingUnit : ModelBase
    {
        [Key]
        public int BillingUnitId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }

        [ForeignKey("Office")]
        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }

        [ForeignKey("MeterReader")]
        public int MeterReaderId { get; set; }
        public virtual AppUser MeterReader { get; set; }

        public bool IsDeleted { get; set; }
    }
}
