using MediatR;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.CommandHandlers
{
    public class MarkAllNotificationsAsReadCommand : IRequest<bool>
    {
        public string email { get; set; }
    }
    public class MarkAllNotificationsAsReadCommandHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, bool>
    {
        private readonly INotificationRepository _notificationRepository;
        public MarkAllNotificationsAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.MarkAllNotificationsAsRead(request.email);
        }
    }
}
