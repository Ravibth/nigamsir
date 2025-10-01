using AutoMapper;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Handlers.CommandHandlers;
using RMT.Projects.Application.Handlers.QueryHandlers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Mappers
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, ProjectDetailsRequestorDto>()
                //.ForMember(m1 => m1.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.GetLocalDate()))
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
                .ForMember(m1 => m1.EndDate, opt => opt.MapFrom(src => src.EndDate.GetLocalDate()))
                .ForMember(m1 => m1.StartDate, opt => opt.MapFrom(src => src.StartDate.GetLocalDate()))
                .ReverseMap();

            CreateMap<Project, ProjectDetailsEmployeeDto>()
                //.ForMember(m1 => m1.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.GetLocalDate()))
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
                .ForMember(m1 => m1.EndDate, opt => opt.MapFrom(src => src.EndDate.GetLocalDate()))
                .ForMember(m1 => m1.StartDate, opt => opt.MapFrom(src => src.StartDate.GetLocalDate()))
                .ReverseMap();
            CreateMap<Project, ProjectResponse>()
               .ForMember(m1 => m1.SME, opt => opt.MapFrom(src => src.Sme))//Recheck
               .ForMember(m1 => m1.EndDate, opt => opt.MapFrom(src => src.EndDate.GetLocalDate()))
               .ForMember(m1 => m1.StartDate, opt => opt.MapFrom(src => src.StartDate.GetLocalDate()))
               .ReverseMap()
               .ForMember(m1 => m1.Sme, opt => opt.MapFrom(src => src.SME))//Recheck
               ;
            CreateMap<Project, ProjectFullDetailsResponse>()
               .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
               .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
               .ForMember(m1 => m1.EndDate, opt => opt.MapFrom(src => src.EndDate.GetLocalDate()))
               .ForMember(m1 => m1.StartDate, opt => opt.MapFrom(src => src.StartDate.GetLocalDate()))
               .ReverseMap();

            CreateMap<Project, CreateProjectCommand>()
                .ForMember(m1 => m1.SME, opt => opt.MapFrom(src => src.Sme))//Recheck
                .ForMember(m1 => m1.SMEG, opt => opt.MapFrom(src => src.Smeg))//Recheck
                .ForMember(m1 => m1.EndDate, opt => opt.MapFrom(src => src.EndDate.GetLocalDate()))
                .ForMember(m1 => m1.StartDate, opt => opt.MapFrom(src => src.StartDate.GetLocalDate()))
                .ReverseMap()
                .ForMember(m1 => m1.Sme, opt => opt.MapFrom(src => src.SME))//Recheck
                .ForMember(m1 => m1.Smeg, opt => opt.MapFrom(src => src.SMEG))//Recheck
                ;
            CreateMap<ProjectRoles, AddProjectUserRole>()
                .ForMember(m1 => m1.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
                .ReverseMap();
            CreateMap<Project, UpdateProjectCommand>()
                .ForMember(m1 => m1.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
                .ReverseMap();
            CreateMap<Project, UpdateProjectRolledOverCommand>().ReverseMap();
            CreateMap<Project, UpdateProjectSuspensionStatusQuery>().ReverseMap();
            //ProjectDetailsByPipelineCodeAndUserRoleResponseDTO
            CreateMap<Project, ProjectDetailsAsPerPipelineCodeAndUserRoleResponseDTO>().ReverseMap();
            CreateMap<Project, BasicProjectDetailsRequestorResponse>().ReverseMap();

            CreateMap<ProjectRoles, ProjectRolesResponseDTO>().ReverseMap();
            CreateMap<ProjectRolesView, ProjectRolesResponseDTO>().ReverseMap();
            //   CreateMap<List<ProjectBudget>,List<ProjectBudgetDto>>().ReverseMap();
            //CreateMap<SuspendAllocationCommand, UpdateProjectSuspensionStatusQuery>().ForMember(x => x.IsSuspended, opt => opt.Ignore()).ReverseMap();

            CreateMap<ProjectRoles, ProjectRolesView>()
               .ReverseMap();
        }
    }
}
