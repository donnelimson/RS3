angular.module("MetronicApp").factory('ApprovalStageService', ['CommonService', function (commonService) {
    return {
        SearchApprovalStage: function (args) {
            return commonService.GetData(args, document.ApprovalStage + 'SearchApprovalStage');
        },
        GetApprovalStageLabels: function (args) {
            return commonService.GetData(args, document.ApprovalStage + 'GetApprovalStageLabels');
        },
        GetApprovalStageDetailsById: function (args) {
            return commonService.GetData(args, document.ApprovalStage + 'GetApprovalStageDetailsById');
        },
        GetApproversByApprovalStageId: function (args) {
            return commonService.GetData(args, document.ApprovalStage + 'GetApproversByApprovalStageId');
        },
        AddApprovalStage: function (data) {
            return commonService.PostData(data, document.ApprovalStage + 'AddApprovalStage');
        },
        UpdateApprovalStage: function (data) {
            return commonService.PostData(data, document.ApprovalStage + 'UpdateApprovalStage');
        },
        DeleteApprovalStageById: function (data) {
            return commonService.PostData(data, document.ApprovalStage + 'DeleteApprovalStageById');
        },
        ToggleApprovalStageActiveStatus: function (data) {
            return commonService.PostData(data, document.ApprovalStage + 'ToggleApprovalStageActiveStatus');
        },
        DeleteApprovalStage: function (callbackFunction, name) {
            swal({
                title: "Delete Record?",
                text: "<b>" + name + "</b> will be deleted.",
                type: "error",
                showCancelButton: true,
                confirmButtonColor: "#d9534f",
                confirmButtonText: "Delete",
                confirmButtonClass: 'btn-danger',
                closeOnConfirm: true,
                html: true
            }, callbackFunction
            );
        },
        ReactivateDeactivateApprovalStage: function (callbackFunction, name, isActive) {
            swal({
                title: (!isActive ? "Reactivate" : "Deactivate") + " Record?",
                text: "<b>" + name + "</b> will be " + (!isActive ? "reactivated" : "deactivated") + ".",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: "#0275d8",
                confirmButtonClass: 'btn-info',
                confirmButtonText: !isActive ? "Reactivate" : "Deactivate",
                closeOnConfirm: true,
                html: true
            }, callbackFunction
            );
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.ApprovalStage + 'ExportDataToExcelFile');
        }
    };
}]);