angular.module("MetronicApp").factory('SurchargeService', ['CommonService', function (commonService) {
    return {
        GetSurcharge: function (args) {
            return commonService.GetData(args, document.Surcharge + 'SearchSurcharge');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Surcharge + 'ExportDataToExcelFile');
        },
        GetConsumerClasses: function (args) {
            return commonService.GetData(args, document.Surcharge + 'GetConsumerClass');
        },
        AddSurcharge: function (data) {
            return commonService.PostData(data, document.Surcharge + 'AddSurcharge');
        },
        UpdateSurcharge: function (data) {
            return commonService.PostData(data, document.Surcharge + 'UpdateSurcharge');
        },
        GetEditSurcharge: function (data) {
            return commonService.PostData(data, document.Surcharge + 'GetSurchargeDetails');
        },
        ToggleSurchargeActiveStatus: function (data) {
            return commonService.PostData(data, document.Surcharge + 'ToggleSurchargeActiveStatus');
        },
        DeleteSurcharge: function (data) {
            return commonService.PostData(data, document.Surcharge + 'DeleteSurcharge');
        }
    };
}]);