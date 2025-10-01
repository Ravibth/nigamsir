using MediatR;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class DesignationBudgetQuery : IRequest<List<DesignationBudget>>
    {
        public string JobCode { get; set; }
        public string PipelineCode { get; set; }
    }
    public class DesignationBudgetQueryHandler : IRequestHandler<DesignationBudgetQuery, List<DesignationBudget>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly ProjectServiceHttpApi _projectServiceHttpApi;
        private readonly WCGTTimesheetHttpApi _wCGTTimesheetHttpApi;

        public DesignationBudgetQueryHandler(IResourceAllocationRepository resourceAllocationRepository, ProjectServiceHttpApi projectServiceHttpApi, WCGTTimesheetHttpApi wCGTTimesheetHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _wCGTTimesheetHttpApi = wCGTTimesheetHttpApi;
        }

        public async Task<List<DesignationBudget>> Handle(DesignationBudgetQuery request, CancellationToken cancellationToken)
        {
            List<DesignationBudget> designationBudgets = new List<DesignationBudget>();
            List<ResourceAllocationDesignation> allocationBudget = await _resourceAllocationRepository.GetDesignationBudget(request.PipelineCode, request.JobCode);
            if (allocationBudget == null)
            {
                allocationBudget = new List<ResourceAllocationDesignation>();
            }
            //List<TimesheetResponseDTO> timesheet1 = await _wCGTTimesheetHttpApi.GetTimesheetDataByJobCode(request.JobCode);
            List<TimesheetResponseDTO> timesheet = await _resourceAllocationRepository.GetProjectDesignationTimesheet(request.JobCode);

            var projectBudget = await _projectServiceHttpApi.GetProjectBudget(request.PipelineCode, request.JobCode);
            // projectBudget.Count == 0 ? true : false;
            List<string> gradeList = new List<string>();

            var allocationGrade = allocationBudget
                .Where(m => m.grade != null)
                .Select(a => a.grade)
                .ToList();
            var timesheetGrade = timesheet
                .Where(m => m.Gradename != null)
                .Select(a => a.Gradename)
                .ToList();
            gradeList = projectBudget
                .Where(m => m.Grade != null)
                .Select(a => a.Grade)
                .ToList();
            gradeList.AddRange(allocationGrade);
            gradeList.AddRange(timesheetGrade);
            gradeList = gradeList.Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
            foreach (var item in gradeList)
            {
                DesignationBudget budget = new DesignationBudget();
                if (allocationBudget != null)
                {
                    var selectedAllocation = allocationBudget.Find(t => t.grade != null && t.grade.ToLower() == item.ToLower());
                    budget.AllocatedHrs = (selectedAllocation == null || selectedAllocation.totaleffort == null) ? 0 : (double)selectedAllocation.totaleffort;
                    budget.AllocatedCost = (selectedAllocation == null || selectedAllocation.cost == null) ? 0 : (double)selectedAllocation.cost;
                }
                else
                {
                    budget.AllocatedHrs = 0;
                    budget.AllocatedCost = 0;
                }

                var selectedBudget = projectBudget.Find(t => t.Grade != null && t.Grade.ToLower() == item.ToLower());
                budget.JobCode = request.JobCode;
                budget.Grade = item;
                //budget.Designation = item;

                budget.OriginalBudgetHrs = (selectedBudget == null || selectedBudget.OriginalBudgetedHour == null) ? 0 : selectedBudget.OriginalBudgetedHour;
                budget.OriginalBudgetCost = (selectedBudget == null || selectedBudget.OriginalBudgetedHour == null || selectedBudget.OriginalRatePerHour == null) ? 0 : selectedBudget.OriginalBudgetedHour * selectedBudget.OriginalRatePerHour;
                budget.BudgetCost = (selectedBudget == null || selectedBudget.BudgetedHour == null || selectedBudget.RatePerHour == null) ? 0 : selectedBudget.BudgetedHour * selectedBudget.RatePerHour;
                budget.BudgetHrs = (selectedBudget == null || selectedBudget.BudgetedHour == null ? 0 : selectedBudget.BudgetedHour);

                var selectedTimesheet = timesheet.Find(t => t.Gradename != null && t.Gradename.ToLower() == item.ToLower());
                budget.TimesheetHrs = selectedTimesheet == null ? 0 : selectedTimesheet.TimesheetHrs;
                budget.TimesheetCost = selectedTimesheet == null ? 0 : selectedTimesheet.TimesheetCost;

                // budget.TimesheetHrs = timesheet.ContainsKey(budget.Designation) ? timesheet[budget.Designation] : 0;
                // budget.TimesheetCost = budget.TimesheetHrs * item.RatePerHour;

                //Timesheet Need to Calculate
                designationBudgets.Add(budget);
            }
            return designationBudgets.OrderBy(m => m.Grade).ToList();
        }
    }
}
