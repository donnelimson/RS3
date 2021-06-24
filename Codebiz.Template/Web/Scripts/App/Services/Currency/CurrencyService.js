angular.module("MetronicApp").factory('CurrencyService', ['CommonService', function (commonService) {
    return {
        CurrencyIndex: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/Management/Financial/Currency/GetList', null);
        },
        AddCurrency: function (data) {
            return commonService.PostData(data, document.Currency + 'AddCurrency');
        },
        EditCurrency: function (data) {
            return commonService.PostData(data, document.Currency + 'EditCurrency');
        },
        GetCurrencyDetails: function (data, id) {
            return commonService.GetData(data, document.baseUrlNoArea + 'api/Management/Financial/Currency/GetDetails/' + id, null);
        },
        DeleteCurrency: function (data) {
            return commonService.PostData(data, document.Currency + 'DeleteCurrency', null);
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.Currency + 'ExportDataToExcelFile');
        }
    };
}]);