using MediatR;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class BudgetOverviewCommand : IRequest<List<BudgetOverviewDto>>
    {
        public List<KeyValuePair<string, string>> JobCodes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class BudgetOverviewCommandHandler : IRequestHandler<BudgetOverviewCommand, List<BudgetOverviewDto>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IMediator _mediator;
        private readonly ProjectServiceHttpApi _projectServiceHttpApi;
        private readonly WCGTTimesheetHttpApi _wCGTTimesheetHttpApi;

        public BudgetOverviewCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IMediator mediator,
            ProjectServiceHttpApi projectServiceHttpApi, WCGTTimesheetHttpApi wCGTTimesheetHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _mediator = mediator;
            _projectServiceHttpApi = projectServiceHttpApi;
            _wCGTTimesheetHttpApi = wCGTTimesheetHttpApi;
        }

        public async Task<List<BudgetOverviewDto>> Handle(BudgetOverviewCommand request, CancellationToken cancellationToken)
        {
            BudgetOverviewRequest budgetOverviewRequest = AllocationMapper.Mapper.Map<BudgetOverviewRequest>(request);
            List<BudgetOverviewDto> resourceAllocationDetails = await _resourceAllocationRepository.GetBudgetOverview(budgetOverviewRequest);
            if (request.JobCodes != null && request.JobCodes.Count > 0)
            {
                var projectDetails = await _projectServiceHttpApi.GetProjectDetailsByCode(request.JobCodes[0].Key, request.JobCodes[0].Value);
                var jobData = await _wCGTTimesheetHttpApi.GetJobByJobCode(request.JobCodes[0].Key, request.JobCodes[0].Value);
                string jobType = string.Empty;
                Double nonChargeableBudget = 0;
                Double nonChargeableOriginalBudget = 0;
                double jobFees = 0;
                if (jobData != null)
                {
                    Double.TryParse(jobData.agreedJobFee + string.Empty, out jobFees);
                }

                if (projectDetails != null && jobData != null && projectDetails.ChargableType == Constants.nonChargeableType)
                {
                    jobType = Constants.nonChargeableType;
                    Double.TryParse(jobData.jobBudgetValue + string.Empty, out nonChargeableBudget);
                    Double.TryParse(jobData.jobBudgetValue + string.Empty, out nonChargeableOriginalBudget);
                }

                DesignationBudgetQuery query = new() { JobCode = request.JobCodes[0].Value, PipelineCode = request.JobCodes[0].Key };
                var designationBudget = await _mediator.Send(query);
                if (designationBudget != null && designationBudget.Count > 0)
                {
                    double totalBudgetHrs = 0;
                    double totalBudgetCost = 0;
                    double totalOriginalBudgetCost = 0;
                    double totalOriginalBudgetHrs = 0;
                    double totalAllocatedhrs = 0;
                    double totalAllocatedCost = 0;
                    double totalTimesheetHrs = 0;
                    double totalTImesheetCost = 0;

                    foreach (var element in designationBudget)
                    {
                        totalBudgetHrs += element.BudgetHrs;
                        totalBudgetCost += element.BudgetCost;
                        totalOriginalBudgetCost += element.OriginalBudgetCost;
                        totalOriginalBudgetHrs += element.OriginalBudgetHrs;
                        totalAllocatedhrs += element.AllocatedHrs;
                        totalAllocatedCost += element.AllocatedCost;
                        totalTimesheetHrs += element.TimesheetHrs;
                        totalTImesheetCost += element.TimesheetCost;
                    }

                    if (resourceAllocationDetails != null && resourceAllocationDetails.Count > 0)
                    {
                        resourceAllocationDetails[0].TotalAllocatedCost = totalAllocatedCost;
                        resourceAllocationDetails[0].ConsumnedCost = totalTImesheetCost;
                        resourceAllocationDetails[0].ConsumedHours = (int)totalTimesheetHrs;

                        resourceAllocationDetails[0].RemainingCost = totalTImesheetCost - totalAllocatedCost;
                        resourceAllocationDetails[0].RemainingHours = (int)(totalAllocatedhrs - totalTimesheetHrs);
                        resourceAllocationDetails[0].PercentageCost = totalAllocatedCost == 0 ? 0 : Double.IsNaN((totalTImesheetCost / totalAllocatedCost) * 100) ? 0 : (totalTImesheetCost / totalAllocatedCost) * 100;
                        resourceAllocationDetails[0].PercentageHrs = totalAllocatedhrs == 0 ? 0 : (totalTimesheetHrs / totalAllocatedhrs) * 100;
                        resourceAllocationDetails[0].JobFee = jobFees;
                        resourceAllocationDetails[0].TotalAllocatedHours = Convert.ToInt32(totalAllocatedhrs);
                        if (jobType == Constants.nonChargeableType)
                        {
                            resourceAllocationDetails[0].BudgetedCost = nonChargeableBudget;
                            resourceAllocationDetails[0].OriginalBudgetCost = nonChargeableOriginalBudget;
                            resourceAllocationDetails[0].BudgetedHrs = 0;
                            resourceAllocationDetails[0].OriginalBudgetHrs = 0;
                        }
                        else
                        {
                            resourceAllocationDetails[0].BudgetedCost = totalBudgetCost;
                            resourceAllocationDetails[0].OriginalBudgetCost = totalOriginalBudgetCost;
                            resourceAllocationDetails[0].BudgetedHrs = totalBudgetHrs;
                            resourceAllocationDetails[0].OriginalBudgetHrs = totalOriginalBudgetHrs;
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Allocation wise budget not found");
                    }
                }
                else
                {
                    //Console.WriteLine("Project Designation wise budget not found");
                }
            }
            else
            {
                //Console.WriteLine("request is null or count is zero");
            }

            return resourceAllocationDetails;
        }

    }
}
