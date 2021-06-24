angular.module("MetronicApp").factory('BillingCategoryService',
    ['CommonService', function (commonService) {
            return {
                GetCategories: function (args) {
                    return commonService.GetData(args, document.BillingCategory + 'GetCategories', null);
                },

                GetAllCategories: function (args) {
                    return commonService.GetData(args, document.BillingCategory + 'GetAllCategories', null);
                },

                ToggleCategoryActiveStatus: function (data) {
                    return commonService.PostData(data, document.BillingCategory + 'ToggleCategoryActiveStatus', null);
                },

                categoryDelete: function (data) {
                    return commonService.PostData(data, document.BillingCategory + 'DeleteCategory', null);
                },

                AddCategory: function (data) {
                    return commonService.PostData(data, document.BillingCategory + 'AddCategory', null);
                },

                ExportDataToExcelFile: function (data) {
                    return commonService.PostData(data, document.BillingCategory + 'ExportDataToExcelFile', null);
                }

            }
        }]);