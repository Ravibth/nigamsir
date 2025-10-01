using MediatR;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices
{
    public class WCGTResourceTimesheetHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WCGTResourceTimesheetHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        //Not in use 
        //public async Task<List<WCGTResourceTimesheetDTO>> GetResourceTimesheetDataByJobCode(string jobCode, DateTime startDate, DateTime endDate)
        //{

        //    string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
        //    string getTimesheetPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetResourceTimesheetPath").Value;

        //    var apiResponse = await _httpClient.GetAsync(baseurl + getTimesheetPath + "?JobCode=" + jobCode + "&StartDate=" + startDate.ToString("MM/dd/yyyy") + "&EndDate=" + endDate.ToString("MM/dd/yyyy"));

        //    if (apiResponse.IsSuccessStatusCode)
        //    {
        //        List<WCGTResourceTimesheetDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTResourceTimesheetDTO>>();
        //        return finalResponse;
        //    }
        //    else
        //    {
        //        throw new Exception("Error fetching GetResourceTimesheetDataByJobCode");
        //    }
        //}

        //Not in use 
        //public async Task<List<WCGTTimesheetGroupDTO>> GetTimesheetGroupDataByJobCode(string jobCode, string timeOption, DateTime startDate, DateTime endDate)
        //{

        //    string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
        //    string getTimesheetPath = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTTimesheetGroup").Value;

        //    var apiResponse = await _httpClient.GetAsync(baseurl + getTimesheetPath + "?JobCode=" + jobCode + "&TimeOption=" + timeOption + "&StartDate=" + startDate.ToString("MM/dd/yyyy") + "&EndDate=" + endDate.ToString("MM/dd/yyyy"));

        //    if (apiResponse.IsSuccessStatusCode)
        //    {
        //        List<WCGTTimesheetGroupDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTTimesheetGroupDTO>>();
        //        return finalResponse;
        //    }
        //    else
        //    {
        //        throw new Exception("Error fetching GetTimesheetGroupDataByJobCode");
        //    }
        //}
    }
}
