angular.module("MetronicApp").factory('RouteService', ['CommonService', function (commonService) {
    return {
        SearchRoute: function (args) {
            return commonService.GetData(args, document.Route + 'SearchRoute');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Route + 'ExportDataToExcelFile');
        },
        AddRoute: function (data) {
            return commonService.PostData(data, document.Route + 'AddRoute');
        },
        UpdateRoute: function (data) {
            return commonService.PostData(data, document.Route + 'UpdateRoute');
        },
        AddUpdate: function (data) {
            return commonService.PostData(data, document.Route + 'AddUpdate');
        },
        GetRouteDetails: function (args) {
            return commonService.GetData(args, document.Route + 'GetRouteDetails');
        },
        DeleteRoute: function (data) {
            return commonService.PostData(data, document.Route + 'DeleteRoute');
        },
        ToggleRouteActiveStatus: function (data) {
            return commonService.PostData(data, document.Route + 'ToggleRouteActiveStatus');
        }
    };
}]);
