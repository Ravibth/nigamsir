using AutoMapper;
using RMT.Report.Application.Handlers.QueryHandlers;
using RMT.Reports.Application.DTO.Request;
using RMT.Reports.Application.DTO.Response;
using RMT.Reports.Domain.Entities;
using RMT.Reports.Infrastructure.Infra.Request;
using RMT.Reports.Infrastructure.Infra.Response;

namespace RMT.Allocation.Application.Mappers
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<EmployeeAllocationTimeSheetEntity, GetEmployeeAllocationTimeSheetQuery>().ReverseMap();
            CreateMap<CapacityUtiliationOverviewRequestInfra, CapacityUtiliationOverviewRequestDto>().ReverseMap();
            CreateMap<CapacityUtilizationOverviewResponseDto, CapacityUtilizationOverviewResponseInfra>().ReverseMap();
            CreateMap<EmployeeAllocationTimeSheetEntity, CapacityUtilizationOverviewResponseDto>().ReverseMap();
            CreateMap<ScheduledVsActualVarianceChartRequestDto, ScheduledVsActualVarianceChartRequestInfra>().ReverseMap();
            CreateMap<ScheduledVsActualVarianceChartResponseDto, ScheduledVsActualVarianceChartResponseInfra>().ReverseMap();

            CreateMap<SummaryStatisticsChartRequestDto, SummaryStatisticsChartRequestInfra>().ReverseMap();
            CreateMap<SummaryStatisticsChartDataDto, SummaryStatisticsChartResponseInfra>().ReverseMap();
        }
    }
}
