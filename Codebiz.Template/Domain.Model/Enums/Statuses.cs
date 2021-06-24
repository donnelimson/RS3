using System.ComponentModel;

namespace Codebiz.Domain.Common.Model
{
    public enum StatusesEnum
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Disapproved")]
        Disapproved = 3,

        [Description("Rejected")]
        Rejected = 4,

        [Description("Closed and Transfer")]
        ClosedandTransfer = 5,

        [Description("Connected")]
        Connected = 6,

        [Description("Disconnected")]
        Disconnected = 7,

        [Description("Paid")]
        Paid = 8,

        [Description("Passed")]
        Passed = 9,

        [Description("Failed")]
        Failed = 10,

        [Description("Active")]
        Active = 11,

        [Description("For Inspection")]
        ForInspection = 12,

        [Description("Received")]
        Received = 13,

        [Description("Endorse To Division")]
        EndorseToDivision = 14,

        [Description("Processing")]
        Processing = 15,

        [Description("Processed")]
        Processed = 16,

        [Description("In-Progress")]
        InProgress = 17, 

        [Description("For Approval")]
        ForApproval = 18, 

        [Description("For Renewal")]
        ForRenewal = 19,

        [Description("For Claim")]
        ForClaim = 20,

        [Description("Accomplished")]
        Accomplished = 21,

        [Description("Unaccomplished")]
        Unaccomplished = 22,

        [Description("Delivered")]
        Delivered = 23,

        [Description("Cancelled")]
        Cancelled = 24,
        [Description("Terminated")]
        Terminated = 25,
        [Description("For Termination")]
        ForTermination = 26
    }
}
