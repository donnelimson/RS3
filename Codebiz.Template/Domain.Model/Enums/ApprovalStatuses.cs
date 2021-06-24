using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ApprovalStatuses
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Disapproved")]
        Disapproved = 3,

        [Description("Rejected")]
        Rejected = 4,
    }

    public enum LeaveApprovalStatuses
    {
        [Description("For Endorsement Approval")]
        ForEndorsementApproval = 1,

        [Description("Disapproved By Unit")]
        DisapprovedByUnit = 2,

        [Description("For Issuance Of Order")]
        ForIssuanceOfOrder = 3,

        [Description("Disapproved By NHQ")]
        DisapprovedByNhq = 4,

        [Description("For Order Approval")]
        ForOrderApproval = 5,

        [Description("Order Rejected")]
        OrderRejected = 6,

        [Description("Leave Order Approved")]
        Approved = 7,

        [Description("Cancelled")]
        Cancelled = 8,

        [Description("Pending")]
        Pending = 9
    }

    public enum LeaveCreditMonetizationApprovalStatuses
    {
        [Description("For Endorsement Approval")]
        ForEndorsementApproval = 1,

        [Description("Disapproved By Unit")]
        DisapprovedByUnit = 2,

        [Description("For Issuance Of Order")]
        ForIssuanceOfOrder = 3,

        [Description("Disapproved By NHQ")]
        DisapprovedByNhq = 4,

        [Description("For Order Approval")]
        ForOrderApproval = 5,

        [Description("Order Rejected")]
        OrderRejected = 6,

        [Description("Leave Credit Monetization Order Approved")]
        Approved = 7,

        [Description("Cancelled")]
        Cancelled = 8,

        [Description("Pending")]
        Pending = 9
    }



    //CSA
    public enum HwiStatuses
    {
        [Description("Approved")]
        Approved = 1,

        [Description("Rejected")]
        Rejected = 2,

        [Description("Pending")]
        Pending = 3,
    }

    public enum AccountStatuses
    {
        [Description("Approved")]
        Approved = 1,

        [Description("Rejected")]
        Rejected = 2,

        [Description("Pending")]
        Pending = 3
    }

    public enum MemberShipStatuses
    {
        [Description("Approved")]
        Approved = 2,

        [Description("Rejected")]
        Rejected = 3,

        [Description("Pending")]
        Pending = 1,
    }

}
