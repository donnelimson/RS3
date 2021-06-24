using Codebiz.Domain.Common.Model.DataModel.Notification;
using Codebiz.Domain.Repository;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface INotificationRepository : IRepositoryBase<Notification>
    {
        void UpdateDetail(NotificationDetail entity);
        Notification GetById(int id);
        NotificationDetail GetDetailById(int id);
        IEnumerable<Notification> GetAllNotifications();
        IEnumerable<NotificationDetail> GetAllNotificationDetailsByUserName(string userName);
        IEnumerable<NotificationDetail> GetAllNotificationDetailsByUserId(int appUserId);
        int GetPendingApprovalCountByAppUserId(int currentUserId);
        int GetPendingHwiCountByAppUserId(int employeeId, bool hasOverride);
        Notification GetNotification(string destination, string transaction, int referenceId, int userId);
        int GetPendingForConnectionCountByAppUserId(int currentUser);
        int? GetPendingIssueForConnectionCountByAppUserId(int employeeId);
        int GetPendingForDisconnectionCountByAppUserId(int currentUser);
        int GetPendingProcessJobOrderCountByAppUserId(int currentUser);
        int GetPendingForAssignedJobOrderCountByAppUserId(int currentUser);
    }
}
