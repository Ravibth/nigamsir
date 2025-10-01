using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMT.Notification.Application.Constants;
using RMT.Notification.Application.HttpServices.DTO;
using RMT.Notification.Domain.DTO;
using RMT.Notification.Infrastructure.Constants;
using ServiceLayer.Services.ConfigurationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static RMT.Notification.Application.Constants.Constants;

namespace RMT.Notification.Application.Helpers
{
    public class NotificationPayloadDTO
    {
        public string token { get; set; }
        public string action { get; set; }
        public string payload { get; set; }
    }

    public class InitNotificationDTO
    {
        public string path { get; set; }
        public string response_payload { get; set; }
        public string? token { get; set; }
        public string? request_payload { get; set; }
        public string? userinfo { get; set; }
    }

    //item_id   abc-def-ghi-jkl
    //EmpEmail  
    //EmpName   abc tyagi
    //PipelineCode pc101
    //JobCode jc101
    //due_date
    public static class Helper
    {
        public static string ComposeMsg(List<string> keys, string msg, ref Dictionary<string, string> DTokens, string prefix, string suffix)
        {
            string finalStr = msg;
            foreach (var key in keys)
            {
                string customResult = GetCustomValuesOfKeys(key, DTokens);
                string finalKey = prefix + key + suffix;
                if (!string.IsNullOrEmpty(customResult))
                {
                    finalKey = customResult;
                }
                else
                {
                    //All keys containing ## will be url encoded to handle the special characters
                    string tempKey = string.Empty;
                    if (key.Contains("##"))
                    {
                        tempKey = key.Split(":")[0].Split("##")[0].Trim();
                    }
                    else
                    {
                        tempKey = key.Split(":")[0].Trim();
                    }
                    DTokens.TryGetValue(tempKey, out finalKey);
                }
                if (key.Contains("##"))
                {
                    if (key.Contains("jobcode") && string.IsNullOrEmpty(finalKey))
                    {
                        finalKey = "undefined";
                    }
                    if (!string.IsNullOrEmpty(finalKey))
                        finalKey = HttpUtility.UrlEncode(finalKey);
                }
                finalStr = finalStr.Replace(prefix + key + suffix, finalKey);
            }
            return finalStr;
        }

        private static string GetCustomValuesOfKeys(string key, Dictionary<string, string> DTokens)
        {
            string keyName = key.Split(":")[0];
            switch (keyName)
            {
                case "ProjectName":
                    string jobCode = string.Empty;
                    string projectName = string.Empty;
                    string pipelineCode = string.Empty;
                    DTokens.TryGetValue("jobCode", out jobCode);
                    //try to get jobCode with lowercase key name
                    if (string.IsNullOrEmpty(jobCode))
                    {
                        DTokens.TryGetValue("jobcode", out jobCode);
                    }
                    var res = string.IsNullOrEmpty(jobCode) == false ?
                       DTokens.TryGetValue("jobname", out projectName) : DTokens.TryGetValue("pipelinename", out projectName);
                    return projectName;
                default:
                    return string.Empty;
                    break;
            }
            return string.Empty;
        }

        public static string[] GetEmailIdFromMID(List<string> input)
        {
            List<string> EmailIds = new List<string>();
            if (input != null)
            {
                foreach (var item in input)
                {
                    try
                    {
                        string emailPart = GetEmailPart(item, string.Empty);
                        EmailIds.Add(emailPart);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            EmailIds = EmailIds.Where(x => !string.IsNullOrEmpty(x)).ToList();
            return EmailIds.ToArray();
        }

        public static string GetEmailPart(string emailString, string? name)
        {
            if (!string.IsNullOrEmpty(emailString) && emailString.Contains("__"))
            {
                var spiltMail = emailString.Split("__");
                if (spiltMail.Length > 1)
                {
                    return string.IsNullOrEmpty(name) ? spiltMail[1] : name + " (" + spiltMail[1] + ")";
                }
            }
            else if (!string.IsNullOrEmpty(emailString) && !emailString.Contains("__"))
            {
                return emailString;
            }
            return string.Empty;
        }

        public static async Task<string> GetEmailBodyWithEmailAddress(string emailBody, IConfigurationService configurationService)
        {

            if (!string.IsNullOrEmpty(emailBody))
            {
                string pattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";
                Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection matchCollection = rx.Matches(emailBody);
                List<string> emailList = matchCollection.Cast<Match>().Select(m => m.Value).ToList();
                if (emailList != null && emailList.Count > 0)
                {
                    List<IdentityUserResponseDTO> userList = await configurationService.GetUserDetailsByUserEmailId(emailList);
                    Dictionary<string, string> emailName = new Dictionary<string, string>();
                    if (userList != null && userList.Count > 0)
                    {
                        foreach (var item in userList)
                        {
                            emailName.Add(item.emailId, item.name);
                        }
                    }

                    foreach (Match match in matchCollection)
                    {
                        string name = string.Empty;
                        emailName.TryGetValue(match.Value.ToString(), out name);
                        string emailOnly = GetEmailPart(match.Value.ToString(), name);
                        emailBody = emailBody.Replace(match.Value.ToString(), emailOnly);
                    }
                }

            }
            return emailBody;
        }

        public static Dictionary<string, string> ConvertDateTimeKeysToDateOnly(Dictionary<string, string> DTokens)
        {
            foreach (var dateTimeKey in Constants.Constants.KeysOfDateTimes)
            {
                if (DTokens.ContainsKey(dateTimeKey) && !string.IsNullOrEmpty(DTokens[dateTimeKey]))
                {
                    try
                    {
                        DTokens[dateTimeKey] = DateOnly.FromDateTime(Convert.ToDateTime(DTokens[dateTimeKey])).ToString("dd-MM-yyyy");
                    }
                    catch { }
                }
            }
            return DTokens;
        }

        public static List<string> ExtractKeysFromMsg(string input, string notificationPlaceholderRegex)
        {
            //string pattern = Constants.Constants.NotificationTokenPattern;
            string pattern = notificationPlaceholderRegex;

            // Match the pattern in the input string
            MatchCollection matches = Regex.Matches(input, pattern);

            // Extract values from matches
            List<string> extractedValues = new List<string>();
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    extractedValues.Add(match.Groups[1].Value);
                }
            }

            return extractedValues;
        }


        public static string UrlBuilderByQuery(string baseUrl, Dictionary<string, dynamic> queries)
        {
            var urlBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            foreach (var item in queries)
            {
                if (item.Value is string)
                {
                    query.Add(item.Key, item.Value);
                }
                else if (item.Value is List<string>)
                {
                    foreach (var item1 in item.Value)
                    {
                        query.Add(item.Key, item1);
                    }
                }
                else if (item.Value is null)
                {
                    query.Add(item.Key, "null");
                }
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }

        public static string ParseKeyName(string keyName)
        {
            string parsed = keyName.Trim().ToLower().Replace("-", "");
            return parsed;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="marketPlaceProjectListing"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string GenerateMarketplaceProjectDetailsBody(List<MarketPlaceProjectDetailDTO> marketPlaceProjectListing, string title)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (marketPlaceProjectListing.Count > 0)
            {
                tableContent.Append("<table border='1'><tr>" +
                      "<th>Project Name</th>" +
                      "<th>Date of Exit</th>" +
                      "<th>Resource Requestor</th>" +
                   " </tr>");
                foreach (var item in marketPlaceProjectListing)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(string.IsNullOrEmpty(item.JobCode) ? item.JobName : item.PipelineName).Append("</td>");
                    tableContent.Append("<td>").Append(Convert.ToString(DateOnly.FromDateTime((DateTime)item.MarketPlaceExpirationDate))).Append("</td>");
                    tableContent.Append("<td>").Append(string.Join(",", item.ResourceRequestors.Select(m => m.Email))).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");

                body.Append("<h2>").Append(title).Append("</h2>");
                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();

        }

        public static string ParseMarketplaceSubscriptionSummary(List<MarketPlaceProjectDetailDTO> marketPlaceProjectListing)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (marketPlaceProjectListing.Count > 0)
            {
                //Recheck
                tableContent.Append("<table border='1'><tr>" +
                      "<th>Project Name</th>" +
                      "<th>BU</th>" +
                      "<th>Offerings</th>" +
                      "<th>Solutions</th>" +
                      "<th>Description</th>" +
                      "<th>CSP</th>" +
                      "<th>Date of Exit</th>" +
                   " </tr>");
                foreach (var item in marketPlaceProjectListing)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(string.IsNullOrEmpty(item.JobCode) ? item.PipelineName : item.JobName).Append("</td>");
                    tableContent.Append("<td>").Append(item.BusinessUnit).Append("</td>");
                    tableContent.Append("<td>").Append(item.Offerings).Append("</td>");
                    tableContent.Append("<td>").Append(item.Solutions).Append("</td>");
                    tableContent.Append("<td>").Append(item.Description).Append("</td>");
                    tableContent.Append("<td>").Append(string.IsNullOrEmpty(item.JobCode) ? item.ProposedCsp : item.Csp).Append("</td>");
                    tableContent.Append("<td>").Append(Convert.ToString(DateOnly.FromDateTime((DateTime)item.MarketPlaceExpirationDate))).Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");

                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(tableContent);
            }
            return emailContent.ToString();
        }

        public static string ParsePendingAllocationTaskSummary(List<WorkflowDTO> allocationWorkflowTask, int adjustDays)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();
            if (allocationWorkflowTask.Count > 0)
            {
                tableContent.Append("<table border='1'><tr>" +
                    "<th>Title</th>" +
                    "<th>Resource Name</th>" +
                    "<th>Designation</th>" +
                    "<th>Grade</th>" +
                    "<th>Project Name</th>" +
                    "<th>Request Received Date</th>" +
                    "<th>Request Received From</th>" +
                    "<th> Due Date </th>" +
                    "<th>Status </th>" +
              " </tr>");
                foreach (var item in allocationWorkflowTask)
                {
                    if (CheckIsValidJsonObject(Convert.ToString(item.workflow.entity_meta_data)))
                    {
                        var meta = JObject.Parse(item.workflow.entity_meta_data.ToString());
                        var requistionData = meta.ContainsKey("Requisitions") ? meta["Requisitions"] :
                            meta.ContainsKey("Requisition")
                            ? meta["Requisition"] : null;
                        if (requistionData != null && CheckIsValidJsonObject(Convert.ToString(requistionData)))
                        {
                            var metaReq = JObject.Parse(Convert.ToString(requistionData));
                            tableContent.Append("<tr>");
                            tableContent.Append("<td>").Append(item.title).Append("</td>");
                            tableContent.Append("<td>").Append(meta["EmpName"] != null ? Convert.ToString(meta["EmpName"]) : "").Append("</td>");
                            tableContent.Append("<td>").Append(metaReq["Designation"] != null ? Convert.ToString(metaReq["Designation"]) : "").Append("</td>");
                            tableContent.Append("<td>").Append(metaReq["Grade"] != null ? Convert.ToString(metaReq["Grade"]) : "").Append("</td>");
                            tableContent.Append("<td>").Append(String.IsNullOrEmpty(meta["JobCode"]?.ToString()) ? meta["PipelineName"]?.ToString() : meta["JobName"]?.ToString()).Append("</td>");
                            tableContent.Append("<td>").Append(item.created_at != null ? Convert.ToString(DateOnly.FromDateTime(item.created_at)) : "").Append("</td>");
                            tableContent.Append("<td>").Append(item?.created_by).Append("</td>");
                            tableContent.Append("<td>").Append(item?.due_date != null ? Convert.ToString(DateOnly.FromDateTime((DateTime)item.due_date)) : "").Append("</td>");
                            tableContent.Append("<td>").Append(item?.due_date != null ? DateOnly.FromDateTime((DateTime)item?.due_date) < DateOnly.FromDateTime(DateTime.Now.Date.AddDays(adjustDays)) ? "Overdue" : "Pending" : "Pending").Append("</td>");
                            tableContent.Append("</tr>");
                        }
                    }
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");
                body.Append("<h2>Allocation Requests:</h2></br>");
                body.Append("<p>In case no action is taken on the allocation request, the allocation will be auto-approved.\r\n:</p>");
                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(body);
            }
            return emailContent.ToString();
        }

        public static string ParsePendingSkillTaskSummary(List<WorkflowDTO> skillWorkflowTask, int adjustDays)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (skillWorkflowTask.Count > 0)
            {
                tableContent.Append("<table border='1'><tr>" +
                    "<th>Title</th>" +
                    "<th>Task ID</th>" +
                    "<th>Request Received Date</th>" +
                    "<th>Request Age (Days)</th>" +
                    "<th>Status </th>" +
                    "<th>Employee Name</th>" +
                    "<th>Skills Name </th>" +
                    "<th>Skill Proficiency Level </th>" +
                    " </tr>");
                foreach (var item in skillWorkflowTask)
                {
                    if (item != null && item.workflow != null && CheckIsValidJsonObject(Convert.ToString(item.workflow.entity_meta_data)))
                    {
                        var meta = JObject.Parse(item.workflow.entity_meta_data.ToString());
                        tableContent.Append("<tr>");
                        tableContent.Append("<td>").Append(item.title).Append("</td>");
                        tableContent.Append("<td>").Append(item.id).Append("</td>");
                        tableContent.Append("<td>").Append(item.created_at != null ? DateOnly.FromDateTime(item.created_at.Date) : "").Append("</td>");
                        tableContent.Append("<td>").Append(item.created_at != null ? (DateTime.Now.Date.AddDays(adjustDays) - item.created_at.Date).Days : "").Append("</td>");
                        tableContent.Append("<td>").Append(item.status).Append("</td>");
                        tableContent.Append("<td>").Append(meta["Name"] != null ? meta["Name"]?.ToString() : "").Append("</td>");
                        tableContent.Append("<td>").Append(meta["SkillName"] != null ? meta["SkillName"]?.ToString() : "").Append("</td>");
                        tableContent.Append("<td>").Append(meta["Proficiency"] != null ? meta["Proficiency"]?.ToString() : "").Append("</td>");
                        tableContent.Append("</tr>");

                    }

                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");
                if (skillWorkflowTask.Count > 0)
                {
                    body.Append("<h2>Skill Assessment Requests:</h2></br>");
                }
                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(body);
            }
            return emailContent.ToString();
        }

        public static string ParseNewProjectDetails(List<ProjectFullDetailsResponse> projects)
        {
            StringBuilder emailContent = new StringBuilder();
            // Generate the table dynamically
            StringBuilder tableContent = new StringBuilder();

            if (projects.Count > 0)
            {
                tableContent.Append("<table border='1'><tr>" +
                    "<th>Pipeline/Job Name</th>" +
                    "<th>Pipeline/Job Code</th>" +
                    "<th>Creation Date</th>" +
                    "<th>Link</th>" +
                    " </tr>");
                foreach (var item in projects)
                {
                    tableContent.Append("<tr>");
                    tableContent.Append("<td>").Append(string.IsNullOrEmpty(item.JobCode) == false ? item.JobName : item.PipelineName).Append("</td>");
                    tableContent.Append("<td>").Append(string.IsNullOrEmpty(item.JobCode) == false ? item.JobCode : item.PipelineCode).Append("</td>");
                    tableContent.Append("<td>").Append(DateOnly.FromDateTime((DateTime)item.CreatedAt)).Append("</td>");
                    tableContent.Append("<td>").Append("").Append("</td>");
                    tableContent.Append("</tr>");
                }
                tableContent.Append("</table>");

                // Email body with the dynamically generated table
                StringBuilder body = new StringBuilder();
                body.Append("<html><body>");
                body.Append(tableContent);
                body.Append("</body></html>");
                emailContent.Append(body);
            }
            return emailContent.ToString();
        }

        public static List<CommonSenderDTO> FlattenRolesEmails(List<RoleEmailsByPipelineCodeResponse> inputList)
        {
            List<CommonSenderDTO> flattenedList = new List<CommonSenderDTO>();

            foreach (var inputItem in inputList)
            {
                foreach (var kvp in inputItem.RolesEmails)
                {
                    string role = kvp.Key;
                    foreach (var email in kvp.Value)
                    {
                        flattenedList.Add(new CommonSenderDTO
                        {
                            Email = email,
                            Role = role,
                            PipelineCode = inputItem.PipelineCode,
                            JobCode = inputItem.JobCode
                        });
                    }
                }
            }

            return flattenedList;
        }

        public static JArray TransposeRolesData(List<RoleEmailsByPipelineCodeResponse> pipelineDataList)
        {
            // Get all unique role names from the list
            var uniqueRoles = pipelineDataList.SelectMany(data => data.RolesEmails.Keys).Distinct().OrderBy(role => role).ToList();

            // Construct the transposed data structure
            JArray transposedData = new JArray();

            // Add header row
            JObject headerRow = new JObject();
            headerRow.Add("PipelineCode", "PipelineCode");
            headerRow.Add("JobCode", "JobCode");
            foreach (var role in uniqueRoles)
            {
                headerRow.Add(role, role);
            }
            transposedData.Add(headerRow);

            // Add data rows
            foreach (var pipelineData in pipelineDataList)
            {
                JObject rowData = new JObject();
                rowData.Add("PipelineCode", pipelineData.PipelineCode);
                rowData.Add("JobCode", pipelineData.JobCode ?? "");
                foreach (var role in uniqueRoles)
                {
                    if (pipelineData.RolesEmails.ContainsKey(role))
                    {
                        rowData.Add(role, new JArray(pipelineData.RolesEmails[role]));
                    }
                    else
                    {
                        rowData.Add(role, new JArray());
                    }
                }
                transposedData.Add(rowData);
            }

            return transposedData;
        }

        public static DateTime TruncateMilliseconds(DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond), dateTime.Kind);
        }


        /// <summary>
        /// CheckIsValidJsonToken
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckIsValidJsonToken(string? str)
        {
            bool flag = false;
            if (CheckIsValidJsonArray(str) || CheckIsValidJsonObject(str))
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// CheckIsValidJsonObject
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckIsValidJsonObject(string? str)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(str) && str.Trim().StartsWith("{") && str.Trim().EndsWith("}"))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// CheckIsValidJsonArray
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckIsValidJsonArray(string? str)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(str) && str.Trim().StartsWith("[") && str.Trim().EndsWith("]"))
            {
                flag = true;
            }
            return flag;
        }

    }
}
