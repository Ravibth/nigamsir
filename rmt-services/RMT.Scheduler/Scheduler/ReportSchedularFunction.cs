using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;
using RMT.Scheduler.service.AzureServices;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// ReportSchedularFunction > Materized view refresh for report
    /// </summary>
    public class ReportSchedularFunction
    {
        private readonly ITokenService _tokenService;

        public ReportSchedularFunction(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [FunctionName("ReportSchedularFunction")]
        public async Task RunAsync([TimerTrigger("%ReportSchedularTriggerTime%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"ReportSchedularFunction  executed at: {DateTime.Now}");
            string currentToken = await _tokenService.GetToken();
            log.LogInformation($"ReportSchedularFunction  Token Aquired at: {DateTime.Now}");

            //bool result = await RefreshEmployeeAllocation(log, currentToken);
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var refreshViewURLS = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.REFRESH_REPORT_VIEWNAMES_URL]);
            if (!string.IsNullOrEmpty(refreshViewURLS))
            {
                foreach (var item in refreshViewURLS.Split(','))
                {
                    try
                    {
                        log.LogInformation($"RefreshReportMaterizedView ViewName-{item} Request");
                        Dictionary<string, bool> result = await RefreshReportMaterizedView(log, currentToken, item);
                        log.LogInformation($"RefreshReportMaterizedView ViewName-{item} Response-{JsonConvert.SerializeObject(result)}");
                    }
                    catch (Exception ex1)
                    {
                        log.LogInformation($"RefreshReportMaterizedView ViewName-{item} Exception-{JsonConvert.SerializeObject(ex1)}");
                    }
                }
            }
            else
            {
                log.LogInformation("--ReportSchedularFunction--RefreshReportMaterizedView--refreshViewURLS is empty-{0}", refreshViewURLS);
            }
            log.LogInformation($"ReportSchedularFunction  Execution Completed at: {DateTime.Now}");
        }

        private async Task<bool> RefreshEmployeeAllocation(ILogger log, string currentToken)
        {
            try
            {

                using (var client = AzureHttpClient.GetAzureHttpClient(true))
                {
                    var environmentVaribles = Environment.GetEnvironmentVariables();
                    var reportRefreshUrl = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.REPORT_URL]);
                    var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                    client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                    log.LogInformation("--ReportSchedularFunction--RefreshEmployeeAllocation--URL-{0}", gateway + reportRefreshUrl);

                    bool IsRefresh = false;
                    var response = await client.PostAsync(gateway + reportRefreshUrl, null);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        IsRefresh = JsonConvert.DeserializeObject<bool>(result);
                        log.LogInformation("--ReportSchedularFunction--RefreshEmployeeAllocation--IsRefresh-{0}", IsRefresh);
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        log.LogInformation("--ReportSchedularFunction--RefreshEmployeeAllocation--Response Failed-{0}", result);
                    }
                    return IsRefresh;
                }

            }
            catch (Exception ex)
            {
                log.LogInformation("--ReportSchedularFunction--RefreshEmployeeAllocation--Exception-{0}", ex);
                throw;
            }
        }

        private async Task<Dictionary<string, bool>> RefreshReportMaterizedView(ILogger log, string currentToken, string viewName)
        {
            try
            {
                using (var client = AzureHttpClient.GetAzureHttpClient(true))
                {
                    var environmentVaribles = Environment.GetEnvironmentVariables();
                    var reportRefreshViewUrl = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.REFRESH_REPORT_VIEWS_URL]);
                    var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                    Dictionary<string, bool> responseData = new Dictionary<string, bool>();

                    Dictionary<string, bool> viewNames = new Dictionary<string, bool>
                    {
                        { viewName, true }
                    };

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                    client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                    log.LogInformation("--ReportSchedularFunction--RefreshReportMaterizedView--URL-{0}", gateway + reportRefreshViewUrl);

                    string httpContent = JsonConvert.SerializeObject(viewNames);
                    var requestData = new StringContent(httpContent, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(gateway + reportRefreshViewUrl, requestData);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        responseData = JsonConvert.DeserializeObject<Dictionary<string, bool>>(result);
                        log.LogInformation("--ReportSchedularFunction--RefreshReportMaterizedView--responseData-{0}", result);
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        log.LogInformation("--ReportSchedularFunction--RefreshReportMaterizedView--Response Failed-{0}", result);
                    }

                    return responseData;
                }

            }
            catch (Exception ex)
            {
                log.LogInformation("--ReportSchedularFunction--RefreshReportMaterizedView--Exception-{0}", ex);
                throw;
            }
        }

    }
}
