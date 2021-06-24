angular.module("MetronicApp").factory('BankService', ['CommonService', function (commonService) {
    return {
        Search: function (data) {
            return commonService.PostData(data, document.APIBank + 'GetBankList');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.FINBank + 'ExportDataToExcelFile');
        },
        Delete: function (args) {
            return commonService.GetData(args, document.FINBank + 'Delete');
        },
        AddOrUpdate: function (data) {
            return commonService.PostData(data, document.FINBank + 'AddOrUpdate');
        },
        SearchGLAccount: function (args) {
            return commonService.GetData(args, document.baseUrlNoArea + 'api/FIN/Setup/SearchGLAccount/{searcher}');
        },
        GetBankDetails: function (args) {
            return commonService.GetData(args, document.APIBank + 'GetBankDetails/{id}');
        },
        CheckBICSwiftCode: function (args) {
            return commonService.GetData(args, document.APIBank + 'CheckBICSwiftCode/{code}/{id}');
        },
        CheckBankCode: function (args) {
            return commonService.GetData(args, document.APIBank + 'CheckBankCode/{code}/{id}/{countryCodeId}');
        },
        CheckBankName: function (args) {
            return commonService.GetData(args, document.APIBank + 'CheckBankName/{name}/{id}');
        },
        Delete: function (args) {
            return commonService.GetData(args, document.FINBank + 'Delete');
        },
        GetBankShortCut: function (args) {
            return commonService.GetData(args, document.APIBank + 'GetBankShortCut/{searcher}');
        },

    };
}]);