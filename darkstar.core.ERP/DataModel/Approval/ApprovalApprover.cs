using Codebiz.Domain.Common.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.ERP.Model
{
    public class ApprovalApprover
    {
        [Key]
        public int ApprovalApproverId { get; set; }

        public bool IsActive { get; set; }

        public string  Remarks { get; set; }

        [ForeignKey("ApprovalApproverStage")]
        public int ApprovalApproverStageId { get; set; }
        public virtual ApprovalApproverStage ApprovalApproverStage { get; set; }

        [ForeignKey("Approver")]
        public int ApproverId { get; set; }
        public virtual AppUser Approver { get; set; }

        [ForeignKey("ApprovalStatus")]
        public int? ApprovalStatusId { get; set; }
        public virtual ApprovalStatus ApprovalStatus { get; set; }

        public DateTime? ActionDate { get; set; }  
    }
}
