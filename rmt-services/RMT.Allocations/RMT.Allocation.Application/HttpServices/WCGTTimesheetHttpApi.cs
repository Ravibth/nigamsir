using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices
{
    public class WCGTTimesheetHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WCGTTimesheetHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        //Not in use 
        //public async Task<List<TimesheetResponseDTO>> GetTimesheetDataByJobCode(string jobCode)
        //{

        //    string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
        //    string getTimesheetPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetTimesheetPath").Value;

        //    var apiResponse = await _httpClient.GetAsync(baseurl + getTimesheetPath + "?jobCode=" + jobCode);

        //    if (apiResponse.IsSuccessStatusCode)
        //    {
        //        List<WCGTTimesheet> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTTimesheet>>();

        //        var groupedResponse = finalResponse.GroupBy(u => u.gradename).ToList();
        //        List<TimesheetResponseDTO> result = new List<TimesheetResponseDTO>();

        //        if (groupedResponse.Count > 0)
        //        {
        //            foreach (var group in groupedResponse)
        //            {
        //                TimesheetResponseDTO timesheet = new TimesheetResponseDTO();
        //                timesheet.Gradename = group.Key;
        //                //timesheet.Designation = group.FirstOrDefault();
        //                timesheet.TimesheetCost = group.Sum(a => (a.totaltime) * a.rate);
        //                timesheet.TimesheetHrs = group.Sum(a => a.totaltime);
        //                result.Add(timesheet);
        //            }
        //        }
        //        return result;
        //    }
        //    else
        //    {
        //        throw new Exception("Error fetching GetTimesheetDataByJobCode");
        //    }
        //}

        public async Task<WCGTJobDTO> GetJobByJobCode(string pipelineCode, string jobCode)
        {

            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getJobCodePath = _config.GetSection("MicroserviceApiSettings").GetSection("GetJobByJobCodePath").Value;

            var apiResponse = await _httpClient.GetAsync(baseurl + getJobCodePath + "?pipelineCode=" + pipelineCode + "&jobCode=" + jobCode);

            var resp = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode)
            {
                if (apiResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new WCGTJobDTO();
                }
                WCGTJobDTO finalResponse = JsonConvert.DeserializeObject<WCGTJobDTO>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception($"Error fetching GetJobByJobCode :- {resp}");
            }
        }
    }
}
