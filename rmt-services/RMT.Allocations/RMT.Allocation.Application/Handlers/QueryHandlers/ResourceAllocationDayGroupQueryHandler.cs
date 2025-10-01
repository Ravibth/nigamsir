using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class ResourceAllocationDayGroupQuery : IRequest<List<AllocationDayGroupResponseDTO>>
    {
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public string TimeOption { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class ResourceAllocationDayGroupQueryHandler : IRequestHandler<ResourceAllocationDayGroupQuery, List<AllocationDayGroupResponseDTO>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly ProjectServiceHttpApi _projectServiceHttpApi;
        private readonly WCGTResourceTimesheetHttpApi _wCGTesourceTimesheetHttpApi;
        public ResourceAllocationDayGroupQueryHandler(IResourceAllocationRepository resourceAllocationRepository, ProjectServiceHttpApi projectServiceHttpApi, WCGTResourceTimesheetHttpApi wCGTResourceTimesheetHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _wCGTesourceTimesheetHttpApi = wCGTResourceTimesheetHttpApi;
        }
        public async Task<List<AllocationDayGroupResponseDTO>> Handle(ResourceAllocationDayGroupQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _resourceAllocationRepository.ResourceAllocationDayGroup(request.TimeOption, request.PipelineCode, request.JobCode, request.StartDate, request.EndDate);
                var resourceAllocationResponse = AllocationMapper.Mapper.Map<List<AllocationDayGroupResponseDTO>>(response);

                //fetch budget
                var projectBudget = await _projectServiceHttpApi.GetProjectBudget(request.PipelineCode, request.JobCode);
                Dictionary<string, double> allocationBudget = new Dictionary<string, double>();
                foreach (var item in projectBudget)
                {
                    allocationBudget.Add(item.Grade, item.RatePerHour);
                }

                //Calculate Allocation Cost
                foreach (var allocation in resourceAllocationResponse)
                {
                    //bool isRate = allocationBudget.TryGetValue(allocation.designation, out double rate);
                    //if(isRate) {
                    //    allocation.totalAllocationCost = rate * allocation.totalAllocationTime;
                    //}
                    allocation.key = GetKey(allocation.monthname, request.TimeOption);
                }

                //fetch timesheet
                //List<WCGTTimesheetGroupDTO> timesheetResponse1 = await _wCGTesourceTimesheetHttpApi.GetTimesheetGroupDataByJobCode(request.JobCode, request.TimeOption, request.StartDate, request.EndDate);

                List<TimesheetDaysReponseDTO> timesheetResponse = await _resourceAllocationRepository.GetTimesheetDaysReponse(request.JobCode, request.TimeOption, request.StartDate, request.EndDate);


                //Calculate Timesheet cost
                foreach (var timesheet in timesheetResponse)
                {
                    //bool isRate = allocationBudget.TryGetValue(timesheet.designation, out double rate);
                    //if (isRate)
                    //{
                    //    timesheet.totaltimesheetcost = rate * timesheet.totaltime;
                    //}
                    timesheet.key = GetKey(timesheet.monthname, request.TimeOption);
                }

                // timesheet in combine data 

                List<AllocationDayGroupResponseDTO> combineData = timesheetResponse.GroupBy(t => t.key).Select(group => new AllocationDayGroupResponseDTO
                {
                    key = group.Key,
                    totalTimesheetTime = (double)group.Sum(g => g.totaltime),
                    totalTimesheetCost = (double)group.Sum(g => g.timesheetcost)
                }).ToList();


                //Combine allocation with timehseet
                var temp = resourceAllocationResponse.GroupBy(t => t.key).Select(group => new AllocationDayGroupResponseDTO
                {
                    key = group.Key,
                    totalAllocationCost = group.Sum(g => g.totalAllocationCost),
                    totalAllocationTime = group.Sum(g => g.totalAllocationTime)
                }).ToList();

                foreach (var allocation in temp)
                {
                    var obj = combineData.FirstOrDefault(c => c.key == allocation.key);
                    if (obj != null)
                    {
                        obj.totalAllocationCost = allocation.totalAllocationCost / 8;
                        obj.totalAllocationTime = allocation.totalAllocationTime;
                    }
                    else
                    {
                        combineData.Add(allocation);
                    }
                }

                return combineData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetKey(DateTime date, string timeOption)
        {
            if (timeOption.ToLower() == "monthly" || timeOption.ToLower() == "quarterly")
            {
                DateTime ist = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                return ist.ToString("MM/yy");
            }
            else
            {
                DateTime ist = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                return ist.ToString("yyyy/MM/dd");
            }
        }
    }
}
