using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Reports.Application.DTO.Request;
using RMT.Reports.Application.DTO.Response;
using RMT.Reports.Infrastructure.Infra.Request;
using RMT.Reports.Infrastructure.Infra.Response;
using RMT.Reports.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.Handlers.QueryHandlers
{
    public class SummaryStatisticsChartQuery : IRequest<SummaryStatisticsChartResponseDto>
    {
        public SummaryStatisticsChartRequestDto args { get; set; }
    }
    public class SummaryStatisticsChartQueryHandler : IRequestHandler<SummaryStatisticsChartQuery, SummaryStatisticsChartResponseDto>
    {
        private readonly IReportRepository _reportRepository;

        public SummaryStatisticsChartQueryHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<SummaryStatisticsChartResponseDto> Handle(SummaryStatisticsChartQuery request, CancellationToken cancellationToken)
        {
            SummaryStatisticsChartRequestInfra req = ReportMapper.Mapper.Map<SummaryStatisticsChartRequestInfra>(request.args);
            List<SummaryStatisticsChartResponseInfra> resultData = await _reportRepository.GetSummaryStatisticsChart(req);
            if (resultData.Count == 0)
            {
                return new SummaryStatisticsChartResponseDto();
            }
            //List<SummaryStatisticsChartResponseInfra> result2Data = await _reportRepository.GetSummaryStatisticsChartFutureAllocation(req);

            List<SummaryStatisticsChartDataDto> chartDataTable = ReportMapper.Mapper.Map<List<SummaryStatisticsChartDataDto>>(resultData);
            //List<SummaryStatisticsChartDataDto> chart2DataTable = ReportMapper.Mapper.Map<List<SummaryStatisticsChartDataDto>>(result2Data);
            List<SummaryStatisticsChartDataDto> chart2DataTable = chartDataTable;

            SummaryStatisticsChartResponseDto result = new SummaryStatisticsChartResponseDto();

            result.SummaryStatisticsData = chartDataTable;

            double totalSum = 0;
            DateTime currentDate = DateTime.Now.Date;

            //Changing
            totalSum = chart2DataTable.AsEnumerable().Sum(m => m.pipeline_code_count);
            //[todo] PipelineCode and JobCode is not fetched from back end
            //totalSum = chart2DataTable.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.pipeline_code) && !string.IsNullOrEmpty(x.job_code)).Select(x => string.Format("{0}-##-{1}", x.pipeline_code, x.job_code)).Distinct().Count();
            result.TotalProjectCount = totalSum;

            //[done] PipelineCode and JobCode is not fetched from back end
            totalSum = chart2DataTable.AsEnumerable().Where(x => x.allocation_hours > 0).Sum(row => Convert.ToDouble(row.allocation_hours));
            result.FutureAllocationHrs = totalSum;

            //[done] PipelineCode and JobCode is not fetched from back end
            //totalSum = chartDataTable.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.pipeline_code) && !string.IsNullOrEmpty(x.job_code) && x.allocation_hours > 0).Sum(row => Convert.ToDouble(row.allocation_hours));
            result.JobAllocationsHrs = totalSum;

            //[done] PipelineCode and JobCode is not fetched from back end
            //totalSum = chartDataTable.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.pipeline_code) && x.job_code == null && x.allocation_hours > 0).Sum(row => Convert.ToDouble(row.allocation_hours));
            //result.PipelineAllocationsHrs = totalSum;

            //string currentMonth = request.args.EndDate.Date.ToString("yyyy MMM");
            //string previousMonth = request.args.StartDate.ToString("yyyy MMM");

            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocation_hours > 0).Sum(row => Convert.ToDouble(row.allocation_hours));//
            result.TotalAllocatedHrsCurrent = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_cost > 0).Sum(row => Convert.ToDouble(row.allocated_cost));//
            result.TotalAllocatedCostCurrent = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.capacity > 0).Sum(row => Convert.ToDouble(row.capacity));//
            result.TotalCapacityHrsCurrent = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.capacity_cost > 0).Sum(row => Convert.ToDouble(row.capacity_cost));//
            result.TotalCapacityCostCurrent = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.actual_log_hours > 0).Sum(row => Convert.ToDouble(row.actual_log_hours));//
            result.TotalActualHrsPrevious = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.actual_cost > 0).Sum(row => Convert.ToDouble(row.actual_cost));//
            result.TotalActualCostPrevious = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.capacity > 0).Sum(row => Convert.ToDouble(row.capacity));//
            result.TotalCapacityHrsPrevious = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.capacity_cost > 0).Sum(row => Convert.ToDouble(row.capacity_cost));//
            result.TotalCapacityCostPrevious = totalSum;


            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_chargable_hr > 0).Sum(row => Convert.ToDouble(row.allocated_chargable_hr));//
            result.ChargeableAllocatedHrsCurrent = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_chargable_cost > 0).Sum(row => Convert.ToDouble(row.allocated_chargable_cost));//
            result.ChargeableAllocatedCostCurrent = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_non_chargable_hr > 0).Sum(row => Convert.ToDouble(row.allocated_non_chargable_hr));//
            result.NonChargeableAllocatedHrsCurrent = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_non_chargable_cost > 0).Sum(row => Convert.ToDouble(row.allocated_non_chargable_cost));//
            result.NonChargeableAllocatedCostCurrent = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_chargeable_hours > 0).Sum(row => Convert.ToDouble(row.job_chargeable_hours));//
            result.ChargeAbleActualHrsPrevious = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_chargeable_cost > 0).Sum(row => Convert.ToDouble(row.job_chargeable_cost));//
            result.ChargeAbleActualCostPrevious = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_non_chargeable_hours > 0).Sum(row => Convert.ToDouble(row.job_non_chargeable_hours));//
            result.NonChargeableActualHrsPrevious = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_non_chargeable_cost > 0).Sum(row => Convert.ToDouble(row.job_non_chargeable_cost));//
            result.NonChargeableActualCostPrevious = totalSum;


            totalSum = chartDataTable.AsEnumerable().Where(x => x.capacity > 0).Sum(row => Convert.ToDouble(row.capacity));
            result.TotalCapacityHrs = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.capacity_cost > 0).Sum(row => Convert.ToDouble(row.capacity_cost));
            result.TotalCapacityCost = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocation_hours > 0).Sum(row => Convert.ToDouble(row.allocation_hours));
            result.TotalAllocatedHrs = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_cost > 0).Sum(row => Convert.ToDouble(row.allocated_cost));
            result.TotalAllocatedCost = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.actual_log_hours > 0).Sum(row => Convert.ToDouble(row.actual_log_hours));
            result.TotalActualHrs = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.actual_cost > 0).Sum(row => Convert.ToDouble(row.actual_cost));
            result.TotalActualCost = totalSum;

            //var allocatedRecords = chartDataTable.AsEnumerable().Where(x => x.allocation_date.HasValue).ToList();//&& x.allocation_date.Value >= DateOnly.FromDateTime(currentDate.Date)).ToList();

            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_chargable_hr > 0).Sum(row => Convert.ToDouble(row.allocated_chargable_hr));
            result.ChargeableAllocatedHrs = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_chargable_cost > 0).Sum(row => Convert.ToDouble(row.allocated_chargable_cost));
            result.ChargeableAllocatedCost = totalSum;

            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_non_chargable_hr > 0).Sum(row => Convert.ToDouble(row.allocated_non_chargable_hr));
            result.NonChargeableAllocatedHrs = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.allocated_non_chargable_cost > 0).Sum(row => Convert.ToDouble(row.allocated_non_chargable_cost));
            result.NonChargeableAllocatedCost = totalSum;

            //var futureAllocatedRecords = chart2DataTable.AsEnumerable().Where(x => x.allocation_date.HasValue).ToList();//&& x.allocation_date.Value >= DateOnly.FromDateTime(currentDate.Date)).ToList();

            totalSum = chart2DataTable.AsEnumerable().Where(x => x.allocated_chargable_hr > 0).Sum(row => Convert.ToDouble(row.allocated_chargable_hr));
            result.FutureChargeableAllocatedHrs = totalSum;
            totalSum = chart2DataTable.AsEnumerable().Where(x => x.allocated_chargable_cost > 0).Sum(row => Convert.ToDouble(row.allocated_chargable_cost));
            result.FutureChargeableAllocatedCost = totalSum;

            totalSum = chart2DataTable.AsEnumerable().Where(x => x.allocated_non_chargable_hr > 0).Sum(row => Convert.ToDouble(row.allocated_non_chargable_hr));
            result.FutureNonChargeableAllocatedHrs = totalSum;
            totalSum = chart2DataTable.AsEnumerable().Where(x => x.allocated_non_chargable_cost > 0).Sum(row => Convert.ToDouble(row.allocated_non_chargable_cost));
            result.FutureNonChargeableAllocatedCost = totalSum;

            //[todo] how to get actual non chargeble hours
            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_chargeable_hours > 0).Sum(row => Convert.ToDouble(row.job_chargeable_hours));
            result.ChargeAbleActualHrs = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_chargeable_cost > 0).Sum(row => Convert.ToDouble(row.job_chargeable_cost));
            result.ChargeAbleActualCost = totalSum;

            //[todo] how to get actual non chargeble hours
            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_non_chargeable_hours > 0).Sum(row => Convert.ToDouble(row.job_non_chargeable_hours));
            result.NonChargeableActualHrs = totalSum;
            totalSum = chartDataTable.AsEnumerable().Where(x => x.job_non_chargeable_cost > 0).Sum(row => Convert.ToDouble(row.job_non_chargeable_cost));
            result.NonChargeableActualCost = totalSum;


            return result;
        }
    }
}
