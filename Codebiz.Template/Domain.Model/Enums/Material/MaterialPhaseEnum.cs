using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum MaterialPhaseEnums
    {
        [Description("Request")]
        MaterialRequest = 1,

        [Description("Approval")]
        Approval = 2,

        [Description("Material Request For RV")]
        ForRV = 3,

        [Description("Requisition Voucher")]
        RequisitionVoucher = 4,

        [Description("Purchase Quotation")]
        PurchaseQuotation = 5,

        [Description("Purchase Option")]
        PurchaseOption = 6,

        [Description("Purchase Delivery")]
        PurchaseDelivery = 7,

        [Description("Done")]
        Done = 8,

        [Description("Recommendation For Approval")]
        RecommendForApproval = 9,

        [Description("For Checking")]
        ForChecking = 10,

        //[Description("Approval (RV)")]
        //ApprovalForRV = 11,

        [Description("Purchase Order")]
        PurchaseOrder = 12,

        [Description("Stock RV")]
        StockRV = 13,

        [Description("Purchase RV")]
        PurchaseRV = 14,

        [Description("Goods Issue")]
        GoodsIssue = 15,
    }
}
