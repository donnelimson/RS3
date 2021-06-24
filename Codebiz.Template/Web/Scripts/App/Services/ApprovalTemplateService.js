
angular.module("MetronicApp").factory('ApprovalTemplateService', ['CommonService', function (commonService) {
    return {
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.ApprovalTemplate + 'ExportDataToExcelFile');
        },
        GetAllApprovalTemplate: function (args) {
            return commonService.GetData(args, document.ApprovalTemplate + 'GetAllApprovalTemplate');
        },
        SaveApprovalTemplate: function (data) {
            return commonService.PostData(data, document.ApprovalTemplate + 'AddOrUpdate', null);
        },
        ActivateOrDeactivateApprovalTemplate: function (data) {
            return commonService.PostData(data, document.ApprovalTemplate + 'ActivateOrDeactivate', null);
        },
        DeleteApprovalTemplate: function (data) {
            return commonService.PostData(data, document.ApprovalTemplate + 'Delete', null);
        },
        GetApprovalTemplateDetails: function (args) {
            return commonService.GetData(args, document.ApprovalTemplate + 'GetApprovalTemplateDetails');
        },
        GetAllTransactionForApproval: function (args) {
            return commonService.GetData(args, document.ApprovalTemplate + 'GetAllTransactionForApproval');
        },
        CheckApprovalTemplateIfHasConflict: function (data) {
            return commonService.PostData(data, document.ApprovalTemplate + 'CheckApprovalTemplateIfHasConflict', null);
        }
    }
}]);