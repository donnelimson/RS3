using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ApprovalTemplate
{
    public class ApprovalTemplateIndexDTO
    {
        public int ApprovalTemplateId { get; set; }
        [DisplayName("NAME")]
        public string Name { get; set; }
        [DisplayName("ACTIVE")]
        public bool Status { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }

        public bool CanDelete { get; set; }

    }
    public class ApprovalTemplateOriginatorsDTO
    {
        public int ApprovalTemplateOriginatorId { get; set; }
        public bool ToDelete { get; set; }
    }
    public class ApprovalTemplateTransactionDTO
    {
        public int ApprovalTemplateTransactionId { get; set; }
        public bool ToDelete { get; set; }
    }
    public class ApprovalTemplateStagesDataDTO
    {
        public int ApprovalTemplateStagesDataId { get; set; }
        public bool ToDelete { get; set; }
    }
    public class ApprovalTemplateStagesDetailsDTO
    {
        public int Id { get; set; }
        public int ApprovalStageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovals { get; set; }
        public int Priority { get; set; }
    }
    public class AllAprovalStageLookUpForUpdateDTO : ApprovalTemplateStagesDetailsDTO
    {
        public int ApprovalTemplateStageDataId { get; set; }
    }
    public class ApprovalTemplateTransactionCSADetails
    {
        public int ApprovalTemplateTransactionId { get; set; }
        public string Description { get; set; }
    }

    public class ApprovalTemplateWarningDTO
    {
        public string Originators { get; set; }
        public List<string> Transactions { get; set; }
        public string Stages { get; set; }
    }
}
