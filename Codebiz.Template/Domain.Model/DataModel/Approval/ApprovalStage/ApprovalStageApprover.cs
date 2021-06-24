using Codebiz.Domain.Common.Model.EnumBaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel.Approval.ApprovalStage
{
    public class ApprovalStageApprover
    {
        [Key]
        public int ApprovalStageApproverId { get; set; }
        [ForeignKey("ApprovalStage")]
        public int ApprovalStageId { get; set; }
        public virtual Codebiz.Domain.Common.Model.ApprovalStage ApprovalStage { get; set; }
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
