using MediatR;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.DTO;
using RMT.Notification.Domain.Entities;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.CommandHandlers
{
    public class PostNewNotificationCommand : IRequest<List<UserNotificationsResponse>>
    {
        public Int64 notification_template_id {get;set;}
        public string[] users { get; set; }
        public PostNewNotificationRequestDTO data { get; set; }
    }
    public class PostNewNotificationCommandHandler:IRequestHandler<PostNewNotificationCommand, List<UserNotificationsResponse>>
    {
        private readonly INotificationRepository _notificationRepository;

        public PostNewNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<UserNotificationsResponse>> Handle(PostNewNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationsAdded = await _notificationRepository.PostNewNotification(request.users, request.data);
            var response = new List<UserNotificationsResponse>();
            foreach (var item in notificationsAdded)
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
                    link = item.notification.link,
                });
            }
            return response;
        }
    }
}
