using MediatR;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.DTO;
using RMT.Notification.Domain.Repositories;
using RMT.Notification.Infrastructure.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RMT.Notification.Application.Constants;
using RMT.Notification.Application.HttpServices;
using Newtonsoft.Json;
using System.Xml.Linq;
using RMT.Notification.Application.Helpers;
using System.Reflection.Emit;
using RMT.Notification.Application.HttpServices.DTO;
using ServiceLayer.Services.EmailService;
using Newtonsoft.Json.Linq;
using ServiceLayer.Services.ConfigurationService;
using ServiceLayer.Services.PushNotificationService;
using System.Xml;
using RMT.Notification.Application.HttpServices.SkillHtppService;
using Azure.Core;
using RMT.Notification.Domain.Entities;
using Microsoft.Extensions.Configuration;
using static RMT.Notification.Application.Constants.Constants;
using Microsoft.VisualBasic;
using Aspose.Email.Clients.Graph;
using static RMT.Notification.Infrastructure.Constants.Constants;
using Aspose.Email.Clients.Activity;
using System.Numerics;
using Azure;
using RMT.Notification.Application.HttpServices.AllocationService;
using Aspose.Email.Tools;
using RMT.Notification.Application.HttpServices.MarketPlaceService;
using System.Security.Principal;
using Microsoft.Extensions.Logging;
using RMT.Notification.Application.HttpServices.ProjectConfigurationService;

namespace RMT.Notification.Application.Handlers.CommandHandlers
{
    public class SendNotificationCommand : IRequest<Boolean>
    {
        public string NotificationType { get; set; }
        public string NotificationKey { get; set; }
        public GetNotificationTemplateResponse NotificationTemplate { get; set; }
        public string meta { get; set; }
    }

    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Boolean>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IProjectRoleHttpService _projectRoleHttpService;
        private readonly IEmailService _emailService;
        private readonly IConfigurationService _configurationService;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly ISkillHttpService _skillHttpService;
        private readonly IConfiguration _configuration;
        private readonly IAllocationHttpService _allocationHttpService;
        private readonly IMarketPlaceHttpService _marketPlaceHttpService;
        private readonly IProjectConfigurationService _projectConfigService;
        private readonly int NoOfDays = N0_OF_DAY;

        private readonly ILogger<SendNotificationCommandHandler> _logger;




        public SendNotificationCommandHandler(INotificationRepository notificationRepository, IProjectRoleHttpService projectRoleHttpService,
            IEmailService emailService, IConfigurationService configurationService, IPushNotificationService pushNotificationService,
            ISkillHttpService skillHttpService, IConfiguration configuration, IAllocationHttpService allocationHttpService,
            IMarketPlaceHttpService marketPlaceHttpService, IProjectConfigurationService projectConfigService, ILogger<SendNotificationCommandHandler> logger)
        {
            _notificationRepository = notificationRepository;
            _projectRoleHttpService = projectRoleHttpService;
            _emailService = emailService;
            _configurationService = configurationService;
            _pushNotificationService = pushNotificationService;
            _skillHttpService = skillHttpService;
            _configuration = configuration;
            _allocationHttpService = allocationHttpService;
            _marketPlaceHttpService = marketPlaceHttpService;
            _projectConfigService = projectConfigService;
            _logger = logger;
        }

        //projectRoles[]
        //supercoach --> employeeEmail
        //Req , response object due date
        //

        public async Task<List<string>> GetRecipientFromRoleAsync(string resepients, string response, Dictionary<string, string> DTokens)
        {
            List<string> results = new List<string>();

            if (!string.IsNullOrEmpty(resepients) && !string.IsNullOrEmpty(response))
            {
                foreach (string resepient in resepients.Split(','))
                {
                    JObject obj = new JObject();

                    if (Helper.CheckIsValidJsonToken(Convert.ToString(response)))
                    {
                        var jToken = JToken.Parse(response);
                        if (jToken is JArray)
                        {
                            JArray jsonArray = JArray.Parse(response);
                            if (Helper.CheckIsValidJsonObject(Convert.ToString(jsonArray[0])))
                            {
                                obj = JObject.Parse(jsonArray[0].ToString());
                            }
                        }
                        else if (jToken is JObject)
                        {
                            obj = JObject.Parse(response);
                        }
                    }

                    // var obj = JObject.Parse(response);
                    if (Constants.Constants.ProjectRoles.Contains(resepient.Trim()))
                    {
                        List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto = new List<PipelineCodeAndRolesDto>();
                        string pipelineCode = null;
                        string jobCode = null;
                        DTokens.TryGetValue("pipelinecode", out pipelineCode);
                        DTokens.TryGetValue("jobcode", out jobCode);
                        pipelineCodeAndRolesDto.Add(new PipelineCodeAndRolesDto
                        {
                            pipelineCode = pipelineCode,
                            jobCode = jobCode
                        });
                        switch (resepient)
                        {
                            case UserRoles.Reviewer:
                                var revierRoles = await _configurationService.GetResourceReviewerEmailsByPipelineCode(pipelineCode, jobCode);
                                if (Helper.CheckIsValidJsonArray(revierRoles))
                                {
                                    var reviewerList = JArray.Parse(revierRoles).Select(e => e["user"]);
                                    if (reviewerList != null)
                                    {
                                        foreach (var item in reviewerList)
                                        {
                                            results.Add(Convert.ToString(item));
                                        }
                                    }
                                }
                                break;
                            case UserRoles.ResourceRequestor:
                                var requestorRoles = await _configurationService.GetResourceRequestorEmailsByPipelineCode(pipelineCode, jobCode);
                                if (Helper.CheckIsValidJsonArray(requestorRoles))
                                {
                                    var resourceRequestorList = JArray.Parse(requestorRoles).Select(e => e["user"]);
                                    if (resourceRequestorList != null)
                                    {
                                        foreach (var item in resourceRequestorList)
                                        {
                                            results.Add(Convert.ToString(item));
                                        }
                                    }
                                }
                                break;
                            default:
                                var allProjectRoles = await _configurationService.GetRolesEmailByPipelineCodesAndRoles(pipelineCodeAndRolesDto);
                                var currentProjectRoles = allProjectRoles.FirstOrDefault();
                                List<string> rolesEmail = new List<string>();
                                currentProjectRoles.RolesEmails.TryGetValue(resepient.Trim(), out rolesEmail);
                                if (rolesEmail != null && rolesEmail.Count > 0)
                                {
                                    foreach (var item in rolesEmail)
                                    {
                                        results.Add(item);
                                    }
                                }

                                break;
                        }
                        pipelineCodeAndRolesDto.Add(new PipelineCodeAndRolesDto
                        {
                            pipelineCode = pipelineCode,
                            jobCode = jobCode
                        });

                    }
                    else if (resepient.Trim().Equals("newadditionalels", StringComparison.OrdinalIgnoreCase))
                    {
                        string newAdditionalEl = DTokens["newadditionalels"];
                        string[] additionalEls = newAdditionalEl.Split(",");
                        foreach (var email in additionalEls)
                        {
                            results.Add(email);
                        }
                    }
                    else if (resepient.Trim().Equals("supercoach_email", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmail = string.Empty;
                        DTokens.ContainsKey("empemail");
                        DTokens.TryGetValue("empemail", out employeeEmail);
                        var employeeInfo = await _configurationService.GetUsersByUsersEmail(new List<string>() { employeeEmail });
                        if (Helper.CheckIsValidJsonArray(employeeInfo))
                        {
                            var empInfo = JObject.Parse(Convert.ToString(JArray.Parse(employeeInfo).FirstOrDefault()));
                            results.Add(empInfo["supercoach_name"]?.ToString());
                        }
                    }
                    else if (resepient.Trim().Equals("sender_for_notification", StringComparison.OrdinalIgnoreCase))
                    {
                        string newRecievers = DTokens["sender_for_notification"];
                        string[] newRecieversList = newRecievers.Split(",");
                        foreach (var email in newRecieversList)
                        {
                            results.Add(email);
                        }
                    }
                    else if (resepient.Trim().Equals("allocation_supercoach_email", StringComparison.OrdinalIgnoreCase))
                    {
                        string newRecievers = DTokens["allocation_supercoach_email"];
                        string[] newRecieversList = newRecievers.Split(",");
                        foreach (var email in newRecieversList)
                        {
                            results.Add(email);
                        }
                    }
                    else if (resepient.Trim().Equals("skill_supercoach_email", StringComparison.OrdinalIgnoreCase))
                    {
                        string newRecievers = DTokens["skill_supercoach_email"];
                        string[] newRecieversList = newRecievers.Split(",");
                        foreach (var email in newRecieversList)
                        {
                            results.Add(email);
                        }
                    }
                    else if (resepient.Trim().Equals("newadditionaldelegates", StringComparison.OrdinalIgnoreCase))
                    {
                        string newAdditionalDelegate = DTokens["newadditionaldelegates"];
                        string[] newAdditionalDelegates = newAdditionalDelegate.Split(",");
                        foreach (var email in newAdditionalDelegates)
                        {
                            results.Add(email);
                        }
                    }
                    else if (resepient.Trim().Equals("previousrolesemails", StringComparison.OrdinalIgnoreCase))
                    {
                        if (DTokens.ContainsKey("previousrolesemails"))
                        {
                            if (!string.IsNullOrEmpty(DTokens["previousrolesemails"]))
                            {
                                var rrList1 = JArray.Parse(DTokens["previousrolesemails"]);
                                if (rrList1 != null)
                                {
                                    foreach (var item in rrList1)
                                    {
                                        results.Add(Convert.ToString(item));
                                    }
                                }
                            }
                        }
                    }
                    else if (resepient.Trim().Equals("resource_requestor_allocation_wf", StringComparison.OrdinalIgnoreCase))
                    {
                        string pipelineCode = string.Empty;
                        DTokens.TryGetValue("pipelinecode", out pipelineCode);
                        string jobCode = string.Empty;
                        DTokens.TryGetValue("jobcode", out jobCode);
                        string workflowCreatedBy = string.Empty;
                        DTokens.TryGetValue("created_by", out workflowCreatedBy);
                        string resourceRequestors = await _configurationService.GetRequestorEmailsForAllocationWorkflow(pipelineCode, jobCode, workflowCreatedBy);

                        if (Helper.CheckIsValidJsonArray(resourceRequestors))
                        {
                            var rrList1 = JArray.Parse(resourceRequestors).Select(e => e["user"]);
                            var rrList2 = JArray.Parse(resourceRequestors).Where(x => !string.IsNullOrEmpty(Convert.ToString(x["delegateEmail"]))).Select(e => e["delegateEmail"]);
                            if (rrList1 != null)
                            {
                                foreach (var item in rrList1)
                                {
                                    results.Add(Convert.ToString(item));
                                }
                            }
                            if (rrList2 != null)
                            {
                                foreach (var item in rrList2)
                                {
                                    results.Add(Convert.ToString(item));
                                }
                            }
                        }
                        //JToken tasksList = null;
                        //JToken taskListReviewer = null;
                        //if (obj.ContainsKey("result"))
                        //{
                        //    tasksList = JArray.Parse(Convert.ToString(obj["result"]))
                        //                           .Where(task => Convert.ToString(task["status"]).Equals("pending", StringComparison.OrdinalIgnoreCase)
                        //                            && Convert.ToString(task["title"]).Equals("Employee Allocation Approval Task For Employee", StringComparison.OrdinalIgnoreCase))
                        //                           .FirstOrDefault();
                        //    taskListReviewer = JArray.Parse(Convert.ToString(obj["result"]))
                        //                                            .Where(task => Convert.ToString(task["title"]).Equals("Employee Allocation Approval Task For Reviewer", StringComparison.OrdinalIgnoreCase))
                        //                                            .FirstOrDefault();
                        //}
                        //string employeeEmail = null;
                        //DTokens.TryGetValue("empEmail", out employeeEmail);
                        //if (tasksList != null)
                        //{
                        //    string taskString = Convert.ToString(tasksList);
                        //    var taskJson = JObject.Parse(taskString);
                        //    string dueDate = Convert.ToString(taskJson["due_date"]);
                        //    DTokens.Add("due_date", dueDate);
                        //}
                        //if (taskListReviewer != null)
                        //{
                        //    var taskListReviewerJson = JObject.Parse(Convert.ToString(taskListReviewer));
                        //    string status = null;
                        //    string rejectionComments = null;
                        //    status = Convert.ToString(taskListReviewerJson.GetValue("status"));
                        //    rejectionComments = Convert.ToString(taskListReviewerJson.GetValue("comment"));
                        //    if (status.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                        //    {
                        //        DTokens.Add("ReasonForRejectionProvidedByReviewer", rejectionComments);
                        //    }
                        //}
                    }
                    else if (resepient.Trim().Equals("employee_name_roll_over_draft", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmailsList = null;
                        DTokens.TryGetValue("employeeemailindraft", out employeeEmailsList);
                        if (Helper.CheckIsValidJsonArray(Convert.ToString(employeeEmailsList)))
                        {
                            var employeeEmails = JArray.Parse(employeeEmailsList);
                            if (employeeEmails != null)
                            {
                                foreach (var item in employeeEmails)
                                {
                                    results.Add(Convert.ToString(item));
                                }
                            }
                        }
                    }
                    else if (resepient.Trim().Equals("employee_name_suspend", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmailsList = null;
                        DTokens.TryGetValue("employeereleasedduetoprojectsuspend", out employeeEmailsList);
                        if (Helper.CheckIsValidJsonArray(Convert.ToString(employeeEmailsList)))
                        {
                            var employeeEmails = JArray.Parse(employeeEmailsList);
                            if (employeeEmails != null)
                            {
                                foreach (var item in employeeEmails)
                                {
                                    results.Add(Convert.ToString(item));
                                }
                            }
                        }
                    }
                    else if (resepient.Trim().Equals("employee_jobcode_change", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmailsList = null;
                        DTokens.TryGetValue("employee_jobcode_change", out employeeEmailsList);
                        if (Helper.CheckIsValidJsonArray(Convert.ToString(employeeEmailsList)))
                        {
                            var employeeEmails = JArray.Parse(employeeEmailsList);
                            if (employeeEmails != null)
                            {
                                foreach (var item in employeeEmails)
                                {
                                    results.Add(Convert.ToString(item));
                                }
                            }
                        }
                    }
                    else if (resepient.Trim().Equals("employee_name_roll_over_terminated", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmailsList = null;
                        DTokens.TryGetValue("employeeemailterminated", out employeeEmailsList);
                        if (Helper.CheckIsValidJsonArray(Convert.ToString(employeeEmailsList)))
                        {
                            var employeeEmails = JArray.Parse(employeeEmailsList);
                            if (employeeEmails != null)
                            {
                                foreach (var item in employeeEmails)
                                {
                                    results.Add(Convert.ToString(item));
                                }
                            }
                        }
                    }
                    else if (resepient.Trim().Equals("employee_name_allocation_wf", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmail = null;
                        DTokens.TryGetValue("empemail", out employeeEmail);
                        results.Add(employeeEmail);
                    }
                    else if (resepient.Trim().Equals("employee_name_release_resource", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmail = null;
                        DTokens.TryGetValue("empemail", out employeeEmail);
                        results.Add(employeeEmail);
                    }
                    else if (resepient.Trim().Equals("employee_name_assign_as_admin", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmail = null;
                        DTokens.TryGetValue("email_id", out employeeEmail);
                        results.Add(employeeEmail);
                    }
                    else if (resepient.Trim().Equals("userskillemail", StringComparison.OrdinalIgnoreCase))
                    {
                        string employeeEmail = null;
                        DTokens.TryGetValue("email", out employeeEmail);
                        results.Add(employeeEmail);
                        if (obj.ContainsKey("result"))
                        {
                            var result = JArray.Parse(Convert.ToString(obj["result"]))
                                                   .Where(task => Convert.ToString(task["title"]).Equals("User Skill Assessment Task For Supercoach after Employee added or updated a skill", StringComparison.OrdinalIgnoreCase))
                                                   .FirstOrDefault();
                            var comment = JArray.Parse(Convert.ToString(result["comment"])).OrderByDescending(o => Convert.ToString(o["created_at"]))
                                            .FirstOrDefault();

                            if (comment != null)
                            {
                                var superCoachComment = JObject.Parse(Convert.ToString(comment));
                                string rejectionRemark = Convert.ToString(superCoachComment.GetValue("comment"));
                                DTokens.Add("rejection_remark", rejectionRemark);

                            }
                        }

                    }

                    if (obj.ContainsKey("result"))
                    {
                        if (Helper.CheckIsValidJsonArray(Convert.ToString(obj["result"])))
                        {
                            var employeeTasksList = JArray.Parse(Convert.ToString(obj["result"]))
                                                   .Where(task => Convert.ToString(task["title"]).Equals("Employee Allocation Approval Task For Employee", StringComparison.OrdinalIgnoreCase))
                                                   .FirstOrDefault();
                            if (employeeTasksList != null && Helper.CheckIsValidJsonObject(Convert.ToString(employeeTasksList)))
                            {
                                var employeeTasksListJson = JObject.Parse(Convert.ToString(employeeTasksList));
                                string status = null;
                                string rejectionComments = null;
                                status = Convert.ToString(employeeTasksListJson.GetValue("status"));
                                rejectionComments = Convert.ToString(employeeTasksListJson.GetValue("comment"));
                                if (status.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!DTokens.ContainsKey("ReasonForRejectionProvidedByEmployee"))
                                    {
                                        DTokens.Add("ReasonForRejectionProvidedByEmployee", rejectionComments);
                                    }
                                }
                                if (status.Equals("pending", StringComparison.OrdinalIgnoreCase) || status.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!DTokens.ContainsKey("due_date"))
                                    {
                                        try
                                        {
                                            var due_date_value = DateOnly.FromDateTime(Convert.ToDateTime((DateTime)employeeTasksListJson["due_date"])).ToString("dd-MM-yyyy");
                                            DTokens.Add("due_date", due_date_value);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                        if (Helper.CheckIsValidJsonArray(Convert.ToString(obj["result"])))
                        {
                            var taskListReviewer = JArray.Parse(Convert.ToString(obj["result"]))
                                                                    .Where(task => Convert.ToString(task["title"]).Equals("Employee Allocation Approval Task For Reviewer", StringComparison.OrdinalIgnoreCase) ||
                                                                     Convert.ToString(task["title"]).Equals("Employee Allocation Task For Reviewer After Resource Requestor Rejection", StringComparison.OrdinalIgnoreCase)
                                                                    )
                                                                    .FirstOrDefault();
                            if (taskListReviewer != null && Helper.CheckIsValidJsonObject(Convert.ToString(taskListReviewer)))
                            {
                                var taskListReviewerJson = JObject.Parse(Convert.ToString(taskListReviewer));
                                string status = null;
                                string rejectionComments = null;
                                status = Convert.ToString(taskListReviewerJson.GetValue("status"));
                                rejectionComments = Convert.ToString(taskListReviewerJson.GetValue("comment"));
                                if (status.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                                {
                                    if (!DTokens.ContainsKey("ReasonForRejectionProvidedByReviewer"))
                                    {
                                        DTokens.Add("ReasonForRejectionProvidedByReviewer", rejectionComments);

                                    }
                                }
                            }
                        }
                    }
                    if (obj.ContainsKey("task_list"))
                    {
                        if (Helper.CheckIsValidJsonArray(Convert.ToString(obj["task_list"])))
                        {
                            var employeeTasksList = JArray.Parse(Convert.ToString(obj["task_list"]))
                                                   .Where(task => Convert.ToString(task["title"]).Equals("Employee Allocation Approval Task For Employee", StringComparison.OrdinalIgnoreCase))
                                                   .FirstOrDefault();
                            if (employeeTasksList != null)
                            {
                                if (Helper.CheckIsValidJsonObject(Convert.ToString(employeeTasksList)))
                                {
                                    var employeeTasksListJson = JObject.Parse(Convert.ToString(employeeTasksList));
                                    string status = null;
                                    string rejectionComments = null;
                                    status = Convert.ToString(employeeTasksListJson.GetValue("status"));
                                    rejectionComments = Convert.ToString(employeeTasksListJson.GetValue("comment"));
                                    if (status.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (!DTokens.ContainsKey("ReasonForRejectionProvidedByEmployee"))
                                        {
                                            DTokens.Add("ReasonForRejectionProvidedByEmployee", rejectionComments);
                                        }
                                    }
                                    if (status.Equals("pending", StringComparison.OrdinalIgnoreCase) || status.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (!DTokens.ContainsKey("due_date"))
                                        {
                                            try
                                            {
                                                //var due_date_value = Convert.ToString(DateOnly.FromDateTime((DateTime)employeeTasksListJson["due_date"])).ToString("dd-MM-yyyy"));
                                                var due_date_value = DateOnly.FromDateTime(Convert.ToDateTime((DateTime)employeeTasksListJson["due_date"])).ToString("dd-MM-yyyy");
                                                DTokens.Add("due_date", due_date_value);
                                            }
                                            catch { }
                                        }
                                    }
                                }
                            }
                        }

                        if (Helper.CheckIsValidJsonArray(Convert.ToString(obj["task_list"])))
                        {
                            var taskListReviewer = JArray.Parse(Convert.ToString(obj["task_list"]))
                                                                    .Where(task => Convert.ToString(task["title"]).Equals("Employee Allocation Approval Task For Reviewer", StringComparison.OrdinalIgnoreCase))
                                                                    .FirstOrDefault();
                            if (taskListReviewer != null)
                            {
                                if (Helper.CheckIsValidJsonObject(Convert.ToString(taskListReviewer)))
                                {
                                    var taskListReviewerJson = JObject.Parse(Convert.ToString(taskListReviewer));
                                    string status = null;
                                    string rejectionComments = null;
                                    status = Convert.ToString(taskListReviewerJson.GetValue("status"));
                                    rejectionComments = Convert.ToString(taskListReviewerJson.GetValue("comment"));
                                    if (status.Equals("rejected", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (!DTokens.ContainsKey("ReasonForRejectionProvidedByReviewer"))
                                        {
                                            DTokens.Add("ReasonForRejectionProvidedByReviewer", rejectionComments);

                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (resepient.Trim().Equals("employee_name_allocation", StringComparison.OrdinalIgnoreCase))
                    {
                        string employee = string.Empty;
                        DTokens.TryGetValue("empEmail", out employee);
                        //try to get empemail with lowercase key name
                        if (string.IsNullOrEmpty(employee))
                            DTokens.TryGetValue("empemail", out employee);
                        results.Add(employee);
                    }
                }
            }
            return results.Distinct().ToList();
        }

        private Dictionary<string, string> TransformResponse(string response, Dictionary<string, string> DToken)
        {
            if (response == null || Helper.CheckIsValidJsonObject(response) == false)
            {
                return DToken;
            }
            JObject responseObject = new JObject();
            if (Helper.CheckIsValidJsonToken(Convert.ToString(response)))
            {
                var jToken = JToken.Parse(response);
                if (jToken is JArray)
                {
                    JArray jsonArray = JArray.Parse(response);
                    if (Helper.CheckIsValidJsonObject(Convert.ToString(jsonArray[0])))
                    {
                        responseObject = JObject.Parse(jsonArray[0].ToString());
                    }
                }
                else if (jToken is JObject)
                {
                    responseObject = JObject.Parse(response);
                }
            }

            if (responseObject.ContainsKey("workflowResult"))
            {
                string workflowResultString = Convert.ToString(responseObject.GetValue("workflowResult"));
                DToken = FillToken(workflowResultString, DToken);
                if (Helper.CheckIsValidJsonObject(workflowResultString))
                {
                    var workflowResultJson = JObject.Parse(workflowResultString);
                    string entityString = Convert.ToString(workflowResultJson.GetValue("entity_meta_data"));
                    DToken = FillToken(entityString, DToken);
                }
            }
            if (responseObject.ContainsKey("entity_meta_data"))
            {
                string workflowEntityMetaData = Convert.ToString(responseObject.GetValue("entity_meta_data"));
                DToken = FillToken(workflowEntityMetaData, DToken);
            }
            if (responseObject.ContainsKey("projectRolloverRequest"))
            {
                string projectRolloverRequest = Convert.ToString(responseObject.GetValue("projectRolloverRequest"));
                DToken = FillToken(projectRolloverRequest, DToken);
            }
            else if (responseObject is JObject)
            {
                string entityString = Convert.ToString(responseObject);
                DToken = FillToken(entityString, DToken);
            }
            return DToken;
        }

        public async Task<List<Task>> AllocationSummary(GetNotificationTemplateResponse template)
        {
            string notificationRegexPrefix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexPrefix).Value;
            string notificationRegexSuffix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexsuffix).Value;
            int adjustDays = Convert.ToInt32(_configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.AllocationSummaryAdjustDays).Value);
            DateTime createdAt = DateTime.Now.AddDays(adjustDays);
            DateTime modifiedAt = DateTime.Now.AddDays(adjustDays);

            var tasks = new List<Task>();
            var allocations = await _allocationHttpService.GetAllocationByDate(createdAt, modifiedAt);
            var requistions = await _allocationHttpService.GetRequistionByDate(createdAt, modifiedAt);
            var workflow = await _configurationService.GetWorkflowByModuleOutcomeAndUpdateDate(EMPLOYEE_ALLOCATION_WF, WF_STATUS_CLOSE, DateTime.UtcNow);

            var marketPlaceList = await _marketPlaceHttpService.GetMarketPlaceDetailsIntrest();
            string notificationPlaceholderRegex = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationTokenPatternRegex).Value;
            List<string> keys = Helper.ExtractKeysFromMsg(template.template, notificationPlaceholderRegex);

            // Get Roles and mail
            List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto = new List<PipelineCodeAndRolesDto>();

            List<PipelineCodeAndRolesDto> distinctJobs = new List<PipelineCodeAndRolesDto>();
            if (allocations != null && allocations.Count > 0)
            {
                distinctJobs = allocations
                                 .Select(m => new PipelineCodeAndRolesDto
                                 { pipelineCode = m.Requisition.PipelineCode, jobCode = m.Requisition.JobCode }).Distinct().ToList();
            }
            if (workflow != null && workflow.Count > 0)
            {
                var workflowJobs = workflow.Select(e => new PipelineCodeAndRolesDto
                {
                    pipelineCode = JsonConvert.DeserializeObject<ResourceAllocationDetailsResponseForWorkflowMeta>(e.entity_meta_data.ToString()).PipelineCode,
                    jobCode = JsonConvert.DeserializeObject<ResourceAllocationDetailsResponseForWorkflowMeta>(e.entity_meta_data.ToString()).JobCode
                });
                distinctJobs.AddRange(workflowJobs);
            }
            if (requistions != null && requistions.Count > 0)
            {
                var requsitionJobs = requistions
                                     .Select(m => new PipelineCodeAndRolesDto
                                     { pipelineCode = m.PipelineCode, jobCode = m.JobCode }).Distinct().ToList();

                distinctJobs.AddRange(requsitionJobs);
            }

            if (marketPlaceList != null && marketPlaceList.Count > 0)
            {
                var marketPlaceJobs = marketPlaceList
                              .Select(m => new PipelineCodeAndRolesDto
                              { pipelineCode = m.PipelineCode, jobCode = m.JobCode }).Distinct().ToList();

                distinctJobs.AddRange(marketPlaceJobs);
            }

            distinctJobs = distinctJobs
                    .DistinctBy(x => new { x.pipelineCode, x.jobCode })
                    .ToList();

            List<RoleEmailsByPipelineCodeResponse> allProjectRoles = new List<RoleEmailsByPipelineCodeResponse>();
            /********** Loginformation  **/
            string result = string.Join("\n", distinctJobs.Select(x =>
                $"PipelineCode: {x.pipelineCode}, JobCode: {x.jobCode}, Roles: [{string.Join(", ", x.roles ?? new List<string>())}]"
                ));
            _logger.LogInformation("AllocationSummary>>" + result);
            allProjectRoles = await _configurationService.GetRolesEmailByPipelineCodesAndRoles(distinctJobs);


            List<RoleEmailsByPipelineCodeResponse> requestorsList = new();
            foreach (var item in pipelineCodeAndRolesDto)
            {
                var RequestorInfo = await _configurationService.GetResourceRequestorEmailsByPipelineCode(item.pipelineCode, item.jobCode);
                List<string> requestors = new List<string>();
                List<string> resourceRequestorList = new List<string>();
                if (Helper.CheckIsValidJsonArray(RequestorInfo))
                {
                    resourceRequestorList = JArray.Parse(RequestorInfo).Select(e => e["user"].ToString()).ToList();
                }
                requestorsList.Add(new RoleEmailsByPipelineCodeResponse
                {
                    PipelineCode = item.pipelineCode,
                    JobCode = item.jobCode,
                    RolesEmails = new Dictionary<string, List<string>>()
                    {
                        {UserRoles.ResourceRequestor , resourceRequestorList }
                    }
                });
            }

            allProjectRoles.AddRange(requestorsList);

            List<CommonSenderDTO> flattenedData = Helper.FlattenRolesEmails(allProjectRoles);
            //checking for configuration of Reviewer
            var configOfReviewerForAllocationReview = await _projectConfigService.GetProjectConfiguration("default", "Resource_allocation_review");
            var configValue = configOfReviewerForAllocationReview?.FirstOrDefault()?.AttributeValue;
            if (configValue == "-1")
            {
                flattenedData = flattenedData
                    .Where(x => !string.Equals(x.Role, "Reviewer", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            var groupedData = flattenedData.GroupBy(a => a.Email);
            var emailNewNotificationPayload = new List<EmailMetaPayloadDTO>();

            foreach (var group in groupedData)
            {
                // For each Email
                NotificationDTO EmailContent = new NotificationDTO();
                List<string> pipelineJobs = new List<string>();

                Dictionary<string, string> DTokens = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var item in group)
                {
                    if (ALLOCATION_SUMMARY_SENDER.Contains(item.Role))
                    {
                        if (!pipelineJobs.Contains(string.Format("{0};#{1}", item.PipelineCode, item.JobCode)))
                        {
                            pipelineJobs.Add(string.Format("{0};#{1}", item.PipelineCode, item.JobCode));
                        }
                    }
                }

                // filter list which this user has roles 
                var filterdAllocation = allocations?.Where(item => pipelineJobs.Contains(string.Format("{0};#{1}", item.Requisition.PipelineCode, item.Requisition.JobCode))).ToList();

                var closedAllocation = workflow?.Where(item =>
                                pipelineJobs.Contains(string.Format("{0};#{1}", JsonConvert.DeserializeObject<ResourceAllocationDetailsResponseForWorkflowMeta>(item.entity_meta_data.ToString()).PipelineCode,
                                                                                JsonConvert.DeserializeObject<ResourceAllocationDetailsResponseForWorkflowMeta>(item.entity_meta_data.ToString()).JobCode)))
                                .Select(e => JsonConvert.DeserializeObject<ResourceAllocationDetailsResponseForWorkflowMeta>(e.entity_meta_data.ToString())).ToList();

                var filteredRequistion = requistions?.Where(item => pipelineJobs.Contains(string.Format("{0};#{1}", item.PipelineCode, item.JobCode))).ToList();

                var filterdMarketPlace = marketPlaceList?.Where(item => pipelineJobs.Contains(string.Format("{0};#{1}", item.PipelineCode, item.JobCode))).ToList();

                string summary = String.Empty;
                string requsitionSummary = String.Empty;
                string marketPlaceSummary = String.Empty;

                if ((filterdAllocation != null && filterdAllocation.Count > 0) || (closedAllocation != null && closedAllocation.Count > 0))
                {
                    summary = ParseAllocationSummary(filterdAllocation, closedAllocation);
                    DTokens.Add("AllocationSummary", summary);
                }

                if (filteredRequistion != null && filteredRequistion.Count > 0)
                {
                    requsitionSummary = ParseRequistionSummary(filteredRequistion);
                    DTokens.Add("RequstionSummary", requsitionSummary);
                }
                // Marketplace content 
                if (filterdMarketPlace != null && filterdMarketPlace.Count > 0)
                {
                    marketPlaceSummary = ParseMarketPlaceSummary(filterdMarketPlace);
                    DTokens.Add("MarketPlaceSummary", marketPlaceSummary);
                }

                string baseUrl = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.BaseUrl).Value;
                if (!DTokens.ContainsKey("BaseUrl")) DTokens.Add("BaseUrl", baseUrl);

                //To  
                EmailContent.To = group.Key;

                // Subject 
                EmailContent.Subject = ALLOCATION_SUMMARY_SUBJECT;
                // Body with EMP ID 
                string emailBodyWithEmpId = Helper.ComposeMsg(keys, template.template, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);
                //Body
                EmailContent.Body = await Helper.GetEmailBodyWithEmailAddress(emailBodyWithEmpId, _configurationService);

                if (!(string.IsNullOrEmpty(summary) && string.IsNullOrEmpty(requsitionSummary) && string.IsNullOrEmpty(marketPlaceSummary)))
                {
                    emailNewNotificationPayload.Add(new EmailMetaPayloadDTO
                    {
                        body = EmailContent.Body,
                        to = Helper.GetEmailIdFromMID(EmailContent.To.Split(",").ToList()),
                        cc = new string[] { },
                        meta = { },
                        subject = EmailContent.Subject
                    });
                }

            }

            /********** Loginformation  **/
            string result01 = string.Join("\n", emailNewNotificationPayload.Select(x =>
                    $"To: [{string.Join(", ", x.to ?? Array.Empty<string>())}], CC: [{string.Join(", ", x.cc ?? Array.Empty<string>())}], Subject: {x.subject}"
                ));
            _logger.LogInformation("AllocationSummary>> Send Email>>" + result01);

            tasks.Add(_emailService.SendEmail(emailNewNotificationPayload));
            return tasks;
        }

        public async Task<Boolean> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--NOTIFICATION_SERVICE--Handle--{0}", JsonConvert.SerializeObject(request));

            string notificationRegexPrefix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexPrefix).Value;
            string notificationRegexSuffix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexsuffix).Value;
            string SummaryNotificationItemsToRunStr = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.SummaryNotificationItemsToRun).Value;

            List<string> SummaryNotificationItemsToRun = SummaryNotificationItemsToRunStr.Split(';').ToList();

            string notificationType = request.NotificationType;
            InitNotificationDTO metaJson = JsonConvert.DeserializeObject<InitNotificationDTO>(request.meta);
            Dictionary<string, string> DTokens = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            NotificationDTO EmailContent = new NotificationDTO();

            //var inspect = await PendingAllocationAndSkillsNotificationTask(request.NotificationTemplate, metaJson);

            var tasks = new List<Task>();
            if (metaJson != null)
            {
                DTokens = TransformResponse(metaJson.response_payload, DTokens);
            }
            if (request.NotificationKey == ALLOCATION_SUMMARY_NOTIFICATION && SummaryNotificationItemsToRun.Contains(ALLOCATION_SUMMARY_NOTIFICATION))
            {
                var summaryTask = AllocationSummary(request.NotificationTemplate);
                tasks.Add(summaryTask);
            }
            else if (request.NotificationKey == CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE && SummaryNotificationItemsToRun.Contains(CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE))
            {
                var summaryTask = MarketPlaceSubscription(request.NotificationTemplate);
                tasks.Add(summaryTask);
            }
            else if (request.NotificationKey == RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL && SummaryNotificationItemsToRun.Contains(RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL))
            {
                var summaryTask = PendingAllocationAndSkillsNotificationTask(request.NotificationTemplate);
                tasks.Add(summaryTask);
            }
            else if (request.NotificationKey == ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR && SummaryNotificationItemsToRun.Contains(ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR))
            {
                var summaryTask = AdditionOfNewProjectNotificationToResourceRequestor(request.NotificationTemplate);
                tasks.Add(summaryTask);
            }
            else
            {
                //To store all the email notifications
                var emailNewNotificationPayload = new List<EmailMetaPayloadDTO>();
                var pushNewNotificationPayload = new List<PostNewPushNotificationDTO>();

                GetNotificationTemplateResponse template = request.NotificationTemplate;


                if (metaJson != null)
                {
                    DTokens = FillToken(metaJson.response_payload, DTokens);
                    DTokens = FillToken(metaJson.request_payload, DTokens);
                }

                string notificationPlaceholderRegex = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationTokenPatternRegex).Value;

                List<string> keys = Helper.ExtractKeysFromMsg(template.template, notificationPlaceholderRegex);


                DTokens = await DictionaryGeneration(keys, DTokens, metaJson);


                string pipelineCode = string.Empty;
                string jobCode = string.Empty;
                //   DTokens.TryGetValue("PipelineCode",out pipelineCode);
                //   DTokens.TryGetValue("JobCode",out jobCode);
                if (metaJson != null)
                {
                    EmailContent.To = $"{String.Join(',', await GetRecipientFromRoleAsync(template.to, metaJson.response_payload, DTokens))}";
                    EmailContent.CC = $"{String.Join(',', await GetRecipientFromRoleAsync(template.cc, metaJson.response_payload, DTokens))}";
                }
                //date-time-change
                DTokens = Helper.ConvertDateTimeKeysToDateOnly(DTokens);


                EmailContent.From = "";
                //Subject with EMP ID
                string emailSubjectWithEmpId = Helper.ComposeMsg(keys, template.subject, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);
                //Subject
                EmailContent.Subject = await Helper.GetEmailBodyWithEmailAddress(emailSubjectWithEmpId, _configurationService);

                // Body with EMP ID 
                string emailBodyWithEmpId = Helper.ComposeMsg(keys, template.template, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);
                //Body
                EmailContent.Body = await Helper.GetEmailBodyWithEmailAddress(emailBodyWithEmpId, _configurationService);

                if (notificationType.Equals(Constants.Constants.EmailType, StringComparison.OrdinalIgnoreCase))
                {
                    //Send Email
                    emailNewNotificationPayload.Add(new EmailMetaPayloadDTO
                    {
                        body = EmailContent.Body,
                        to = Helper.GetEmailIdFromMID(EmailContent.To.Split(",").ToList()),
                        cc = Helper.GetEmailIdFromMID(EmailContent.CC.Split(",").ToList()),
                        meta = { },
                        subject = EmailContent.Subject
                    });
                    tasks.Add(_emailService.SendEmail(emailNewNotificationPayload));
                }
                else if (notificationType.Equals(Constants.Constants.PushType, StringComparison.OrdinalIgnoreCase))
                {
                    var reqPay = new PostNewPushNotificationDTO
                    {
                        type = request.NotificationKey,
                        message = EmailContent.Body,
                        meta = { },
                        users = EmailContent.To.Split(","),
                        notification_template_id = template.Id
                    };
                    //Send Push
                    pushNewNotificationPayload.Add(reqPay);
                    tasks.Add(PushNotification(reqPay, DTokens));
                    //tasks.Add(_pushNotificationService.PostNewPushNotification(pushNewNotificationPayload, metaJson.token));
                }
                _logger.LogInformation("--NOTIFICATION_SERVICE--emailNewNotificationPayload--{0}", JsonConvert.SerializeObject(emailNewNotificationPayload));

            }

            //To store all the push notifications
            // var pushNewNotificationPayload = new List<PostNewPushNotificationDTO>();

            await Task.WhenAll(tasks);
            return true;
        }

        public static Dictionary<string, string> FillToken(string meta, Dictionary<string, string> DTokens)
        {
            if (Helper.CheckIsValidJsonToken(meta))
            {
                var jToken = JToken.Parse(meta);
                JObject obj = new JObject();
                if (jToken is JArray)
                {
                    JArray jsonArray = JArray.Parse(meta);
                    string jsonStr = Convert.ToString(jsonArray[0]);
                    if (Helper.CheckIsValidJsonObject(jsonStr))
                    {
                        obj = JObject.Parse(jsonStr);
                    }
                }
                else if (jToken is JObject)
                {
                    obj = JObject.Parse(meta);
                }
                //obj = JObject.Parse(meta);
                foreach (var property in obj.Properties())
                {
                    if (!DTokens.ContainsKey(property.Name.ToLower().Trim()))
                    {
                        DTokens.Add(property.Name.ToLower().Trim(), Convert.ToString(property.Value));
                    }
                }
            }
            return DTokens;
        }

        //public async Task<List<UserNotification>> PostNewNotification(string[] users, PostNewNotificationRequestDTO request)

        private async Task<List<UserNotificationsResponse>> PushNotification(PostNewPushNotificationDTO payload, Dictionary<string, string> DTokens)
        {
            string[] request = { payload.type };
            var template = await _notificationRepository.GetNotificationTemplate(request);
            string link = string.Empty;
            if (template != null)
            {
                var pushTemplate = template.Where(t => t.notification_type.ToLower().Trim()
                    == Constants.Constants.PushType.ToLower().Trim()).FirstOrDefault();
                if (pushTemplate != null && pushTemplate.link != null)
                {
                    link = pushTemplate.link.Link;
                }
                if (link != "")
                {
                    string notificationRegexPrefix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexPrefix).Value;
                    string notificationRegexSuffix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexsuffix).Value;
                    string notificationPlaceholderRegex = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationTokenPatternRegex).Value;

                    List<string> keys = Helper.ExtractKeysFromMsg(link, notificationPlaceholderRegex);
                    link = Helper.ComposeMsg(keys, link, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);

                }
            }
            PostNewNotificationRequestDTO req = new PostNewNotificationRequestDTO
            {
                createdBy = "System@gt.com",
                createdDate = DateTime.Now,
                link = link,
                message = payload.message,
                meta = payload.meta,
                notification_template_id = payload.notification_template_id,
                type = payload.type
            };
            var notificationsAdded = await _notificationRepository.PostNewNotification(payload.users, req);
            var response = new List<UserNotificationsResponse>();
            foreach (var item in notificationsAdded)
            {
                response.Add(new UserNotificationsResponse
                {
                    id = item.id,
                    email = item.email,
                    employee_id = item.employee_id,
                    createdBy = item.createdBy,
                    createdDate = item.createdDate,
                    isRead = item.isRead,
                    notification_id = item.notification_id,
                    message = item.notification.message,
                    meta = item.notification.meta,
                    type = item.notification.type,
                    link = item.notification.link,
                });
            }
            return response;
        }

        private async Task<List<Task>> MarketPlaceSubscription(GetNotificationTemplateResponse template)
        {
            string notificationRegexPrefix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexPrefix).Value;
            string notificationRegexSuffix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexsuffix).Value;
            int adjustDays = Convert.ToInt32(_configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.MarketPlaceSubscriptionAdjustDays).Value);

            Dictionary<string, string> DTokens = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(adjustDays));
            var task = new List<Task>();
            List<MarketPlaceProjectDetailDTO> marketplaceProjectList = await _configurationService.GetMarketPlaceProjectListByPublishDate(currentDate);

            List<NotificationSubscription> notificationSubscriptions = await _notificationRepository.GetNotificationSubscriptionByModuleAndSubscription("MarketPlace", "MarketPlaceListing");

            var emailNewNotificationPayload = new List<EmailMetaPayloadDTO>();

            string notificationPlaceholderRegex = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationTokenPatternRegex).Value;

            List<string> keys = Helper.ExtractKeysFromMsg(template.template, notificationPlaceholderRegex);

            if (marketplaceProjectList != null && marketplaceProjectList.Count > 0)
            {
                string summary = Helper.ParseMarketplaceSubscriptionSummary(marketplaceProjectList);
                DTokens.Add("projectlistinginmarketplacenotificationtoemployee", summary);

                string baseUrl = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.BaseUrl).Value;
                if (!DTokens.ContainsKey("BaseUrl")) DTokens.Add("BaseUrl", baseUrl);

                foreach (var item in notificationSubscriptions)
                {
                    NotificationDTO EmailContent = new NotificationDTO();
                    EmailContent.To = Helper.GetEmailPart(item.user_emailid, string.Empty);
                    EmailContent.Subject = template.subject;

                    // Body with EMP ID 
                    string emailBodyWithEmpId = Helper.ComposeMsg(keys, template.template, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);
                    //Body
                    EmailContent.Body = await Helper.GetEmailBodyWithEmailAddress(emailBodyWithEmpId, _configurationService);

                    emailNewNotificationPayload.Add(new EmailMetaPayloadDTO
                    {
                        body = EmailContent.Body,
                        to = Helper.GetEmailIdFromMID(EmailContent.To?.Split(",").ToList()),
                        cc = new string[] { },
                        meta = { },
                        subject = EmailContent.Subject
                    });

                }
                task.Add(_emailService.SendEmail(emailNewNotificationPayload));
            }
            else
            {
                Console.WriteLine($"No Projects in Marketplace for date:{currentDate}");
            }

            return task;
        }

        private async Task<List<Task>> PendingAllocationAndSkillsNotificationTask(GetNotificationTemplateResponse template)
        {
            string notificationRegexPrefix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexPrefix).Value;
            string notificationRegexSuffix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexsuffix).Value;
            int adjustDays = Convert.ToInt32(_configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.PendingAllocationAndSkillsTaskAdjustDays).Value);

            var task = new List<Task>();

            Dictionary<string, string> DTokens = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            string baseUrl = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.BaseUrl).Value;
            if (!DTokens.ContainsKey("BaseUrl")) DTokens.Add("BaseUrl", baseUrl);

            string notificationPlaceholderRegex = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationTokenPatternRegex).Value;

            List<string> keys = Helper.ExtractKeysFromMsg(template.template, notificationPlaceholderRegex);

            //  1.    pending tasks for allocation and skills
            List<WorkflowDTO> pendingTasks = await _configurationService.GetWorkflowPendingTasks();
            //  2.    email Grouping

            if (pendingTasks != null && pendingTasks.Count > 0)
            {
                Dictionary<string, List<WorkflowDTO>> emailAndTasks = new();
                emailAndTasks = pendingTasks
                    .SelectMany(task => task.assigned_to.Split(',')
                        .Select(email => new { Email = email.ToLower().Trim(), Task = task }))
                    .GroupBy(x => x.Email)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.Task).ToList());
                var emailNewNotificationPayload = new List<EmailMetaPayloadDTO>();
                // Dictionary<string, string> DTokens = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var kv in emailAndTasks)
                {
                    var employeeEmail = kv.Key;
                    var pendingAllocationTasks = kv.Value.Where(t => t.workflow.module.ToLower().Trim() == EMPLOYEE_ALLOCATION.ToLower().Trim()).ToList();
                    var pendingSkillsTask = kv.Value.Where(t => t.workflow.module.ToLower().Trim() == WORKFLOW_MODULE_USER_SKILL_ASSESSMENT.ToLower().Trim()).ToList();
                    if (DTokens.ContainsKey("allocationtablecontent"))
                    {
                        DTokens["allocationtablecontent"] = null;
                    }
                    if (DTokens.ContainsKey("skilltablecontent"))
                    {
                        DTokens["skilltablecontent"] = null;
                    }
                    if (pendingAllocationTasks != null && pendingAllocationTasks.Count > 0)
                    {
                        string allocationTableContent = Helper.ParsePendingAllocationTaskSummary(pendingAllocationTasks, adjustDays);
                        if (!DTokens.ContainsKey("allocationtablecontent"))
                        {
                            DTokens.Add("allocationtablecontent", allocationTableContent);
                        }
                        else
                        {
                            DTokens["allocationtablecontent"] = allocationTableContent;
                        }
                    }
                    if (pendingSkillsTask != null && pendingSkillsTask.Count > 0)
                    {
                        string skillTableContent = Helper.ParsePendingSkillTaskSummary(pendingSkillsTask, adjustDays);
                        if (!DTokens.ContainsKey("skilltablecontent"))
                        {
                            DTokens.Add("skilltablecontent", skillTableContent);
                        }
                        else
                        {
                            DTokens["skilltablecontent"] = skillTableContent;
                        }

                    }
                    // if ((pendingAllocationTasks != null && pendingAllocationTasks.Count > 0) || (pendingSkillsTask != null && pendingSkillsTask.Count > 0))
                    // {


                    //     //foreach (var item in notificationSubscriptions)
                    //     //{
                    //     NotificationDTO EmailContent = new NotificationDTO();
                    //     EmailContent.To = employeeEmail;
                    //     EmailContent.Subject = template.subject;
                    //     EmailContent.Body = Helper.ComposeMsg(keys, template.template, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);

                    //     emailNewNotificationPayload.Add(new EmailMetaPayloadDTO
                    //     {
                    //         body = EmailContent.Body,
                    //         to = Helper.GetEmailIdFromMID(EmailContent.To.Split(",").ToList()),
                    //         cc = new string[] { },
                    //         meta = { },
                    //         subject = EmailContent.Subject
                    //     });
                    //     //}
                    // }
                    if (pendingSkillsTask.Count > 0)
                    {
                        string skillTableContent = Helper.ParsePendingSkillTaskSummary(pendingSkillsTask, adjustDays);
                        if (!DTokens.ContainsKey("skilltablecontent"))
                        {
                            DTokens.Add("skilltablecontent", skillTableContent);
                        }
                        else
                        {
                            DTokens["skilltablecontent"] = skillTableContent;
                        }

                    }
                    if (pendingAllocationTasks.Count > 0 || pendingSkillsTask.Count > 0)
                    {


                        //foreach (var item in notificationSubscriptions)
                        //{
                        NotificationDTO EmailContent = new NotificationDTO();
                        EmailContent.To = employeeEmail;
                        EmailContent.Subject = template.subject;

                        // Body with EMP ID 
                        string emailBodyWithEmpId = Helper.ComposeMsg(keys, template.template, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);
                        //Body
                        EmailContent.Body = await Helper.GetEmailBodyWithEmailAddress(emailBodyWithEmpId, _configurationService);

                        emailNewNotificationPayload.Add(new EmailMetaPayloadDTO
                        {
                            body = EmailContent.Body,
                            to = Helper.GetEmailIdFromMID(EmailContent.To.Split(",").ToList()),
                            cc = new string[] { },
                            meta = { },
                            subject = EmailContent.Subject
                        });
                        //}
                    }
                }
                // List<NotificationLog> _logs = new List<NotificationLog>();
                // foreach (var item in emailNewNotificationPayload)
                // {
                //     _logs.Add(new NotificationLog {                      
                //      to = string.Join(",", item.to ?? Array.Empty<string>()),
                //      cc = string.Join(",", item.cc ?? Array.Empty<string>()),
                //      body = item.body
                //     });
                // }
               // await _notificationRepository.AddNotificationLog(_logs);
                task.Add(_emailService.SendEmail(emailNewNotificationPayload));
            }

            return task;
        }
        private async Task<List<Task>> AdditionOfNewProjectNotificationToResourceRequestor(GetNotificationTemplateResponse template)
        {
            string notificationRegexPrefix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexPrefix).Value;
            string notificationRegexSuffix = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationRegexsuffix).Value;
            int adjustDays = Convert.ToInt32(_configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.AdditionOfNewProjectToRMSAdjustDays).Value);

            string notificationPlaceholderRegex = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.NotificationTokenPatternRegex).Value;
            //1. List of all projects added today to RMS
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(adjustDays));

            var result = await _configurationService.GetAllProjectByCreationDate(currentDate);

            var task = new List<Task>();

            var emailAndProject = result
                .SelectMany(e => e.ProjectRolesView.Where(t => t.IsActive == true && t.ApplicationRole.ToLower().Trim() == UserRoles.ResourceRequestor.ToLower().Trim()).Select(r => new { Email = r.User, Project = e }))
                .Where(f => string.IsNullOrEmpty(f.Email) == false)
                .GroupBy(x => x.Email)
                .ToDictionary(g => g.Key, g => g.Select(t => t.Project).ToList());

            Dictionary<string, string> DTokens = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            string baseUrl = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.BaseUrl).Value;
            if (!DTokens.ContainsKey("BaseUrl")) DTokens.Add("BaseUrl", baseUrl);

            List<string> keys = Helper.ExtractKeysFromMsg(template.template, notificationPlaceholderRegex);

            var emailNewNotificationPayload = new List<EmailMetaPayloadDTO>();

            foreach (var kv in emailAndProject)
            {
                var employeeEmail = kv.Key;
                if (kv.Value.Count > 0)
                {
                    string projectDetails = Helper.ParseNewProjectDetails(kv.Value);
                    DTokens["newprojectdetailstable"] = projectDetails;
                    NotificationDTO EmailContent = new NotificationDTO();
                    EmailContent.To = employeeEmail;
                    EmailContent.Subject = template.subject;

                    // Body with EMP ID 
                    string emailBodyWithEmpId = Helper.ComposeMsg(keys, template.template, ref DTokens, notificationRegexPrefix, notificationRegexSuffix);
                    //Body
                    EmailContent.Body = await Helper.GetEmailBodyWithEmailAddress(emailBodyWithEmpId, _configurationService);

                    emailNewNotificationPayload.Add(new EmailMetaPayloadDTO
                    {
                        body = EmailContent.Body,
                        to = Helper.GetEmailIdFromMID(EmailContent.To.Split(",").ToList()),
                        cc = new string[] { },
                        meta = { },
                        subject = EmailContent.Subject
                    });
                }
            }
            task.Add(_emailService.SendEmail(emailNewNotificationPayload));
            return task;
        }

        public async Task<Dictionary<string, string>> FillToken(InitNotificationDTO meta, string keys, Dictionary<string, string> DTokens)
        {

            string[] keyParams = keys.Split(':');
            string keyName = keyParams[0];
            string serviceName = string.Empty;
            string[] serviceParams = new string[] { };
            if (keyParams.Length == 3)
            {
                serviceName = keyParams[1].ToUpper().Trim();
                serviceParams = keyParams[2].Split('|');
            }
            else if (keyParams.Length == 2)
            {
                serviceName = keyParams[1].ToUpper().Trim();
            }
            if (!DTokens.ContainsKey(keyName))
            {
                switch (serviceName)
                {
                    case Constants.Constants.GET_RESOURCE_ALLOCATION_DETAILS:
                        if (serviceParams.Length == 1)
                        {
                            var keyItem = serviceParams[0].Trim();
                            var guidItemId = DTokens.GetValueOrDefault(keyItem);
                            if (guidItemId != null)
                            {
                                var allocationDetails = await _configurationService.GetResourceAllocationDetailsByGuid(guidItemId);
                                DTokens = FillToken(allocationDetails, DTokens);
                            }

                        }
                        break;
                    case Constants.Constants.GET_USER_INFO:
                        if (serviceParams.Length == 1)
                        {
                            var userInfo = await _configurationService.GetUserInfoByUserEmailId(DTokens[serviceParams[0]]);
                            DTokens = FillToken(userInfo, DTokens);

                        }
                        break;
                    case Constants.Constants.GET_PROJECT_DETAILS_BY_PIPELINE_CODE_JOB_CODE:
                        if (serviceParams.Length == 2)
                        {
                            var projectDetails = await _configurationService.GetProjectDetailsByPipelineCodeAndJobCode(DTokens[serviceParams[0]], DTokens[serviceParams[1]]);
                            DTokens = FillToken(projectDetails, DTokens);
                        }
                        break;
                    case Constants.Constants.GET_PROJECT_RESOURCE_REQUESTOR:
                        if (serviceParams.Length == 2)
                        {
                            var requestorRoles = await _configurationService.GetResourceRequestorEmailsByPipelineCode(DTokens[serviceParams[0]], DTokens[serviceParams[1]]);
                            if (Helper.CheckIsValidJsonArray(requestorRoles))
                            {
                                var resourceRequestorList = JArray.Parse(requestorRoles).Select(e => e["user"]);
                                List<string> userReqRoles = new List<string>();


                                if (resourceRequestorList != null)
                                {
                                    foreach (var item in resourceRequestorList)
                                    {
                                        userReqRoles.Add(Convert.ToString(item));
                                    }
                                    var requesroEmails = new JObject
                                    {
                                        {"projectresourcerequestor", string.Join( ",",userReqRoles)}
                                    };
                                    DTokens = FillToken(JsonConvert.SerializeObject(requesroEmails), DTokens);
                                }
                            }
                        }
                        break;
                    case "UserSkill":
                        if (serviceParams.Length == 2)
                        {
                            var userSkills = await _skillHttpService.GetUserSkillById(DTokens[serviceParams[0]]);
                            DTokens = FillToken(userSkills, DTokens);
                        }
                        break;
                    case Constants.Constants.EXTRACT_BASE_URL:
                        string baseUrl = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.BaseUrl).Value;
                        DTokens.Add("BaseUrl", baseUrl);
                        break;
                    default:
                        break;
                }

            }
            return DTokens;
        }

        public async Task<Dictionary<string, string>> DictionaryGeneration(List<string> keys, Dictionary<string, string> DTokens, InitNotificationDTO meta)
        {

            foreach (var key in keys)
            {
                DTokens = await FillToken(meta, key, DTokens);
            }
            return DTokens;
        }
        /// <summary>
        /// Parse the allocation 
        /// </summary>
        /// <param name="publishedAllocationResponses"></param>
        /// <param name="closedAllocations"></param>
        /// <returns></returns>
        public string ParseAllocationSummary(List<PublishedAllocationResponse> publishedAllocationResponses, List<ResourceAllocationDetailsResponseForWorkflowMeta> closedAllocations)
        {
            List<PublishedAllocationResponse> newAllocations = new List<PublishedAllocationResponse>();
            List<PublishedAllocationResponse> updatedllocations = new List<PublishedAllocationResponse>();
            List<PublishedAllocationResponse> releaseAllocations = new List<PublishedAllocationResponse>();
            string AllocationEmail = string.Empty;
            foreach (var allocation in publishedAllocationResponses)
            {
                if (allocation.IsActive == false)
                {
                    releaseAllocations.Add(allocation);
                }
                else if (Helper.TruncateMilliseconds(allocation.ModifiedAt) > Helper.TruncateMilliseconds(allocation.CreatedAt) || allocation.AllocationVersion > 1)
                {
                    updatedllocations.Add(allocation);
                }
                else
                {
                    newAllocations.Add(allocation);
                }
            }
            if (newAllocations.Count > 0 || releaseAllocations.Count > 0 || updatedllocations.Count > 0 || closedAllocations.Count > 0)
            {
                AllocationEmail += "<br />  <h2> Allocations </h2> ";
            }
            if (newAllocations.Count > 0)
            {
                AllocationEmail += SummaryHelper.GenerateAllocationEmailBody(newAllocations
                                  , NEW_ALLOCATION);
            }
            if (releaseAllocations.Count > 0)
            {
                AllocationEmail += SummaryHelper.GenerateAllocationEmailBody(releaseAllocations, RELEASE_ALLOCATION);
            }
            if (updatedllocations.Count > 0)
            {
                AllocationEmail += SummaryHelper.GenerateAllocationEmailBody(updatedllocations, UPDATE_ALLOCATION);
            }
            if (closedAllocations.Count > 0)
            {
                AllocationEmail += SummaryHelper.GenerateAllocationEmailBody(closedAllocations, CLOSED_ALLOCATION);
            }

            return AllocationEmail;
        }

        public string ParseRequistionSummary(List<RequisitionResponse> publishedRequistionResponses)
        {
            List<RequisitionResponse> newRequsition = new List<RequisitionResponse>();
            List<RequisitionResponse> updatedRequsition = new List<RequisitionResponse>();
            List<RequisitionResponse> releaseRequsition = new List<RequisitionResponse>();
            string AllocationEmail = string.Empty;
            foreach (var allocation in publishedRequistionResponses)
            {
                if (allocation.IsActive == false)
                {
                    releaseRequsition.Add(allocation);
                }
                else if (DateTime.Compare(Helper.TruncateMilliseconds(allocation.ModifiedAt), Helper.TruncateMilliseconds(allocation.CreatedAt)) > 0)
                {
                    updatedRequsition.Add(allocation);
                }
                else
                {
                    newRequsition.Add(allocation);
                }
            }

            if (newRequsition.Count > 0 || releaseRequsition.Count > 0 || updatedRequsition.Count > 0)
            {
                AllocationEmail += "<br />  <h2> Requisitions </h2>";
            }
            if (newRequsition.Count > 0)
            {
                releaseRequsition = releaseRequsition.Where(x => x.RequisitionTypeId == 3 || x.RequisitionTypeId == 6).ToList();
                AllocationEmail += SummaryHelper.GenerateRequistionEmailBody(newRequsition, NEW_REQUISTION);
            }
            if (releaseRequsition.Count > 0)
            {
                releaseRequsition = releaseRequsition.Where(x => x.RequisitionTypeId == 3 || x.RequisitionTypeId == 6).ToList();
                AllocationEmail += SummaryHelper.GenerateRequistionEmailBody(releaseRequsition, RELEASE_REQUISTION);
            }
            if (updatedRequsition.Count > 0)
            {
                releaseRequsition = releaseRequsition.Where(x => x.RequisitionTypeId == 3 || x.RequisitionTypeId == 6).ToList();
                AllocationEmail += SummaryHelper.GenerateRequistionEmailBody(updatedRequsition, UPDATE_REQUISTION);
            }

            return AllocationEmail;
        }

        public static string ParseMarketPlaceSummary(List<MarketPlaceProjectDetaillsIntrestDTO> marketplaceProjects)
        {
            var parsedProject = marketplaceProjects.Select(a => new MarketPlaceSummaryDTO
            {
                PipelineCode = a.PipelineCode,
                PipelineName = a.PipelineName,
                JobCode = a.JobCode,
                ExitDate = (DateTime)a.MarketPlaceExpirationDate,
                NoOfInterest = a.EmployeeInterest.Count,
                PublishDate = (DateTime)a.CreatedDate,
                JobName = a.JobName
            }).ToList();
            string summary = SummaryHelper.GenerateMarketPlaceEmailBody(parsedProject);
            return summary;
        }
    }
}