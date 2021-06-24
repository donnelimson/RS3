using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum TransactionTypeEnum
    {
        [Description("Member Account Application")]
        MemberAccountApplication = 1,

        [Description("Change of Name")]
        ChangeOfName = 2,

        [Description("Discount Application")]
        DiscountApplication = 3,

        [Description("Burial Assistance")]
        BurialAssistance = 4,

        [Description("Reconnection")]
        Reconnection = 5,

        [Description("Close and Transfer")]
        CloseAndTransfer = 6,

        [Description("Disconnection")]
        Disconnection = 7,

        [Description("Net Metering")]
        NetMetering = 8,

        [Description("Item Master Data")]
        ItemMasterData = 9,

        [Description("Job Order - Complaint")]
        JobOrderComplaint = 10,

        [Description("Payment")]
        Payment = 11,

        [Description("Job Order - Request")]
        JobOrderRequest = 12,

        [Description("Discount Application Renewal")]
        DiscountApplicationRenewal = 13,

        [Description("Vehicle Request")]
        VehicleRequest = 14,

        [Description("Vehicle Inspection")]
        VehicleInspection = 15,

        [Description("Motorpol Section")]
        MotorpolSection = 16,

        [Description("Inventory Transfer")]
        InventoryTransfer = 17,

        [Description("Business Partner - Customer")]
        BusinessPartnerCustomer = 18,

        [Description("Job Order - Receive")]
        JobOrderReceive = 19,

        /*pur documents*/
        [Description("Purchase Quotation")]
        PQU = 20,
        [Description("Purchase Order")]
        POR = 21,
        [Description("Purchase Delivery")]
        PDN = 22,
        [Description("Purchase Return")]
        PRD = 23,
        [Description("Purchase DownPayment Invoice")]
        PDP = 24,
        [Description("Purchase Invoice")]
        PIN = 25,
        [Description("Purcahse CreditNote")]
        PCR= 26,

        /*sal documents*/
        [Description("Sales Quotation")]
        SQU = 27,
        [Description("Sales Order")]
        SOR = 28,
        [Description("Sales Delivery")]
        SDN = 29,
        [Description("Sales Return")]
        SRD = 30,
        [Description("Sales DownPayment Invoice")]
        SDP = 31,
        [Description("Sales Invoice")]
        SIN = 34,
        [Description("Sales CreditNote")]
        SCR = 33,

        [Description("Inventory Receiving")]
        InventoryReceiving = 37,

        [Description("Job Order Request - Indirect")]
        JobOrderRequestIndirect = 38,

        [Description("Job Order Request - Direct")]
        JobOrderRequestDirect = 39,

        [Description("Job Order Complaint - Indirect")]
        JobOrderComplaintIndirect = 40,

        [Description("Job Order Complaint - Direct")]
        JobOrderComplaintDirect = 41,

        [Description("Outgoing Distribution Transformer")]
        OutgoingDistributionTransformer = 42,

        //Other Request
        [Description("Other Request")]
        OtherRequest = 43,

        [Description("Payment Cancellation")]
        PaymentCancellation = 44,

        [Description("Billing Adjustment")]
        BillingAdjustment = 45,

        [Description("Incoming Payment")]
        SPY = 46,

        [Description("Outgoing Payment")]
        PPY = 47,

        [Description("Journal Voucher")]
        JVO = 48,

        [Description("Journal Entry")]
        JEN = 49,

        [Description("Bill of Materials")]
        BOM = 50,

        [Description("Meter Withdrawal")]
        MeterWithdrawal = 51,

        [Description("Material Request")]
        MaterialRequest = 52,

        [Description("Contestable Application")]
        ContestableApplication = 53,

        [Description("Business Partner - Vendor")]
        BusinessPartnerVendor = 54,

        [Description("Stock Requisition Voucher")]
        SRV = 55,

        [Description("Transformer Rental")]
        TransformerRental = 56,

        [Description("Goods Receipt")]
        IGN = 57,

        [Description("Good Issue")]
        IGE = 58,

        [Description("Openning Balances")]
        IQI = 59,

        [Description("Inventory Tracking")]
        IQC = 60,

        [Description("Inventory Posting")]
        IQP = 61,

        [Description("Purchase Requisition Voucher")]
        PRV = 62,

        [Description("Approval-Application")]
        ApprovalApplication = 63,

        [Description("Approval-Job Order")]
        ApprovalJO = 64,

        [Description("Approval-Receive")]
        ApprovalReceive = 65,

        [Description("Change Of Meter")]
        ChangeOfMeter = 66,

        [Description("Inventory Revaluation")]
        IMV = 68,

        [Description("Approval")]
        Approval = 69,

        [Description("Special Lighting Application")]
        SpecialLightingApplication = 70,

        [Description("Applicant")]
        Applicant = 71,

        [Description("Deposit")]
        DPS = 72,

        [Description("Coop Vehicle")]
        CoopVehicle = 73,
    }
}