using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Application.Services.HttpServices;
using WCGT.Application.Services.IHttpServices;
using WCGT.Domain;
using WCGT.Domain.DTO;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;
using WCGT.Infrastructure.Helpers;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static WCGT.Infrastructure.Constants;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetEmployeesForPortfolioQuery : IRequest<List<EmployeeLeavesHolidayAndAvailabity>>
    {
        public string? emp_mid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int? availablity { get; set; }
        public List<string>? grade { get; set; }
        public List<string>? designation { get; set; }//done
        public List<string>? employeename { get; set; }//done
        public List<string>? location { get; set; }//done
        public List<string>? supercoach { get; set; }//done
        public List<string>? cosupercoach { get; set; }//done
        public List<string>? clientname { get; set; }
        public List<string>? clientgroupname { get; set; }
        public List<string>? business_unit_ids { get; set; }//done
        public List<string>? competency_ids { get; set; }//done
        public List<string>? roles { get; set; }
        public UserDecorator? userDecorator { get; set; }
    }

    public class GetEmployeesForPortfolioQueryHandler : IRequestHandler<GetEmployeesForPortfolioQuery, List<EmployeeLeavesHolidayAndAvailabity>>
    {
        private readonly IWcgtDataRepository _repository;
        private readonly IAllocationHttpService _allocationHttpService;
        private readonly IIdentityHttpService _identityHttpService;
        public GetEmployeesForPortfolioQueryHandler(IWcgtDataRepository repository, IAllocationHttpService allocationHttpService, IIdentityHttpService identityHttpService)
        {
            _repository = repository;
            _allocationHttpService = allocationHttpService;
            this._identityHttpService = identityHttpService;
        }


        public async Task<List<EmployeeLeavesHolidayAndAvailabity>> Handle(GetEmployeesForPortfolioQuery request, CancellationToken cancellationToken)
        {
            List<BUTreeMapping> buTreeGroupByMid = new(), AllBUTreeMappingDTO = new();
            //List<Competency> buCompetencyList = new();
            List<Employee>? cscEmployees = new();   //SUPER COACH OR CO SUPER COACH
            //List<string> buids = new();
            List<Employee> finalResult = new List<Employee>();
            List<string> grade = new List<string>();
            List<string> designation = new List<string>();
            List<string> employeename = new List<string>();
            List<string> supercoach = new List<string>();
            List<string> cosupercoach = new List<string>();
            List<string> clientname = new List<string>();
            List<string> clientgroupname = new List<string>();
            List<string> business_unit_ids = new List<string>();
            List<string> competency_ids = new List<string>();
            List<string> location = new List<string>();
            List<string> selectedEmpByClientId = new List<string>();

            bool isSuperCoachOrCo = request.userDecorator != null && request.userDecorator.roles != null &&
              (request.userDecorator.roles.Contains(ConfigConstants.UserRoles.SuperCoach) || request.userDecorator.roles.Contains(ConfigConstants.UserRoles.csc));

            bool isLeaderRequest = request.roles != null && request.roles.Contains(ConfigConstants.UserRoles.Leaders);

            if (!isSuperCoachOrCo && !isLeaderRequest && request.userDecorator != null &&
                request.userDecorator.roles != null &&
                request.roles != null &&
                !request.roles.Any(role => request.userDecorator.roles.Contains(role)))
            {
                return new List<EmployeeLeavesHolidayAndAvailabity>();
            }

            if (request.business_unit_ids != null && request.business_unit_ids.Count > 0)
            {
                business_unit_ids = request.business_unit_ids;
            }
            if (request.competency_ids != null && request.competency_ids.Count > 0)
            {
                competency_ids = request.competency_ids;
            }

            if (isSuperCoachOrCo &&
                (
                    (request.roles == null || request.roles.Count == 0)
                    || (request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.SuperCoach, StringComparison.OrdinalIgnoreCase)) || request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.csc, StringComparison.OrdinalIgnoreCase)))
                    )
                )
                cscEmployees = await _repository.GetEmployeeBySuperCoachOrCSC(request.emp_mid);// Get employees where current user is a SUPER COACH

            //Filter employees based on role from UI
            if (cscEmployees.Count > 0 && request.roles != null)
            {
                if (request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.SuperCoach, StringComparison.OrdinalIgnoreCase)))
                {
                    var supercoachemployees = cscEmployees.Where(x => x.supercoach_mid == request.emp_mid).ToList();
                    finalResult.AddRange(supercoachemployees);
                }
                if (request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.csc, StringComparison.OrdinalIgnoreCase)))
                {
                    var supercoachemployees = cscEmployees.Where(x => x.reporting_partner_mid == request.emp_mid).ToList();
                    finalResult.AddRange(supercoachemployees);
                }
            }

            if (request.roles != null && request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.SuperCoach, StringComparison.OrdinalIgnoreCase)))
            {
                //check super coach delegate and get super coach mid from identity
                var superCoachMid = await Helper.GetSuperCoachMid(request.emp_mid, _identityHttpService);
                if (superCoachMid.Count > 0)// Get employees where current user is a SUPER COACH Delegate
                {                    
                    var delegateEmployees = await _repository.GetEmployeeBySuperCoachOrCSCList(superCoachMid);
                    finalResult.AddRange(delegateEmployees);                   
                }
            }

            var employees = (await _repository.GetEmployeesByBUAndCompetencies(business_unit_ids, competency_ids));

            if (
                ((request.roles == null || request.roles.Count == 0) || (request.roles.Any(x => x.Equals(ConfigConstants.UserRoles.Leaders, StringComparison.OrdinalIgnoreCase))))
                && employees != null)
            {
                finalResult.AddRange(employees);
            }

            // Remove duplicates (if any employee appears in both CSC and Leader sets)
            finalResult = finalResult
                            .Where(e => e != null)
                            .DistinctBy(e => e.employee_mid)
                            .ToList();
            List<string> userEmpMids = finalResult.Select(e => e.employee_mid).Distinct().ToList();

            if (request.roles != null && request.roles.Any(x => x.Equals("employee", StringComparison.OrdinalIgnoreCase)) && !string.IsNullOrEmpty(request.emp_mid))
            {
                userEmpMids.Add(request.emp_mid);
                userEmpMids = userEmpMids.Distinct().ToList();
            }

            if (userEmpMids == null || userEmpMids.Count == 0)
            {
                return new();
            }

            if (request.grade != null && request.grade.Count > 0)
            {
                grade = request.grade;
            }
            if (request.designation != null && request.designation.Count > 0)
            {
                designation = request.designation;
            }
            if (request.employeename != null && request.employeename.Count > 0)
            {
                userEmpMids = request.employeename;
            }
            if (request.supercoach != null && request.supercoach.Count > 0)
            {
                supercoach = request.supercoach;
            }
            if (request.cosupercoach != null && request.cosupercoach.Count > 0)
            {
                cosupercoach = request.cosupercoach;
            }
            if (request.clientname != null && request.clientname.Count > 0)
            {
                clientname = request.clientname;
            }
            if (request.clientgroupname != null && request.clientgroupname.Count > 0)
            {
                clientgroupname = request.clientgroupname;
            }
          

            var employeeAvailablityWithLeavesAndHolidays = await _repository.GetEmployeeAvailabilityWithLeavesAndHolidays(
                request.StartDate,
                request.EndDate,
                grade,
                designation,
                userEmpMids,
                supercoach,
                cosupercoach,
                clientname,
                clientgroupname,
                business_unit_ids,
                competency_ids
                );
            if (employeeAvailablityWithLeavesAndHolidays != null && employeeAvailablityWithLeavesAndHolidays.Count > 0)
            {
                var uniqueUserIds = employeeAvailablityWithLeavesAndHolidays.Select(e => e.email_id_uid).Distinct().ToList();
                if (uniqueUserIds != null && uniqueUserIds.Count > 0)
                {
                    var publishedAllocationDays = await _allocationHttpService.PublishedResourceAllocationDays(uniqueUserIds, request.StartDate, request.EndDate);
                    if (publishedAllocationDays != null && publishedAllocationDays.Count > 0)
                    {
                        var uniqueJobCodes = publishedAllocationDays.Select(x => x.jobCode).Where(code => !string.IsNullOrEmpty(code)).Distinct().ToList();
                        var clients = await _repository.GetAllClientsByJobCode(uniqueJobCodes);
                        if (request.clientname != null && request.clientname.Any())
                        {
                            clients = clients.Where(a => request.clientname.Any(cn => cn.Equals(a.client_id, StringComparison.OrdinalIgnoreCase))).ToList();
                        }
                        if (request.clientgroupname != null && request.clientgroupname.Any())
                        {
                            clients = clients.Where(a => request.clientgroupname.Any(cn => cn.Equals(a.client_group_code, StringComparison.OrdinalIgnoreCase))).ToList();
                        }
                        if (request.location != null && request.location.Any())
                        {
                            employeeAvailablityWithLeavesAndHolidays = employeeAvailablityWithLeavesAndHolidays?.Where(a => request.location.Any(loc => loc == a.location_id)).ToList();
                        }
                        if (publishedAllocationDays != null &&(request.clientname != null && request.clientname.Any() || request.clientgroupname != null && request.clientgroupname.Any()))
                        {
                            var clientJobCodes = clients.Select(c => c.job_code).ToHashSet();
                            selectedEmpByClientId = publishedAllocationDays.Where(a => clientJobCodes.Contains(a.jobCode)).Select(m=>m.emailId).Distinct().ToList();                            
                        }
                        var publishedEmployeeAllocationDayGrp = publishedAllocationDays
                                                            .GroupBy(x => new { x.emailId, x.allocationDate })
                                                            .Select(g => new
                                                            {
                                                                EmailId = g.Key.emailId,
                                                                AllocationDate = g.Key.allocationDate.ToDateTime(TimeOnly.MinValue),
                                                                TotalEffort = g.Sum(x => x.efforts)
                                                            });
                    
                        var publishedEmployeeAllocationDayUniqDict = publishedEmployeeAllocationDayGrp.ToDictionary(g => string.Concat(g.EmailId, g.AllocationDate.ToString()), g => new
                        {
                            EmployeeEmail = g.EmailId,
                            AllocationDate = g.AllocationDate,
                            TotalEffort = g.TotalEffort
                        });
                        if (request.clientname != null && request.clientname.Any() || request.clientgroupname != null && request.clientgroupname.Any())
                        {
                            //var filteremployees = publishedAllocationDays.Select(c => c.emailId).ToHashSet();
                            //var publishedEmailIds = publishedAllocationDays.Select(p => p.emailId).ToHashSet(StringComparer.OrdinalIgnoreCase);
                            employeeAvailablityWithLeavesAndHolidays = employeeAvailablityWithLeavesAndHolidays
                                .Where(a => selectedEmpByClientId.Contains(a.email_id_uid))
                                .ToList();
                        }
                        foreach (var item in employeeAvailablityWithLeavesAndHolidays)
                        {
                            var keyCheck = string.Concat(item.email_id_uid, DateOnly.FromDateTime(item.working_date).ToDateTime(TimeOnly.MinValue));
                            if (publishedEmployeeAllocationDayUniqDict.ContainsKey(keyCheck))
                            {
                                item.allocation_hrs = (int)publishedEmployeeAllocationDayUniqDict[keyCheck].TotalEffort;
                            }
                        }
                    }
                }
            }
            var employeeFinalDictonary = employeeAvailablityWithLeavesAndHolidays.GroupBy(g => g.employee_mid).ToDictionary(x => x.Key, x => x.ToList());
            var employeeGroupedResponse = employeeAvailablityWithLeavesAndHolidays
                .GroupBy(x => x.employee_mid)
                .Select(g =>
                new
                {
                    EmployeeMid = g.Key,
                    TotalAvailableHrs = g.Sum(x => x.available_hrs),
                    TotalAllocationHrs = g.Sum(x => x.allocation_hrs),
                    TotalLeaveHrs = g.Sum(x => x.leave_hrs),
                    TotalHolidayHrs = g.Sum(x => x.holiday_hrs)
                })
                .OrderBy(x => x.EmployeeMid).ToList();

            if (request.availablity != null)
            {
                employeeGroupedResponse = employeeGroupedResponse
                    .Where(x =>
                        x.TotalAvailableHrs > 0 &&  // prevent divide-by-zero
                        (((double)(x.TotalAvailableHrs - (x.TotalAllocationHrs + x.TotalLeaveHrs + x.TotalHolidayHrs)) / x.TotalAvailableHrs) * 100) >= request.availablity
                    ).ToList();

                var filteredAvailableEmployeeMids = employeeGroupedResponse.Select(x => x.EmployeeMid).ToDictionary(t => t, t => 1);
                var filtered = employeeFinalDictonary
                                .Where(kvp => filteredAvailableEmployeeMids
                                .ContainsKey(kvp.Key)).ToDictionary(l => l.Key, l => l.Value);
                employeeFinalDictonary = filtered;
            }
            if (request.PageNumber != 0)
            {
                employeeFinalDictonary = employeeFinalDictonary.OrderBy(kvp => kvp.Key).Skip(((request.PageNumber - 1) * request.PageSize)).Take(request.PageSize).ToDictionary(x => x.Key, x => x.Value);
            }
            return employeeFinalDictonary.Values.SelectMany(x => x).ToList();
        }
    }
}
