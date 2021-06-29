
angular.module("MetronicApp")
    .factory('ChooseFromListService', ['CommonService', function (commonService) {
        return {
            GetAllAppuserForCFL: function (args) {
                return commonService.PostData(args, document.AppUsers + 'GetAllAppuserForCFL', null);
            },
        };
    }]);