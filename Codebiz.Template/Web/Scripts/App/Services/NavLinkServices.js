angular.module("MetronicApp").factory('NavLinkServices', ['CommonService', function (commonService) {
    return {
        SearchNavLink: function (args) {
            return commonService.GetData(args, document.NavLink + 'SearchNavLink', null);
        },
        DeleteNavLink: function (args) {
            return commonService.PostData(args, document.NavLink + 'Delete', null);
        },
        ExportDataToExcelFile: function (args) {
            return commonService.GetData(args, document.NavLink + 'ExportDataToExcelFile');
        }
    };
}]);