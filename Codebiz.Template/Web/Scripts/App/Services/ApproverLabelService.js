angular.module("MetronicApp").factory('ApproverLabelService', ['CommonService', function (commonService) {
    return {
        SearchApproverLabel: function (args) {
            return commonService.GetData(args, document.ApproverLabel + 'SearchApproverLabel');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.ApproverLabel + 'ExportDataToExcelFile');
        },
        ToggleActiveStatus: function (data) {
            return commonService.PostData(data, document.ApproverLabel + 'ToggleApprovalStageActiveStatus');
        },
        Delete: function (data) {
            return commonService.PostData(data, document.ApproverLabel + 'DeleteApproverLabel');
        },
        GetDetailsToUpdate: function (args) {
            return commonService.PostData(args, document.ApproverLabel + 'GetDetailsToUpdate');
        },
        UpdateData: function (data) {
            return commonService.PostData(data, document.ApproverLabel + 'SaveData');
        }
    };
}]);