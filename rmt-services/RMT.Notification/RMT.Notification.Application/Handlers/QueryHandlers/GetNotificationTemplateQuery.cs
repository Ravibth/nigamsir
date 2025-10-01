using MediatR;
using RMT.Notification.Application.Mappers;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.Entities;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.QueryHandlers
{
    public class GetNotificationTemplateQuery : IRequest<List<GetNotificationTemplateResponse>>
    {
        public string[] type { get; set; }
    }
    public class GetNotificationTemplateQueryHandler : IRequestHandler<GetNotificationTemplateQuery, List<GetNotificationTemplateResponse>>
    {
        private readonly INotificationRepository _notificationRepository;
        public GetNotificationTemplateQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<List<GetNotificationTemplateResponse>> Handle(GetNotificationTemplateQuery request, CancellationToken cancellationToken)
        {
            var templates =  await _notificationRepository.GetNotificationTemplate(request.type);
            var response = NotificationMapper.Mapper.Map<List<GetNotificationTemplateResponse>>(templates);
            return response;
        }
    }
}
