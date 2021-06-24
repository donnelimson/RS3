angular.module("MetronicApp").factory('MeterReadingRemarksService', ['CommonService', function (commonService) {
    return {
        SearchMeterReadingRemarks: function (args) {
            return commonService.GetData(args, document.MeterReadingRemarks + 'SearchMeterReadingRemarks');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.MeterReadingRemarks + 'ExportDataToExcelFile');
        },
        AddUpdateMeterReadingRemarks: function (args) {
            return commonService.PostData(args, document.MeterReadingRemarks + 'AddUpdateMeterReadingRemarks');
        },
        GetDetailsForUpdate: function (args) {
            return commonService.PostData(args, document.MeterReadingRemarks + 'GetDetailsForUpdate');
        },
        DeleteMeterReadingRemarks: function (args) {
            return commonService.PostData(args, document.MeterReadingRemarks + 'DeleteMeterReadingRemarks');
        },
        ToggleMeterReadingRemarksStatus: function (args) {
            return commonService.PostData(args, document.MeterReadingRemarks + 'ToggleMeterReadingRemarksStatus');
        }
    };
}]);

