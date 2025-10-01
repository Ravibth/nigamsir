using RMT.Notification.Domain.DTO;
using RMT.Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<NotificationTemplate> AddAsync(NotificationTemplate entity);

        Task<List<NotificationTemplate>> GetAllAsync();

        //custom operations here
        Task<NotificationTemplate> GetNotificationTemplateById(Int64 id);

        Task<List<UserNotification>> GetLoggedInUserNotifications(string email, Int64 limit, Int64 pagination, Int16 pushNotificationFromLastDays);

        Task<List<UserNotification>> PostNewNotification(string[] users, PostNewNotificationRequestDTO request);

        Task<bool> MarkNotificationsAsRead(Guid[] id);

        Task<bool> MarkAllNotificationsAsRead(string email);
        Task<List<NotificationTemplate>> GetNotificationTemplate(string[] type);

        Task<int> GetLoggedInUserAllNotificationsCount(string email);

        Task<NotificationSubscription> SubmitNotificationSubscriptionDTO(NotificationSubscription entity);

        Task<List<NotificationSubscription>> GetNotificationSubscriptionDTO(NotificationSubscription entity);

        Task<bool> SeedNotificationToDB(List<NotificationTemplate> entity);
        Task<bool> AddNotificationLog(List<NotificationLog> entity);

        Task<List<NotificationSubscription>> GetNotificationSubscriptionByModuleAndSubscription(string moduleName, string subscription_role);

    }
}
