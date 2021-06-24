angular.module("MetronicApp").factory('PermissionServices', ['CommonService', function (commonService) {
    return {
        SearchPermission: function (args) {
            return commonService.GetData(args, document.Permission + 'SearchPermission', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Permission + 'ExportDataToExcelFile', null);
        }
    };
}]);