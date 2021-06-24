using System;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class ApprovalFilter : FilterBase
    {
        public int? TransactionTypeId { get; set; }
        public string TransactionCode { get; set; }
        public int? StatusId { get; set; }
        public DateTime? ActionDateFrom { get; set; }
        public DateTime? ActionDateTo { get; set; }
    }
}
