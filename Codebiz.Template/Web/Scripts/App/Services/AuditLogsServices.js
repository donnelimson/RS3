angular.module("MetronicApp").factory('AuditLogsServices', ['CommonService', function (commonService) {
    return {
        SearchAuditLogs: function (args) {
            return commonService.GetData(args, document.AuditLogs + 'SearchAuditLogs', null);
        },
        GetEventTitles: function (args) {
            return commonService.GetData(args, document.AuditLogs + 'GetEventTitles', null);
        },
        GetAuditLogDetails: function (args) {
            return commonService.PostData(args, document.AuditLogs + 'GetAuditLogDetails', null);
        }
    };
}]);