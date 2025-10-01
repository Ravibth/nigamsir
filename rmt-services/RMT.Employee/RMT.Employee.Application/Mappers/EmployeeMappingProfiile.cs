using AutoMapper;
using RMT.Employee.Application.DTOs;
using RMT.Employee.Application.DTOs.EmployeePreferenceDTOs;
using RMT.Employee.Application.Handlers.CommandHandlers;
using RMT.Employee.Application.Response;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Infrastructure.Util;

namespace RMT.Employee.Application.Mappers
{
    public class EmployeeMappingProfiile : Profile
    {
        public EmployeeMappingProfiile()
        {
            CreateMap<EmployeePreferenceResponse, EmployeePreference>().ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            
            CreateMap<PreferenceMasterResponse, PreferenceMaster>().ReverseMap()
            ;
            CreateMap<PreferenceMaster, PreferenceMasterCommand>()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
            .ReverseMap();
            CreateMap<EmployeePreference, EmployeePreferenceCommand>()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
            .ReverseMap();
            CreateMap<EmployeePreference, EmployeePreferenceDTO>()
            .ReverseMap();
            CreateMap<EmployeePreference, UpdateEmployeePreferenceDTO>()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
            .ReverseMap();
            CreateMap<PreferenceMasterCommand, PreferenceMaster>().ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
                ;
            CreateMap<UpdatePreferenceMasterCommand, PreferenceMaster>().ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
                ;

            CreateMap<EmployeeProjectMapping, EmployeeProjectMappingDTO>()
            .ReverseMap();
            CreateMap<EmployeeProjectMapping, EmpProjectMappingResponse>()
            .ReverseMap();

            CreateMap<EmployeeProfile, EmployeeProfileResponseDTO>()
                .ReverseMap();

        }
    }
}
