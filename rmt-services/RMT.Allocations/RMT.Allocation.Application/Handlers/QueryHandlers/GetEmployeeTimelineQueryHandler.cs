//Not in use
//using MediatR;
//using RMT.Allocation.Application.HttpServices.DTOs;
//using RMT.Allocation.Application.IHttpServices;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{
//    public class GetEmployeeTimelineQuery : IRequest<List<EmployeeAllocationTimelineResponse>>
//    {
//        public string PipelineCode { get; set; }
//        public string JobCode { get; set; }
//    }
//    public class GetEmployeeTimelineQueryHandler : IRequestHandler<GetEmployeeTimelineQuery, List<EmployeeAllocationTimelineResponse>>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
//        public GetEmployeeTimelineQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
//        }
//        public async Task<List<EmployeeAllocationTimelineResponse>> Handle(GetEmployeeTimelineQuery request, CancellationToken cancellationToken)
//        {
//            List<EmployeeAllocationTimelineResponse> timelineResponses = new();
//            var allocations = await _resourceAllocationRepository.GetProjectsEmployeeByPipelineCode(request.PipelineCode, request.JobCode);
//            var allocationDetailsGrouping = allocations
//                .GroupBy(d => d.ResAllocDetailsId)
//                .Select(grp =>
//                    new
//                    {
//                        ResAllocDetailsId = grp.Key,
//                        MinStartDate = grp.Min(item => item.ConfirmedAllocationStartDate),
//                        MaxEndDate = grp.Max(item => item.ConfirmedAllocationEndDate)
//                    });
//            foreach (var allocation in allocations)
//            {
//                var allocationDetail = allocationDetailsGrouping.Where(d => d.ResAllocDetailsId == allocation.ResAllocDetailsId).FirstOrDefault();
//                var employeeEmails = new List<string>();
//                employeeEmails.Add(allocation.EmpEmail);
//                if (allocationDetail != null)
//                {
//                    GetEmployeeLeaves getEmployeeLeaves = new()
//                    {
//                        emails = employeeEmails,
//                        start_date = (DateTime)allocationDetail.MinStartDate,
//                        end_date = (DateTime)allocationDetail.MaxEndDate
//                    };
//                    List<EmployeeLeavesDTO> employeeLeaves = await _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);
//                    var allocList = new List<ResourceAllocation>();
//                    allocList.Add(allocation);
//                    List<GetUsersTimelineResponse> userTimeline = GetUsersTimelineQueryHandler.GetUserTimelines(getEmployeeLeaves.start_date, getEmployeeLeaves.end_date, employeeEmails, employeeLeaves, allocList);
//                    foreach (var timeline in userTimeline[0].usersTimelines)
//                    {
//                        EmployeeAllocationTimelineResponse resp = new()
//                        {
//                            //ProjectCode = allocation.ProjectCode,
//                            PipelineCode = allocation.PipelineCode,
//                            EmpEmail = allocation.EmpEmail,
//                            //Skills = allocation.Skills,
//                            StartDate = timeline.start,
//                            EndDate = timeline.end,
//                            ConfirmedAllocationStartDate = allocation.ConfirmedAllocationStartDate,
//                            ConfirmedAllocationEndDate = allocation.ConfirmedAllocationEndDate,
//                            ConfirmedPerDayHours = allocation.ConfirmedPerDayHours,
//                            JobCode = allocation.JobCode,
//                            JobName = allocation.JobName,
//                            Id = allocation.Id,
//                            RecordType = allocation.RecordType,
//                            AllocationStatus = allocation.AllocationStatus,
//                            //AllocationType = allocation.allocationt,
//                            //guid = allocation.,
//                            //Designation = ,
//                            EmpName = allocation.EmpName,
//                            PipelineName = allocation.PipelineName,
//                            RequisitionId = allocation.RequisitionId,
//                            timeline_display_text = timeline.timeline_display_text,
//                            timeline_type = timeline.timeline_type,
//                        };
//                        timelineResponses.Add(resp);
//                    }
//                }
//            }

//            return timelineResponses;
//        }

//        public void GetBreakup(List<ResourceAllocation> resourceAllocations, List<LeavesDTO> leaves)
//        {
//            List<GetUsersTimelineResponse> usersTimelineResponses = new List<GetUsersTimelineResponse>();
//            var allocations = resourceAllocations.OrderBy(d => d.ConfirmedAllocationStartDate).ToList();
//            var leave = leaves.OrderBy(d => d.start_date).ToList();
//            var i = 0;
//            var j = 0;
//            List<UsersTimeline> usersTimelines = new();
//            Dictionary<DateTime, int> dateIndex = new();
//            DateTime minAllocationStartDate = ((DateTime)allocations[0].ConfirmedAllocationStartDate).Date;
//            DateTime maxAllocationEndDate = ((DateTime)allocations[allocations.Count - 1].ConfirmedAllocationEndDate).Date;
//            DateTime minStartDate = minAllocationStartDate.Date;
//            DateTime maxEndDate = maxAllocationEndDate.Date;
//            var start = minStartDate;
//            while (start < maxEndDate)
//            {
//                usersTimelines.Add(new UsersTimeline()
//                {
//                    start = start,
//                    end = start,
//                    timeline_display_text = TimelineDisplayText.AVAILABLE,
//                    timeline_type = TimelineType.AVAILABLE
//                });
//                dateIndex.Add(start, usersTimelines.Count - 1);
//                start.AddDays(1);
//            }

//        }
//    }
//}
