angular.module("MetronicApp").factory('ComplaintTypeService', ['CommonService', function (commonService) {
    return {
        SearchComplaintType: function (args) {
            return commonService.GetData(args, document.ComplaintType + 'SearchComplaintType');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.ComplaintType + 'ExportDataToExcelFile');
        },
        ToggleComplaintTypeActiveStatus: function (data) {
            return commonService.PostData(data, document.ComplaintType + 'ToggleComplaintTypeActiveStatus');
        },
        DeleteComplaintType: function (data) {
            return commonService.PostData(data, document.ComplaintType + 'DeleteComplaintType');
        },
        GetComplaintTypeDetailsForUpdateById: function (data) {
            return commonService.GetData(data, document.ComplaintType + 'GetComplaintTypeDetailsForUpdateById');
        },
        AddUpdateComplaintType: function (data) {
            return commonService.PostData(data, document.ComplaintType + 'AddUpdateComplaintType');
        },
        GetNatureTypes: function (params, id) {
            return commonService.GetData(params, document.ComplaintType + 'GetNatureTypeLookUp', id);
        },
    };
}]);
