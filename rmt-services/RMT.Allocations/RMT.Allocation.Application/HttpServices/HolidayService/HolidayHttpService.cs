using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.HolidayService
{
    public class HolidayHttpService : IHolidayHttpService
    {
        private readonly IIdentityUserDetailsHttpApi _identityUserDetailsHttpApi;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public HolidayHttpService(IIdentityUserDetailsHttpApi identityUserDetailsHttpApi, IConfiguration configuration, HttpClient httpClient)
        {
            _identityUserDetailsHttpApi = identityUserDetailsHttpApi;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<HolidayResponseDTO> GetLocationSpecificHolidays(List<string> emailIds, List<EmployeeMasterDTO>? employeeMaster, DateTime? startDate)
        {
            if (startDate == null)
            {
                startDate = DateTime.Now;
            }
            HolidayResponseDTO holidayResponse = new HolidayResponseDTO();

            List<IdentityUserResponseDTO> employeeDetails = new();
            if (employeeMaster != null)
            {
                foreach (var item in employeeMaster)
                {
                    employeeDetails.Add(new()
                    {
                        business_unit = item.business_unit,
                        competencyId = item.competencyId,
                        competency = item.competency,
                        uemail_id = item.uemail_id,
                        employee_id = item.employee_id,
                        supercoach_mid = item.supercoach_mid,
                        co_supercoach_mid = item.co_supercoach_mid,
                        supercoach_name = item.supercoach_name,
                        co_supercoach_name = item.co_supercoach_name,
                        Supercoach = item.supercoach,
                        designation = item.designation,
                        BusinessUnit = item.business_unit,
                        emailId = item.email,
                        location = item.location,
                    });
                }
            }
            else
            {
                employeeDetails = await _identityUserDetailsHttpApi.GetEmployeesDataHttpApiQuery(emailIds);
            }

            Dictionary<string, string> EmployeeEmailLocation = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            List<string> uniqueLocation = new List<string>();
            foreach (var employee in employeeDetails)
            {
                EmployeeEmailLocation.Add(employee.emailId, employee.location);
                if (!uniqueLocation.Contains(employee.location))
                {
                    uniqueLocation.Add(employee.location);
                }
            }

            holidayResponse.EmailLocationCollection = EmployeeEmailLocation;

            //Holiday Request 
            List<HolidayRequest> holidayRequest = new List<HolidayRequest>();
            foreach (var location in uniqueLocation)
            {
                if (location != null)
                {
                    holidayRequest.Add(new HolidayRequest
                    {
                        HolidayStartDate = startDate,
                        LocationName = location
                    });
                }

            }
            if (holidayRequest.Count == 0)
            {
                return new HolidayResponseDTO();
            }

            HolidayRequestDTO request = new HolidayRequestDTO { holidayParamsDTOs = holidayRequest };


            string baseurl = _configuration.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getHolidaysLeaves = _configuration.GetSection("MicroserviceApiSettings").GetSection("GetHolidayPathByLocation").Value;

            var HolidayContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseurl + getHolidaysLeaves, HolidayContent);

            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                List<GTHolidayDTO> holidayList = JsonConvert.DeserializeObject<List<GTHolidayDTO>>(resp);
                holidayResponse.HolidayList = holidayList;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetLocationSpecificHolidays URL:" + baseurl + getHolidaysLeaves + ", Response:" + result);
            }

            return holidayResponse;
        }
    }
}
