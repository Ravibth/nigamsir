using MediatR;
using RMT.Notification.Application.Mappers;
using RMT.Notification.Application.RequestDTO;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.Entities;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.CommandHandlers
{
    public class NotificationSubscriptionCommand : IRequest<NotificationSubscriptionDTO>
    {
        public Int64 id { get; set; }
        public string? module { get; set; }
        public string? subscription_role { get; set; }
        public string? user_emailid { get; set; }
        public string? user_name { get; set; }
        public bool? is_active { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public string? createdBy { get; set; }
        public string? modifiedBy { get; set; }
    }

    public class NotificationSubscriptionCommandHandler : IRequestHandler<NotificationSubscriptionCommand, NotificationSubscriptionDTO>
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationSubscriptionCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<NotificationSubscriptionDTO> Handle(NotificationSubscriptionCommand request, CancellationToken cancellationToken)
        {
            NotificationSubscription entitiy = NotificationMapper.Mapper.Map<NotificationSubscription>(request);
            if (entitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            var entityAdded = await _notificationRepository.SubmitNotificationSubscriptionDTO(entitiy);
            NotificationSubscriptionDTO entityResponse = NotificationMapper.Mapper.Map<NotificationSubscriptionDTO>(entityAdded);

            return entityResponse;

        }
    }
}
