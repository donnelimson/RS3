angular.module("MetronicApp").factory('BrandService', ['CommonService', function (commonService) {
    return {
        Search: function (data) {
            return commonService.PostData(data, document.Brand + 'Search');
        },
        AddOrUpdate: function (data) {
            return commonService.PostData(data, document.Brand + 'AddOrUpdate');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.PostData(data, document.Brand + 'ExportDataToExcelFile');
        },
        GetBrandForItemMaster: function (data) {
            return commonService.PostData(data, document.Brand + 'GetBrandForItemMaster');
        },
        GetDetailsById: function (data) {
            return commonService.GetData(data, document.Brand + 'GetDetailsById');
        },

    };
}]);