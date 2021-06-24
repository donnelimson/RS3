using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum MemberStatuses
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Active")]
        Active = 2,

        [Description("Inactive")]
        Inactive = 3,

        [Description("Closed")]
        Close = 4,

        [Description("Rejected")]
        Rejected = 5,

        [Description("Withdrawn")]
        Withdrawn = 6,
        [Description("Deceased")]
        Deceased = 7,
        [Description("Disapproved")]
        Disapproved = 8,
        [Description("Temporary")]
        Temporary = 9


    }
}
