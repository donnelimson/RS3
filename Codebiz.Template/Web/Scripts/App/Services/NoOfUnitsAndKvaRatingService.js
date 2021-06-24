angular.module("MetronicApp").factory('NoOfUnitsAndKvaRatingService', ['CommonService', function (commonService) {
    return {
        GetNoOfUnitsAndKvaRating: function (args) {
            return commonService.GetData(args, document.NoOfUnitsAndKvaRating + 'GetNoOfUnitsAndKvaRating');
        },
        AddUpdateNoOfUnitsAndKvaRating: function (data) {
            return commonService.PostData(data, document.NoOfUnitsAndKvaRating + 'AddOrUpdateNoOfUnitsAndKvaRating');
        },
        ToggleNoOfUnitsAndKvaRatingActiveStatus: function (args) {
            return commonService.PostData(args, document.NoOfUnitsAndKvaRating + 'ToggleNoOfUnitsAndKvaRatingActiveStatus');
        },
        GetEditNoOfUnitsAndKvaRating: function (data) {
            return commonService.GetData(data, document.NoOfUnitsAndKvaRating + 'GetNoOfUnitsAndKvaRatingDetails');
        },
        DeleteNoOfUnitsAndKvaRating: function (data) {
            return commonService.PostData(data, document.NoOfUnitsAndKvaRating + 'DeleteNoOfUnitsAndKvaRating');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.NoOfUnitsAndKvaRating + 'ExportDataToExcelFile');
        },
    };
}]);