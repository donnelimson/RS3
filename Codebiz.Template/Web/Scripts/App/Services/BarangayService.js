
angular.module("MetronicApp").
    factory('BarangayService', ['CommonService', function (commonService) {
        return {
            Search: function (args) {
                return commonService.GetData(args, document.Barangay + 'Search', null);
            },
            ReactivateDeactivate: function (data) {
                return commonService.PostData(data, document.Barangay + 'ReactivateDeactivate');
            },
            Delete: function (data) {
                return commonService.PostData(data, document.Barangay + 'Delete');
            },
            AddOrUpdate: function (data) {
                return commonService.PostData(data, document.Barangay + 'AddOrUpdate');
            },
            GetBarangayDetails: function (data) {
                return commonService.PostData(data, document.Barangay + 'GetBarangayDetails');
            },
            ExportDataToExcelFile: function (args) {
                return commonService.GetData(args, document.Barangay + 'ExportDataToExcelFile', null);
            },
        }
    }]);