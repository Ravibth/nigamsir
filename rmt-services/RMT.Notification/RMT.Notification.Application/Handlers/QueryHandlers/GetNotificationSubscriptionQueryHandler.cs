using MediatR;
using RMT.Notification.Application.Mappers;
using RMT.Notification.Application.RequestDTO;
using RMT.Notification.Domain.Entities;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.QueryHandlers
{
    public class GetNotificationSubscriptionQuery : IRequest<List<NotificationSubscriptionDTO>>
    {
        public string? module { get; set; }
        public string? subscription_role { get; set; }
        public string user_emailid { get; set; }

    }

    public class GetNotificationSubscriptionQueryHandler : IRequestHandler<GetNotificationSubscriptionQuery, List<NotificationSubscriptionDTO>>
    {
        private readonly INotificationRepository _Repo;
        public GetNotificationSubscriptionQueryHandler(INotificationRepository Repository)
        {
            _Repo = Repository;
        }

        public async Task<List<NotificationSubscriptionDTO>> Handle(GetNotificationSubscriptionQuery request, CancellationToken cancellationToken)
        {
            NotificationSubscription query = NotificationMapper.Mapper.Map<NotificationSubscription>(request);
            if (query is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            var response = await _Repo.GetNotificationSubscriptionDTO(query);
            List<NotificationSubscriptionDTO> result = NotificationMapper.Mapper.Map<List<NotificationSubscriptionDTO>>(response);

            return result;
        }
    }
}
