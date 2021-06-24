using System;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class JobOrderRequestFilter : FilterBase
    {
        public string TrackingNo { get; set; }
        public string Requestor { get; set; }
        public string Request { get; set; }
        public string Memo { get; set; }
        public string Status { get; set; }
        public DateTime? ActedUponFrom { get; set; }
        public DateTime? ActedUponTo { get; set; }

        public int? CurrentUserOfficeId { get; set; }
        public int? CurrentUserDepartmentId { get; set; }
        public int? CurrentUserDivisionId { get; set; }
        public int? CurrentUserDivisionCategoryId { get; set; }
        public int CurrentUserId { get; set; }
        public bool ForProcess { get; set; }
    }
}
