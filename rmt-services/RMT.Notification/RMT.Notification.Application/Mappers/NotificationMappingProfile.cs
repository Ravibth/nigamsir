using AutoMapper;
using RMT.Notification.Application.Handlers.CommandHandlers;
using RMT.Notification.Application.Handlers.QueryHandlers;
using RMT.Notification.Application.RequestDTO;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Mappers
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<NotificationTemplate, NotificationTemplateResponse>().ReverseMap();
            CreateMap<GetNotificationTemplateResponse, NotificationTemplate>().ReverseMap()
                .ForMember(m1 => m1.link, opt => opt.MapFrom(m2 => m2.link.Link));
            CreateMap<NotificationSubscription, NotificationSubscriptionDTO>().ReverseMap();
            CreateMap<NotificationSubscription, NotificationSubscriptionCommand>().ReverseMap();
            CreateMap<NotificationSubscription, GetNotificationSubscriptionQuery>().ReverseMap();
           // CreateMap<List<NotificationTemplate>, List<NotificationSeederDTO>>().ReverseMap();
        }
    }
}
