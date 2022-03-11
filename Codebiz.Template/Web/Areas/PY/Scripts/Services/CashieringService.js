angular.module("MetronicApp").factory('CashieringService', ['CommonService', function (commonService) {
    return {
        Search: function (data) {
            return commonService.PostData(data, document.Cashiering + 'Search');
        },
        GetDetailsById: function (data) {
            return commonService.GetData(data, document.Cashiering + 'GetDetailsById');
        },
        AddOrUpdate: function (data) {
            return commonService.PostData(data, document.Cashiering + 'AddOrUpdate');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.PostData(data, document.Cashiering + 'ExportDataToExcelFile');
        },

    };
}]);