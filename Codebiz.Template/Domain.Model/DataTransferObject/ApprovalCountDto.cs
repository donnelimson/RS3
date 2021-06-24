namespace Codebiz.Domain.Common.Model.DataTransferObject
{
   public class ApprovalCountDto
    {
        public int ForApprovalCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int CancelledCount { get; set; }
    }
}
