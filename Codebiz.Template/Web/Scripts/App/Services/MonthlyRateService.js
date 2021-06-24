angular.module("MetronicApp")
    .factory('MonthlyRateService', ['CommonService', function (commonService) {
        return {
            MonthlyRateIndex: function (args) {
                return commonService.GetData(args, document.BillingMonthlyRate + 'MonthlyRateIndex');
            },
            GenerateMonthlyRateDescrition: function (data) {
                return commonService.PostData(data, document.BillingMonthlyRate + 'GenerateMonthlyRateDescrition', null);
            },
            Save: function (data) {
                return commonService.PostData(data, document.BillingMonthlyRate + 'Save', null);
            },
            GetUnbundledTransactionCategory: function (data) {
                return commonService.PostData(data, document.BillingMonthlyRate + 'GetUnbundledTransactionCategory', null);
            },
            GetMonthlyRateDetails: function (data) {
                return commonService.PostData(data, document.BillingMonthlyRate + 'GetMonthlyRateDetails', null);
            },
            GetUnbundledNameByCode: function (data) {
                return commonService.PostData(data, document.BillingMonthlyRate + 'GetUnbundledNameByCode', null);
            },
            ExportDataToExcelFile: function (args) {
                return commonService.GetData(args, document.BillingMonthlyRate + 'ExportDataToExcelFile', null);
            }
        }
    }]);