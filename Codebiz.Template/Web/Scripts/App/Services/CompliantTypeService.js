angular.module("MetronicApp").factory('ComplaintTypeService', ['CommonService', function (commonService) {
    return {
        SearchComplaintType: function (args) {
            return commonService.GetData(args, document.ComplaintType + 'SearchComplaintType');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.ComplaintType + 'ExportDataToExcelFile');
        }
    };
}]);
