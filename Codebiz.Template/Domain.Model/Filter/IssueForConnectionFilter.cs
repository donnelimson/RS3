using System;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class IssueForConnectionFilter : FilterBase
    {
        public int? LinemanId { get; set; }  
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public int? TransactionTypeId { get; set; }
        public string ActedUponBy { get; set; }
        public bool? IsConnected { get; set; }
        public int? ConsumerTypeId { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
    }

    public class ForConnectionFilter : FilterBase
    {
        public int? LinemanId { get; set; }
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public int? TransactionTypeId { get; set; }
        public bool? IsConnected { get; set; }
        public int? ConsumerTypeId { get; set; }
        public string Status { get; set; }
        public DateTime? ActedUponFrom { get; set; }
        public DateTime? ActedUponTo { get; set; }

        public bool HasOverridePermission { get; set; }
    }
}
