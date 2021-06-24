using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.EnumBaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.ERP.Model
{
    public class ApprovalApproverStage
    {
        public ApprovalApproverStage()
        {
            ApprovalApprovers = new HashSet<ApprovalApprover>();
        }

        [Key]
        public int ApprovalApproverStageId { get; set; }

        [ForeignKey("Approval")]
        public int ApprovalId { get; set; }
        public virtual Approval Approval { get; set; }

        public int Stage { get; set; }

        [ForeignKey("ApproverLabel")]
        public int? ApproverLabelId { get; set; }
        public virtual ApproverLabel ApproverLabel { get; set; }

        public int ApproveCount { get; set; }

        public int RejectCount { get; set; }

        public int RequiredApprovals { get; set; }

        public int RequiredRejections { get; set; }

        public int DisapprovalCount { get; set; }

        public bool IsPassed { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<ApprovalApprover> ApprovalApprovers { get; set; }
    }
}
