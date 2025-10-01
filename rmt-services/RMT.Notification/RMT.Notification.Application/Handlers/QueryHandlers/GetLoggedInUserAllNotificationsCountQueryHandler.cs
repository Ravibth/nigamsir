using MediatR;
using RMT.Notification.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Handlers.QueryHandlers
{
    public class GetLoggedInUserAllNotificationsCountQuery : IRequest<int>
    {
        public string email { get; set; }
    }
    public class GetLoggedInUserAllNotificationsCountQueryHandler : IRequestHandler<GetLoggedInUserAllNotificationsCountQuery, int>
    {
        private readonly INotificationRepository _Repo;
        public GetLoggedInUserAllNotificationsCountQueryHandler(INotificationRepository Repository)
        {
            _Repo = Repository;
        }

        public async Task<int> Handle(GetLoggedInUserAllNotificationsCountQuery request, CancellationToken cancellationToken)
        {
            return await _Repo.GetLoggedInUserAllNotificationsCount(request.email);
        }
    }
}
