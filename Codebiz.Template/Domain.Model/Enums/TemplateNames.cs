
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum TemplateNames
    {
        [Description("Checks for Payment - CR (US) (System)")]
        CFP = 1,
        [Description("Checks not Based on Invoice_US (System)")]
        CNB = 2,
        [Description("Check-Stub-Stub (System)")]
        CSS = 3,
        [Description("Long Stub_Check (System)")]
        LSC = 4,
        [Description("No Stub-Stub-Check (System)")]
        NSSC = 5,
        [Description("Stub-Check-Stub (System)")]
        SCS = 6,
        [Description("Stub-Stub-Check (System)")]
        SSC = 7,


    }
}
