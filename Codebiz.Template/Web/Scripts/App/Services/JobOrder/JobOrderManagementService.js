angular.module("MetronicApp").factory('JobOrderManagementService', ['CommonService', function (commonService) {
    return {
        GetJobOrderNatureTypeList: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/APIController/JobOrder/GetJobOrderNatureTypeList', null);
        },
        GetJobOrderTaskToPerformList: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/APIController/JobOrder/GetJobOrderTaskToPerformList', null);
        },
        GetJobOrderNatureTypes: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/APIController/JobOrder/GetJobOrderNatureTypes', null);
        },
        GetJobOrderNatureType: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/APIController/JobOrder/JobOrderNatureType', null);
        },
        GetJobOrderNatureTypesForSelect: function (args) {
            return commonService.GetData(args, document.baseUrlNoArea + 'api/JobOrder/NatureType/GetList', null);
        },
        GetJobOrderTaskToPerforms: function (args) {
            return commonService.PostData(args, document.baseUrlNoArea + 'api/APIController/JobOrder/GetJobOrderTaskToPerforms', null);
        },
        ToggleActiveStatus: function (args) {
            return commonService.PostData(args, document.JobOrderManagement + 'ToggleJobOrderTypeActiveStatus', null);
        },
        SaveTaskToBePerform: function (data) {
            return commonService.PostData(data, document.JobOrderManagement + 'SaveTaskToBePerform');
        },
        SaveNatureType: function (data) {
            return commonService.PostData(data, document.JobOrderManagement + 'SaveNatureType');
        },
        ExportJONatureTypeDataToExcelFile: function (args) {
            return commonService.GetData(args, document.JobOrderManagement + 'ExportJONatureTypeDataToExcelFile', null);
        },
        ExportJOTaskToBePerformDataToExcelFile: function (args) {
            return commonService.GetData(args, document.JobOrderManagement + 'ExportJOTaskToBePerformDataToExcelFile', null);
        }
    };
}]);