using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System.Text.Json;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetProjectsByPipelineCodeQuery : IRequest<List<EmployeeProject>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string EmailId { get; set; }
        public string[]? userAppRoles { get; set; } = null;
        public bool? isAllocationDetailsFilterByUserRoles { get; set; } = false;
    }
    public class GetProjectsByPipelineCodeQueryHandler : IRequestHandler<GetProjectsByPipelineCodeQuery, List<EmployeeProject>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
        private readonly IMediator _mediator;
        private string defaultValueForNoAllocation = "";

        public GetProjectsByPipelineCodeQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi, IMediator mediator)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
            _mediator = mediator;
        }

        public async Task<List<EmployeeProject>> Handle(GetProjectsByPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            var activeAllocationsForUser = await _mediator.Send(new GetActiveAllocationQuery()
            {
                JobCode = request.JobCode,
                PipelineCode = request.PipelineCode,
                isAllocationDetailsFilterByUserRoles = true,
                userEmail = request.EmailId,
                userAppRoles = request.userAppRoles,
                status = "Approved"
            });
            List<EmployeeProject> employeeTimeline = new();
            foreach (ResourceAllocationDetailsResponse details in activeAllocationsForUser)
            {
                if (details?.ResourceAllocations?.Count > 0)
                {
                    DateTime minStartDate = details.ResourceAllocations.Min(d => d.StartDate).ToDateTime(TimeOnly.MinValue);
                    DateTime minStartDateUtc = minStartDate.Date;
                    DateTime maxEndDate = details.ResourceAllocations.Max(d => d.EndDate).ToDateTime(TimeOnly.MaxValue);
                    DateTime maxEndDateUtc = maxEndDate.Date;
                    List<string> Emails = new()
                    {
                        details.EmpEmail
                    };
                    GetEmployeeLeaves getEmployeeLeaves = new() { emails = Emails, start_date = minStartDate, end_date = maxEndDate };
                    List<EmployeeLeavesDTO> employeeLeaves = await _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);
                    if (employeeLeaves.Count > 0)
                    {
                        var randomLeaves = employeeLeaves[0].leaves;
                        List<LeavesDTO> leaves = randomLeaves.OrderBy(d => d.start_date).ThenBy(d => d.end_date).ToList();
                        var allocations = details.ResourceAllocations.OrderBy(d => d.StartDate).ToList();
                        List<UsersTimeline> userTimelines = await GetUserAllocationTimeline(allocations, leaves);
                        foreach (var userTimeline in userTimelines)
                        {
                            EmployeeProject empDetail = new()
                            {
                                PipelineCode = details.PipelineCode,
                                PipelineName = details.PipelineName,
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
                                ConfirmedPerDayHours = userTimeline.timeline_type.ToLower().ToString() == TimelineType.FULL_DAY_LEAVE.ToLower().ToString()
                                                        || userTimeline.timeline_type.ToLower().ToString() == TimelineType.HOLIDAY.ToLower().ToString()
                                                        ? 0 : userTimeline.HoursAlotted,
                                timeline_type = userTimeline.timeline_type,
                                timeline_display_text = userTimeline.timeline_display_text,
                                isUpdated = details.IsUpdated,
                                WeeklyBreakup = userTimeline.WeeklyBreakup,
                                WeeklyTotal = userTimeline.WeeklyTotal,
                            };
                            employeeTimeline.Add(empDetail);
                        }
                    }
                }
            }
            return employeeTimeline;
        }

        public async Task<List<UsersTimeline>> GetUserAllocationTimeline(List<ResourceAllocationResponse> allocations, List<LeavesDTO> leaves)
        {
            leaves = leaves.Where(l => l.start_date.DayOfWeek != DayOfWeek.Saturday || l.start_date.DayOfWeek != DayOfWeek.Sunday).ToList();
            List<UsersTimeline> usersTimelines = new();
            Dictionary<DateTime, int> dateIndex = new();
            DateTime minAllocationStartDate = (DateTime)allocations[0].StartDate.ToDateTime(TimeOnly.MinValue);
            DateTime maxAllocationEndDate = (DateTime)allocations[allocations.Count - 1].EndDate.ToDateTime(TimeOnly.MaxValue);
            DateTime minStartDate = minAllocationStartDate.Date;
            DateTime maxEndDate = maxAllocationEndDate.Date;
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
            for (var s = 0; s < allocations.Count; s++)
            {
                DateTime allocStartDate0 = (DateTime)allocations[s].StartDate.ToDateTime(TimeOnly.MinValue);
                DateTime allocEndDate0 = (DateTime)allocations[s].EndDate.ToDateTime(TimeOnly.MaxValue);
                DateTime allocStartDate = allocStartDate0.Date;
                DateTime allocEndDate = allocEndDate0.Date;
                var startIndex = dateIndex[allocStartDate];
                var endIndex = dateIndex[allocEndDate];

                Dictionary<DateTime, LeavesDTO> leavesDict = new Dictionary<DateTime, LeavesDTO>();
                foreach (var leave in leaves)
                {
                    leavesDict.Add(leave.start_date, leave);
                }

                var perDayEntry = await _resourceAllocationRepository.GetResAllocDaysRespFromPubResAllocId(allocations[s].Id);

                foreach (var item in perDayEntry)
                {
                    var userTimelineEntryIndex = usersTimelines
                        .FindIndex(m => DateOnly.FromDateTime(m.start) == item.AllocationDate);
                    usersTimelines[userTimelineEntryIndex].HoursAlotted = (int?)item.Efforts;
                    usersTimelines[userTimelineEntryIndex].timeline_type = TimelineType.ALLOCATION;
                    usersTimelines[userTimelineEntryIndex].timeline_display_text = TimelineDisplayText.ALLOCATION;
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
            }

            List<UsersTimeline> finalTimeline = new List<UsersTimeline>();

            bool addToTimeline = false;

            var objToAdd = new UsersTimeline();

            for (int i = 0; i < usersTimelines.Count; i++)
            {
                UsersTimeline user = usersTimelines[i];
                UsersTimeline? prevItem = null;
                if (finalTimeline.Count > 0)
                {
                    prevItem = finalTimeline.Last();
                }
                if (string.IsNullOrEmpty(objToAdd.timeline_type))
                {
                    objToAdd = InitializeNewTimeline(user, prevItem);                    
                    addToTimeline = false;
                }
                else if (objToAdd.timeline_type?.ToLower() == user.timeline_type)
                {
                    objToAdd.end = user.end;
                    objToAdd.HoursAlotted = objToAdd.HoursAlotted + user.HoursAlotted;

                    objToAdd.WeeklyBreakup = GetWeeklyBreakup(objToAdd.WeeklyBreakup, user, prevItem);
                    objToAdd.WeeklyTotal = GetWeeklyTotal(objToAdd.WeeklyBreakup);
                }
                else if (objToAdd.timeline_type?.ToLower() != user.timeline_type)
                {
                    addToTimeline = true;
                }

                if (addToTimeline)
                {
                    if (!(objToAdd.start.DayOfWeek == DayOfWeek.Saturday || objToAdd.start.DayOfWeek == DayOfWeek.Sunday
                    || objToAdd.end.DayOfWeek == DayOfWeek.Saturday || objToAdd.end.DayOfWeek == DayOfWeek.Sunday))
                    {
                        objToAdd.WeeklyBreakup = GetWeeklyBreakup(objToAdd.WeeklyBreakup, user, prevItem);
                        objToAdd.WeeklyTotal = GetWeeklyTotal(objToAdd.WeeklyBreakup);
                        finalTimeline.Add(objToAdd);
                    }

                    objToAdd = InitializeNewTimeline(user, prevItem);
                    addToTimeline = false;
                }
              
            }

            if (!string.IsNullOrEmpty(objToAdd.timeline_type))
            {
                finalTimeline.Add(objToAdd);
            }

            List<UsersTimeline> response = finalTimeline
                            .Where(d =>
                                d.timeline_type.ToLower().Trim() != TimelineType.AVAILABLE.ToLower().Trim()
                                && d.start != new DateTime() && d.end != new DateTime()
                            )
                            .ToList();

            if (response != null && response.Count > 0)
            {
                for (int i = response.Count - 1; i > 0; i--)
                {
                    var curr = response[i];
                    var pre = response[i - 1];
                    bool isSameWeekDates = IsSameWeekDates(curr.end, pre.start);
                    if (isSameWeekDates)
                    {
                        response[i - 1].WeeklyBreakup = response[i].WeeklyBreakup;
                        response[i - 1].WeeklyTotal = GetWeeklyTotal(response[i].WeeklyBreakup);
                    }
                }
            }

            return response;
        }

        private UsersTimeline InitializeNewTimeline(UsersTimeline user, UsersTimeline? prevItem)
        {
            return new UsersTimeline()
            {
                start = user.start,
                end = user.end,
                timeline_type = user.timeline_type,
                timeline_display_text = user.timeline_display_text,
                HoursAlotted = user.leave_hours==4?Math.Min(4, user.HoursAlotted??0) : user.HoursAlotted,
                WeeklyBreakup = GetWeeklyBreakup(new UsersTimelineWeeklyAllocation(), user, prevItem),
                WeeklyTotal = GetWeeklyTotal(GetWeeklyBreakup(new UsersTimelineWeeklyAllocation(), user, prevItem))
            };
        }

        private int GetWeeklyTotal(UsersTimelineWeeklyAllocation weeklyBreakup)
        {
            int num = 0;
            int total = 0;

            int.TryParse(weeklyBreakup.Mon, out num);
            total += num;
            num = 0;
            int.TryParse(weeklyBreakup.Tue, out num);
            total += num;
            num = 0;
            int.TryParse(weeklyBreakup.Wed, out num);
            total += num;
            num = 0;
            int.TryParse(weeklyBreakup.Thu, out num);
            total += num;
            num = 0;
            int.TryParse(weeklyBreakup.Fri, out num);
            total += num;
            num = 0;

            return total;
        }

        private bool IsSameWeekDates(DateTime date1, DateTime date2)
        {
            bool flag = false;
            flag = date1.AddDays(-(int)date1.DayOfWeek) == date2.AddDays(-(int)date2.DayOfWeek);
            return flag;
        }

        private UsersTimelineWeeklyAllocation GetWeeklyBreakup(UsersTimelineWeeklyAllocation weeklyBreakup, UsersTimeline currentItem, UsersTimeline? prevItem)
        {
            if ((currentItem.start.Date.DayOfWeek == DayOfWeek.Monday || currentItem.end.Date.DayOfWeek == DayOfWeek.Monday))
            {
                weeklyBreakup.Mon = GetWeeklyBreakupText(currentItem);
            }
            else if ((currentItem.start.Date.DayOfWeek == DayOfWeek.Tuesday || currentItem.end.Date.DayOfWeek == DayOfWeek.Tuesday))
            {
                weeklyBreakup.Tue = GetWeeklyBreakupText(currentItem);
            }
            else if ((currentItem.start.Date.DayOfWeek == DayOfWeek.Wednesday || currentItem.end.Date.DayOfWeek == DayOfWeek.Wednesday))
            {
                weeklyBreakup.Wed = GetWeeklyBreakupText(currentItem);
            }
            else if ((currentItem.start.Date.DayOfWeek == DayOfWeek.Thursday || currentItem.end.Date.DayOfWeek == DayOfWeek.Thursday))
            {
                weeklyBreakup.Thu = GetWeeklyBreakupText(currentItem);
            }
            else if ((currentItem.start.Date.DayOfWeek == DayOfWeek.Friday || currentItem.end.Date.DayOfWeek == DayOfWeek.Friday))
            {
                weeklyBreakup.Fri = GetWeeklyBreakupText(currentItem);
            }

            bool isSameWeek = prevItem != null && (prevItem.start.Date.AddDays(-1 * (int)prevItem.start.DayOfWeek) == currentItem.start.Date.AddDays(-1 * (int)currentItem.start.DayOfWeek));

            if (prevItem != null && isSameWeek)
            {
                if (string.IsNullOrEmpty(weeklyBreakup.Mon))
                {
                    weeklyBreakup.Mon = GetNoAllocationText(!string.IsNullOrEmpty(prevItem.WeeklyBreakup.Mon) ? prevItem.WeeklyBreakup.Mon : weeklyBreakup.Mon);
                }
                if (string.IsNullOrEmpty(weeklyBreakup.Tue))
                {
                    weeklyBreakup.Tue = GetNoAllocationText(!string.IsNullOrEmpty(prevItem.WeeklyBreakup.Tue) ? prevItem.WeeklyBreakup.Tue : weeklyBreakup.Tue);
                }
                if (string.IsNullOrEmpty(weeklyBreakup.Wed))
                {
                    weeklyBreakup.Wed = GetNoAllocationText(!string.IsNullOrEmpty(prevItem.WeeklyBreakup.Wed) ? prevItem.WeeklyBreakup.Wed : weeklyBreakup.Wed);
                }
                if (string.IsNullOrEmpty(weeklyBreakup.Thu))
                {
                    weeklyBreakup.Thu = GetNoAllocationText(!string.IsNullOrEmpty(prevItem.WeeklyBreakup.Thu) ? prevItem.WeeklyBreakup.Thu : weeklyBreakup.Thu);
                }
                if (string.IsNullOrEmpty(weeklyBreakup.Fri))
                {
                    weeklyBreakup.Fri = GetNoAllocationText(!string.IsNullOrEmpty(prevItem.WeeklyBreakup.Fri) ? prevItem.WeeklyBreakup.Fri : weeklyBreakup.Fri);
                }
            }

            return weeklyBreakup;
        }

        private string GetWeeklyBreakupText(UsersTimeline currentItem)
        {
            int multiplier = currentItem.timeline_type.ToLower() == TimelineType.FULL_DAY_LEAVE.ToLower() || currentItem.timeline_type.ToLower() == TimelineType.HOLIDAY.ToLower()
                ? -1 : 1;
            string str = string.Empty;
            int? leaveHours = currentItem?.leave_hours==null ? 0 : currentItem.leave_hours;

            if(leaveHours == 4 && currentItem?.HoursAlotted != null)            
                str = Math.Min(4, (int)currentItem.HoursAlotted) + string.Empty;            
            else                
                str = currentItem.HoursAlotted != null ? (Convert.ToInt32(currentItem.HoursAlotted) * multiplier) + string.Empty : defaultValueForNoAllocation;

            if (multiplier == -1)
            {
                str = defaultValueForNoAllocation;
            }
            return GetNoAllocationText(str);
        }

        private string GetNoAllocationText(string value)
        {
            int hr = 0;
            bool flag = int.TryParse(value, out hr);
            if (flag && hr > 0)
            {
                return value;
            }
            else
            {
                return defaultValueForNoAllocation;
            }
        }

    }
}
