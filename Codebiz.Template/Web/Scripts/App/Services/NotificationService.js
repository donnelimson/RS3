angular.module("MetronicApp").factory('NotificationService', ['CommonService', function (commonService) {
    return {
        GetNotifications: function (args) {
            return commonService.GetData(args, document.Notification + 'GetNotifications', null);
        },
        MarkNotificationAllAsRead: function (args) {
            return commonService.GetData(args, document.Notification + 'MarkNotificationAllAsRead', null);
        },
        MarkAsRead: function (args) {
            return commonService.PostData(args, document.Notification + 'MarkAsRead', null);
        }
    };
}]);