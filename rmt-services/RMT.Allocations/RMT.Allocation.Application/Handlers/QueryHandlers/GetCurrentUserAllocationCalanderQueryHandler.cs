using AutoMapper.Internal;
using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//already made changes
namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetCurrentUserAllocationCalanderQuery : IRequest<List<EmployeeProject>>
    {
        public string UserEmail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string>? PipelineCodes { get; set; }
    }
    public class GetCurrentUserAllocationCalanderQueryHandler : IRequestHandler<GetCurrentUserAllocationCalanderQuery, List<EmployeeProject>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
        public GetCurrentUserAllocationCalanderQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
        }
        public async Task<List<EmployeeProject>> Handle(GetCurrentUserAllocationCalanderQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationDetailsResponse> currentResourceAllocationDetails = await _resourceAllocationRepository.GetUserAllocationDetailsByEmailAndDates(new List<string> { request.UserEmail }, request.StartDate, request.EndDate, request.PipelineCodes, false);
            GetEmployeeLeaves getEmployeeLeavesForMonth = new GetEmployeeLeaves { emails = new List<string>() { request.UserEmail }, start_date = request.StartDate, end_date = request.EndDate };
            List<EmployeeLeavesDTO> genericEmployeeLeaves = await _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeavesForMonth);
            List<LeavesDTO> employeeMonthLeaves = new List<LeavesDTO>();
            if (genericEmployeeLeaves.Count > 0)
            {
                employeeMonthLeaves = genericEmployeeLeaves[0].leaves.OrderBy(d => d.start_date).ThenBy(d => d.end_date).ToList();
            }
            List<UsersTimeline> userLeavesTimelines = GetUserLeavesTimeline(employeeMonthLeaves, request.StartDate, request.EndDate);

            List<EmployeeProject> employeeTimeline = new List<EmployeeProject>();
            foreach (var leaveTimeline in userLeavesTimelines)
            {
                EmployeeProject empProject = new EmployeeProject()
                {
                    StartDate = leaveTimeline.start,
                    EndDate = leaveTimeline.end,
                    timeline_type = leaveTimeline.timeline_type,
                    timeline_display_text = leaveTimeline.timeline_display_text,
                    ConfirmedPerDayHours = 0,
                };
                employeeTimeline.Add(empProject);
            }
            foreach (ResourceAllocationDetailsResponse details in currentResourceAllocationDetails)
            {
                if (details.ResourceAllocations.Count > 0)
                {
                    DateTime minStartDate = details.ResourceAllocations.Min(d => d.StartDate).ToDateTime(TimeOnly.MinValue);
                    DateTime minStartDateUtc = minStartDate.Date;
                    DateTime maxEndDate = details.ResourceAllocations.Max(d => d.EndDate).ToDateTime(TimeOnly.MaxValue);
                    DateTime maxEndDateUtc = maxEndDate.Date;
                    List<string> Emails = new()
                    {
                        details.EmpEmail
                    };
                    //GetEmployeeLeaves getEmployeeLeaves = new GetEmployeeLeaves { emails = Emails, start_date = minStartDate, end_date = maxEndDate };
                    //List<EmployeeLeavesDTO> employeeLeaves = await _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);

                    if (genericEmployeeLeaves.Count > 0)
                    {
                        var randomLeaves = genericEmployeeLeaves[0].leaves;
                        List<LeavesDTO> leaves = randomLeaves.OrderBy(d => d.start_date).ThenBy(d => d.end_date).ToList();

                        var allocations = details.ResourceAllocations.OrderBy(d => d.StartDate).ToList();
                        List<UsersTimeline> userTimelines = GetCurrentUserAllocationTimeline(allocations, leaves, request.StartDate, request.EndDate);
                        foreach (var userTimeline in userTimelines)
                        {
                            EmployeeProject empDetail = new()
                            {
                                PipelineCode = details.PipelineCode,
                                PipelineName = details.PipelineName,
                                //ProjectCode = details.ProjectCode,
                                AllocationStatus = details.AllocationStatus,
                                EmpName = details.EmpName,
                                EmpEmail = details.EmpEmail,
                                JobCode = details.JobCode,
                                JobName = details.JobName,
                                Id = details.Id,
                                guid = details.Id,
                                StartDate = userTimeline.start,
                                EndDate = userTimeline.end,
                                RequisitionId = details.RequisitionId,
                                //RecordType = details.RecordType,
                                ConfirmedPerDayHours = userTimeline.timeline_type.ToLower().ToString() == TimelineType.FULL_DAY_LEAVE.ToLower().ToString()
                                                        || userTimeline.timeline_type.ToLower().ToString() == TimelineType.HOLIDAY.ToLower().ToString()
                                                        ? 0 : userTimeline.HoursAlotted,
                                timeline_type = userTimeline.timeline_type,
                                timeline_display_text = userTimeline.timeline_display_text
                                //Skills = details.ResourceAllocationDetailsSkills
                                //AllocationType = details.alloca
                                //Designation = details.de
                            };
                            employeeTimeline.Add(empDetail);
                        }
                    }
                }
            }
            //var finalEmployeeTimeline = RemoveDuplicateLeavesAndHolidays(employeeTimeline);
            return employeeTimeline;
        }
        public static List<UsersTimeline> GetUserLeavesTimeline(List<LeavesDTO> leaves, DateTime startDate, DateTime endDate)
        {
            List<UsersTimeline> usersTimelines = new List<UsersTimeline>();
            Dictionary<DateTime, int> dateIndex = new Dictionary<DateTime, int>();
            DateTime minMonthStartDate = startDate;
            DateTime maxMonthEndDate = endDate;
            DateTime minStartDate = minMonthStartDate.Date;
            DateTime maxEndDate = maxMonthEndDate.Date;
            var start = minStartDate;
            while (start <= maxEndDate)
            {
                usersTimelines.Add(new UsersTimeline()
                {
                    start = start.Date,
                    end = start.Date,
                    timeline_display_text = TimelineDisplayText.AVAILABLE,
                    timeline_type = TimelineType.AVAILABLE,
                    HoursAlotted = 0
                });
                dateIndex.Add(start.Date, usersTimelines.Count - 1);
                start = start.AddDays(1);
            }
            for (var s = 0; s < leaves.Count; s++)
            {
                DateTime leaveDate = leaves[s].start_date.Date;
                if (dateIndex.ContainsKey(leaveDate))
                {
                    var leaveDateIndex = dateIndex[leaveDate];
                    switch (leaves[s].leaveType)
                    {
                        case TimelineType.HOLIDAY:
                            usersTimelines[leaveDateIndex].timeline_type = TimelineType.HOLIDAY;
                            usersTimelines[leaveDateIndex].timeline_display_text = TimelineDisplayText.HOLIDAY;
                            usersTimelines[leaveDateIndex].HoursAlotted = 0;
                            usersTimelines[leaveDateIndex].leave_hours = leaves[s].hours;
                            break;
                        case TimelineType.FULL_DAY_LEAVE:
                            usersTimelines[leaveDateIndex].timeline_type = TimelineType.FULL_DAY_LEAVE;
                            usersTimelines[leaveDateIndex].timeline_display_text = TimelineDisplayText.FULL_DAY_LEAVE;
                            usersTimelines[leaveDateIndex].HoursAlotted = 0;
                            usersTimelines[leaveDateIndex].leave_hours = leaves[s].hours;
                            break;
                        case TimelineType.FIRST_HALF_LEAVE:
                            usersTimelines[leaveDateIndex].timeline_type = TimelineType.FIRST_HALF_LEAVE;
                            usersTimelines[leaveDateIndex].timeline_display_text = TimelineDisplayText.FIRST_HALF_LEAVE;
                            usersTimelines[leaveDateIndex].leave_hours = leaves[s].hours;
                            break;
                        case TimelineType.SECOND_HALF_LEAVE:
                            usersTimelines[leaveDateIndex].timeline_type = TimelineType.SECOND_HALF_LEAVE;
                            usersTimelines[leaveDateIndex].timeline_display_text = TimelineDisplayText.SECOND_HALF_LEAVE;
                            usersTimelines[leaveDateIndex].leave_hours = leaves[s].hours;
                            break;
                        default:
                            break;
                    }
                }
                //DateTime leaveStartDate = leaves[s].start_date.Date;
                //DateTime leaveEndDate = leaves[s].end_date.Date;
                ////Console.WriteLine("leaves " + leaveStartDate + " " + leaveEndDate);
                //if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate < usersTimelines[0].start.Date)
                //{
                //    //Console.WriteLine("case - 1");
                //    continue;
                //}
                //else if (leaveStartDate > usersTimelines[usersTimelines.Count - 1].end.Date && leaveEndDate > usersTimelines[usersTimelines.Count - 1].end.Date)
                //{
                //    //Console.WriteLine("case - 2");
                //    continue;
                //}
                //else if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
                //{
                //    //Console.WriteLine("case - 3");
                //    var startIndex = 0;
                //    var endIndex = dateIndex[leaveEndDate];
                //    for (var x = startIndex; x <= endIndex; x++)
                //    {
                //        usersTimelines[x].timeline_type = leaves[s].leaveType;
                //        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
                //    }
                //}
                //else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
                //{
                //    //Console.WriteLine("case - 4");
                //    var startIndex = dateIndex[leaveStartDate];
                //    var endIndex = dateIndex[leaveEndDate];
                //    for (var x = startIndex; x <= endIndex; x++)
                //    {
                //        usersTimelines[x].timeline_type = leaves[s].leaveType;
                //        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
                //    }
                //}
                //else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
                //{
                //    //Console.WriteLine("case - 5");
                //    var startIndex = dateIndex[leaveStartDate];
                //    var endIndex = usersTimelines.Count - 1;
                //    for (var x = startIndex; x <= endIndex; x++)
                //    {
                //        usersTimelines[x].timeline_type = leaves[s].leaveType;
                //        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
                //    }
                //}
                //else if (leaveStartDate <= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
                //{
                //    //Console.WriteLine("case - 6");
                //    var startIndex = 0;
                //    var endIndex = usersTimelines.Count - 1;
                //    for (var x = startIndex; x <= endIndex; x++)
                //    {
                //        usersTimelines[x].timeline_type = leaves[s].leaveType;
                //        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
                //    }
                //}
            }
            List<UsersTimeline> result = new List<UsersTimeline>();
            var strPtr = 0;
            var endPtr = 0;
            while (endPtr < usersTimelines.Count)
            {
                if (usersTimelines[strPtr].timeline_type.ToLower().Trim() == usersTimelines[endPtr].timeline_type.ToLower().Trim())
                {
                    endPtr++;
                }
                else
                {
                    //check if leave hours needed or not
                    UsersTimeline addPart = new UsersTimeline()
                    {
                        start = usersTimelines[strPtr].start,
                        end = usersTimelines[endPtr - 1].start,
                        timeline_type = usersTimelines[strPtr].timeline_type,
                        timeline_display_text = usersTimelines[strPtr].timeline_display_text,
                        HoursAlotted = usersTimelines[strPtr].HoursAlotted
                    };
                    result.Add(addPart);
                    strPtr = endPtr;
                }
            }
            if (strPtr != endPtr)
            {
                UsersTimeline addLast = new UsersTimeline()
                {
                    start = usersTimelines[strPtr].start,
                    end = usersTimelines[endPtr - 1].start,
                    timeline_type = usersTimelines[strPtr].timeline_type,
                    timeline_display_text = usersTimelines[strPtr].timeline_display_text,
                    HoursAlotted = usersTimelines[strPtr].HoursAlotted
                };
                result.Add(addLast);
            }
            //should consider only leave and holidays should not consider partial leaves
            return result.Where(d => d.timeline_type.ToLower().Trim() != TimelineType.AVAILABLE.ToLower().Trim()).ToList();
        }
        /// <summary>
        /// NOT IN USE
        /// </summary>
        /// <param name="allocations"></param>
        /// <param name="leaves"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        //public static List<UsersTimeline> GetUserAllocationTimeline(List<ResourceAllocationResponse> allocations, List<LeavesDTO> leaves, DateTime startDate, DateTime endDate)
        //{
        //    List<UsersTimeline> usersTimelines = new List<UsersTimeline>();
        //    Dictionary<DateTime, int> dateIndex = new Dictionary<DateTime, int>();
        //    DateTime minAllocationStartDate = allocations[0].StartDate > DateOnly.FromDateTime(startDate) ? startDate : (DateTime)allocations[0].StartDate.ToDateTime(TimeOnly.MinValue);
        //    //DateTime minAllocationStartDate = (DateTime)startDate;
        //    DateTime maxAllocationEndDate = allocations[allocations.Count - 1].EndDate > DateOnly.FromDateTime(endDate) ? (DateTime)allocations[allocations.Count - 1].EndDate.ToDateTime(TimeOnly.MaxValue) : endDate;
        //    //DateTime maxAllocationEndDate = (DateTime)endDate;
        //    DateTime minStartDate = minAllocationStartDate.Date;
        //    DateTime maxEndDate = maxAllocationEndDate.Date;
        //    var start = minStartDate;
        //    while (start <= maxEndDate)
        //    {
        //        usersTimelines.Add(new UsersTimeline()
        //        {
        //            start = start.Date,
        //            end = start.Date,
        //            timeline_display_text = TimelineDisplayText.AVAILABLE,
        //            timeline_type = TimelineType.AVAILABLE,
        //            HoursAlotted = 0
        //        });
        //        dateIndex.Add(start.Date, usersTimelines.Count - 1);
        //        start = start.AddDays(1);
        //    }
        //    for (var s = 0; s < allocations.Count; s++)
        //    {
        //        DateTime allocStartDate0 = (DateTime)allocations[s].StartDate.ToDateTime(TimeOnly.MinValue);
        //        DateTime allocEndDate0 = (DateTime)allocations[s].EndDate.ToDateTime(TimeOnly.MaxValue);
        //        DateTime allocStartDate = allocStartDate0.Date;
        //        DateTime allocEndDate = allocEndDate0.Date;
        //        var startIndex = dateIndex[allocStartDate];
        //        var endIndex = dateIndex[allocEndDate];
        //        //Console.WriteLine("allocation date " + allocStartDate + " " + allocEndDate);
        //        for (var x = startIndex; x <= endIndex; x++)
        //        {
        //            usersTimelines[x].HoursAlotted = (int?)allocations[s].Efforts;
        //            usersTimelines[x].timeline_type = TimelineType.ALLOCATION;
        //            usersTimelines[x].timeline_display_text = TimelineDisplayText.ALLOCATION;
        //        }

        //    }
        //    for (var s = 0; s < leaves.Count; s++)
        //    {
        //        DateTime leaveStartDate = leaves[s].start_date.Date;
        //        DateTime leaveEndDate = leaves[s].end_date.Date;
        //        //Console.WriteLine("leaves " + leaveStartDate + " " + leaveEndDate);
        //        if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate < usersTimelines[0].start.Date)
        //        {
        //            //Console.WriteLine("case - 1");
        //            continue;
        //        }
        //        else if (leaveStartDate > usersTimelines[usersTimelines.Count - 1].end.Date && leaveEndDate > usersTimelines[usersTimelines.Count - 1].end.Date)
        //        {
        //            //Console.WriteLine("case - 2");
        //            continue;
        //        }
        //        else if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
        //        {
        //            //Console.WriteLine("case - 3");
        //            var startIndex = 0;
        //            var endIndex = dateIndex[leaveEndDate];
        //            for (var x = startIndex; x <= endIndex; x++)
        //            {
        //                usersTimelines[x].timeline_type = leaves[s].leaveType;
        //                usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
        //            }
        //        }
        //        else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
        //        {
        //            //Console.WriteLine("case - 4");
        //            var startIndex = dateIndex[leaveStartDate];
        //            var endIndex = dateIndex[leaveEndDate];
        //            for (var x = startIndex; x <= endIndex; x++)
        //            {
        //                usersTimelines[x].timeline_type = leaves[s].leaveType;
        //                usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
        //            }
        //        }
        //        else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
        //        {
        //            //Console.WriteLine("case - 5");
        //            var startIndex = dateIndex[leaveStartDate];
        //            var endIndex = usersTimelines.Count - 1;
        //            for (var x = startIndex; x <= endIndex; x++)
        //            {
        //                usersTimelines[x].timeline_type = leaves[s].leaveType;
        //                usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
        //            }
        //        }
        //        else if (leaveStartDate <= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
        //        {
        //            //Console.WriteLine("case - 6");
        //            var startIndex = 0;
        //            var endIndex = usersTimelines.Count - 1;
        //            for (var x = startIndex; x <= endIndex; x++)
        //            {
        //                usersTimelines[x].timeline_type = leaves[s].leaveType;
        //                usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
        //            }
        //        }
        //    }

        //    List<UsersTimeline> result = new List<UsersTimeline>();
        //    var strPtr = 0;
        //    var endPtr = 0;
        //    while (endPtr < usersTimelines.Count)
        //    {
        //        if (usersTimelines[strPtr].timeline_type.ToLower().Trim() == usersTimelines[endPtr].timeline_type.ToLower().Trim())
        //        {
        //            endPtr++;
        //        }
        //        else
        //        {
        //            UsersTimeline addPart = new UsersTimeline()
        //            {
        //                start = usersTimelines[strPtr].start,
        //                end = usersTimelines[endPtr - 1].start,
        //                timeline_type = usersTimelines[strPtr].timeline_type,
        //                timeline_display_text = usersTimelines[strPtr].timeline_display_text,
        //                HoursAlotted = usersTimelines[strPtr].HoursAlotted
        //            };
        //            result.Add(addPart);
        //            strPtr = endPtr;
        //        }
        //    }
        //    if (strPtr != endPtr)
        //    {
        //        UsersTimeline addLast = new UsersTimeline()
        //        {
        //            start = usersTimelines[strPtr].start,
        //            end = usersTimelines[endPtr - 1].start,
        //            timeline_type = usersTimelines[strPtr].timeline_type,
        //            timeline_display_text = usersTimelines[strPtr].timeline_display_text,
        //            HoursAlotted = usersTimelines[strPtr].HoursAlotted
        //        };
        //        result.Add(addLast);
        //    }
        //    return result.Where(d => d.timeline_type.ToLower().Trim() != TimelineType.AVAILABLE.ToLower().Trim()).ToList();
        //}

        public static List<UsersTimeline> GetCurrentUserAllocationTimeline(List<ResourceAllocationResponse> allocations, List<LeavesDTO> leaves, DateTime startDate, DateTime endDate)
        {
            List<UsersTimeline> usersTimelines = new List<UsersTimeline>();
            Dictionary<DateTime, int> dateIndex = new Dictionary<DateTime, int>();
            DateTime minStartDate = allocations[0].StartDate > DateOnly.FromDateTime(startDate) ? startDate.Date : (DateTime)allocations[0].StartDate.ToDateTime(TimeOnly.MinValue).Date;
            DateTime maxEndDate = allocations[allocations.Count - 1].EndDate > DateOnly.FromDateTime(endDate) ? (DateTime)allocations[allocations.Count - 1].EndDate.ToDateTime(TimeOnly.MaxValue).Date : endDate.Date;
            var start = minStartDate;
            Dictionary<DateTime, LeavesDTO> leavesDict = new Dictionary<DateTime, LeavesDTO>();
            foreach (var leave in leaves)
            {
                leavesDict.Add(leave.start_date, leave);
            }
            //mark everything as available
            while (start <= maxEndDate)
            {
                usersTimelines.Add(new UsersTimeline()
                {
                    start = start.Date,
                    end = start.Date,
                    timeline_display_text = TimelineDisplayText.AVAILABLE,
                    timeline_type = TimelineType.AVAILABLE,
                    HoursAlotted = 0
                });
                dateIndex.Add(start.Date, usersTimelines.Count - 1);
                start = start.AddDays(1);
            }
            //filling up allocation
            for (var s = 0; s < allocations.Count; s++)
            {
                DateTime allocStartDate0 = (DateTime)allocations[s].StartDate.ToDateTime(TimeOnly.MinValue);
                DateTime allocEndDate0 = (DateTime)allocations[s].EndDate.ToDateTime(TimeOnly.MaxValue);
                DateTime allocStartDate = allocStartDate0.Date;
                DateTime allocEndDate = allocEndDate0.Date;

                if (allocStartDate < usersTimelines[0].start.Date && allocEndDate < usersTimelines[0].start.Date)
                {
                    //Console.WriteLine("case - 1");
                    continue;
                }
                else if (allocStartDate > usersTimelines[usersTimelines.Count - 1].end.Date && allocEndDate > usersTimelines[usersTimelines.Count - 1].end.Date)
                {
                    //Console.WriteLine("case - 2");
                    continue;
                }
                else if (allocStartDate < usersTimelines[0].start.Date && allocEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
                {
                    //Console.WriteLine("case - 3");
                    var startIndex = 0;
                    var endIndex = dateIndex[allocEndDate];
                    for (var x = startIndex; x <= endIndex; x++)
                    {
                        var currentDate = usersTimelines[x].start.Date;
                        if ((!leavesDict.ContainsKey(currentDate) || (leavesDict.ContainsKey(currentDate)
                            && leavesDict[currentDate].leaveType != TimelineType.FULL_DAY_LEAVE
                            && leavesDict[currentDate].leaveType != TimelineType.HOLIDAY))
                            && !(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday))
                        {
                            usersTimelines[x].HoursAlotted = (int?)allocations[s].Efforts;
                            usersTimelines[x].timeline_type = TimelineType.ALLOCATION;
                            usersTimelines[x].timeline_display_text = TimelineDisplayText.ALLOCATION;
                        }
                    }
                }
                else if (allocStartDate >= usersTimelines[0].start.Date && allocEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
                {
                    //Console.WriteLine("case - 4");
                    var startIndex = dateIndex[allocStartDate];
                    var endIndex = dateIndex[allocEndDate];
                    for (var x = startIndex; x <= endIndex; x++)
                    {
                        var currentDate = usersTimelines[x].start.Date;
                        if ((!leavesDict.ContainsKey(currentDate) || ( leavesDict.ContainsKey(currentDate)
                            && leavesDict[currentDate].leaveType != TimelineType.FULL_DAY_LEAVE
                            && leavesDict[currentDate].leaveType != TimelineType.HOLIDAY))
                            && !(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday))
                        {
                            usersTimelines[x].HoursAlotted = (int?)allocations[s].Efforts;
                            usersTimelines[x].timeline_type = TimelineType.ALLOCATION;
                            usersTimelines[x].timeline_display_text = TimelineDisplayText.ALLOCATION;
                        }
                    }
                }
                else if (allocStartDate >= usersTimelines[0].start.Date && allocEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
                {
                    //Console.WriteLine("case - 5");
                    var startIndex = dateIndex[allocStartDate];
                    var endIndex = usersTimelines.Count - 1;
                    for (var x = startIndex; x <= endIndex; x++)
                    {
                        var currentDate = usersTimelines[x].start.Date;
                        if ((!leavesDict.ContainsKey(currentDate) || (leavesDict.ContainsKey(currentDate)
                            && leavesDict[currentDate].leaveType != TimelineType.FULL_DAY_LEAVE
                            && leavesDict[currentDate].leaveType != TimelineType.HOLIDAY))
                            && !(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday))
                        {
                            usersTimelines[x].HoursAlotted = (int?)allocations[s].Efforts;
                            usersTimelines[x].timeline_type = TimelineType.ALLOCATION;
                            usersTimelines[x].timeline_display_text = TimelineDisplayText.ALLOCATION;
                        }
                    }
                }
                else if (allocStartDate <= usersTimelines[0].start.Date && allocEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
                {
                    //Console.WriteLine("case - 6");
                    var startIndex = 0;
                    var endIndex = usersTimelines.Count - 1;
                    for (var x = startIndex; x <= endIndex; x++)
                    {
                        var currentDate = usersTimelines[x].start.Date;
                        if ((!leavesDict.ContainsKey(currentDate) || (leavesDict.ContainsKey(currentDate)
                            && leavesDict[currentDate].leaveType != TimelineType.FULL_DAY_LEAVE
                            && leavesDict[currentDate].leaveType != TimelineType.HOLIDAY))
                            && !(currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday))
                        {
                            usersTimelines[x].HoursAlotted = (int?)allocations[s].Efforts;
                            usersTimelines[x].timeline_type = TimelineType.ALLOCATION;
                            usersTimelines[x].timeline_display_text = TimelineDisplayText.ALLOCATION;
                        }
                    }
                }
                //Console.WriteLine("allocation date " + allocStartDate + " " + allocEndDate);
                //for (var x = startIndex; x <= endIndex; x++)
                //{
                //    usersTimelines[x].HoursAlotted = (int?)allocations[s].Efforts;
                //    usersTimelines[x].timeline_type = TimelineType.ALLOCATION;
                //    usersTimelines[x].timeline_display_text = TimelineDisplayText.ALLOCATION;
                //}
            }
            //filling up leaves
            //for (var s = 0; s < leaves.Count; s++)
            //{
            //    DateTime leaveStartDate = leaves[s].start_date.Date;
            //    DateTime leaveEndDate = leaves[s].end_date.Date;
            //    //Console.WriteLine("leaves " + leaveStartDate + " " + leaveEndDate);
            //    if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate < usersTimelines[0].start.Date)
            //    {
            //        //Console.WriteLine("case - 1");
            //        continue;
            //    }
            //    else if (leaveStartDate > usersTimelines[usersTimelines.Count - 1].end.Date && leaveEndDate > usersTimelines[usersTimelines.Count - 1].end.Date)
            //    {
            //        //Console.WriteLine("case - 2");
            //        continue;
            //    }
            //    else if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
            //    {
            //        //Console.WriteLine("case - 3");
            //        var startIndex = 0;
            //        var endIndex = dateIndex[leaveEndDate];
            //        for (var x = startIndex; x <= endIndex; x++)
            //        {
            //            usersTimelines[x].timeline_type = leaves[s].leaveType;
            //            usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
            //        }
            //    }
            //    else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate <= usersTimelines[usersTimelines.Count - 1].end.Date)
            //    {
            //        //Console.WriteLine("case - 4");
            //        var startIndex = dateIndex[leaveStartDate];
            //        var endIndex = dateIndex[leaveEndDate];
            //        for (var x = startIndex; x <= endIndex; x++)
            //        {
            //            usersTimelines[x].timeline_type = leaves[s].leaveType;
            //            usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
            //        }
            //    }
            //    else if (leaveStartDate >= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
            //    {
            //        //Console.WriteLine("case - 5");
            //        var startIndex = dateIndex[leaveStartDate];
            //        var endIndex = usersTimelines.Count - 1;
            //        for (var x = startIndex; x <= endIndex; x++)
            //        {
            //            usersTimelines[x].timeline_type = leaves[s].leaveType;
            //            usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
            //        }
            //    }
            //    else if (leaveStartDate <= usersTimelines[0].start.Date && leaveEndDate >= usersTimelines[usersTimelines.Count - 1].end.Date)
            //    {
            //        //Console.WriteLine("case - 6");
            //        var startIndex = 0;
            //        var endIndex = usersTimelines.Count - 1;
            //        for (var x = startIndex; x <= endIndex; x++)
            //        {
            //            usersTimelines[x].timeline_type = leaves[s].leaveType;
            //            usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower() ? TimelineDisplayText.LEAVE : TimelineDisplayText.HOLIDAY;
            //        }
            //    }
            //}

            List<UsersTimeline> result = new List<UsersTimeline>();
            var strPtr = 0;
            var endPtr = 0;
            while (endPtr < usersTimelines.Count)
            {
                if (usersTimelines[strPtr].timeline_type.ToLower().Trim() == usersTimelines[endPtr].timeline_type.ToLower().Trim())
                {
                    endPtr++;
                }
                else
                {
                    UsersTimeline addPart = new UsersTimeline()
                    {
                        start = usersTimelines[strPtr].start,
                        end = usersTimelines[endPtr - 1].start,
                        timeline_type = usersTimelines[strPtr].timeline_type,
                        timeline_display_text = usersTimelines[strPtr].timeline_display_text,
                        HoursAlotted = usersTimelines[strPtr].HoursAlotted
                    };
                    result.Add(addPart);
                    strPtr = endPtr;
                }
            }
            if (strPtr != endPtr)
            {
                UsersTimeline addLast = new UsersTimeline()
                {
                    start = usersTimelines[strPtr].start,
                    end = usersTimelines[endPtr - 1].start,
                    timeline_type = usersTimelines[strPtr].timeline_type,
                    timeline_display_text = usersTimelines[strPtr].timeline_display_text,
                    HoursAlotted = usersTimelines[strPtr].HoursAlotted
                };
                result.Add(addLast);
            }
            return result.Where(d => d.timeline_type.ToLower().Trim() != TimelineType.AVAILABLE.ToLower().Trim()).ToList();
        }
        /// <summary>
        /// NOT IN USE AS OF NOW
        /// </summary>
        /// <param name="employeeTimeline"></param>
        /// <returns></returns>
        //public static List<EmployeeProject> RemoveDuplicateLeavesAndHolidays(List<EmployeeProject> employeeTimeline)
        //{
        //    var listOfLeavesAndHolidays = employeeTimeline.Where((et) =>
        //                                                                    et.timeline_type.ToLower().Trim() == TimelineType.LEAVE.ToLower().Trim() ||
        //                                                                    et.timeline_type.ToLower().Trim() == TimelineType.HOLIDAY.ToLower().Trim()).OrderBy(e => e.StartDate).ToList();

        //    List<EmployeeProject> employeeTimelineLeavesOrHolidays = new List<EmployeeProject>();
        //    for (int i = 0; i < listOfLeavesAndHolidays.Count; i++)
        //    {
        //        if (i != 0 && listOfLeavesAndHolidays[i].StartDate == listOfLeavesAndHolidays[i - 1].StartDate && listOfLeavesAndHolidays[i].EndDate == listOfLeavesAndHolidays[i - 1].EndDate && listOfLeavesAndHolidays[i].timeline_type == listOfLeavesAndHolidays[i - 1].timeline_type)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            employeeTimelineLeavesOrHolidays.Add(listOfLeavesAndHolidays[i]);
        //        }
        //    }
        //    var timelineWithoutLeaves = employeeTimeline.Where((et) => !(et.timeline_type.ToLower().Trim() == TimelineType.LEAVE.ToLower().Trim() ||
        //                                   et.timeline_type.ToLower().Trim() == TimelineType.HOLIDAY.ToLower().Trim())).ToList();
        //    var finalEmployeeTimeline = timelineWithoutLeaves.Concat(employeeTimelineLeavesOrHolidays).ToList();
        //    return finalEmployeeTimeline;
        //}

    }
}
