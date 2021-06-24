using Codebiz.Domain.Common.Model.DataModel.Approval.ApprovalStage;
using Codebiz.Domain.Common.Model.EnumBaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ApprovalStage : ModelBase
    {
        public ApprovalStage()
        {
            this.ApprovalStageApprovers = new HashSet<ApprovalStageApprover>();
        }

        [Key]
        public int ApprovalStageId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public int RequiredApprovals { get; set; }

        [Required]
        public int RequiredRejections { get; set; }


        [ForeignKey("ApproverLabel")]
        public int ApproverLabelId { get; set; }
        public virtual ApproverLabel ApproverLabel { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ApprovalStageApprover> ApprovalStageApprovers { get; set; }
        public virtual ICollection<ApprovalTemplateStages> ApprovalTemplateStages { get; set; }
    }
}
