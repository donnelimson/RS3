angular.module("MetronicApp").factory('OfficeService',['CommonService', function (commonService) {
    return {

        GetOffices: function (args) {
            return commonService.GetData(args, document.Office + 'GetOffice', null);
        },

        ToggleOfficeActiveStatus: function (data) {
            return commonService.PostData(data, document.Office + 'ToggleOfficeActiveStatus', null);
        },

        AddOffice: function (data) {
            return commonService.PostData(data, document.Office + 'AddOffice', null);
        },

        UpdateOffice: function (data) {
            return commonService.PostData(data, document.Office + 'UpdateOffice', null);
        },

        officeDelete: function (data) {
            return commonService.PostData(data, document.Office + 'DeleteOffice', null);
        },
        
        GetEditOffices: function (data) {
            return commonService.PostData(data, document.Office + 'GetOfficeDetails', null);
        },

        GetOfficesLookUp: function (data,id) {
            return commonService.GetData(data, document.Office + 'GetOfficesLookUp', id);
        },

        ExportDataToExcelFile: function (data) {
            return commonService.PostData(data, document.Office + 'ExportDataToExcelFile', null);
        },
        CheckIfHasMainOffice: function (params) {
            return commonService.GetData(params, document.Office + 'CheckIfHasMainOffice');
        },
    }
}]);