using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.DTOs;
using WCGT.Application.Handlers.QueryHandlers;
using WCGT.Application.Responses;
using WCGT.Domain.DTO;

//using WCGT.Application.DTOs.GetDTOs;
//using WCGT.Application.DTOs.UpdateDTOs;
//using WCGT.Application.Handlers.QueryHandlers;
//using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Mappers
{
    public class WcgtMappingProfile : Profile
    {
        public WcgtMappingProfile()
        {
            CreateMap<Client, GTClientDTO>().ReverseMap();
            //CreateMap<Project, GTProjectDTO>().ReverseMap();
            //CreateMap<ProjectRole, GTProjectRoleDTO>().ReverseMap();
            CreateMap<Employee, GTEmployeeDTO>().ReverseMap();
            CreateMap<Competency, GTCompetencyDTO>().ReverseMap();
            CreateMap<Designation, GTDesignationDTO>().ReverseMap();
            CreateMap<BUTreeMapping, GTBUTreeMappingDTO>().ReverseMap();
            CreateMap<Holiday, GTHolidayDTO>().ReverseMap();
            CreateMap<Job, GTJobDTO>().ReverseMap();
            CreateMap<JobRole, GTJobRoleDTO>().ReverseMap();
            CreateMap<Leave, GTLeaveDTO>()
                .ForMember(dest => dest.start_date_half, opt => opt.MapFrom(src => src.start_date_half))
                .ForMember(dest => dest.end_date_half, opt => opt.MapFrom(src => src.end_date_half))
                .ReverseMap()
                .ForMember(dest => dest.start_date_half, opt => opt.MapFrom(src => src.start_date_half))
                .ForMember(dest => dest.end_date_half, opt => opt.MapFrom(src => src.end_date_half));


            CreateMap<Location, GTLocationDTO>().ReverseMap();
            CreateMap<ClientLegalEntity, GTClientLegalEntityDTO>().ReverseMap();
            CreateMap<Pipeline, GTPipelineDTO>().ReverseMap();
            CreateMap<PipelineRole, GTPipelineRoleDTO>().ReverseMap();
            CreateMap<SectorIndustry, GTSectorIndustryDTO>().ReverseMap();


            CreateMap<BUTreeMapping, BUTreeMappingListResponse>().ReverseMap();
            CreateMap<ClientLegalEntity, ClientLegalEntityListResponse>().ReverseMap();
            CreateMap<Client, ClientListResponse>().ReverseMap();
            CreateMap<Designation, DesignationListResponse>().ReverseMap();
            CreateMap<Employee, EmployeeListResponse>().ReverseMap();
            CreateMap<Competency, CompetencyResponse>().ReverseMap();
            CreateMap<Holiday, HolidayListResponse>().ReverseMap();
            CreateMap<Leave, LeaveListResponse>().ReverseMap();
            CreateMap<Location, LocationListResponse>().ReverseMap();
            CreateMap<SectorIndustry, SectorIndustryListResponse>().ReverseMap();
            CreateMap<Pipeline, PipelineListResponse>().ReverseMap();
            CreateMap<Job, JobListResponse>().ReverseMap();


            CreateMap<GTBUTreeMappingDTO, BUTreeMappingListResponse>().ReverseMap();
            CreateMap<GTClientLegalEntityDTO, ClientLegalEntityListResponse>().ReverseMap();
            CreateMap<GTClientDTO, ClientListResponse>().ReverseMap();
            CreateMap<GTDesignationDTO, DesignationListResponse>().ReverseMap();
            CreateMap<GTEmployeeDTO, EmployeeListResponse>().ReverseMap();
            CreateMap<GTCompetencyDTO, CompetencyResponse>().ReverseMap();
            CreateMap<GTHolidayDTO, HolidayListResponse>().ReverseMap();
            CreateMap<GTLeaveDTO, LeaveListResponse>().ReverseMap();
            CreateMap<GTLocationDTO, LocationListResponse>().ReverseMap();
            CreateMap<GTSectorIndustryDTO, SectorIndustryListResponse>().ReverseMap();
            CreateMap<GTPipelineDTO, PipelineListResponse>().ReverseMap();
            CreateMap<GTJobDTO, JobListResponse>().ReverseMap();
            CreateMap<TimesheetRequestDTO, GetTimesheetQuery>().ReverseMap();
            CreateMap<RateDesignationDTO, RateDesignationMaster>().ReverseMap();
            CreateMap<BudgetResponse, GTBudgetDTO>().ReverseMap();
            CreateMap<Budget, GTBudgetDTO>().ReverseMap();

            CreateMap<DesignationRateMasterResponse, GTDesignationRateMasterDTO>().ReverseMap();

            CreateMap<RateDesignationMaster, GTDesignationRateMasterDTO>()
                .ForMember(m1 => m1.grade, opt => opt.MapFrom(src => src.grade))
                .ReverseMap()
                .ForMember(m1 => m1.grade, opt => opt.MapFrom(src => src.grade));

            CreateMap<BUEfficiencyLeaderDTO, GTBUEfficiencyLeaderResponse>().ReverseMap();
            CreateMap<Employee, SuperCoach>().ReverseMap();
        }
    }
}
