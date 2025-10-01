using MediatR;
using System.Collections.Generic;
using System.Linq;
using WCGT.Application.Services.DTO;
using WCGT.Application.Services.HttpServices;
using WCGT.Application.Services.IHttpServices;
using WCGT.Domain;
using WCGT.Domain.DTO;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.Helpers;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static WCGT.Application.Common;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public enum ReportPeriodType { Weekly, Monthly }

    public class GetEmployeesForPortfolioReportQuery : IRequest<List<EmployeesForPortfolioReportDTO>>
    {
        public string? emp_mid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? availablity { get; set; }
        public List<string>? grade { get; set; }
        public List<string>? designation { get; set; }
        public List<string>? employeename { get; set; }
        public List<string>? location { get; set; }//done

        public List<string>? supercoach { get; set; }
        public List<string>? cosupercoach { get; set; }
        public List<string>? clientname { get; set; }
        public List<string>? clientgroupname { get; set; }
        public List<string>? business_unit_ids { get; set; }
        public List<string>? competency_ids { get; set; }
        public List<string>? roles { get; set; }
        public ReportPeriodType periodType { get; set; } = ReportPeriodType.Weekly;
        public UserDecorator? userDecorator { get; set; }
    }

    public class GetEmployeesForPortfolioReportQueryHandler : IRequestHandler<GetEmployeesForPortfolioReportQuery, List<EmployeesForPortfolioReportDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        private readonly IAllocationHttpService _allocationHttpService;
        private readonly IIdentityHttpService _identityHttpService;

        public GetEmployeesForPortfolioReportQueryHandler(IWcgtDataRepository repository, IAllocationHttpService allocationHttpService, IIdentityHttpService identityHttpService)
        {
            _repository = repository;
            _allocationHttpService = allocationHttpService;
            _identityHttpService = identityHttpService;
        }

        public async Task<List<EmployeesForPortfolioReportDTO>> Handle(GetEmployeesForPortfolioReportQuery request, CancellationToken cancellationToken)
        {
            // Authorization check
            if (!IsAuthorized(request))
                return new List<EmployeesForPortfolioReportDTO>();

            // Get employee data based on filters
            var employees = await GetFilteredEmployees(request);
            if (employees.Count == 0)
                return new List<EmployeesForPortfolioReportDTO>();

            var uniqueemployees = employees.Select(e => e.employee_mid).Distinct().ToList();
            if (request.employeename != null && request.employeename.Count > 0)
            {
                uniqueemployees = request.employeename;
            }
            //var uniqueemployees = new List<string>
            //    {
            //      // "EM001149",
            //        "E3"
            //    };
            // Get availability and allocation data
            var employeeAvailability = await _repository.GetEmployeeAvailabilityWithLeavesAndHolidays(
                request.StartDate,
                request.EndDate,
                request.grade ?? new List<string>(),
                request.designation ?? new List<string>(),
                uniqueemployees,
                request.supercoach ?? new List<string>(),
                request.cosupercoach ?? new List<string>(),
                request.clientname ?? new List<string>(),
                request.clientgroupname ?? new List<string>(),
                request.business_unit_ids ?? new List<string>(),
                request.competency_ids ?? new List<string>());

            if (employeeAvailability == null || employeeAvailability.Count == 0)
                return new List<EmployeesForPortfolioReportDTO>();            
            if (request.location != null && request.location.Count > 0)
            {
                employeeAvailability = employeeAvailability?.Where(a => request.location.Any(loc => loc == a.location_id)).ToList();
            }

            var uniqueUserIds = employeeAvailability.Select(e => e.email_id_uid).Distinct().ToList();
            var publishedAllocations = await _allocationHttpService.PublishedResourceAllocationDays(uniqueUserIds, request.StartDate, request.EndDate);
            var uniqueJobCodes = publishedAllocations.Select(x => x.jobCode).Where(code => !string.IsNullOrEmpty(code)).Distinct().ToList();
            var clients = await _repository.GetAllClientsByJobCode(uniqueJobCodes);
            if (uniqueUserIds != null && uniqueUserIds.Count > 0)
            {
                var publishedAllocationDays = await _allocationHttpService.PublishedResourceAllocationDays(uniqueUserIds, request.StartDate, request.EndDate);
                
                if (request.clientname != null && request.clientname.Any())
                {
                    clients = clients.Where(a => request.clientname.Any(cn => cn.Equals(a.client_id, StringComparison.OrdinalIgnoreCase))).ToList();
                }
                if (request.clientgroupname != null && request.clientgroupname.Any())
                {
                    clients = clients.Where(a => request.clientgroupname.Any(cn => cn.Equals(a.client_group_code, StringComparison.OrdinalIgnoreCase))).ToList();
                }
                if (publishedAllocationDays != null && (request.clientname != null && request.clientname.Any() || request.clientgroupname != null && request.clientgroupname.Any()))
                {
                    var clientJobCodes = clients.Select(c => c.job_code).ToHashSet();
                    publishedAllocations = publishedAllocationDays.Where(a => clientJobCodes.Contains(a.jobCode)).ToList();
                }
                var publishedEmployeeAllocationDayGrp = publishedAllocations
                                                            .GroupBy(x => new { x.emailId, x.allocationDate })
                                                            .Select(g => new
                                                            {
                                                                EmailId = g.Key.emailId,
                                                                AllocationDate = g.Key.allocationDate.ToDateTime(TimeOnly.MinValue),
                                                                TotalEffort = g.Sum(x => x.efforts)
                                                            });
                if (request.clientname != null && request.clientname.Any() || request.clientgroupname != null && request.clientgroupname.Any())
                {
                    var filteremployees = publishedAllocations.Select(c => c.emailId).ToHashSet();
                    var publishedEmailIds = publishedAllocations.Select(p => p.emailId).ToHashSet(StringComparer.OrdinalIgnoreCase);
                    employeeAvailability = employeeAvailability
                        .Where(a => publishedEmailIds.Contains(a.email_id_uid))
                        .ToList();
                }

            }
            return GenerateReport(
                employeeAvailability,
                publishedAllocations,
                clients,
                DateOnly.FromDateTime(request.StartDate),
                DateOnly.FromDateTime(request.EndDate),
                request.periodType);
        }

        public static bool IsAuthorized(GetEmployeesForPortfolioReportQuery request)
        {
            bool isSuperCoachOrCo = request.userDecorator?.roles != null &&
                (request.userDecorator.roles.Contains(ConfigConstants.UserRoles.SuperCoach) ||
                 request.userDecorator.roles.Contains(ConfigConstants.UserRoles.csc));
            bool isLeaderRequest = request.roles != null && request.roles.Contains(ConfigConstants.UserRoles.Leaders);

            if (!isSuperCoachOrCo && !isLeaderRequest && request.userDecorator?.roles != null &&
                request.roles != null && !request.roles.Any(role => request.userDecorator.roles.Contains(role)))
            {
                return false;
            }
            return true;
        }

        private async Task<List<Employee>> GetFilteredEmployees(GetEmployeesForPortfolioReportQuery request)
        {
            var employees = new List<Employee>();

            // Get employees for super coaches
            if (request.roles != null && request.roles.Any(r => r != null && r.ToLower() == ConfigConstants.UserRoles.SuperCoach.ToLower()) && IsSuperCoachRequest(request))
            {
                var cscEmployees = await _repository.GetEmployeeBySuperCoachOrCSC(request.emp_mid);
                if (cscEmployees != null && cscEmployees.Count > 0)
                {
                    cscEmployees = cscEmployees.Where(a => a.supercoach_mid == request.emp_mid).ToList();
                }
                employees.AddRange(cscEmployees);
            }

            // Get employees for super coaches
            if (request.roles != null && request.roles.Any(r => r != null && r.ToLower() == ConfigConstants.UserRoles.csc.ToLower()) && IsCoSuperCoachRequest(request))
            {
                var cscEmployees = await _repository.GetEmployeeBySuperCoachOrCSC(request.emp_mid);
                if (cscEmployees != null && cscEmployees.Count > 0)
                {
                    cscEmployees = cscEmployees.Where(a => a.reporting_partner_mid == request.emp_mid).ToList();
                }
                employees.AddRange(cscEmployees);
            }

            if (request.roles != null && request.roles.Any(r => r != null && r.ToLower() == ConfigConstants.UserRoles.SuperCoach.ToLower()) && IsSuperCoachRequest(request))
            {
                //check super coach delegate and get super coach mid from identity
                var superCoachMid = await Helper.GetSuperCoachMid(request.emp_mid, _identityHttpService);
                if (superCoachMid.Count > 0)
                {
                    var delegateEmployees = await _repository.GetEmployeeBySuperCoachOrCSCList(superCoachMid);
                    employees.AddRange(delegateEmployees);
                }
            }

            // Get employees for business units and competencies
            if (IsLeaderRequest(request))
            {
                var buEmployees = await _repository.GetEmployeesByBUAndCompetencies(
                    request.business_unit_ids ?? new List<string>(),
                    request.competency_ids ?? new List<string>());
                employees.AddRange(buEmployees);
            }

            // Add specific employee if requested
            if (IsEmployeeRequest(request))
            {
                employees.Add(new Employee { employee_mid = request.emp_mid });
            }

            return employees.DistinctBy(e => e.employee_mid).ToList();
        }

        public static bool IsSuperCoachRequest(GetEmployeesForPortfolioReportQuery request)
        {
            return request.userDecorator?.roles != null &&
                (request.roles == null || request.roles.Count == 0 ||
                 request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.SuperCoach, StringComparison.OrdinalIgnoreCase)));
        }

        public static bool IsCoSuperCoachRequest(GetEmployeesForPortfolioReportQuery request)
        {
            return request.userDecorator?.roles != null &&
                (request.roles == null || request.roles.Count == 0 ||
                 request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.csc, StringComparison.OrdinalIgnoreCase)));
        }

        public static bool IsLeaderRequest(GetEmployeesForPortfolioReportQuery request)
        {
            return (request.roles == null || request.roles.Count == 0) ||
                   request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.Leaders, StringComparison.OrdinalIgnoreCase));
        }

        public static bool IsEmployeeRequest(GetEmployeesForPortfolioReportQuery request)
        {
            return request.roles != null &&
                   request.roles.Any(x => x.Equals("employee", StringComparison.OrdinalIgnoreCase)) &&
                   !string.IsNullOrEmpty(request.emp_mid);
        }

        public static List<EmployeesForPortfolioReportDTO> GenerateReport(
            List<EmployeeLeavesHolidayAndAvailabity> employeeData,
            List<PublishedResourceAllocationDayResponse> allocations,
            List<GetJobCodeClientDTO> clients,
            DateOnly startDate,
            DateOnly endDate,
            ReportPeriodType periodType)
        {

            var timePeriods = new List<(DateOnly Start, DateOnly End)> { (startDate, endDate) };
            var allReports = new List<EmployeesForPortfolioReportDTO>();

            var timeBuckets = periodType == ReportPeriodType.Weekly
                ? GetWeeksForPeriod(startDate, endDate)
                : GetMonthsForPeriod(startDate, endDate);

            var periodData = ProcessPeriodData(
                employeeData,
                allocations,
                clients,
                startDate,
                endDate,
                timeBuckets,
                periodType);

            allReports.AddRange(periodData);
            return allReports;
        }

        public static List<EmployeesForPortfolioReportDTO> ProcessPeriodData(
            List<EmployeeLeavesHolidayAndAvailabity> empData,
            List<PublishedResourceAllocationDayResponse> allocData,
            List<GetJobCodeClientDTO> clients,
            DateOnly periodStart,
            DateOnly periodEnd,
            List<(DateOnly Start, DateOnly End)> timeBuckets,
            ReportPeriodType periodType)
        {
            // Filter and process data for the current period
            var periodEmpData = empData
                .Where(e => DateOnly.FromDateTime(e.working_date) >= periodStart && DateOnly.FromDateTime(e.working_date) <= periodEnd)
                .ToList();

            var periodAllocData = allocData
                .Where(e => e.allocationDate >= periodStart && e.allocationDate <= periodEnd)
                .ToList();

            // Calculate working hours per time bucket
            var bucketWorkingHours = timeBuckets.ToDictionary(
                b => $"{(periodType == ReportPeriodType.Monthly ? "M" : "W")}{timeBuckets.IndexOf(b) + 1}",
                b => GetWorkingDays(b.Start, b.End) * 8
            );

            // Process employee and allocation data
            var empList = periodEmpData.Select(emp => new TempEmployeeAllocation
            {
                Email = emp.email_id_uid,
                Name = emp.name,
                Designation = emp.designation_name,
                Grade = emp.grade_name,
                CoSuperCoachMid = emp.co_super_coach_mid,
                SuperCoachMid = emp.super_coach_mid,
                Location = emp.location_name,
                Date = DateOnly.FromDateTime(emp.working_date),
                AvailablevsAllocated = GetAvailabilityStatus(emp),
                AllocationEfforts = CalculateAllocationEffort(emp),
                ClientGroup = emp.clientgroup,
                Client = emp.client,
                IsAllocation = false,
                IsLeave = emp.leave_hrs > 0,
                IsHoliday = emp.holiday_hrs > 0,
                LeaveHours = emp.leave_hrs,
                HolidayHours = emp.holiday_hrs,
                SupercoachName = emp.supercoach_name,
                CoSupercoachName = emp.co_supercoach_name
            }).ToList();

            var allocList = periodAllocData.Select(alloc =>
            {
                var empInfo = empList.FirstOrDefault(e => e.Email == alloc.emailId);
                if (empInfo != null)
                {
                    return new TempEmployeeAllocation
                    {
                        Name = empInfo?.Name,
                        Designation = empInfo?.Designation,
                        Grade = empInfo?.Grade,
                        CoSuperCoachMid = empInfo?.CoSuperCoachMid,
                        SuperCoachMid = empInfo?.SuperCoachMid,
                        Location = empInfo?.Location,
                        Date = alloc.allocationDate,
                        JobCode = alloc.jobCode,
                        JobName = alloc.jobName,
                        PipelineName = alloc.pipelineName,
                        AllocationEfforts = alloc.efforts,
                        AvailablevsAllocated = "Allocated",
                        ClientGroup = empInfo?.ClientGroup,
                        Client = empInfo?.Client,
                        IsAllocation = true,
                        SupercoachName = empInfo?.SupercoachName,
                        CoSupercoachName = empInfo?.CoSupercoachName,
                        Email = empInfo?.Email,
                    };
                }
                return null; // <-- Add this line
            }).Where(x => x != null).ToList(); // Optionally filter out nulls

            var mergedList = empList.Concat(allocList).ToList();

            // Assign time buckets to each record
            foreach (var row in mergedList)
            {
                for (int i = 0; i < timeBuckets.Count; i++)
                {
                    if (row.Date >= timeBuckets[i].Start && row.Date <= timeBuckets[i].End)
                    {
                        row.TimeBucket = $"{(periodType == ReportPeriodType.Monthly ? "M" : "W")}{i + 1}";
                        break;
                    }
                }
            }

            // Generate summary data
            var summaries = mergedList
                .GroupBy(x => new { x.Email, x.TimeBucket })
                .Select(g =>
                {
                    var first = g.First();
                    var bucketKey = g.Key.TimeBucket;
                    var totalPossibleHours = bucketWorkingHours.GetValueOrDefault(bucketKey, 0);

                    return new
                    {
                        EmployeeInfo = first,
                        Bucket = bucketKey,
                        AllocatedHours = g.Where(x => x.AvailablevsAllocated == "Allocated")
                                        .Sum(x => x.AllocationEfforts),
                        LeaveHours = g.Sum(x => x.LeaveHours),
                        HolidayHours = g.Sum(x => x.HolidayHours),
                        AvailableHours = totalPossibleHours - g.Sum(x =>
                            x.AvailablevsAllocated == "Allocated" ? x.AllocationEfforts :
                            x.IsLeave ? x.LeaveHours :
                            x.IsHoliday ? x.HolidayHours : 0)
                    };
                })
                .ToList();

            // Generate report entries
            var reportEntries = new List<dynamic>();

            // Allocated entries
            reportEntries.AddRange(
                mergedList.Where(x => x.AvailablevsAllocated == "Allocated")
                    .GroupBy(x => new { x.Email, x.JobCode })
                    .Select(g =>
                    {
                        var first = g.First();
                        var bucketHours = new Dictionary<string, int>();

                        foreach (var bucket in timeBuckets)
                        {
                            var bucketKey = $"{(periodType == ReportPeriodType.Monthly ? "M" : "W")}{timeBuckets.IndexOf(bucket) + 1}";
                            bucketHours[bucketKey] = g.Where(x => x.TimeBucket == bucketKey)
                                                    .Sum(x => (int)x.AllocationEfforts);
                        }

                        return new
                        {
                            first.Email,
                            first.Name,
                            first.Designation,
                            first.Grade,
                            first.CoSuperCoachMid,
                            first.SuperCoachMid,
                            first.SupercoachName,
                            first.CoSupercoachName,
                            first.Location,
                            first.JobCode,
                            first.JobName,
                            first.ClientGroup,
                            first.Client,
                            BucketHours = bucketHours,
                            Status = "Allocated"
                        };
                    }));

            // Leave, Holiday, Available entries
            foreach (var status in new[] { "Leave", "Holiday", "Available" })
            {
                reportEntries.AddRange(
                    summaries.Where(x => status == "Leave" ? x.LeaveHours > 0 :
                                        status == "Holiday" ? x.HolidayHours > 0 :
                                        x.AvailableHours > 0)
                            .Select(x => new
                            {
                                x.EmployeeInfo.Email,
                                x.EmployeeInfo.Name,
                                x.EmployeeInfo.Designation,
                                x.EmployeeInfo.Grade,
                                x.EmployeeInfo.CoSuperCoachMid,
                                x.EmployeeInfo.SuperCoachMid,
                                x.EmployeeInfo.SupercoachName,
                                x.EmployeeInfo.CoSupercoachName,
                                x.EmployeeInfo.Location,
                                JobCode = status,
                                JobName = status,
                                ClientGroup = "",
                                Client = "",
                                BucketHours = new Dictionary<string, int> {
                                    { x.Bucket, (int)(status == "Leave" ? x.LeaveHours :
                                                  status == "Holiday" ? x.HolidayHours :
                                                  x.AvailableHours) }
                                },
                                Status = status
                            }));
            }

            // Create final report DTOs
            return reportEntries
                .GroupBy(x => new
                {
                    x.Email,
                    x.Name,
                    x.Designation,
                    x.Grade,
                    x.CoSuperCoachMid,
                    x.SuperCoachMid,

                    x.SupercoachName,
                    x.CoSupercoachName,
                    x.Location,
                    x.JobCode,
                    x.JobName,
                    x.ClientGroup,
                    x.Client,
                    x.Status
                })
                .Select(g =>
                {
                    var first = g.First();
                    var clientInfo = clients.FirstOrDefault(c => c.job_code == first.JobCode);
                    var bucketHours = new Dictionary<string, int>();

                    foreach (var bucket in timeBuckets)
                    {
                        var bucketKey = $"{(periodType == ReportPeriodType.Monthly ? "M" : "W")}{timeBuckets.IndexOf(bucket) + 1}";
                        bucketHours[bucketKey] = 0;
                    }

                    foreach (var entry in g)
                    {
                        foreach (var bh in entry.BucketHours)
                        {
                            if (bucketHours.ContainsKey(bh.Key))
                                bucketHours[bh.Key] += bh.Value;
                        }
                    }

                    return new EmployeesForPortfolioReportDTO
                    {
                        // employee_mid = first.Email,
                        Name = first.Name,
                        Designation = first.Designation,
                        Grade = first.Grade,
                        // supercoach_mid = first.SuperCoachMid,
                        //supercoach = first.SuperCoachMid,
                        // cosupercoach_mid = first.CoSuperCoachMid,
                        //cosupercoach = first.CoSuperCoachMid,
                        Officelocation = first.Location,
                        Availablevsallocated = first.Status,
                        Allocatedhours = bucketHours.Sum(x => x.Value),
                        Clientgroup = clientInfo?.client_group_name ?? first.Status,
                        Client = clientInfo?.job_client ?? first.Status,
                        Jobcode = first.JobCode,
                        Jobname = first.JobName,
                        WorkingDate = DateTime.Now,
                        WeeklyHours = bucketHours,
                        Period = periodType == ReportPeriodType.Monthly
                            ? $"M{periodStart.Month}"
                            : $"W{timeBuckets.IndexOf(timeBuckets.First(t => t.Start == periodStart)) + 1}",
                        PeriodType = periodType.ToString(),
                        PeriodStart = periodStart,
                        PeriodEnd = periodEnd,
                        Cosupercoach = first.CoSupercoachName,
                        Supercoach = first.SupercoachName

                    };
                })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Availablevsallocated switch
                {
                    "Allocated" => 0,
                    "Leave" => 1,
                    "Holiday" => 2,
                    "Available" => 3,
                    _ => 4
                })
                .ThenBy(x => x.Jobcode)
                .ToList();
        }

        public static List<(DateOnly Start, DateOnly End)> SplitDateRangeByMonth(DateOnly start, DateOnly end)
        {
            var months = new List<(DateOnly, DateOnly)>();
            var current = start;

            while (current <= end)
            {
                var lastDayOfMonth = new DateOnly(
                    current.Year,
                    current.Month,
                    DateTime.DaysInMonth(current.Year, current.Month));

                var monthEnd = lastDayOfMonth < end ? lastDayOfMonth : end;
                months.Add((current, monthEnd));
                current = monthEnd.AddDays(1);
            }

            return months;
        }

        public static List<(DateOnly Start, DateOnly End)> GetWeeksForPeriod(DateOnly start, DateOnly end)
        {
            var weeks = new List<(DateOnly, DateOnly)>();
            var current = start;

            while (current <= end)
            {
                var weekEnd = current.AddDays(6 - (int)current.DayOfWeek); // End on Sunday
                if (weekEnd > end) weekEnd = end;

                weeks.Add((current, weekEnd));
                current = weekEnd.AddDays(1);
            }

            return weeks;
        }

        public static List<(DateOnly Start, DateOnly End)> GetMonthsForPeriod(DateOnly start, DateOnly end)
        {
            var months = new List<(DateOnly, DateOnly)>();
            var current = start;

            while (current <= end)
            {
                var lastDayOfMonth = new DateOnly(
                    current.Year,
                    current.Month,
                    DateTime.DaysInMonth(current.Year, current.Month));

                var monthEnd = lastDayOfMonth < end ? lastDayOfMonth : end;
                months.Add((current, monthEnd));
                current = monthEnd.AddDays(1);
            }

            return months;
        }

        public static int GetWorkingDays(DateOnly start, DateOnly end)
        {
            int workingDays = 0;
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workingDays++;
                }
            }
            return workingDays;
        }

        public static string GetAvailabilityStatus(EmployeeLeavesHolidayAndAvailabity emp)
        {
            if (emp.holiday_hrs > 0) return "Holiday";
            if (emp.leave_hrs > 0) return "Leave";
            return "Available";
        }

        public static long CalculateAllocationEffort(EmployeeLeavesHolidayAndAvailabity emp)
        {
            if (emp.holiday_hrs > 0) return emp.holiday_hrs;
            if (emp.leave_hrs > 0) return emp.leave_hrs;
            return 0;
        }
    }
}