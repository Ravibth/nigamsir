using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Infrastructure;
using RMT.Allocation.Infrastructure.Data;
using System.Net.Http.Json;
using System.Text;

namespace RMT.Allocation.Application.HttpServices
{
    public class GetEmployeeLeavesHttpApi : IGetEmployeeLeavesHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IIdentityUserDetailsHttpApi _identityUserDetailsHttpApi;
        private readonly IHolidayHttpService _holidayHttpService;
        public GetEmployeeLeavesHttpApi(HttpClient httpClient, IConfiguration config, IIdentityUserDetailsHttpApi identityUserDetailsHttpApi, IHolidayHttpService holidayHttpService)
        {
            _httpClient = httpClient;
            _config = config;
            _identityUserDetailsHttpApi = identityUserDetailsHttpApi;
            _holidayHttpService = holidayHttpService;
        }
        //aayush_cross_check
        public async Task<Int64> CalculateTotalUserLeavesInHours(EmployeeLeavesDTO employeeLeaves, Int64 weekends, Int64 working_hours)
        {
            var leavesHoursCount = 0;
            if (employeeLeaves != null)
                leavesHoursCount = employeeLeaves.leaves.Sum(m => m.hours);
            return (leavesHoursCount + (weekends * working_hours));
        }

        public async Task<List<EmployeeLeavesDTO>> GetEmployeeLeavesByEmails(GetEmployeeLeaves request)
        {
            //Holiday 

            List<GTHolidayDTO> holidays = new List<GTHolidayDTO>();
            Dictionary<string, string> EmployeeEmailLocation = new Dictionary<string, string>();

            var holidayResponse = await _holidayHttpService.GetLocationSpecificHolidays(request.emails, request.userMasterData, request.start_date);
            if (holidayResponse != null)
            {
                if (holidayResponse.HolidayList != null)
                {
                    holidays = holidayResponse.HolidayList;
                }
                if (holidayResponse.EmailLocationCollection != null)
                {
                    EmployeeEmailLocation = holidayResponse.EmailLocationCollection;
                }

            }

            //GetLeavesInfo
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            //string getEmployeeLeaves = _config.GetSection("MicroserviceApiSettings").GetSection("GetEmployeeLeavePath").Value;
            string getEmployeeLeaves = _config.GetSection("MicroserviceApiSettings").GetSection("GetLeavesInfo").Value;


            var employeeLeaveDto = new LeaveParamsDTO()
            {
                emp_mid = request.emails.Select(e => e.Contains("__") ? e.Split("__")[0] : e).ToList(),
                start_date = request.start_date,
                end_date = request.end_date,
            };

            var content = new StringContent(JsonConvert.SerializeObject(employeeLeaveDto), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getEmployeeLeaves, content);

            if (apiResponse.IsSuccessStatusCode)
            {
                List<EmployeeEmailInfoResponse> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<EmployeeEmailInfoResponse>>();
                //finalResponse = finalResponse.Where(e => (request.start_date < e.leave_start_date && request.end_date >= e.leave_start_date) ||
                //                         (request.start_date >= e.leave_start_date && request.end_date <= e.leave_end_date) ||
                //                         (request.start_date >= e.leave_start_date && request.start_date < e.leave_end_date)).ToList();
                var groupedResponse = finalResponse.GroupBy(u => u.emp_mid + "__" + u.employee_email).ToList();
                List<EmployeeLeavesDTO> result = new List<EmployeeLeavesDTO>();
                string? resourceLocation = string.Empty;
                if (groupedResponse.Count > 0)
                {
                    foreach (var group in groupedResponse)
                    {
                        // ((startdate < m.AllocationStartDate && enddate >= m.AllocationStartDate) ||
                        //(startdate >= m.AllocationStartDate && enddate <= m.AllocationEndDate) ||
                        //(startdate >= m.AllocationStartDate && startdate < m.AllocationEndDate))


                        EmployeeEmailLocation.TryGetValue(group.Key, out resourceLocation);
                        List<GTHolidayDTO> resourceHolidayList = new List<GTHolidayDTO>();
                        if (resourceLocation != null && holidays.Count > 0)
                        {
                            resourceHolidayList = holidays.Where(holiday => holiday.location_name.Trim().ToLower() == resourceLocation.Trim().ToLower()).ToList();
                        }


                        List<LeavesDTO> leavesList = new List<LeavesDTO>();

                        foreach (var leaveData in group)
                        {
                            //double revoke = leaveData.revoked_days == null ? 0 : (double)leaveData.revoked_days;
                            //double applied_days = leaveData.applied_days == null ? 0 : (double)leaveData.applied_days;
                            bool? isLeaveAlsoHoliday = resourceHolidayList?.Any(l => l.holiday_date.Date == leaveData.leave_date.ToDateTime(TimeOnly.MinValue));

                            if (isLeaveAlsoHoliday != true)
                            {
                                LeavesDTO leave = new LeavesDTO
                                {
                                    start_date = leaveData.leave_date.ToDateTime(TimeOnly.MinValue),
                                    end_date = leaveData.leave_date.ToDateTime(TimeOnly.MinValue),
                                    hours = leaveData.leave_hours,
                                    leaveType = leaveData.leave_type
                                };
                                if (!leavesList.Any(l => l.start_date.Date == leaveData.leave_date.ToDateTime(TimeOnly.MinValue)))
                                {
                                    leavesList.Add(leave);
                                }
                            }
                        }

                        foreach (GTHolidayDTO holiday in resourceHolidayList)
                        {
                            LeavesDTO holidayLeave = new LeavesDTO
                            {
                                start_date = holiday.holiday_date,
                                end_date = holiday.holiday_date,
                                hours = (int)Constants.WorkingHourPerDay,
                                leaveType = TimelineType.HOLIDAY
                            };
                            if (!leavesList.Any(l => l.start_date.Date == holidayLeave.start_date.Date))
                            {
                                leavesList.Add(holidayLeave);
                            }
                        }
                        result.Add(new EmployeeLeavesDTO { email = group.Key, leaves = leavesList });

                    }
                }
                else
                {
                    foreach (var email in request.emails)
                    {
                        List<LeavesDTO> leavesList = new List<LeavesDTO>();
                        foreach (GTHolidayDTO holiday in holidays)
                        {
                            LeavesDTO holidayLeave = new LeavesDTO
                            {
                                start_date = holiday.holiday_date,
                                end_date = holiday.holiday_date,
                                hours = (int)Constants.WorkingHourPerDay,
                                leaveType = TimelineType.HOLIDAY
                            };
                            leavesList.Add(holidayLeave);
                        }
                        result.Add(new EmployeeLeavesDTO { email = email, leaves = leavesList });
                    }
                }

                return result;
            }
            else
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetEmployeeLeavesByEmails" + response);
            }
        }
    }
}
