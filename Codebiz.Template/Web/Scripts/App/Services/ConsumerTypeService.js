angular.module("MetronicApp").factory('ConsumerTypeService', ['CommonService', function (commonService) {
    return {
        ConsumerTypeIndex: function (args) {
            return commonService.GetData(args, document.ConsumerType + 'ConsumerTypeIndex');
        },
        AddOrUpdate: function (data) {
            return commonService.PostData(data, document.ConsumerType + 'AddOrUpdate');
        },
        GetConsumerTypeDetails: function (args) {
            return commonService.GetData(args, document.ConsumerType + 'GetConsumerTypeDetails');
        },
        Delete: function (data) {
            return commonService.PostData(data, document.ConsumerType + 'Delete');
        },
        ToggleConsumerTypeActiveStatus: function (data) {
            return commonService.PostData(data, document.ConsumerType + 'ToggleConsumerTypeActiveStatus');
        },
        Reactivate: function (data) {
            return commonService.PostData(data, document.ConsumerType + 'Reactivate');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.ConsumerType + 'ExportDataToExcelFile');
        }
    };
}]);