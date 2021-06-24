angular.module("MetronicApp").factory('BillingUnbundledTransactionService',['CommonService', function (commonService) {
    return {
        GetBillingUnbundledTransactions: function (args) {
            return commonService.GetData(args, document.BillingUnbundledTransaction + 'GetBillingUnbundledTransaction', null);
        },

        GetBillingUnbundledTransactionByCategory: function (args) {
            return commonService.GetData(args, document.BillingUnbundledTransaction + 'GetBillingUnbundledTransactionByCategory', null);
        },

        GetbillingUnbundledTransactionLookUp: function (args) {
            return commonService.GetData(args, document.BillingUnbundledTransaction + 'GetbillingUnbundledTransactionLookUp', null);
        },

        ToggleBillingUnbundledTransactionActiveStatus: function (data) {
            return commonService.PostData(data, document.BillingUnbundledTransaction + 'ToggleBillingUnbundledTransactionActiveStatus', null);
        },

        billingUnbundledTransactionDelete: function (data) {
            return commonService.PostData(data, document.BillingUnbundledTransaction + 'DeleteBillingUnbundledTransaction', null);
        },

        GetCategories: function (args,id) {
            return commonService.GetData(args, document.BillingUnbundledTransaction + 'GetCategories', id);
        },

        AddBillingUnbundledTransaction: function (data) {
            return commonService.PostData(data, document.BillingUnbundledTransaction + 'AddBillingUnbundledTransaction', null);
        },

        UpdateBillingUnbundledTransaction: function (data) {
            return commonService.PostData(data, document.BillingUnbundledTransaction + 'UpdateBillingUnbundledTransaction', null);
        },

        GetBillingUnbundledTransactionDetails: function (data) {
            return commonService.PostData(data, document.BillingUnbundledTransaction + 'GetBillingUnbundledTransactionDetails', null);
        },

        GetBillingUnbundledTransactionCategoryDetails: function (data) {

            return commonService.PostData(data, document.BillingUnbundledTransaction + 'GetBillingUnbundledTransactionCategoryDetails', null);
        },

        ExportDataToExcelFile: function (data) {
            return commonService.PostData(data, document.BillingUnbundledTransaction + 'ExportDataToExcelFile', null);
        },
        
        GetBillingUnbundledTransactionByCode: function (data) {
            return commonService.PostData(data, document.BillingUnbundledTransaction + 'GetBillingUnbundledTransactionByCode', null);
        },
        GetUnbundledTransactionByType: function (data) {
            return commonService.GetData(data, document.BillingUnbundledTransaction + 'GetUnbundledTransactionByType', null);
        },
        GetUnbundledTransactionByIdForLookUp: function (data) {
            return commonService.GetData(data, document.BillingUnbundledTransaction + 'GetUnbundledTransactionByIdForLookUp', null);
        },
        GetUnbundledTransactionForVatDetailsById: function (data) {
            return commonService.GetData(data, document.BillingUnbundledTransaction + 'GetUnbundledTransactionForVatDetailsById', null);
        },
        GetUnbundledTransactionForDiscountDetailsById: function (data) {
            return commonService.GetData(data, document.BillingUnbundledTransaction + 'GetUnbundledTransactionForDiscountDetailsById', null);
        },
        CheckIfICERAExist: function (data) {
            return commonService.GetData(data, document.BillingUnbundledTransaction + 'CheckIfICERAExist', null);
        },
        CheckIfGRAMExist: function (data) {
            return commonService.GetData(data, document.BillingUnbundledTransaction + 'CheckIfGRAMExist', null);
        },
 


    }

}]);