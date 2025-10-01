using MediatR;
using RMT.Notification.Application.Mappers;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.QueryHandlers
{
    public class GetNotificationTemplateLinkQuery : IRequest<List<GetNotificationTemplateResponse>>
    {
         public string[] type { get; set; }
    }
    public class GetNotificationTemplateLinkQueryHandler
    {
        private readonly INotificationRepository _notificationRepository;
        public GetNotificationTemplateLinkQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<List<GetNotificationTemplateResponse>> Handle(GetNotificationTemplateLinkQuery request, CancellationToken cancellationToken)
        {
            var templates = await _notificationRepository.GetNotificationTemplate(request.type);
            var response = NotificationMapper.Mapper.Map<List<GetNotificationTemplateResponse>>(templates);
            return response;
        }
    }
}
