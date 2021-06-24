angular.module("MetronicApp").factory('SalesEmployeeService', ['CommonService', function (commonService) {
    return {
        GetSalesEmployee: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/Controller/SalesEmployee/GetList');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.SalesEmployee + 'ExportDataToExcelFile', null);
        },
        GetCommissionGroups: function (data) {
            return commonService.GetData(data, document.SalesEmployee + 'GetCommissionGroups', null);
        },
        SaveData: function (args) {
            return commonService.PostData(args.model, document.baseUrlNoArea + 'api/Controller/SalesEmployee/SaveData');
        },
        GetDetails: function (data) {
            return commonService.GetData(data, document.baseUrlNoArea + 'api/Controller/SalesEmployee/GetDetails/' + data.id, null);
        },
        DeleteSalesEmployee: function (data) {
            return commonService.GetData(data, document.baseUrlNoArea + 'api/Controller/SalesEmployee/Delete/' + data.id, null);
        }
    };
}]);