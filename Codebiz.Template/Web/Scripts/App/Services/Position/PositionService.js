angular.module("MetronicApp").factory('PositionService', ['CommonService', function (commonService) {
    return {
        GetPositions: function (args) {
            return commonService.GetData(args, document.Position + 'GetPosition', null);
        },

        TogglePositionActiveStatus: function (data) {
            return commonService.PostData(data, document.Position + 'TogglePositionActiveStatus', null);
        },

        AddPosition: function (data) {
            return commonService.PostData(data, document.Position + 'AddPosition', null);
        },

        UpdatePosition: function (data) {
            return commonService.PostData(data, document.Position + 'UpdatePosition', null);
        },

        positionDelete: function (data) {
            return commonService.PostData(data, document.Position + 'DeletePosition', null);
        },

        GetEditPosition: function (data) {
            return commonService.PostData(data, document.Position + 'GetPositionDetails', null);
        },

        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.Position + 'ExportDataToExcelFile');
        }
    };
}]);