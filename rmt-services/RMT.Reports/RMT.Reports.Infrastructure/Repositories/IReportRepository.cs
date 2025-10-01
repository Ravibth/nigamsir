using RMT.Reports.Domain.Entities;
using RMT.Reports.Infrastructure.Infra.Request;
using RMT.Reports.Infrastructure.Infra.Response;

namespace RMT.Reports.Infrastructure.Repositories
{
    public interface IReportRepository
    {

        public Task<List<EmployeeAllocationTimeSheetEntity>> GetEmployeeAllocationTimeSheet(EmployeeAllocationTimeSheetEntity args);

        public Task<bool> RefreshEmployeeAllocationView();

        public Task<bool> RefreshEmployeeWorkingDaysView();

        public Task<bool> RefreshProjectBudgetView();

        public Task<List<CapacityUtilizationOverviewResponseInfra>> GetCapacityUtilizationOverview(CapacityUtiliationOverviewRequestInfra args);

        public Task<List<ScheduledVsActualVarianceChartResponseInfra>> GetScheduledVsActualVarianceChart(ScheduledVsActualVarianceChartRequestInfra args);

        public Task<List<SummaryStatisticsChartResponseInfra>> GetSummaryStatisticsChart(SummaryStatisticsChartRequestInfra args);

        public Task<bool> RefreshMaterializedViews(List<string> views);

        //public Task<List<SummaryStatisticsChartResponseInfra>> GetSummaryStatisticsChartFutureAllocation(SummaryStatisticsChartRequestInfra args);

    }
}
