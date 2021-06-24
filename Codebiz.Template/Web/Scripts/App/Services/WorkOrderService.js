
angular.module("MetronicApp").factory('WorkOrderService', ['CommonService', function (commonService) {
    return {
        GetWorkOrder: function (args) {
            return commonService.GetData(args, document.WorkOrder + 'GetWorkOrder');
        },
        AddWorkOrder: function (data) {
            return commonService.PostData(data, document.WorkOrder + 'AddWorkOrder');
        },
        UpdateWorkOrder: function (data) {
            return commonService.PostData(data, document.WorkOrder + 'UpdateWorkOrder');
        },
        ToggleWorkOrderActiveStatus: function (data) {
            return commonService.PostData(data, document.WorkOrder + 'ToggleWorkOrderActiveStatus');
        },
        workOrderDelete: function (data) {
            return commonService.PostData(data, document.WorkOrder + 'DeleteWorkOrder');
        },
        GetWorkOrderDetails: function (args) {
            return commonService.PostData(args, document.WorkOrder + 'GetWorkOrderDetails');
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.WorkOrder + 'ExportDataToExcelFile');
        }
    };
}]);
