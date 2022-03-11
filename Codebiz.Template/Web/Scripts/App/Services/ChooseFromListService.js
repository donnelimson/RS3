
angular.module("MetronicApp")
    .factory('ChooseFromListService', ['CommonService', function (commonService) {
        return {
            GetItemMasterLookUp: function (args) {
                return commonService.PostData(args, document.ItemMaster + 'GetItemMasterLookUp', null);
            },
            
        };
    }]);