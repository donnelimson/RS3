using System.Collections.Generic;

namespace Codebiz.Domain.Common.Model.ViewModel.ApprovalTemplate
{
    public class ApprovalTemplateViewModel
    {
        public int ApprovalTemplateId { get; set; }
        public string Name { get; set; }

        public List<ApprovalTemplateTransactionViewModel> Transactions { get; set; }
        public List<ApprovalTemplateOriginatorViewModel> Originators { get; set; }
        public List<ApprovalTemplateStagesViewModel> Stages { get; set; }
    }
    public class ApprovalTemplateTransactionViewModel
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int TransactionTypeId { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }

    }
    public class ApprovalTemplateOriginatorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public bool IsSelected { get; set; }
    }
    public class ApprovalTemplateStagesViewModel
    {
        public int ApprovalStageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovals { get; set; }
        public bool IsSelected { get; set; }
    }

}
