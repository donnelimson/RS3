using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication.Request
{
    public enum RequestPhaseEnum
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Disapproved")]
        Disapproved = 3,

        [Description("Rejected")]
        Rejected = 4,

        [Description("Done")]
        Done = 5,
        [Description("Approval")]
        Approval = 6,

    }
}
