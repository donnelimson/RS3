angular.module("MetronicApp").factory('PurokService', ['CommonService', function (commonService) {
    return {
        GetPuroks: function (data) {
            return commonService.GetData(data, document.Purok + 'GetPuroks', null);
        },
        TogglePurokActiveStatus: function (data) {
            return commonService.PostData(data, document.Purok + 'TogglePurokActiveStatus');
        },
        AddPurok: function (data) {
            return commonService.PostData(data, document.Purok + 'AddPurok');
        },
        UpdatePurok: function (data) {
            return commonService.PostData(data, document.Purok + 'UpdatePurok');
        },
        purokDelete: function (data) {
            return commonService.PostData(data, document.Purok + 'DeletePurok');
        },
        GetEditPurok: function (data) {
            return commonService.GetData(data, document.Purok + 'GetPurokDetails', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Purok + 'ExportDataToExcelFile', null);
        }
    };
}]);