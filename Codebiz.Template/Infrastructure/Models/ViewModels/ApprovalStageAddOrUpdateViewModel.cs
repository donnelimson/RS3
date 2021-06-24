using System.Collections.Generic;

namespace Infrastructure.Models.ViewModels
{
    public class ApprovalStageAddOrUpdateViewModel
    {
        public List<int> ApprovalStageApproverIds { get; set; }
        public int ApprovalStageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovals { get; set; }
        public int RequiredRejections { get; set; }
        public int LabelId { get; set; }
        public List<ApprovalStageApproverViewModel> ApprovalStageApprovers { get; set; }
    }

    public class ApprovalStageApproverViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public bool IsSelected { get; set; }
    }
}
