using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum AccountPhaseEnum
    {
        [Description("House Wiring Inspection")]
        HouseWiringInspection = 2,

        [Description("Payment")]
        Payment = 3,

        [Description("Approval")]
        Approval = 4,

        [Description("Connection")]
        Connection = 5,

        [Description("Application")]
        Application = 6,

        [Description("Pre-Membership Orientation Seminar")]
        PreMembershipOrientationSeminar = 7,

        [Description("Done")]
        Done = 8,

        [Description("Request")]
        Request = 9,

        [Description("Issue for connection")]
        IssueForConnection = 10,

        [Description("Coop Evaluation")]
        CoopEvaluation = 11,

        [Description("Job Order")]
        JobOrder = 12,

        [Description("Endorsement")]
        Endorsement = 13,

        [Description("Validating Requirements")]
        ValidatingRequirements = 14,

        [Description("Recommendation for Approval")]
        RecommendationForApproval = 15,

        [Description("Endorsement for house wiring inspection")]
        EndorsementForHWInspection = 16,

        [Description("Re-endorsement for house wiring inspection")]
        ReendorsementForHWInspection = 17,

        [Description("Re-check requirements")]
        ReCheckRequirements = 18,

        [Description("For connection")]
        ForConnection = 19,
        [Description("For Requisition Voucher")]

        ForRequisitionVoucher = 20,

        [Description("Vehicle Request")]
        VehicleRequest = 21,

        [Description("Recommend For Inspection")]
        RecommendForInspection = 22,

        [Description("Recommendation for Payment")]
        RecommendationForPayment = 23,

        [Description("For Disconnection")]
        ForDisconnection = 24,

        [Description("Job Order - Complaint")]
        JOComplaint = 25,

        [Description("New Account Application")]
        NewAccountApplication = 26,

        [Description("Finance")]
        Finance = 27,

        [Description("For Close and Transfer")]
        ForCloseAndTransfer = 28,

        [Description("Processing for change of name")]
        ProcessingForChangeOfName = 29,

        [Description("Processing")]
        Processing = 30,

        [Description("Endorsement to issue for connection")]
        RecommendToIssueForConnection = 31,
        [Description("Feasible for Connection")]
        FeasibleForConnection = 32,
        [Description("Inspect Location")]
        InspectLocation = 33,
        [Description("Compute and Pay Coop Fees and Charges")]
        ComputePayCoopCharges = 34,
    }
}
