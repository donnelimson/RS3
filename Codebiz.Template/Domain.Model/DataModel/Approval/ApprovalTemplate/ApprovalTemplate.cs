using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class ApprovalTemplate : ModelBase
    {
        public ApprovalTemplate()
        {
            ApprovalTemplateOriginators = new HashSet<ApprovalTemplateOriginator>();
            ApprovalTemplateStages = new HashSet<ApprovalTemplateStages>();
            ApprovalTemplateTransactions = new HashSet<ApprovalTemplateTransaction>();
        }

        [Key]
        public int ApprovalTemplateId { get; set; }
        [DisplayName("NAME")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<ApprovalTemplateOriginator> ApprovalTemplateOriginators { get; set; }
        public virtual ICollection<ApprovalTemplateStages> ApprovalTemplateStages { get; set; }
        public virtual ICollection<ApprovalTemplateTransaction> ApprovalTemplateTransactions { get; set; }
    }
}
