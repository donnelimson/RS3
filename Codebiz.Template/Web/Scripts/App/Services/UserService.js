angular.module("MetronicApp").factory('UserService', ['CommonService', function (commonService) {
    return {
        SearchAppUser: function (args) {
            return commonService.GetData(args, document.AppUsers + 'SearchAppUser');
        },
        GetAppUserStatuses: function (args, id) {
            return commonService.GetData(args, document.AppUsers + 'GetAppUserStatuses', id);
        },
        AddAppUser: function (data) {
            return commonService.PostData(data, document.AppUsers + 'AddAppUser');
        },
        UpdateAppUser: function (args) {
            return commonService.PostData(args, document.AppUsers + 'UpdateAppUser');
        },
        ResendActivationLink: function (args) {
            return commonService.PostData(args, document.AppUsers + 'ResendActivationLink');
        },
        SendResetPasswordLink: function (args) {
            return commonService.PostData(args, document.AppUsers + 'SendResetPasswordLink');
        },
        SendUnlockingAccountLink: function (args) {
            return commonService.PostData(args, document.AppUsers + 'SendUnlockingAccountLink');
        },
        ToggleAppUserActiveStatus: function (args) {
            return commonService.PostData(args, document.AppUsers + 'ToggleAppUserActiveStatus');
        },
        ReactivateDeactivateAppUser: function (callbackFunction, name, selection) {
            swal({
                title: (selection === 1 ? "Reactivate" : "Deactivate") + " Record?",
                text: "User <b>" + name + "</b> will be " + (selection === 1 ? "reactivated" : "deactivated") + ".",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
                confirmButtonText: selection === 1 ? "Reactivate" : "Deactivate",
                closeOnConfirm: true,
                html: true
            }, callbackFunction
            );
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.AppUsers + 'ExportDataToExcelFile', null);
        }
    };
}]);