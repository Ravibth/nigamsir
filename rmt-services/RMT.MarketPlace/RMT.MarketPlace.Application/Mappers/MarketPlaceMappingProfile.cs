using AutoMapper;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.Handlers.CommandHandlers;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Mappers
{
    public class MarketPlaceMappingProfile : Profile
    {
        public MarketPlaceMappingProfile()
        {
            CreateMap<MarketPlaceProjectDetail, MarketPlaceProjectDetailDTO>()
                .ReverseMap();
            //.ForMember(dest => dest.MarketPlacePublishDate, opt => opt.MapFrom(src => src.MarketPlacePublishDate.Value.ToLocalTime()))
            //.ForMember(dest => dest.MarketPlaceExpirationDate, opt => opt.MapFrom(src => src.MarketPlaceExpirationDate.Value.ToLocalTime()));

            CreateMap<EmpProjectInterestCommand, EmpProjectInterest>().ReverseMap();
            CreateMap<EmpProjectInterest, EmpProjectInterestResponse>().ReverseMap();
            CreateMap<AddProjectToMarketPlaceCommand, MarketPlaceProjectDetail>();
            //.ForMember(dest => dest.MarketPlacePublishDate, opt => opt.MapFrom(src => src.MarketPlacePublishDate.Value.ToLocalTime()))
            //.ForMember(dest => dest.MarketPlaceExpirationDate, opt => opt.MapFrom(src => src.MarketPlaceExpirationDate.Value.ToLocalTime()))
            //.ReverseMap();

            CreateMap<MarketPlaceProjectDetail, MarketPlaceProjectDetailResponse>().ReverseMap();
            //.ForMember(dest => dest.MarketPlacePublishDate, opt => opt.MapFrom(src => src.MarketPlacePublishDate.Value.ToLocalTime()))
            //.ForMember(dest => dest.MarketPlaceExpirationDate, opt => opt.MapFrom(src => src.MarketPlaceExpirationDate.Value.ToLocalTime()));

            CreateMap<EmpProjectInterestScoreRequest, EmpProjectInterestScore>().ReverseMap();
            CreateMap<EmpProjectInterestScoreResponse, EmpProjectInterestScore>().ReverseMap();
            CreateMap<EmpProjectInterestScoreDTO, EmpProjectInterestScore>().ReverseMap();

            CreateMap<UpdateMarketPlaceProjectCommand, MarketPlaceProjectDetail>().ReverseMap();

        }
    }
}
