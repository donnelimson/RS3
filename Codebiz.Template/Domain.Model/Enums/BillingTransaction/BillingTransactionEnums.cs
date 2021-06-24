using System.ComponentModel;

namespace Codebiz.Domain.Common.Model
{
    public enum BillingTransactionEnums
    {
        [Description("Show All")]
        ShowAll = 1,
        [Description("Show With Remarks Only")]
        ShowWithRemarksOnly = 2,

        [Description("Show Posted Only")]
        ShowPostedOnly = 3,

        [Description("Show Unposted Only")]
        ShowUnpostedOnly = 4,
    }
}
