using AutoMapper;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Util;

namespace RMT.Allocation.Application.Mappers
{
    public class AllocationMappingProfile : Profile
    {
        public AllocationMappingProfile()
        {
            CreateMap<BulkUploadValidationResponse, BulkCreateRequisitionDTO>().ReverseMap();
            CreateMap<ResourceAllocationDetailsWithConsumedHours, ResourceAllocationDetailsResponse>().ReverseMap();

            CreateMap<SystemSuggestionResponseDTO, EmployeeDetailsDTO>().ReverseMap();


            CreateMap<RequisitionResponse, CreateResourceAllocationDTO>().ReverseMap();

            //Requisition
            CreateMap<Requisition, RequisitionResponse>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.GetLocalDate()))
                .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => src.ModifiedAt.GetLocalDate()))
                .ReverseMap();

            CreateMap<Requisition, CreateRequisitionCommand>()
                .ReverseMap();

            CreateMap<Requisition, CreateRequisitionDTO>()
                .ReverseMap();
            CreateMap<Requisition, GetAllRequisitionByProjectCodeResponse>()
                .ReverseMap();


            CreateMap<RequisitionRequest, CreateRequisitionCommand>()
                .ReverseMap();

            CreateMap<SuspendAllocationCommand, List<string>>().ReverseMap();

            CreateMap<UpdateRequisitionRequest, UpdateRequisitionCommand>()
                .ReverseMap();

            CreateMap<SuspendAllocationCommand, List<string>>().ReverseMap();



            CreateMap<Requisition, RequisitionRequest>().ReverseMap();
            CreateMap<BudgetOverviewCommand, BudgetOverviewRequest>().ReverseMap();
            CreateMap<AllocationDayGroupResponseDTO, AllocationDayGroup>().ReverseMap().
                ForMember(dest => dest.totalAllocationTime, opt => opt.MapFrom(src => src.totaltime))
                .ForMember(dest => dest.totalAllocationCost, opt => opt.MapFrom(src => src.cost));
            CreateMap<UpdateListOfAllocationDetailsStatusRequest, ResouceAllocationDetailsStatusUpdate>().ReverseMap();
            CreateMap<UpdateDesignationCostDTO, ResouceAllocationDetailsStatusUpdate>().ReverseMap();
            CreateMap<SkillResponseDto, RequisitionSkill>().ReverseMap();

            CreateMap<DateTime, DateOnly>().ConvertUsing<DateTimeToDateOnlyConverter>();
            CreateMap<DateOnly, DateTime?>().ConvertUsing<DateOnlyToDateTimeConverter>();
            CreateMap<BulkCreateRequisitionDTO, BulkRequisition>().ReverseMap();
            CreateMap<PublishedResAllocDetails, PublishAllocationResponse>(MemberList.Source).ReverseMap();
            CreateMap<ResourceAllocationSkillsResponse, SkillsEntities>().ReverseMap();
            CreateMap<GetAllRequisitionByProjectCodeResponse, RequisitionResponse>().ReverseMap();
            CreateMap<ResourceAllocationResponse, EmployeeProject>()
     .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToDateTime(TimeOnly.MinValue)))
     .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToDateTime(TimeOnly.MinValue)))
     .ForMember(dest => dest.ConfirmedPerDayHours, opt => opt.MapFrom(src => (int?)src.Efforts)) // Convert Int64 to nullable int
     .ForMember(dest => dest.AllocationStatus, opt => opt.MapFrom(src => src.Type)) // Assuming 'Type' maps to 'AllocationStatus'
     .ForMember(dest => dest.Skills, opt => opt.Ignore()) // Handle separately if needed
     .ForMember(dest => dest.WeeklyBreakup, opt => opt.Ignore()) // Handle separately if needed
     .ForMember(dest => dest.WeeklyTotal, opt => opt.Ignore()); // If needed, compute separately


        }
    }


    public class DateTimeToDateOnlyConverter : ITypeConverter<DateTime, DateOnly>
    {
        public DateOnly Convert(DateTime source, DateOnly destination, ResolutionContext context)
        {
            return DateOnly.FromDateTime(source);
        }
    }

    public class DateOnlyToDateTimeConverter : ITypeConverter<DateOnly, DateTime?>
    {
        public DateTime? Convert(DateOnly source, DateTime? destination, ResolutionContext context)
        {
            return source.ToDateTime(TimeOnly.MinValue);
        }
    }


}
