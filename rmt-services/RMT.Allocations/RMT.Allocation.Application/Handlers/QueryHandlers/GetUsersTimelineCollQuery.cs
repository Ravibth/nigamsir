// Not in used.

//using MediatR;
//using RMT.Allocation.Application.DTOs;
//using RMT.Allocation.Application.HttpServices.DTOs;
//using RMT.Allocation.Application.IHttpServices;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Application.Utils;
//using RMT.Allocation.Domain;
//using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using RMT.Allocation.Infrastructure;
//using System.Collections.Generic;
//using static RMT.Allocation.Domain.ConstantsDomain;
//using Constants = RMT.Allocation.Infrastructure.Constants;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{
//    public class GetRolloverUsersTimelineCollQuery : IRequest<List<GetUsersTimelineResponse>>
//    {
//        public List<GetUsersTimelineDTO> timeLineQuery { get; set; }

//        public int rollOverDays { get; set; }

//        public string pipelineCode { get; set; }
//        public string jobCode { get; set; }

//    }

//    public class GetRolloverUsersTimelineCollQueryHandler : IRequestHandler<GetRolloverUsersTimelineCollQuery, List<GetUsersTimelineResponse>>
//    {
//        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;

//        private readonly IResourceAllocationRepository _resourceAllocationRepository;

//        public GetRolloverUsersTimelineCollQueryHandler(IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi, IResourceAllocationRepository resourceAllocationRepository)
//        {
//            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
//            _resourceAllocationRepository = resourceAllocationRepository;
//        }

//        public async Task<List<GetUsersTimelineResponse>> Handle(GetRolloverUsersTimelineCollQuery requestColl, CancellationToken cancellationToken)
//        {
//            List<GetUsersTimelineResponse> response = new List<GetUsersTimelineResponse>();

//            DateTime rolledOverStartDate;
//            DateTime rolledOverEndDate;

//            foreach (var request in requestColl.timeLineQuery)
//            {
//                rolledOverStartDate = request.start_date.AddDays(requestColl.rollOverDays);
//                rolledOverEndDate = request.end_date.AddDays(requestColl.rollOverDays);

//                GetEmployeeLeaves getEmployeeLeaves = new GetEmployeeLeaves { emails = new List<string>() { request.emails }, start_date = request.start_date, end_date = rolledOverEndDate };
//                List<EmployeeLeavesDTO> employeeLeaves = await _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);
//                //perday
//                List<ResourceAllocation> resourceAllocations = await _resourceAllocationRepository.GetUserAllocationsByEmailAndDates(new List<string>() { request.emails.ToLower() }, request.start_date, rolledOverEndDate);
//                List<ResAllocationDays> resourceAllocationByDays = await _resourceAllocationRepository.GetUserPerDayAllocationsByEmailAndDates(new List<string>() { request.emails.ToLower() }, request.start_date, rolledOverEndDate);
//                //await Task.WhenAll(allocations);

//                //foreach (var user in request.emails)
//                {
//                    var user = request.emails;
//                    //EmployeeLeavesDTO userLeavesFetched = employeeLeaves.Result.FirstOrDefault(m => m.email.ToLower() == user.ToLower());
//                    //EmployeeLeavesDTO userLeavesFetched = employeeLeaves.Result.FirstOrDefault(m => m.email.ToLower() == user.ToLower()
//                    //&& (m.leaves.Where(l => l.start_date >= request.start_date && l.end_date <= rolledOverEndDate).Any()));
//                    //todo done by me :- 
//                    EmployeeLeavesDTO userLeavesFetched = employeeLeaves.Where(m => m.email.ToLower() == user.ToLower()).FirstOrDefault();
//                    List<LeavesDTO> empLeaves = userLeavesFetched.leaves.OrderBy(d => d.start_date).ThenBy(l => l.end_date).ToList();
//                    var userPerDayAllocationsFetched = resourceAllocationByDays.Where(m => m.EmpEmail.ToLower().Trim() == user.ToLower().Trim()).OrderBy(d => d.ConfirmedAllocationStartDate).ToList();//apply the below conditions
//                    var userAllocationFetched = resourceAllocations
//                                                .Where(m => m.EmpEmail.ToLower().Trim() == user.ToLower().Trim() && m.PipelineCode.ToLower().Trim() == requestColl.pipelineCode.ToLower().Trim())
//                                                .OrderBy(d => d.ConfirmedAllocationStartDate)
//                                                .ToList();

//                    var userTimeline = GetUserRolloverTimeline(request.start_date, rolledOverEndDate, userPerDayAllocationsFetched, empLeaves, rolledOverStartDate, requestColl.pipelineCode, userAllocationFetched, requestColl.rollOverDays);

//                    response.Add(new GetUsersTimelineResponse()
//                    {
//                        email = user,
//                        usersTimelines = userTimeline
//                    });
//                    //return response;
//                    //var userAllocationsFetched = allocations.Result.Where(m => m.EmpEmail.ToLower() == user.ToLower());

//                    //List<UsersTimeline> usersTimelines = new List<UsersTimeline>();
//                    //if (userLeavesFetched != null)
//                    //{
//                    //    foreach (var leaveItem in userLeavesFetched.leaves)
//                    //    {
//                    //        usersTimelines.Add(new UsersTimeline { start = leaveItem.start_date, end = leaveItem.end_date, timeline_type = leaveItem.leaveType, timeline_display_text = "Leave", pipelineCode = string.Empty });
//                    //    }
//                    //}

//                    //if (userAllocationsFetched != null)
//                    //{
//                    //    foreach (var allocationItem in userAllocationsFetched)
//                    //    {
//                    //        usersTimelines.Add(new UsersTimeline
//                    //        {
//                    //            start = (DateTime)allocationItem.ConfirmedAllocationStartDate,
//                    //            end = (DateTime)allocationItem.ConfirmedAllocationEndDate,
//                    //            timeline_type = TimelineType.ALLOCATION,
//                    //            timeline_display_text = Convert.ToString(allocationItem.ConfirmedPerDayHours) + "hrs Allocated",
//                    //            pipelineCode = allocationItem.PipelineCode
//                    //        });

//                    //if (allocationItem.RecordType.ToLower() != EAllocationRecordType.RolloverAllocation.ToString().ToLower() && allocationItem.PipelineCode.ToLower() == requestColl.pipelineCode.ToLower())
//                    //        {
//                    //            //For Rollover proposed records
//                    //            usersTimelines.Add(new UsersTimeline
//                    //            {
//                    //                start = (DateTime)allocationItem.ConfirmedAllocationStartDate.Value.AddDays(requestColl.rollOverDays),
//                    //                end = (DateTime)allocationItem.ConfirmedAllocationEndDate.Value.AddDays(requestColl.rollOverDays),
//                    //                timeline_type = "rolloverproposed",
//                    //                timeline_display_text = Convert.ToString(allocationItem.ConfirmedPerDayHours) + "hrs Allocated",
//                    //                pipelineCode = allocationItem.PipelineCode,
//                    //            });
//                    //        }
//                    //    }
//                    //}
//                    //usersTimelines = usersTimelines.OrderBy(m => m.start).ToList();

//                    //List<UsersTimeline> avaiableTimelines = new List<UsersTimeline>();
//                    //if (usersTimelines.Count > 0)
//                    //{
//                    //    if (request.start_date < usersTimelines[0].start)
//                    //    {
//                    //        var starttime = request.start_date;
//                    //        var endtime = usersTimelines[0].start;
//                    //        avaiableTimelines.Add(new UsersTimeline
//                    //        {
//                    //            start = starttime,
//                    //            end = endtime,
//                    //            timeline_type = TimelineType.AVAILABLE,
//                    //            timeline_display_text = "Available",
//                    //            pipelineCode = string.Empty
//                    //        });
//                    //    }

//                    //    for (int i = 0; i < usersTimelines.Count; i++)
//                    //    {
//                    //        if (usersTimelines[i].start <= rolledOverEndDate && usersTimelines[i].end >= request.start_date)
//                    //        {
//                    //            var starttime = (usersTimelines[i].end > request.start_date) ? usersTimelines[i].end : request.start_date;
//                    //            var endtime = (i + 1 < usersTimelines.Count && usersTimelines[i + 1] != null && usersTimelines[i + 1].start < rolledOverEndDate) ? usersTimelines[i + 1].start : rolledOverEndDate;
//                    //            avaiableTimelines.Add(new UsersTimeline
//                    //            {
//                    //                start = starttime,
//                    //                end = endtime,
//                    //                timeline_type = TimelineType.AVAILABLE,
//                    //                timeline_display_text = "Available",
//                    //                pipelineCode = string.Empty
//                    //            });
//                    //        }
//                    //    }
//                    //}
//                    //else
//                    //{
//                    //    avaiableTimelines.Add(new UsersTimeline { start = request.start_date, end = rolledOverEndDate, timeline_type = TimelineType.AVAILABLE, timeline_display_text = "Available", pipelineCode = string.Empty });
//                    //}
//                    //foreach (var item in avaiableTimelines)
//                    //{
//                    //    usersTimelines.Add(item);
//                    //}

//                    //usersTimelines = usersTimelines.OrderBy(m => m.start).ToList();
//                    //response.Add(new GetUsersTimelineResponse
//                    //{
//                    //    email = user,
//                    //    usersTimelines = usersTimelines
//                    //});
//                }
//            }

//            return response;
//        }

//        public static List<UsersTimeline> GetUserRolloverTimeline(DateTime start_date, DateTime rolledover_end_date, List<ResAllocationDays> perDayAllocations, List<LeavesDTO> leaves, DateTime rolledover_start_date, string pipelineCode, List<ResourceAllocation> userAllocations, int rollOverDays)
//        {
//            List<UsersTimeline> usersTimelines = new List<UsersTimeline>();
//            Dictionary<DateTime, int> dateIndex = new Dictionary<DateTime, int>();
//            //make user time_line by days
//            DateTime start_date_counter = start_date.Date;
//            while (start_date_counter <= rolledover_end_date.Date)
//            {
//                usersTimelines.Add(new UsersTimeline()
//                {
//                    start = start_date_counter,
//                    end = start_date_counter,
//                    HoursAlotted = 0,
//                    timeline_type = TimelineType.AVAILABLE,
//                    timeline_display_text = TimelineDisplayText.AVAILABLE
//                });
//                dateIndex.Add(start_date_counter, usersTimelines.Count - 1);
//                start_date_counter = start_date_counter.AddDays(1);
//            }
//            //putting in allocations
//            for (var x = 0; x < perDayAllocations.Count; x++)
//            {
//                DateTime allocStartDate0 = ((DateTime)perDayAllocations[x].ConfirmedAllocationStartDate).Date;
//                DateTime allocStartDate = allocStartDate0.Date;
//                DateTime allocEndDate0 = ((DateTime)perDayAllocations[x].ConfirmedAllocationStartDate).Date;
//                DateTime allocEndDate = allocEndDate0.Date;
//                var startIndex = dateIndex[allocStartDate];
//                var endIndex = dateIndex[allocEndDate];
//                //Console.WriteLine(allocStartDate + " " + allocEndDate);
//                for (var i = startIndex; i <= endIndex; i++)
//                {
//                    usersTimelines[i].HoursAlotted = usersTimelines[i].HoursAlotted + perDayAllocations[x].ConfirmedPerDayHours;
//                    if (usersTimelines[i].HoursAlotted >= Constants.WorkingHourPerDay)
//                    {
//                        usersTimelines[i].timeline_type = TimelineType.ALLOCATION;
//                        usersTimelines[i].timeline_display_text = TimelineDisplayText.ALLOCATION;
//                    }

//                }
//            }
//            if (userAllocations.Count > 0)
//            {
//                foreach (var userAlloc in userAllocations)
//                {
//                    DateTime allocStartDateTime = ((DateTime)userAlloc.ConfirmedAllocationStartDate).Date;
//                    DateTime allocStartDate = allocStartDateTime.Date;
//                    DateTime allocEndDateTime = ((DateTime)userAlloc.ConfirmedAllocationEndDate).Date;
//                    DateTime allocEndDate = allocEndDateTime.Date;
//                    DateTime rollOverStartDate = allocStartDate.AddDays(rollOverDays).Date;
//                    DateTime rollOverEndDate = allocEndDate.AddDays(rollOverDays).Date;
//                    var startIdx = dateIndex[allocStartDate];
//                    var endIdx = dateIndex[allocEndDate];
//                    var rollOverStartIdx = dateIndex[rollOverStartDate];
//                    var rollOverEndIdx = dateIndex[rollOverEndDate];
//                    for (var i = startIdx; i <= endIdx; i++)
//                    {
//                        usersTimelines[i].timeline_type = userAlloc.RecordType.ToLower().Trim() == EAllocationRecordType.RolloverAllocation.ToString().ToLower().Trim()
//                            ? TimelineType.ROLLOVERALLOCATION : TimelineType.ALLOCATION;//EXPIRED
//                        usersTimelines[i].timeline_display_text = Convert.ToString(userAlloc.ConfirmedPerDayHours) + " hrs Allocated";
//                        //TimelineDisplayText.ALLOCATION + " Same Project";
//                    }
//                    if (userAlloc.RecordType.ToLower().Trim() != EAllocationRecordType.RolloverAllocation.ToString().ToLower().Trim())
//                    {
//                        for (var i = rollOverStartIdx; i <= rollOverEndIdx; i++)
//                        {
//                            usersTimelines[i].timeline_type = TimelineType.ROLLOVERPROPOSED;//ROLLOVERPROPOSED
//                            usersTimelines[i].timeline_display_text = Convert.ToString(userAlloc.ConfirmedPerDayHours) + " hrs Proposed";
//                        }

//                    }
//                }
//            }
//            //putting in rollforward
//            //if (rolledover_start_date != null && rolledover_end_date != null)
//            //{
//            //    var startIdx = dateIndex[rolledover_start_date.Date];
//            //    var endIdx = dateIndex[rolledover_end_date.Date];
//            //    for (var x = startIdx; x <= endIdx; x++)
//            //    {
//            //        usersTimelines[x].timeline_type = TimelineType.ROLLOVERPROPOSED;
//            //        usersTimelines[x].timeline_display_text = TimelineDisplayText.ROLLFORWARD;
//            //    }
//            //}
//            //Inserting Leaves in timeline
//            for (var s = 0; s < leaves.Count; s++)
//            {
//                DateTime leaveStartDate = leaves[s].start_date.Date;
//                DateTime leaveEndDate = leaves[s].end_date.Date;
//                if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate < usersTimelines[0].start.Date)
//                {
//                    //Console.WriteLine("case - 1");
//                    continue;
//                }
//                else if (leaveStartDate > usersTimelines[usersTimelines.Count - 1].end.Date && leaveEndDate > usersTimelines[usersTimelines.Count - 1].end.Date)
//                {
//                    //Console.WriteLine("case - 2");
//                    continue;
//                }
//                else if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
//                {
//                    //Console.WriteLine("case - 3");
//                    var startIndex = 0;
//                    var endIndex = dateIndex[leaveEndDate];
//                    for (var x = startIndex; x <= endIndex; x++)
//                    {
//                        usersTimelines[x].timeline_type = leaves[s].leaveType;
//                        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
//                    }
//                }
//                else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
//                {
//                    //Console.WriteLine("case - 4");
//                    var startIndex = dateIndex[leaveStartDate];
//                    var endIndex = dateIndex[leaveEndDate];
//                    for (var x = startIndex; x <= endIndex; x++)
//                    {
//                        usersTimelines[x].timeline_type = leaves[s].leaveType;
//                        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
//                    }
//                }
//                else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
//                {
//                    //Console.WriteLine("case - 5");
//                    var startIndex = dateIndex[leaveStartDate];
//                    var endIndex = usersTimelines.Count - 1;
//                    for (var x = startIndex; x <= endIndex; x++)
//                    {
//                        usersTimelines[x].timeline_type = leaves[s].leaveType;
//                        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
//                    }
//                }
//                else if (leaveStartDate <= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
//                {
//                    //Console.WriteLine("case - 6");
//                    var startIndex = 0;
//                    var endIndex = usersTimelines.Count - 1;
//                    for (var x = startIndex; x <= endIndex; x++)
//                    {
//                        usersTimelines[x].timeline_type = leaves[s].leaveType;
//                        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
//                    }
//                }
//            }
//            //final result
//            var strPtr = 0;
//            var endPtr = 0;
//            List<UsersTimeline> result = new List<UsersTimeline>();
//            while (endPtr < usersTimelines.Count)
//            {
//                if (usersTimelines[strPtr].timeline_type.ToLower().Trim() == usersTimelines[endPtr].timeline_type.ToLower().Trim())
//                {
//                    endPtr++;
//                }
//                else
//                {
//                    UsersTimeline addPart = new UsersTimeline()
//                    {
//                        start = usersTimelines[strPtr].start,
//                        end = usersTimelines[endPtr - 1].start.AddHours(23).AddMinutes(59).AddSeconds(50),
//                        timeline_type = usersTimelines[strPtr].timeline_type,
//                        timeline_display_text = usersTimelines[strPtr].timeline_display_text,
//                    };
//                    result.Add(addPart);
//                    strPtr = endPtr;
//                }
//            }
//            if (strPtr != endPtr)
//            {
//                UsersTimeline addLast = new UsersTimeline()
//                {
//                    start = usersTimelines[strPtr].start,
//                    end = usersTimelines[endPtr - 1].start.AddHours(23).AddMinutes(59).AddSeconds(50),
//                    timeline_type = usersTimelines[strPtr].timeline_type,
//                    timeline_display_text = usersTimelines[strPtr].timeline_display_text,
//                };
//                result.Add(addLast);
//            }
//            return result;
//        }
//    }
//}
