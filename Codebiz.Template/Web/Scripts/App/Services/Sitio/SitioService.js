angular.module("MetronicApp").factory('SitioService', ['CommonService', function (commonService) {
    return {
        GetSitios: function (args) {
            return commonService.GetData(args, document.Sitio + 'GetSitio', null);
        },
        ToggleSitioActiveStatus: function (data) {
            return commonService.PostData(data, document.Sitio + 'ToggleSitioActiveStatus');
        },
        AddSitio: function (data) {
            return commonService.PostData(data, document.Sitio + 'AddSitio');
        },
        UpdateSitio: function (data) {
            return commonService.PostData(data, document.Sitio + 'UpdateSitio');
        },
        sitioDelete: function (data) {
            return commonService.PostData(data, document.Sitio + 'DeleteSitio');
        },
        GetEditSitios: function (args) {
            return commonService.PostData(args, document.Sitio + 'GetSitioDetails', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Sitio + 'ExportDataToExcelFile', null);
        }
    };
}]);