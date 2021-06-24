angular.module("MetronicApp").factory('HouseBankAccountService', ['CommonService', function (commonService) {
    return {
        Search: function (data) {
   
            return commonService.PostData(data, document.APIHouseBankAccount + 'GetHouseBankAccountList');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.FINHouseBankAccount + 'ExportDataToExcelFile');
        },
        Delete: function (args) {
            return commonService.GetData(args, document.FINHouseBankAccount + 'Delete');
        },
        AddOrUpdate: function (data) {
            return commonService.PostData(data, document.FINHouseBankAccount + 'AddOrUpdate');
        },
        SearchGLAccount: function (args) {
            return commonService.GetData(args, document.baseUrlNoArea + 'api/FIN/Setup/SearchGLAccount/{searcher}');
        },
        GetHouseBankAccountDetails: function (args) {
            return commonService.GetData(args, document.APIHouseBankAccount + 'GetHouseBankAccountDetails/{id}');
        },
        CheckBankIfExist: function (args) {
            return commonService.GetData(args, document.APIHouseBankAccount + 'CheckBankIfExist/{bankId}/{id}');
        },
        Delete: function (args) {
            return commonService.GetData(args, document.FINHouseBankAccount + 'Delete');
        },
        SearchGLAccount: function (args) {
            return commonService.GetData(args, document.baseUrlNoArea + 'api/FIN/Setup/SearchGLAccount/{searcher}');
        },
        GetHouseBankAccountShortCut: function (args) {
            return commonService.GetData(args, document.APIHouseBankAccount + 'GetHouseBankAccountShortCut/{searcher}');
        },
        CheckAccountNoIfExist: function (args) {
            return commonService.GetData(args, document.APIHouseBankAccount + 'CheckAccountNoIfExist/{accountNo}/{bankId}/{id}');
        },

    };
}]);