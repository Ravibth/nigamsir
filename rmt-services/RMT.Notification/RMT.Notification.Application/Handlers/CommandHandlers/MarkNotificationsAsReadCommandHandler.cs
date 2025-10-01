using MediatR;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.CommandHandlers
{
    public class MarkNotificationsAsReadCommand : IRequest<bool>
    {
        public Guid[] id {  get; set; }
    }
    public class MarkNotificationsAsReadCommandHandler : IRequestHandler<MarkNotificationsAsReadCommand, bool>
    {
        private readonly INotificationRepository _notificationRepository;
        public MarkNotificationsAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<bool> Handle(MarkNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.MarkNotificationsAsRead(request.id);
        }
    }
}
