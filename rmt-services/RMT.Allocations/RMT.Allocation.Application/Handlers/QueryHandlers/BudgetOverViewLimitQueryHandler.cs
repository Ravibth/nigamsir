using MediatR;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class BudgetOverViewLimitQuery : IRequest<BudgetOverviewLimit>
    {
        public string JobCode { get; set; }
        public string PipelineCode { get; set; }
    }
    public class BudgetOverViewLimitQueryHandler : IRequestHandler<BudgetOverViewLimitQuery, BudgetOverviewLimit>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IMediator _mediator;
        public BudgetOverViewLimitQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IMediator mediator)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _mediator = mediator;
        }
        public async Task<BudgetOverviewLimit> Handle(BudgetOverViewLimitQuery request, CancellationToken cancellationToken)
        {
            List<KeyValuePair<string, string>> jobCodes = new List<KeyValuePair<string, string>>();
            jobCodes.Add(new KeyValuePair<string, string>(request.JobCode, request.PipelineCode));
            BudgetOverviewRequest budgetOverviewRequest = new BudgetOverviewRequest { JobCodes = jobCodes };
            List<BudgetOverviewDto> resourceAllocationDetails = await _resourceAllocationRepository.GetBudgetOverview(budgetOverviewRequest);

            DesignationBudgetQuery query = new() { JobCode = request.JobCode, PipelineCode = request.PipelineCode };
            double totalBudgetHrs = 0;
            double totalBudgetCost = 0;
            double totalAllocatedhrs = 0;
            double totalAllocatedCost = 0;
            double totalTimesheetHrs = 0;
            double totalTImesheetCost = 0;
            var designationBudget = await _mediator.Send(query);

            foreach (var element in designationBudget)
            {
                totalBudgetHrs += element.BudgetHrs;
                totalBudgetCost += element.BudgetCost;
                totalAllocatedhrs += element.AllocatedHrs;
                totalAllocatedCost += element.BudgetCost;
                totalTimesheetHrs += element.TimesheetHrs;
                totalTImesheetCost += element.TimesheetCost;
            }
            resourceAllocationDetails[0].TotalAllocatedCost = totalAllocatedCost;
            resourceAllocationDetails[0].ConsumnedCost = totalTImesheetCost;
            resourceAllocationDetails[0].ConsumedHours = (int)totalTimesheetHrs;

            resourceAllocationDetails[0].RemainingCost = totalTImesheetCost - totalAllocatedCost;
            resourceAllocationDetails[0].RemainingHours = (int)(totalAllocatedhrs - totalTimesheetHrs);
            resourceAllocationDetails[0].PercentageCost = (totalTImesheetCost / totalAllocatedCost) * 100;
            resourceAllocationDetails[0].PercentageHrs = (totalTimesheetHrs / totalAllocatedhrs) * 100;

            BudgetOverviewLimit limitResult = new BudgetOverviewLimit();
            limitResult.IsTimesheetHoursCross = totalTimesheetHrs >= (0.8 * totalAllocatedhrs) ? true : false;
            limitResult.IsBudgetValueCross = (0.9 * totalAllocatedhrs) >= totalBudgetHrs ? true : false;

            return limitResult;
        }
    }
}
