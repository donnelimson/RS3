using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum WithdrawalStatusesEnum
    {
        [Description("For Approval")]
        ForApproval = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Disapproved")]
        Disapproved = 3
    }
}
