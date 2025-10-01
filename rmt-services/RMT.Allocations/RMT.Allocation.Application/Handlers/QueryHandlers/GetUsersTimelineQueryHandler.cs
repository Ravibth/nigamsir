using MediatR;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Domain.Util;
using RMT.Allocation.Infrastructure;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Migrations;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using Constants = RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetUsersTimelineQuery : IRequest<List<GetUsersTimelineResponse>>
    {
        public List<string> emails { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
    public class GetUsersTimelineQueryHandler : IRequestHandler<GetUsersTimelineQuery, List<GetUsersTimelineResponse>>
    {
        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        public GetUsersTimelineQueryHandler(IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi,
            IResourceAllocationRepository resourceAllocationRepository, IWCGTMasterHttpApi wCGTMasterHttpApi)
        {
            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
            _resourceAllocationRepository = resourceAllocationRepository;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
        }

        public async Task<List<GetUsersTimelineResponse>> Handle(GetUsersTimelineQuery request,
            CancellationToken cancellationToken)
        {
            GetEmployeeLeaves getEmployeeLeaves = new()
            {
                emails = request.emails,
                start_date = request.start_date,
                end_date = request.end_date
            };
            Task<List<EmployeeLeavesDTO>> employeeLeaves = _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);
            Task<List<ResourceAllocationDaysResponse>> allocations = _resourceAllocationRepository
                .GetUserPerDayAllocationsByEmailAndDates(request.emails, request.start_date, request.end_date);

            Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> abscondedResignedUsers =
                _wCGTMasterHttpApi.GetResignedAndAbscondedUsersByEmails(request.emails.ToList());

            await Task.WhenAll(employeeLeaves, allocations, abscondedResignedUsers);


            var allocationsFetched = allocations.Result
                                     .Where(m =>
                                             m.AllocationDate >= DateOnly.FromDateTime(request.start_date)
                                             && m.AllocationDate <= DateOnly.FromDateTime(request.end_date)
                                     )
                                     .ToList();

            List<GetUsersTimelineResponse> response = GetNewUserTimelines(request.start_date, request.end_date,
                request.emails, employeeLeaves.Result, allocations.Result, abscondedResignedUsers.Result);
            //*************** convert date only ***************************            
            foreach (var item in response)
            {
                for (int idx = 0; idx < item.usersTimelines.Count; idx++)
                {
                    item.usersTimelines[idx].start = Convert.ToDateTime(item.usersTimelines[idx].start).Date;
                    item.usersTimelines[idx].end = Convert.ToDateTime(item.usersTimelines[idx].end).Date;
                }
            }
            return response;
        }
        /// <summary>
        /// NOT IN USE
        /// </summary>
        /// <param name="start_date"></param>
        /// <param name="end_date"></param>
        /// <param name="emails"></param>
        /// <param name="employeeLeaves"></param>
        /// <param name="allocations"></param>
        /// <returns></returns>
        //public static List<GetUsersTimelineResponse> GetUserTimelines(DateTime start_date, DateTime end_date, List<string> emails,
        //    List<EmployeeLeavesDTO> employeeLeaves, List<ResourceAllocationResponse> allocations)
        //{
        //    List<GetUsersTimelineResponse> response = new() { };

        //    foreach (var user in emails)
        //    {
        //        //EmployeeLeavesDTO userLeavesFetched = employeeLeaves.Result.FirstOrDefault(m => m.email.ToLower() == user.ToLower());
        //        EmployeeLeavesDTO userLeavesFetched = employeeLeaves.FirstOrDefault(m => m.email.ToLower() == user.ToLower()
        //        && (m.leaves.Where(l => l.start_date >= start_date && l.end_date <= end_date).Any()));//galat hai

        //        var userAllocationsFetched = allocations.Where(m => m.EmpEmail.ToLower() == user.ToLower());

        //        List<UsersTimeline> usersTimelines = new();
        //        if (userLeavesFetched != null)
        //        {
        //            foreach (var leaveItem in userLeavesFetched.leaves)
        //            {
        //                usersTimelines.Add(new()
        //                {
        //                    start = leaveItem.start_date,
        //                    end = leaveItem.end_date,
        //                    timeline_type = leaveItem.leaveType,
        //                    timeline_display_text = "Leave"
        //                }); ;
        //            }
        //        }
        //        if (userAllocationsFetched != null)
        //        {
        //            foreach (var allocationItem in userAllocationsFetched)
        //            {
        //                usersTimelines.Add(new()
        //                {
        //                    start = allocationItem.StartDate.ToDateTime(TimeOnly.MinValue),
        //                    end = allocationItem.EndDate.ToDateTime(TimeOnly.MaxValue),
        //                    timeline_type = TimelineType.ALLOCATION,
        //                    timeline_display_text = Convert.ToString(allocationItem.Efforts) + "hrs Allocated"
        //                });
        //            }
        //        }
        //        usersTimelines = usersTimelines.OrderBy(m => m.start).ToList();

        //        List<UsersTimeline> avaiableTimelines = new();
        //        if (usersTimelines.Count > 0)
        //        {
        //            if (start_date < usersTimelines[0].start)
        //            {
        //                var starttime = start_date;
        //                var endtime = usersTimelines[0].start;
        //                avaiableTimelines.Add(new()
        //                {
        //                    start = starttime,
        //                    end = endtime,
        //                    timeline_type = TimelineType.AVAILABLE,
        //                    timeline_display_text = "Available"
        //                });
        //            }

        //            for (int i = 0; i < usersTimelines.Count; i++)
        //            {
        //                if (usersTimelines[i].start <= end_date && usersTimelines[i].end >= start_date)
        //                {
        //                    var starttime = (usersTimelines[i].end > start_date) ? usersTimelines[i].end : start_date;
        //                    var endtime = (i + 1 < usersTimelines.Count && usersTimelines[i + 1] != null
        //                        && usersTimelines[i + 1].start < end_date) ? usersTimelines[i + 1].start : end_date;
        //                    avaiableTimelines.Add(new()
        //                    {
        //                        start = starttime,
        //                        end = endtime,
        //                        timeline_type = TimelineType.AVAILABLE,
        //                        timeline_display_text = "Available"
        //                    });
        //                }
        //            }
        //        }
        //        else
        //        {
        //            avaiableTimelines.Add(new()
        //            {
        //                start = start_date,
        //                end = end_date,
        //                timeline_type = TimelineType.AVAILABLE,
        //                timeline_display_text = "Available"
        //            });
        //            //  avaiableTimelines.Add(new UsersTimeline { start = request.start_date.AddDays(10), end = request.end_date.AddDays(10), timeline_type = TimelineType.AVAILABLE, timeline_display_text = "Available" });

        //        }
        //        foreach (var item in avaiableTimelines)
        //        {
        //            usersTimelines.Add(item);
        //        }

        //        usersTimelines = usersTimelines.OrderBy(m => m.start).ToList();
        //        response.Add(new()
        //        {
        //            email = user,
        //            usersTimelines = usersTimelines
        //        });
        //    }

        //    return response;
        //}
        public static List<GetUsersTimelineResponse> GetNewUserTimelines(DateTime start_date, DateTime end_date, List<string> emails, List<EmployeeLeavesDTO> employeeLeaves, List<ResourceAllocationDaysResponse> daysAllocation, List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto> abscondedResignedUsers)
        {
            List<GetUsersTimelineResponse> response = new();
            foreach (var user in emails)
            {
                var abscondedResignedLastAvailableDate = abscondedResignedUsers.Where(m => m.email_id.ToLower() == user.ToLower()).FirstOrDefault();
                EmployeeLeavesDTO userLeaves = employeeLeaves.Where(m => m.email.ToLower().Trim() == user.ToLower().Trim()).FirstOrDefault();
                List<LeavesDTO> userLeavesList = new();
                if (userLeaves != null)
                {
                    userLeavesList = userLeaves.leaves;
                }
                var allocations = daysAllocation.Where(a => a.EmailId.ToLower().Trim() == user.ToLower().Trim()).ToList();
                DateOnly? lastAvailableDate = (abscondedResignedLastAvailableDate != null && abscondedResignedLastAvailableDate.last_available_day != null) ? abscondedResignedLastAvailableDate.last_available_day : null;
                if (lastAvailableDate != null && DateOnly.FromDateTime(start_date) >= lastAvailableDate)
                {
                    var userTimelines = GenerateTimeline(start_date, end_date, new List<LeavesDTO>(), new List<ResourceAllocationDaysResponse>(), lastAvailableDate);
                    response.Add(new()
                    {
                        email = user,
                        usersTimelines = userTimelines
                    });
                }
                else
                {
                    var userTimelines = GenerateTimeline(start_date, end_date, userLeavesList,
                        allocations, lastAvailableDate);
                    response.Add(new()
                    {
                        email = user,
                        usersTimelines = userTimelines
                    });
                }
            }
            return response;
        }
        public static List<UsersTimeline> GenerateTimeline(DateTime start_date, DateTime end_date,
            List<LeavesDTO> employeeLeaves, List<ResourceAllocationDaysResponse> daysAllocation, DateOnly? lastAvailableDate)
        {
            var temp_emd_date = DateOnly.FromDateTime(end_date);
            if (lastAvailableDate != null && end_date >= lastAvailableDate.Value.ToDateTime(TimeOnly.MinValue))
            {
                end_date = lastAvailableDate.Value.ToDateTime(TimeOnly.MinValue);
                employeeLeaves = employeeLeaves.Where(m => DateOnly.FromDateTime(m.start_date) < lastAvailableDate).ToList();
                daysAllocation = daysAllocation.Where(m => m.AllocationDate < lastAvailableDate).ToList();
            }

            // ! Warning
            //TODO check for partial allocations as well
            List<UsersTimeline> usersTimelines = new();
            Dictionary<DateTime, int> dateIndex = new();
            var allocations = daysAllocation.OrderBy(a => a.AllocationDate).ToList();
            var leaves = employeeLeaves.OrderBy(l => l.start_date).ThenBy(l => l.end_date).ToList();

            DateTime start_date_counter = start_date.Date;
            while (start_date_counter <= end_date.Date)
            {
                usersTimelines.Add(new()
                {
                    start = start_date_counter,
                    end = start_date_counter,
                    HoursAlotted = 0,
                    timeline_type = TimelineType.AVAILABLE,
                    timeline_display_text = TimelineDisplayText.AVAILABLE,
                });
                dateIndex.Add(start_date_counter, usersTimelines.Count - 1);
                start_date_counter = start_date_counter.AddDays(1);
            }
            for (var x = 0; x < allocations.Count; x++)
            {
                DateTime allocStartDate0 = allocations[x].AllocationDate.ToDateTime(TimeOnly.MinValue);
                DateTime allocStartDate = allocStartDate0.Date;
                DateTime allocEndDate0 = allocations[x].AllocationDate.ToDateTime(TimeOnly.MaxValue);
                DateTime allocEndDate = allocEndDate0.Date;
                if (dateIndex.ContainsKey(allocStartDate))
                {
                    var startIndex = dateIndex[allocStartDate];
                    var endIndex = dateIndex[allocEndDate];
                    for (var i = startIndex; i <= endIndex; i++)
                    {
                        usersTimelines[i].HoursAlotted = (int)(usersTimelines[i].HoursAlotted + allocations[x].Efforts);

                        if (usersTimelines[i].HoursAlotted >= Constants.WorkingHourPerDay)
                        {
                            usersTimelines[i].timeline_type = TimelineType.ALLOCATION;
                            usersTimelines[i].timeline_display_text = TimelineDisplayText.ALLOCATION;
                        }
                    }
                }
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
                            if (usersTimelines[leaveDateIndex].HoursAlotted + Constants.HalfDayHours >= Constants.WorkingHourPerDay)
                            {
                                usersTimelines[leaveDateIndex].timeline_type = TimelineType.FIRST_HALF_LEAVE;
                                usersTimelines[leaveDateIndex].timeline_display_text = TimelineDisplayText.FIRST_HALF_LEAVE;
                            }
                            usersTimelines[leaveDateIndex].leave_hours = leaves[s].hours;
                            break;
                        case TimelineType.SECOND_HALF_LEAVE:
                            if (usersTimelines[leaveDateIndex].HoursAlotted + Constants.HalfDayHours >= Constants.WorkingHourPerDay)
                            {
                                usersTimelines[leaveDateIndex].timeline_type = TimelineType.SECOND_HALF_LEAVE;
                                usersTimelines[leaveDateIndex].timeline_display_text = TimelineDisplayText.SECOND_HALF_LEAVE;
                            }
                            usersTimelines[leaveDateIndex].leave_hours = leaves[s].hours;
                            break;
                        default:
                            break;
                    }
                }
                //DateTime leaveStartDate = leaves[s].start_date.Date;
                //DateTime leaveEndDate = leaves[s].end_date.Date;
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
                //else if (leaveStartDate < usersTimelines[0].start.Date && leaveEndDate
                //    <= usersTimelines[usersTimelines.Count - 1].end.Date)
                //{
                //    //Console.WriteLine("case - 3");
                //    var startIndex = 0;
                //    var endIndex = dateIndex[leaveEndDate];
                //    for (var x = startIndex; x <= endIndex; x++)
                //    {
                //        usersTimelines[x].timeline_type = leaves[s].leaveType;
                //        usersTimelines[x].timeline_display_text =
                //            leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower()
                //            ? TimelineDisplayText.LEAVE
                //            : TimelineDisplayText.HOLIDAY;
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
                //        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower()
                //            ? TimelineDisplayText.LEAVE
                //            : TimelineDisplayText.HOLIDAY;
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
                //        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower()
                //            ? TimelineDisplayText.LEAVE
                //            : TimelineDisplayText.HOLIDAY;
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
                //        usersTimelines[x].timeline_display_text = leaves[s].leaveType.Trim().ToLower() == TimelineDisplayText.LEAVE.Trim().ToLower()
                //            ? TimelineDisplayText.LEAVE
                //            : TimelineDisplayText.HOLIDAY;
                //    }
                //}
            }
            //aayush_cross_check
            var totalAvailableHours = 0;
            var totalHours = 0;
            var item = "";
            DateTime? start = null;
            DateTime? end = null;
            //string _hoursCount = "";

            List<UsersTimeline> finalTimeline = new List<UsersTimeline>();

            usersTimelines = CalculateWeeklyBreakup(usersTimelines);

            UsersTimelineWeeklyAllocation weeklyBreakup = new UsersTimelineWeeklyAllocation();
            int weeklyTotal = 0;

            foreach (var user in usersTimelines)
            {
                //_hoursCount = Helper.IsHolidayOrLeave(user.timeline_type) ? user.timeline_type : Convert.ToString(totalHours);
                if (item.ToLower() != user.timeline_type || user.start.Date.DayOfWeek == DayOfWeek.Saturday || user.start.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (start != null && end != null && start.GetValueOrDefault().Date.DayOfWeek != DayOfWeek.Saturday && start.GetValueOrDefault().Date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        finalTimeline.Add(new()
                        {
                            start = (DateTime)start,
                            end = (DateTime)end,
                            timeline_type = item,
                            timeline_display_text = Convert.ToString(totalHours),
                            WeeklyBreakup = weeklyBreakup,
                            WeeklyTotal = weeklyTotal,
                        });
                    }

                    totalHours = 0;
                    start = user.start;
                    end = user.end;
                    if (user.start.Date.DayOfWeek == DayOfWeek.Saturday || user.start.Date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        item = String.Empty;
                    }
                    else
                    {
                        item = user.timeline_type;
                    }
                    weeklyBreakup = user.WeeklyBreakup;
                    weeklyTotal = (int)user.WeeklyTotal;
                }
                else
                {
                    end = user.end;
                }
                if (!(user.start.Date.DayOfWeek.ToString().ToLower() == "saturday" || user.end.Date.DayOfWeek.ToString().ToLower() == "sunday"))
                {
                    var hoursBlocked = user.timeline_type == TimelineType.HOLIDAY || user.timeline_type == TimelineType.FULL_DAY_LEAVE
                                        ? (int)Constants.WorkingHourPerDay
                                        : (user.HoursAlotted != null ? (int)user.HoursAlotted : 0) + (user.leave_hours != null ? (int)user.leave_hours : 0);

                    totalHours = (int)(totalHours + (Constants.WorkingHourPerDay - (hoursBlocked > 8 ? 8 : hoursBlocked)));
                }
            }
            //_hoursCount = Helper.IsHolidayOrLeave(item) ? item : Convert.ToString(totalHours);
            if (start != null && end != null)
            {
                finalTimeline.Add(new()
                {
                    start = (DateTime)start,
                    end = (DateTime)end,
                    timeline_type = item,
                    timeline_display_text = Convert.ToString(totalHours),
                    WeeklyBreakup = weeklyBreakup,
                    WeeklyTotal = weeklyTotal
                });
            }

            if (lastAvailableDate != null && end_date >= lastAvailableDate.Value.ToDateTime(TimeOnly.MinValue))
            {
                finalTimeline.Add(new()
                {
                    start = lastAvailableDate.Value.ToDateTime(TimeOnly.MinValue),
                    end = temp_emd_date.ToDateTime(TimeOnly.MinValue),
                    timeline_type = Constants.UserAbscondedOrResignedType,
                    timeline_display_text = Constants.UserAbscondedOrResignedType,
                    WeeklyBreakup = weeklyBreakup,
                    WeeklyTotal = weeklyTotal
                });
            }
            foreach (var timeline in finalTimeline)
            {
                timeline.timeline_display_text = Helper.IsHolidayOrLeave(timeline.timeline_type) ? timeline.timeline_type : timeline.timeline_display_text;
            }
            return finalTimeline;
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            var jan1 = new DateTime(time.Year, 1, 1);
            var daysOffset = (int)jan1.DayOfWeek - (int)DayOfWeek.Monday;
            if (daysOffset < 0) daysOffset += 7;
            var firstMonday = jan1.AddDays(-daysOffset).AddDays(3);
            var weekNumber = ((time - firstMonday).Days / 7) + 1;
            return weekNumber;
        }

        public static List<UsersTimeline> CalculateWeeklyBreakup(List<UsersTimeline> data)
        {
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            var groupedData = data
                    .GroupBy(d => new
                    {
                        Year = d.start.Year,
                        Week = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d.start, myCWR, myFirstDOW)
                    });


            foreach (var weekGroup in groupedData)
            {
                var weekBreakup = new UsersTimelineWeeklyAllocation();

                foreach (var day in weekGroup)
                {

                    int hoursBlockedForDay = day.timeline_type == TimelineType.HOLIDAY || day.timeline_type == TimelineType.FULL_DAY_LEAVE
                        ? (int)Constants.WorkingHourPerDay
                        : (day.HoursAlotted != null ? (int)day.HoursAlotted : 0) + (day.leave_hours != null ? (int)day.leave_hours : 0);


                    if (hoursBlockedForDay > Constants.WorkingHourPerDay)
                    {
                        hoursBlockedForDay = (int)Constants.WorkingHourPerDay;
                    }

                    // Only consider Monday to Friday
                    switch (day.start.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            weekBreakup.Mon = Convert.ToString(Constants.WorkingHourPerDay - hoursBlockedForDay);
                            break;
                        case DayOfWeek.Tuesday:
                            weekBreakup.Tue = Convert.ToString(Constants.WorkingHourPerDay - hoursBlockedForDay);
                            break;
                        case DayOfWeek.Wednesday:
                            weekBreakup.Wed = Convert.ToString(Constants.WorkingHourPerDay - hoursBlockedForDay);
                            break;
                        case DayOfWeek.Thursday:
                            weekBreakup.Thu = Convert.ToString(Constants.WorkingHourPerDay - hoursBlockedForDay);
                            break;
                        case DayOfWeek.Friday:
                            weekBreakup.Fri = Convert.ToString(Constants.WorkingHourPerDay - hoursBlockedForDay);
                            break;
                    }
                }

                var finalWeeklyBreakup = new UsersTimelineWeeklyAllocation
                {
                    Mon = weekBreakup.Mon,
                    Tue = weekBreakup.Tue,
                    Wed = weekBreakup.Wed,
                    Thu = weekBreakup.Thu,
                    Fri = weekBreakup.Fri
                };

                var finalWeeklyTotal = (String.IsNullOrEmpty(weekBreakup.Mon) ? 0 : int.Parse(weekBreakup.Mon)) +
                        (String.IsNullOrEmpty(weekBreakup.Tue) ? 0 : int.Parse(weekBreakup.Tue)) +
                          (String.IsNullOrEmpty(weekBreakup.Wed) ? 0 : int.Parse(weekBreakup.Wed)) +
                           (String.IsNullOrEmpty(weekBreakup.Thu) ? 0 : int.Parse(weekBreakup.Thu)) +
                            (String.IsNullOrEmpty(weekBreakup.Fri) ? 0 : int.Parse(weekBreakup.Fri));

                // Assign the weekly breakup to each day's data in the group
                foreach (var day in weekGroup)
                {
                    day.WeeklyBreakup = finalWeeklyBreakup;
                    day.WeeklyTotal = finalWeeklyTotal;
                }
            }
            return data;
        }
    }
}