angular.module("MetronicApp")
    .factory('PoleService', ['CommonService', function (commonService) {
        return {
            PoleIndex: function (args) {
                return commonService.GetData(args, document.Pole + 'PoleIndex', null);
            },
            Delete: function (data) {
                return commonService.PostData(data, document.Pole + 'Delete');
            },
            TogglePoleActiveStatus: function (data) {
                return commonService.PostData(data, document.Pole + 'TogglePoleActiveStatus');
            },
            Form: function (data) {
                return commonService.PostData(data, document.Pole + 'Form');
            },
            GetPoleDetails: function (data) {
                return commonService.PostData(data, document.Pole + 'GetPoleDetails');
            },
            GeneratePolenumber: function (data) {
                return commonService.PostData(data, document.Pole + 'GeneratePolenumber');
            },
            ExportDataToExcelFile: function (args) {
                return commonService.GetData(args, document.Pole + 'ExportDataToExcelFile', null);
            },
            GetAllPole: function (args, id) {
                return commonService.GetData(args, document.Pole + 'GetAllPole', id);
            },
            GetPolesByCityTownId: function (args, id) {
                return commonService.GetData(args, document.Pole + 'GetPolesLookUp', id);
            },
            GetLocationById: function (args) {
                return commonService.GetData(args, document.Pole + 'GetLocationById', null);
            },
            GetPolesLookUpInMasterItem: function (args) {
                return commonService.PostData(args, document.ItemMasterData + 'GetPolesLookUpInMasterItem');
            },
            GetByPoleNo: function (data) {
                return commonService.GetData(data, document.Pole + 'GetByPoleNo', null);
            },
        };
    }]);