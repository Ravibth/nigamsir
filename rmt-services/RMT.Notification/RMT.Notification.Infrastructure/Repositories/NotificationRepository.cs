using Microsoft.EntityFrameworkCore;
using RMT.Notification.Domain.DTO;
using RMT.Notification.Domain.Entities;
using RMT.Notification.Domain.Repositories;
using RMT.Notification.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Notification.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        protected readonly NotificationDbContext _DbContext;

        public NotificationRepository(NotificationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<NotificationTemplate> AddAsync(NotificationTemplate entity)
        {
            await _DbContext.Set<NotificationTemplate>().AddAsync(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AddNotificationLog(List<NotificationLog> log)
        {
            await _DbContext.Set<NotificationLog>().AddRangeAsync(log);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<NotificationTemplate>> GetAllAsync()
        {
            return await _DbContext.NotificationTemplates.ToListAsync();
        }

        public async Task<NotificationTemplate> GetNotificationTemplateById(long id)
        {
            return await _DbContext.NotificationTemplates.Where(m => m.Id == id).FirstAsync<NotificationTemplate>();
            throw new NotImplementedException();
        }


        public async Task<List<UserNotification>> GetLoggedInUserNotifications(string email, Int64 limit, Int64 pagination, Int16 pushNotificationFromLastDays)
        {
            DateTime fifteenDaysAgo = DateTime.Now.AddDays(pushNotificationFromLastDays);
            return await _DbContext.UserNotification
                .Where(m => m.email.ToLower().Trim().Equals(email.ToLower().Trim())
                && m.createdDate.ToUniversalTime() >= fifteenDaysAgo.ToUniversalTime()
                // && m.isRead == false

                )
                .Include(m => m.notification)
                .OrderByDescending(m => m.createdDate)
                .Skip(Convert.ToInt16((pagination - 1) * limit))
                .Take(Convert.ToInt16(limit))
                .ToListAsync();
        }

        public async Task<List<UserNotification>> PostNewNotification(string[] users, PostNewNotificationRequestDTO request)
        {
            try
            {
                var notificationEntity = new NotificationEntity()
                {
                    type = request.type,
                    message = request.message,
                    createdBy = request.createdBy,
                    createdDate = request.createdDate,
                    modifiedBy = request.createdBy,
                    modifiedDate = request.createdDate,
                    item_id = "",
                    meta = request.meta,
                    notification_template_id = request.notification_template_id,
                    link = request.link,
                    to = ""
                };
                var newlyAddedNotification = await _DbContext.Set<NotificationEntity>().AddAsync(notificationEntity);
                await _DbContext.SaveChangesAsync();

                var usersNotification = new List<UserNotification>();
                foreach (var user in users)
                {
                    usersNotification.Add(new UserNotification()
                    {
                        email = user,
                        employee_id = "",
                        notification_id = newlyAddedNotification.Entity.id,
                        isRead = false,
                        createdBy = request.createdBy,
                        createdDate = request.createdDate,
                        modifiedBy = request.createdBy,
                        modifiedDate = request.createdDate,
                    });
                }
                await _DbContext.Set<UserNotification>().AddRangeAsync(usersNotification);
                await _DbContext.SaveChangesAsync();

                var newNotifications = await _DbContext.UserNotification
                    .Where(m => m.notification_id.Equals(newlyAddedNotification.Entity.id))
                    .Include(m => m.notification)
                    .ToListAsync();
                return newNotifications;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> MarkNotificationsAsRead(Guid[] id)
        {
            var notificationsToMarkRead = await _DbContext.UserNotification.Where(m => id.Any(p => p.Equals(m.id))).ToListAsync();
            foreach (var item in notificationsToMarkRead)
            {
                item.isRead = true;
            }
            _DbContext.UserNotification.UpdateRange(notificationsToMarkRead);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAllNotificationsAsRead(string email)
        {
            var notificationsToMarkRead = await _DbContext.UserNotification
                .Where(m => m.email.ToLower().Trim().Equals(email.ToLower().Trim()) && m.isRead == false).ToListAsync();

            foreach (var item in notificationsToMarkRead)
            {
                item.isRead = true;
            }
            _DbContext.UserNotification.UpdateRange(notificationsToMarkRead);
            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<NotificationTemplate>> GetNotificationTemplate(string[] type)
        {
            //Todo is active filter to implement
            var result = await _DbContext.NotificationTemplates
                .Where(m => type.ToArray().Any(p => p.Equals(m.type))
                && m.is_active == true)
                .Include(m => m.payload.Where(p => p.is_active == true))
                .Include(m => m.link)
                .ToListAsync();
            if (result is null)
            {
                throw new Exception("No Template Found");
            }
            return result;
        }

        public async Task<int> GetLoggedInUserAllNotificationsCount(string email)
        {
            return await _DbContext.UserNotification
                .Where(m => m.isRead == false && !String.IsNullOrEmpty(m.email) && m.email.Trim().ToLower().Equals(email.Trim().ToLower()))
                .CountAsync();
        }

        public async Task<NotificationSubscription> SubmitNotificationSubscriptionDTO(NotificationSubscription entity)
        {
            var record = _DbContext.NotificationSubscription.Where(m =>
                m.user_emailid.ToLower().Trim() == entity.user_emailid.ToLower().Trim()
                && m.module.ToLower().Trim() == entity.module.ToLower().Trim()
                && m.subscription_role.ToLower().Trim() == entity.subscription_role.ToLower().Trim())
                .OrderByDescending(o => o.createdDate).FirstOrDefault();

            if (record == null)
            {
                await _DbContext.Set<NotificationSubscription>().AddAsync(entity);
                await _DbContext.SaveChangesAsync();
                return entity;
            }
            else
            {
                record.module = entity.module;
                record.subscription_role = entity.subscription_role;
                record.user_emailid = entity.user_emailid;
                record.is_active = entity.is_active;
                record.createdDate = entity.createdDate;
                record.modifiedDate = entity.modifiedDate;
                record.createdBy = entity.createdBy;
                record.modifiedBy = entity.modifiedBy;

                _DbContext.NotificationSubscription.Update(record);
                await _DbContext.SaveChangesAsync();
                return record;
            }
        }

        public async Task<List<NotificationSubscription>> GetNotificationSubscriptionDTO(NotificationSubscription entity)
        {
            var record = await _DbContext.NotificationSubscription.Where(m =>
                m.user_emailid.ToLower().Trim() == entity.user_emailid.ToLower().Trim()
                && (string.IsNullOrEmpty(entity.module) || m.module.ToLower().Trim() == entity.module.ToLower().Trim())
                && (string.IsNullOrEmpty(entity.subscription_role) || m.subscription_role.ToLower().Trim() == entity.subscription_role.ToLower().Trim())
                && m.is_active == true)
                .OrderByDescending(o => o.createdDate).ToListAsync();

            return record;
        }
        public async Task<List<NotificationSubscription>> GetNotificationSubscriptionByModuleAndSubscription(string moduleName, string subscription_role)
        {
            var record = await _DbContext.NotificationSubscription.Where(m =>
                        m.module != null && m.module.ToLower().Trim() == moduleName.ToLower().Trim()
                        && m.subscription_role != null && m.subscription_role.ToLower().Trim() == subscription_role.ToLower().Trim()
                        && m.is_active == true).ToListAsync();
            return record;
        }
        public async Task<bool> SeedNotificationToDB(List<NotificationTemplate> entity)
        {
            if (entity != null)
            {
                foreach (var item in entity)
                {
                    await _DbContext.Set<NotificationTemplate>().AddAsync(item);
                }

                await _DbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
