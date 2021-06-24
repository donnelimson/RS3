angular.module("MetronicApp").factory('ProvinceService', ['CommonService', function (commonService) {
    return {
        GetProvinces: function (args) {
            return commonService.GetData(args, document.Province + 'GetProvinces', null);
        },
        ToggleProvinceActiveStatus: function (data) {
            return commonService.PostData(data, document.Province + 'ToggleProvinceActiveStatus');
        },
        AddProvince: function (data) {
            return commonService.PostData(data, document.Province + 'AddProvince');
        },
        UpdateProvince: function (data) {
            return commonService.PostData(data, document.Province + 'UpdateProvince');
        },
        provinceDelete: function (data) {
            return commonService.PostData(data, document.Province + 'DeleteProvince');
        },
        GetEditProvinces: function (args) {
            return commonService.GetData(args, document.Province + 'GetProvinceDetails', null);
        },
        GetProvincesLookUp: function (args) {
            return commonService.GetData(args, document.Province + 'GetProvincesLookUp', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Province + 'ExportDataToExcelFile');
        }
    };
}]);