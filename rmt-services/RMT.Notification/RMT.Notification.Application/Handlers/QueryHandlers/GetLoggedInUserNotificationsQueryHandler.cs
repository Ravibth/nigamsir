using MediatR;
using Microsoft.Extensions.Configuration;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.Entities;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace RMT.Notification.Application.Handlers.QueryHandlers
{
    public class GetLoggedInUserNotificationsQuery : IRequest<List<UserNotificationsResponse>>
    {
        public string email { get; set; }
        public Int64 limit { get; set; }
        public Int64 pagination { get; set; }
    }
    public class GetLoggedInUserNotificationsQueryHandler : IRequestHandler<GetLoggedInUserNotificationsQuery, List<UserNotificationsResponse>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IConfiguration _configuration;

        public GetLoggedInUserNotificationsQueryHandler(INotificationRepository notificationRepository, IConfiguration configuration)
        {
            _notificationRepository = notificationRepository;
            _configuration = configuration;
        }

        public async Task<List<UserNotificationsResponse>> Handle(GetLoggedInUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            Int16 pushNotificationFromLastDays = Convert.ToInt16(_configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.PushNotificationSinceNumberOfDays).Value);

            var notificationsFetched = await _notificationRepository.GetLoggedInUserNotifications(request.email, request.limit, request.pagination, pushNotificationFromLastDays);
            var response = new List<UserNotificationsResponse>();
            foreach (var item in notificationsFetched)
            {
                response.Add(new UserNotificationsResponse
                {
                    id = item.id,
                    email = item.email,
                    employee_id = item.employee_id,
                    createdBy = item.createdBy,
                    createdDate = item.createdDate,
                    isRead = item.isRead,
                    notification_id = item.notification_id,
                    message = item.notification.message,
                    meta = item.notification.meta,
                    type = item.notification.type,
                    link = item.notification.link
                });
            }
            return response;
        }
    }
}
