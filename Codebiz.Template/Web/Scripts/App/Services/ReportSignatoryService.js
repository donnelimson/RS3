angular.module("MetronicApp").factory('ReportSignatoryService', ['CommonService', function (commonService) {
    return {

        SearchReportSignatories: function (args) {
            return commonService.GetData(args, document.ReportSignatory + 'SearchReportSignatories', null);
        },

        ExportDataToExcelFile: function (data) {
            return commonService.PostData(data, document.ReportSignatory + 'ExportDataToExcelFile', null);
        },

        GetReportCategories: function (args) {
            return commonService.GetData(args, document.ReportSignatory + 'GetReportCategories');
        },

        GetReportsByCategoryId: function (args) {
            return commonService.GetData(args, document.ReportSignatory + 'GetReportsByCategoryId');
        },

        GetApproverLabels: function (args) {
            return commonService.GetData(args, document.ApproverLabel + 'GetApproverLabels');
        },

        SaveReportSignatory: function (data) {
            return commonService.PostData(data, document.ReportSignatory + 'SaveReportSignatory', null);
        },

        DeleteReportSignatory: function (data) {
            return commonService.PostData(data, document.ReportSignatory + 'DeleteReportSignatory', null);
        },

        ToggleActiveStatus: function (data) {
            return commonService.PostData(data, document.ReportSignatory + 'ToggleActiveStatus', null);
        },

        GetDetailsById: function (data) {
            return commonService.PostData(data, document.ReportSignatory + 'GetDetailsById', null);
        }
    }
}]);