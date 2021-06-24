angular.module("MetronicApp").factory('LifelineSubsidyService', ['CommonService', function (commonService) {
    return {
        GetLifelineSubsidy: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/Controller/LifelineSubsidy/GetList');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.LifelineSubsidy + 'ExportDataToExcelFile', null);
        },
        DeleteLifelineSubsidy: function (data) {
            return commonService.GetData(data, document.baseUrlNoArea + 'api/Controller/LifelineSubsidy/Delete/' + data.id, null);
        },
        ToggleActiveStatus: function (data) {
            return commonService.GetData(data, document.baseUrlNoArea + 'api/Controller/LifelineSubsidy/ToggleActiveStatus/' + data.id, null);
        },
        SaveData: function (args) {
            return commonService.PostData(args.model, document.baseUrlNoArea + 'api/Controller/LifelineSubsidy/SaveData');
        },
        GetDetails: function (data) {
            return commonService.GetData(data, document.baseUrlNoArea + 'api/Controller/LifelineSubsidy/GetDetails/' + data.id, null);
        }
    };
}]);