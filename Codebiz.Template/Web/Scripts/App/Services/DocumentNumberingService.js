angular.module("MetronicApp")
    .factory('DocumentNumberingService', ['CommonService', function (commonService) {
        return {
            UpdateDocumentNumbering: function (data) {
                return commonService.PostData(data, document.DocumentNumbering + 'UpdateDocumentNumbering');
            },
            GetAllTransactionTypeForDocumentNumbering: function (args,id) {
                return commonService.GetData(args, document.DocumentNumbering + 'GetAllTransactionTypeForDocumentNumbering',id);
            },
            Search: function (args) {
                return commonService.GetData(args, document.DocumentNumbering + 'Search');
            },
            ExportDataToExcelFile: function (args) {
                return commonService.GetData(args, document.DocumentNumbering + 'ExportDataToExcelFile');
            },
            AddOrUpdate: function (data) {
                return commonService.PostData(data, document.DocumentNumbering + 'AddOrUpdateDocumentNumbering');
            },
            GetDetailsById: function (args) {
                return commonService.GetData(args, document.DocumentNumbering + 'GetDetailsById');
            },
            GetDetails: function (args) {
                return commonService.GetData(args, document.DocumentNumbering + 'GetDetails');
            },
            GetAllTransactionTypeForDocumentNumberingNotYetUsed: function (args) {
                return commonService.GetData(args, document.DocumentNumbering + 'GetAllTransactionTypeForDocumentNumberingNotYetUsed');
            },
          
        };

    }]);