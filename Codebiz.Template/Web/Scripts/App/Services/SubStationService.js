angular.module("MetronicApp")
    .factory('SubStationService', ['CommonService', function (commonService) {
        return {
            SearchSubStation: function (args) {
                return commonService.GetData(args, document.SubStation + 'SearchSubStation');
            },
            GetSubStationLookUp: function (args,id) {
                return commonService.GetData(args, document.SubStation + 'GetSubStationLookUp',id);
            },
            AddSubStation: function (data) {
                return commonService.PostData(data, document.SubStation + 'AddSubStation');
            },
            UpdateSubStation: function (data) {
                return commonService.PostData(data, document.SubStation + 'UpdateSubStation');
            },
            DeleteSubStationById: function (data) {
                return commonService.PostData(data, document.SubStation + 'DeleteSubStationById');
            },
            DeactivateReactivate: function (data) {
                return commonService.PostData(data, document.SubStation + 'DeactivateReactivate');
            },
            GetSubStationDetailsForUpdateById: function (data) {
                return commonService.GetData(data, document.SubStation + 'GetSubStationDetailsForUpdateById');
            },
            ExportDataToExcelFile: function (args) {
                return commonService.GetData(args, document.SubStation + 'ExportDataToExcelFile');
            },
        };
    }]);