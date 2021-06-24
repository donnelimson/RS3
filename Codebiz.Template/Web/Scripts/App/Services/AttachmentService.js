angular.module("MetronicApp").factory('AttachmentService', ['CommonService', function (commonService) {
    return {
        GetAttachmentsByHistoryId: function (args) {
            return commonService.GetData(args, document.CSADiscountApplication + 'GetAttachmentsByHistoryId');
        },
        GetAttachmentsByDiscountApplicationId: function (args) {
            return commonService.GetData(args, document.CSADiscountApplication + 'GetAttachmentsByDiscountApplicationId');
        }
    };
}]);