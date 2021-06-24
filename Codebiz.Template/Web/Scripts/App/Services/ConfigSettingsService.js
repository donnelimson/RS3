angular.module("MetronicApp").factory('ConfigSettingsService', ['CommonService', function (commonService) {
    return {
        SearchConfigSettings: function (args) {
            return commonService.GetData(args, document.ConfigSetting + 'SearchConfigSettings', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.ConfigSetting + 'ExportDataToExcelFile');
        }
    };
}]);

