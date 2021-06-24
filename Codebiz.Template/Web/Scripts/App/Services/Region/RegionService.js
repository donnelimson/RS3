angular.module("MetronicApp").factory('RegionService', ['CommonService', function (commonService) {
    return {
        ToggleRegionActiveStatus: function (data) {
            return commonService.PostData(data, document.Region + 'ToggleRegionActiveStatus');
        },
        AddRegion: function (data) {
            return commonService.PostData(data, document.Region + 'AddRegion');
        },
        GetRegions: function (args) {
            return commonService.GetData(args, document.Region + 'GetRegions', null);
        },
        UpdateRegion: function (data) {
            return commonService.PostData(data, document.Region + 'UpdateRegion');
        },
        regionDelete: function (data) {
            return commonService.PostData(data, document.Region + 'DeleteRegion');
        },
        GetEditRegions: function (args) {
            return commonService.GetData(args, document.Region + 'GetRegionDetails', null);
        },
        GetRegionsLookUp: function (args) {
            return commonService.GetData(args, document.Region + 'GetRegionLookUp', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.Region + 'ExportDataToExcelFile', null);
        }
    };
}]);