
angular.module("MetronicApp")
    .factory('PurposeService', ['CommonService', function (commonService) {
        return {
            Search: function (data) {
                return commonService.PostData(data, document.Purpose + 'Search');
            },
            Delete: function (data) {
                return commonService.GetData(data, document.Purpose + 'Delete');
            },
            ExportDataToExcelFile: function (data) {
                return commonService.PostData(data, document.Purpose + 'ExportDataToExcelFile');
            },
            AddOrUpdate: function (data) {
                return commonService.PostData(data, document.Purpose + 'InsertOrUpdate');
            },
            GetTransactionTypesForPurose: function (data) {
                return commonService.GetData(data, document.Purpose + 'GetTransactionTypesForPurose');
            },
            GetDetailsById: function (data) {
                return commonService.GetData(data, document.Purpose + 'GetDetailsById');
            },
            GetAllTransactionTypeForPurposeUnused: function (data) {
                return commonService.GetData(data, document.Purpose + 'GetAllTransactionTypeForPurposeUnused');
            },
            
        };
    }]);