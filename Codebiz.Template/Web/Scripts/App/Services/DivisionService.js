angular.module("MetronicApp").factory('DivisionService', ['CommonService', function (commonService) {
    return {
        SearchDivision: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/Division/GetList');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Division + 'ExportDataToExcelFile');
        },
        AddUpdateDivision: function (args) {
            return commonService.PostData(args, document.Division + 'AddUpdateDivision');
        },
        GetDetailsForUpdate: function (args, id) {
            return commonService.GetData(args, document.baseUrlNoArea + 'api/Division/GetDetailsForUpdate/' + id);
        },
        ToggleDivisionStatus: function (args) {
            return commonService.PostData(args, document.Division + 'ToggleDivisionStatus');
        },
        DeleteDivision: function (args) {
            return commonService.PostData(args, document.Division + 'DeleteDivision');
        },
        GetCategories: function (args) {
            return commonService.GetData(args, document.Division + 'GetAllDivisionTypes');
        }
    };
}]);
