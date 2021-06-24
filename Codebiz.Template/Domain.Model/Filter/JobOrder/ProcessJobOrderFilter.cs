using System;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class ProcessJobOrderFilter : FilterBase
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string Status { get; set; }

        public string JobOrderNo { get; set; }
        public string Type { get; set; }
        public string ActionBy { get; set; }
        public DateTime? ActionOnFrom { get; set; }
        public DateTime? ActionOnTo { get; set; }

        public int? CurrentUserOfficeId { get; set; }
        public int? CurrentUserDivisionId { get; set; }
        public int? CurrentUserDepartmentId { get; set; }
        public int? CurrentUserDivisionCategoryId { get; set; }
        public int? CurrentUserId { get; set; }
        public int? CurrentEmployeeId { get; set; }
    }

    public class InvoicesFilter : FilterBase
    {
        public string Searcher { get; set; }
        public int AccountId { get; set; }
    }

    public class JobOrderProcessHistoryFilter : FilterBase
    {
        public int RoutingId { get; set; }
    }
}
