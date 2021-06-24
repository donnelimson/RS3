angular.module("MetronicApp").factory('UserGroupService', ['CommonService', function (commonService) {
    return {
        UserGroupIndex: function (args) {
            return commonService.GetData(args, document.UserGroup + 'UserGroupIndex', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.UserGroup + 'ExportDataToExcelFile');
        },
        Delete: function (args) {
            return commonService.PostData(args, document.UserGroup + 'Delete');
        },
        DeactivateActivate: function (args) {
            return commonService.PostData(args, document.UserGroup + 'DeactivateActivate');
        }
    };
}]);
