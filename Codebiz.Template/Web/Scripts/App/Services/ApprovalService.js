angular.module("MetronicApp").factory('ApprovalService', ['CommonService', '$window', function (commonService, $window) {
    return {

        ApprovalIndex: function (args) {
            return commonService.GetData(args, document.Approval + 'ApprovalIndex', null);
        },

        SearchApprovalResult: function (args) {
            return commonService.GetData(args, document.Approval + 'SearchApprovalResult', null);
        },

        GetAccountDetails: function (args) {
            return commonService.GetData(args, document.Approval + 'GetAccountDetails', null);
        },

        GetApprovalInformation: function (args) {
            return commonService.GetData(args, document.Approval + 'GetApprovalInformation', null);
        },

        GetChangeOfNameDetails: function (args) {
            return commonService.GetData(args, document.Approval + 'GetChangeOfNameDetails', null);
        },

        GetDisconnectionRequestDetails: function (args) {
            return commonService.GetData(args, document.Approval + 'GetDisconnectionRequestDetails', null);
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.Approval + 'ExportDataToExcelFile', null);
        },

        GetApprovalStatus: function (args) {
            return commonService.GetData(args, document.Approval + 'GetApprovalStatus', null);
        },

        GetTransactionType: function (args) {
            return commonService.GetData(args, document.Approval + 'GetTransactionType', null);
        },

        Process: function (data) {
            return commonService.PostData(data, document.Approval + 'Process', null);
        },
        CheckIfHasApporvalTemplateSetup: function (args) {
            return commonService.GetData(args, document.Approval + 'CheckIfHasApporvalTemplateSetup', null);
        },
        GoToDetails: function (data) {
            var controller = "";
            switch (data.TransactionTypeId) {
                case TRANSACTION_TYPE.Member_Application.Value:
                    $window.open(document.CSAMemberAccounts
                        + "Details?accountId=" + data.TransactionId
                        + "&forView=" + true, '_blank');
                    break;
                case TRANSACTION_TYPE.Discount_Application.Value:
                    controller = document.CSADiscountApplication;
                    break;
                case TRANSACTION_TYPE.Change_of_Name.Value:
                    controller = document.CSAChangeOfName;
                    break;
                case TRANSACTION_TYPE.Discount_Application.Value:
                    controller = document.CSADiscountApplication;
                    break;
                case TRANSACTION_TYPE.Discount_Application_Renewal.Value:
                    controller = document.CSADiscountApplication;
                    break;
                case TRANSACTION_TYPE.Burial_Assistance.Value:
                    controller = document.CSABurialAssistance;
                    break;
                case TRANSACTION_TYPE.Net_Metering.Value:
                    controller = document.CSANetMetering;
                    break;
                case TRANSACTION_TYPE.Transformer_Rental.Value:
                    controller = document.CSATransformerRental;
                    break;
                case TRANSACTION_TYPE.Change_of_Meter.Value:
                    controller = document.CSAChangeOfMeter;
                    break;
                case TRANSACTION_TYPE.Job_Order_Request_Indirect.Value:
                    $window.open(document.JobOrderRequest
                        + "Details?jobOrderId=" + data.TransactionId
                        + "&forView=" + true, '_blank');
                    break;
                case TRANSACTION_TYPE.Job_Order_Request_Direct.Value:
                    $window.open(document.JobOrderRequest
                        + "Details?jobOrderId=" + data.TransactionId
                        + "&forView=" + true, '_blank');
                    break;
                case TRANSACTION_TYPE.Job_Order_Complaint_Indirect.Value:
                    $window.open(document.JobOrderComplaint
                        + "Details?jobOrderId=" + data.TransactionId
                        + "&forView=" + true, '_blank');
                    break;
                case TRANSACTION_TYPE.Job_Order_Complaint_Direct.Value:
                    $window.open(document.JobOrderComplaint
                        + "Details?jobOrderId=" + data.TransactionId
                        + "&forView=" + true, '_blank');
                    break;
                default:
                    break;
            }

            $window.open(controller
                + "Details?id=" + data.TransactionId
                + "&forView=" + true, '_blank');
        }
    }
}]);