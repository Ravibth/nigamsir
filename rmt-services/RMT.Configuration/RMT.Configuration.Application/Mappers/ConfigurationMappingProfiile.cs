using AutoMapper;
using RMT.Configuration.Application.DTOs.ApplicationConfigurationDTOs;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.DTOs.Response;
using RMT.Configuration.Application.Handlers.CommandHandlers;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Infrastructure.Util;

namespace RMT.Configuration.Application.Mappers
{
    public class ConfigurationMappingProfiile : Profile
    {
        public ConfigurationMappingProfiile()
        {
            //CreateMap<CreateConfigurationGroupCommand, ConfigurationGroup>()
            //.ReverseMap()
            //.ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
            //.ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            //CreateMap<CreateProjectConfigurationCommand, ProjectConfiguration>()
            //.ReverseMap()
            //.ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
            //.ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<UpdateConfigurationDTO, ProjectConfiguration>()
            .ReverseMap();
            CreateMap<UpdateConfiguration, ProjectConfiguration>()
            .ReverseMap()
            .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
            .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<UpdateConfiguration, ConfigurationGroup>()
            .ReverseMap()
            .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
            .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            //CreateMap<CreateBusinessUnitMasterCommand, BusinessUnitMaster>()
            //    .ReverseMap()
            //    .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
            //    .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            //CreateMap<CreateExpertiesMasterCommand, ExpertiesMaster>()
            //    .ReverseMap()
            //    .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
            //    .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            //CreateMap<CreateBuExpertiesGroupCommand, Bu_Experties_Grp>()
            //    .ReverseMap()
            //    .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
            //    .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<UpdateConfigurationCommand, ConfigurationGroup>()
            .ReverseMap();

            CreateMap<MenuMasterDTO, MenuMaster>()
            .ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<UpdateMenuMasterCommand, MenuMaster>()
            .ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<RoleMenuDTO, RoleMenu>()
           .ReverseMap()
               .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
               .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<UpdateRoleMenuCommand, RoleMenu>()
            .ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));


            CreateMap<ContextMenuMasterDTO, ContextMenuMaster>()
           .ReverseMap()
               .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
               .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<UpdateContextMenuMasterCommand, ContextMenuMaster>()
            .ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<RoleContextMenuDTO, RoleContextMenu>()
           .ReverseMap()
               .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
               .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));
            CreateMap<UpdateRoleContextMenuCommand, RoleContextMenu>()
            .ReverseMap()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()));

            CreateMap<LoggerCommand, LoggerDTO>().ReverseMap();

            CreateMap<ConfigurationGroup, ConfigurationGroup>()
            .ReverseMap()
            .ForMember(m1 => m1.ConfigGroup, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ConfigGroup : null))
            .ForMember(m1 => m1.ConfigGroupDisplay, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ConfigGroupDisplay : null))
            .ForMember(m1 => m1.ConfigKey, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ConfigKey : null))
            .ForMember(m1 => m1.CongigDisplayText, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.CongigDisplayText : null))
            .ForMember(m1 => m1.ValueType, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ValueType : null)
            );

            CreateMap<ConfigurationGroupResponse, ConfigurationGroup>()
            .ReverseMap()
            .ForMember(m1 => m1.ConfigGroup, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ConfigGroup : null))
            .ForMember(m1 => m1.ConfigGroupDisplay, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ConfigGroupDisplay : null))
            .ForMember(m1 => m1.ConfigKey, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ConfigKey : null))
            .ForMember(m1 => m1.CongigDisplayText, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.CongigDisplayText : null))
            .ForMember(m1 => m1.ValueType, opt => opt.MapFrom(src => src.ConfigurationGroupMaster != null ? src.ConfigurationGroupMaster.ValueType : null)
            );

            CreateMap<ConfigurationGroupMaster, ConfigurationGroupMasterResponse>()
            .ReverseMap();

            CreateMap<ProjectConfiguration, ProjectConfigurationResponse>()
            .ReverseMap();

        }
    }
}
