angular.module("MetronicApp").factory('AppUserService', ['CommonService', function (commonService) {
    return {
        GetAppUserLookUp: function (data) {
            return commonService.GetData(data, document.AppUsers + 'GetAppUserLookUp');
        },
        SaveAppUserPhoto: function (data) {
            return commonService.PostData(data, document.AppUsers + 'SaveAppUserPhoto');
        },
        SaveAppUser: function (data) {
            return commonService.PostData(data, document.AppUsers + 'SaveAppUser');
        },
        AddAppUserPhoto: function (data) {
            return commonService.PostData(data, document.AppUsers + 'AddAppUserPhoto');
        },
        UploadAppUserPhoto: function (data) {
            return commonService.PostData(data, document.FileUpload + 'UploadAppUserPhoto');
        },
        UploadAppUserSignature: function (data) {
            return commonService.PostData(data, document.FileUpload + 'UploadAppUserSignature');
        },
        GetAppUsersDetails: function (data) {
            return commonService.GetData(data, document.AppUsers + 'GetAppUsersDetails');
        },
        GetEditPhoto: function (data) {
            return commonService.GetData(data, document.AppUsers + 'GetEditPhoto');
        },
        GetEditSignature: function (data) {
            return commonService.GetData(data, document.AppUsers + 'GetEditSignature');
        },
        GetAllUserGroup: function (args) {
            return commonService.GetData(args, document.AppUsers + 'GetAllUserGroup');
        },
        GetEmployeeByCode: function (args) {
            return commonService.GetData(args, document.Employees + 'GetEmployeeByCode');
        },
        GetAppUsersDetailsById: function (data) {
            return commonService.GetData(data, document.AppUsers + 'GetAppUsersDetailsById');
        }
    };
}]);