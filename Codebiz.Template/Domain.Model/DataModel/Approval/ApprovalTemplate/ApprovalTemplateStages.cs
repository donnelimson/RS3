using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ApprovalTemplateStages
    {
        [Key]
        public int ApprovalTemplateStagesDataID { get; set; }
        [ForeignKey("ApprovalTemplate")]
        public int ApprovalTemplateId { get; set; }
        public virtual ApprovalTemplate ApprovalTemplate{get;set;}
        [ForeignKey("ApprovalStage")]
        public int ApprovalStageId { get; set; }
        public virtual ApprovalStage ApprovalStage { get; set; }
        public int Priority { get; set; }
    }
}
