angular.module("MetronicApp").factory('SupportingDocumentsService', ['CommonService', function (commonService) {
    return {
        SearchSupportingDocument: function (data) {
            return commonService.GetData(data, document.SupportingDocuments + 'SearchSupportingDocument');
        },
        AddSupportingDocument: function (data) {
            return commonService.PostData(data, document.SupportingDocuments + 'AddSupportingDocument');
        },
        UpdateSupportingDocument: function (data) {
            return commonService.PostData(data, document.SupportingDocuments + 'UpdateSupportingDocument');
        },
        GetSupportingDocumentDetailsForUpdateById: function (data) {
            return commonService.GetData(data, document.SupportingDocuments + 'GetSupportingDocumentDetailsForUpdateById');
        },
        DeleteSupportingDocumentById: function (data) {
            return commonService.PostData(data, document.SupportingDocuments + 'DeleteSupportingDocumentById');
        },
        GetSupportingDocumentDetailsById: function (data) {
            return commonService.GetData(data, document.SupportingDocuments + 'GetSupportingDocumentDetailsById');
        },
        ExportDataToExcelFile: function (data) {
            return commonService.GetData(data, document.SupportingDocuments + 'ExportDataToExcelFile');
        }
    };
}]);