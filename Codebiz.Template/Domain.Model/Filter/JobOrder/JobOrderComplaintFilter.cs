using System;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class JobOrderComplaintFilter: FilterBase
    {
        public string TrackingNo { get; set; }
        public string Complainant { get; set; }
        public string Complaint { get; set; }
        public string Memo { get; set; }
        public string Status { get; set; }
        public DateTime? ActedUponFrom { get; set; }
        public DateTime? ActedUponTo { get; set; }
        public int? CurrentUserOfficeId { get; set; }
        public int? CurrentUserDivisionId { get; set; }
        public int? CurrentUserDepartmentId { get; set; }
        public int? CurrentUserDivisionCategoryId { get; set; }
    }
}
