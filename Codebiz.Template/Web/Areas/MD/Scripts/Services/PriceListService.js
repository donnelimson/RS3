angular.module("MetronicApp").factory('PriceListService', ['CommonService', function (commonService) {
    return {
        Search: function (data) {
            return commonService.PostData(data, document.PriceList + 'Search');
        },
        AddOrUpdate: function (data) {
            return commonService.PostData(data, document.PriceList + 'AddOrUpdate');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.PostData(data, document.PriceList + 'ExportDataToExcelFile');
        },
        GetAllItemsForPriceList: function (data) {
            return commonService.PostData(data, document.PriceList + 'GetAllItemsForPriceList');
        },
        GetPriceListForItemMaster: function (data) {
            return commonService.PostData(data, document.PriceList + 'GetPriceListForItemMaster');
        },
        GetDetailsById: function (data) {
            return commonService.GetData(data, document.PriceList + 'GetDetailsById');
        },
    };
}]);