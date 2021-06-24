angular.module("MetronicApp").factory('BillingUnitService', ['CommonService', function (commonService) {
    return {
        GetUnits: function (args) {
            return commonService.GetData(args, document.BillingUnit + 'GetUnits');
        },
        AddUnits: function (data) {
            return commonService.PostData(data, document.BillingUnit + 'AddUnits');
        },
        UpdateUnits: function (data) {
            return commonService.PostData(data, document.BillingUnit + 'UpdateUnits');
        },
        GetUnitsDetails: function (args) {
            return commonService.GetData(args, document.BillingUnit + 'GetUnitsDetails');
        },
        GetAllPositionsByOfficeId: function (args) {
            return commonService.GetData(args, document.BillingUnit + 'GetAllPositionsByOfficeId');
        },
        unitsDelete: function (data) {
            return commonService.PostData(data, document.BillingUnit + 'DeleteUnits');
        },
        ToggleUnitsActiveStatus: function (data) {
            return commonService.PostData(data, document.BillingUnit + 'ToggleUnitsActiveStatus');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.BillingUnit + 'ExportDataToExcelFile');
        }
    };
}]);