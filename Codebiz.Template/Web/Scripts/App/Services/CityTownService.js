
angular.module("MetronicApp")
    .factory('CityTownService', ['CommonService', function (commonService) {
        return {
            Search: function (args) {
                return commonService.GetData(args, document.CityTown + 'Search', null);
            },
            GetCityTownDetails: function (data) {
                return commonService.PostData(data, document.CityTown + 'GetCityTownDetails');
            },
            AddOrUpdate: function (data) {
                return commonService.PostData(data, document.CityTown + 'InsertOrUpdate');
            },
            ReactivateDeactivate: function (data) {
                return commonService.PostData(data, document.CityTown + 'ReactivateDeactivate');
            },
             Delete: function (data) {
                return commonService.PostData(data, document.CityTown + 'Delete');
            },
            GetAllCityTown: function (data) {
                 return commonService.PostData(data, document.CityTown + 'GetAllCityTown');
             },
            ExportDataToExcelFile: function (args) {
                return commonService.GetData(args, document.CityTown + 'ExportDataToExcelFile', null);
            },
        };
    }]);