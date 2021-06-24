using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum AccountStatusEnum
    {
        [Description("Disconnected")]
        Disconnected = 2,

        [Description("Pending")]
        Pending = 3,

        [Description("Active")]
        Active = 4,

        [Description("Close And Transfer")]
        CloseAndTransfer = 5,

        [Description("Rejected")]
        Rejected = 6,

        [Description("Disapproved")]
        Disapproved = 7,

        [Description("Deceased")]
        Deceased = 8,

        [Description("Terminated")]
        Terminated = 9,

        [Description("Apprehended")]
        Apprehended = 10,

        [Description("For Inspection")]
        ForInspection = 11,

        [Description("Permanently Disconnected")]
        PermanentlyDisconnected = 12,

    }

    public enum AccountTypesEnum
    {
        [Description("SPL")]
        SpecialLighting = 1,

        [Description("P")]
        Permanent = 2,

        [Description("SE")]
        SoleUse = 3,
    }
}
