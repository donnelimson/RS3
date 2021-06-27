using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ContentFileTypes : int
    {
        [Description("Employee Photo")]
        EmployeePhoto = 1,

        [Description("Member Supporting Document")]
        MemberSupportingDocument = 2,

        [Description("Account Supporting Document")]
        AccountSupportingDocument = 3,

        [Description("House Wiring Inspection Supporting Document")]
        HouseWiringInspectionSupportingDocument = 4,

        [Description("Discount Application Supporting Document")]
        DiscountApplicationSupportingDocument = 5,

        [Description("Membership Barcode")]
        MembershipBarcode = 6,

        [Description("Photo")]
        Photo = 7,

        [Description("Signature")]
        Signature = 8,

        [Description("Connection Order PDF")]
        ConnectionOrderPdf = 9,

        [Description("Material Charge Ticket PDF")]
        MaterialChargeTicketPdf = 10,

        [Description("Job Order PDF")]
        JobOrderPdf = 11,

        [Description("Purchase Order PDF")]
        PurchaseOrderPdf = 12,

        [Description("Billing Address PDF")]
        BillingAddressPdf = 13,

        [Description("Burial Assistance Supporting Document")]
        BurialAssistanceSupportingDocument = 14,

        [Description("Change Of Name Supporting Document")]
        ChangeOfNameSupportingDocument = 15,

        [Description("Net Metering Documents Folder")]
        NetMeteringDocumentsFolder = 16,

        [Description("Job Order Complaint Supporting Document")]
        JobOrderComplaintSupportingDocument = 17,

        [Description("Job Order Request Supporting Document")]
        JobOrderRequestSupportingDocument = 18,

        [Description("Item Master Data Attachment Folder")]
        ItemMasterDataAttachment = 19,

        [Description("Business Partner Supporting Documents" + " Folder")]
        BusinessPartnerSupportingDocumentsFolder = 20,

        [Description("Inventory Transfer Supporting Documents Folder")]
        InventoryTransferSupportingDocumentsFolder = 21,

        [Description("Employee Signature")]
        EmployeeSignature = 22,

        [Description("Inventory Receiving Supporting Documents Folder")]
        InventoryReceivingSupportingDocumentsFolder = 23,

        [Description("Other Request Supporting Document")]
        OtherRequestSupportingDocument = 24,

        [Description("Counter Setup Supporting Document")]
        CounterSetupSupportingDocument = 25,

        [Description("Process Job Order Folder")]
        ProcessJobOrderDocumentsFolder = 26,

        [Description("Material Request Supporting Document")]
        MaterialRequestSupportingDocument = 27,

        [Description("Material Request Folder")]
        MaterialRequestFolder = 28,

        [Description("Contestable Application Folder")]
        ContestableApplicationFolder = 29,

        [Description("Transformer Rental Folder")]
        TransformerRentalFolder = 30,

        [Description("Transformer Rental Witness Signature Folder")]
        TransformerRentalWitnessSignatureFolder = 31,

        [Description("Change of Meter Supporting Documents Folder")]
        ChangeOfMeterSupportingDocumentsFolder = 32,

        [Description("Requisition Voucher Supporting Documents Folder")]
        RequisitionVoucherSupportingDocumentsFolder = 33,

        [Description("Requisition Voucher Folder")]
        RequisitionVoucherFolder = 34,

        [Description("Special Lighting Supporting Documents Folder")]
        SpecialLightingSupportingDocumentsFolder = 35,

        [Description("Sales Invoice Data Attachment Folder")]
        SalesInvoiceDataAttachment = 36,

        [Description("Billing Adjustment Attachment Folder")]
        BillingAdjustmentAttachmentFolder = 37,

        [Description("Sales Delivery Data Attachment Folder")]
        SalesDeliveryDataAttachment = 38,

        [Description("Sales Quotation Data Attachment Folder")]
        SalesQuotationDataAttachment = 39,

        [Description("Sales Order Data Attachment Folder")]
        SalesOrderDataAttachment = 40,

        [Description("Statement of Account Folder")]
        StatementOfAccountFolder = 41,

        [Description("Coop Vehicle Supporting Documents Folder")]
        CoopVehicleSupportingDocumentsFolder = 42,

        [Description("Ticket Attachment Folder")]
        TicketAttachmentFolder = 43,
    }

    public enum ConfigurationSettings : int
    {
        [Description("Database Connection String")]
        ConnectionString = 1,

        [Description("DisplayPhoto")]
        DisplayPhoto = 2,

        [Description("Mail Template Path")]
        MailTemplatePath = 3,

        [Description("SmtpDisplayName")]
        SmtpDisplayName = 4,

        [Description("SmtpUsername")]
        SmtpUsername = 5,

        [Description("SmtpPassword")]
        SmtpPassword = 6,

        [Description("SmtpHost")]
        SmtpHost = 7,

        [Description("SmtpPort")]
        SmtpPort = 8,

        [Description("Site Public Base Url")]
        SitePublicBaseUrl = 9, 

        [Description("Site Local Network Base Url")]
        SiteLocalNetworkBaseUrl = 10,

        [Description("Email Alert Recipients")]
        EmailAlertRecipients = 11,

        [Description("Maximum Login Attempts")]
        MaxLoginAttempt = 12,

        #region Folders for files/attachments

        [Description("Member Photo Folder")]
        MemberPhotoFolder = 13,

        [Description("Member Signature Folder")]
        MemberSignatureFolder = 14,

        [Description("Member Supporting Documents Folder")]
        MemberSupportingDocumentsFolder = 15,

        [Description("Account Supporting Documents Folder")]
        AccountSupportingDocumentsFolder = 16,

        [Description("Connection Order PDF Folder")]
        ConnectionOrderPdfFolder = 17,

        [Description("Material Charge Ticket PDF Folder")]
        MaterialChargeTicketPdfFolder = 18,

        [Description("Job Order PDF Folder")]
        JobOrderPdfFolder = 19,

        [Description("Purchase Order PDF Folder")]
        PurchaseOrderPdfFolder = 20,

        [Description("Goods Receipt PDF Folder")]
        GoodsReceiptPdfFolder = 21,

        [Description("Billing Address PDF Folder")]
        BillingAddressPdfFolder = 22,

        [Description("Membership Barcode Folder")]
        MembershipBarcodeFolder = 23,

        [Description("Employee Photo Folder")]
        EmployeePhotoFolder = 24,

        [Description("House Wiring Inspection Supporting Documents Folder")]
        HouseWiringInspectionSupportingDocumentsFolder = 25,

        [Description("Discount Application Supporting Documents Folder")]
        DiscountApplicationSupportingDocumentsFolder = 26,

        [Description("Burial Assistance Supporting Documents Folder")]
        BurialAssistanceSupportingDocumentsFolder = 27,

        [Description("Default Folder")]
        DefaultFolder = 28,

        [Description("Change Of Name Supporting Documents Folder")]
        ChangeOfNameSupportingDocumentsFolder = 29,

        [Description("Reconnection Supporting Documents Folder")]
        ReconnectionSupportingDocumentsFolder = 30,

        [Description("Disconnection Supporting Documents Folder")]
        DisconnectionSupportingDocumentsFolder = 31,

        [Description("Close And Transfer Supporting Documents Folder")]
        CloseAndTransferSupportingDocumentsFolder = 32,

        [Description("Net Metering Documents Folder")]
        NetMeteringDocumentsFolder = 33,

        [Description("Job Order Complaint Supporting Documents Folder")]
        JobOrderComplaintSupportingDocumentsFolder = 34,

        [Description("Job Order Request Supporting Documents Folder")]
        JobOrderRequestSupportingDocumentsFolder = 35,

        [Description("Item Master Data Attachment Folder")]
        ItemMasterDataAttachment = 36,

        [Description("Gas Slip PDF Folder")]
        GasSlipPdfFolder = 37,

        [Description("Business Partner Supporting Documents Folder")]
        BusinessPartnerSupportingDocumentsFolder = 38,

        [Description("Inventory Transfer Supporting Documents Folder")]
        InventoryTransferSupportingDocumentsFolder = 39,

        [Description("Employee Signature Folder")]
        EmployeeSignatureFolder = 40,

        [Description("Inventory Receiving Supporting Documents Folder")]
        InventoryReceivingSupportingDocumentsFolder = 41,

        [Description("Other Request Supporting Documents Folder")]
        OtherRequestSupportingDocumentsFolder = 42,

        [Description("Counter Setup Documents Folder")]
        CounterSetupDocumentsFolder = 43,

        [Description("Process Job Order Folder")]
        ProcessJobOrderDocumentsFolder = 44,

        [Description("Official Receipts Folder")]
        OfficialReceiptsFolder = 45,

        [Description("Withdrawal Slip PDF Folder")]
        WithdrawalSlipPDFFolder = 46,

        [Description("Material Request Supporting Documents Folder")]
        MaterialRequestSupportingDocumentsFolder = 47,

        [Description("Material Request Folder")]
        MaterialRequestFolder = 48,

        [Description("Contestable Application Folder")]
        ContestableApplicationFolder = 49,

        [Description("Transformer Rental Folder")]
        TransformerRentalFolder = 50,

        [Description("Transformer Rental Witness Signature Folder")]
        TransformerRentalWitnessSignatureFolder = 51,

        [Description("Change of Meter Supporting Documents Folder")]
        ChangeOfMeterSupportingDocumentsFolder = 52,

        [Description("Requisition Voucher Supporting Documents Folder")]
        RequisitionVoucherSupportingDocumentsFolder = 53,

        [Description("Requisition Voucher Folder")]
        RequisitionVoucherFolder = 54,

        [Description("Special Lighting Supporting Documents Folder")]
        SpecialLightingSupportingDocumentsFolder = 55,

        [Description("Sales Invoice Data Attachment Folder")]
        SalesInvoiceDataAttachment = 56,

        [Description("Billing Adjustment Attachment Folder")]
        BillingAdjustmentAttachmentFolder = 57,

        [Description("Sales Delivery Data Attachment Folder")]
        SalesDeliveryDataAttachment = 58,

        [Description("Sales Quotation Data Attachment Folder")]
        SalesQuotationDataAttachment = 59,

        [Description("Sales Order Data Attachment Folder")]
        SalesOrderDataAttachment = 60,
        [Description("Statement of Account Folder")]
        StatementOfAccountFolder = 61,

        [Description("Coop Vehicle Supporting Documents Folder")]
        CoopVehicleSupportingDocumentsFolder = 62,
        #region rs3

        [Description("Ticket Attachment Folder")]
        TicketAttachmentFolder = 63,

        #endregion
        #endregion

    }
}
