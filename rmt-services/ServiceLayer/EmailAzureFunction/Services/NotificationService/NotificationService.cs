using Azure.Core;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceLayer.Constants;
using ServiceLayer.DTOs;
using ServiceLayer.Services.ConfigurationService;
using ServiceLayer.Services.EmailService;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.MarketPlaceHelper;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper.RolesAndPermissionHelper;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper;
using ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper.DTOs;
using ServiceLayer.Services.ProjectService;
using ServiceLayer.Services.PushNotificationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ServiceLayer.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private readonly IConfigurationService _configurationService;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IEmailService _emailService;
        private readonly IProjectService _projectService;
        private readonly IRolesAndPermission _rolesAndPermission;
        private readonly IWorkflowNotificationHelper _workflowNotificationHelper;
        private readonly IMarketPlace _marketPlace;
        private readonly ILogger<NotificationSubscription> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configurationService"></param>
        /// <param name="pushNotificationService"></param>
        /// <param name="emailService"></param>
        public NotificationService(IConfigurationService configurationService
            , IPushNotificationService pushNotificationService
            , IEmailService emailService
            , IProjectService projectService
            , IRolesAndPermission rolesAndPermission
            , IWorkflowNotificationHelper workflowNotificationHelper
            , IMarketPlace marketPlace,
             ILogger<NotificationSubscription> log
            )
        {
            _configurationService = configurationService;
            _pushNotificationService = pushNotificationService;
            _emailService = emailService;
            _projectService = projectService;
            _rolesAndPermission = rolesAndPermission;
            _workflowNotificationHelper = workflowNotificationHelper;
            _marketPlace = marketPlace;
            this._logger = log;
        }


        public async Task InitialiseNotificationTemplates(NotificationPayloadDTO notificationPayloadDTO)
        {

            _logger.LogInformation($"InitialiseNotificationTemplates -> Start");

            //var responsePayload = JsonConvert.DeserializeObject<Dictionary<string, List<NotificationPayloadDTO>>>(notificationPayloadDTO.payload);


            _logger.LogInformation($"InitialiseNotificationTemplates -> GetNotificationTemplate-Start");

            //Fetch all the templated for specified action

            _logger.LogInformation($"InitialiseNotificationTemplates -> GetNotificationTemplate-End");


            //if (templateFetch == null || templateFetch.Count == 0)
            //{
            //    //TODO No template found condition
            //    return;
            //}
            ////To store all the push notifications
            //var pushNewNotificationPayload = new List<PostNewPushNotificationDTO>();

            ////To store all the email notifications
            //var emailNewNotificationPayload = new List<EmailMetaPayloadDTO>();

            _logger.LogInformation($"InitialiseNotificationTemplates -> GetRequiredKeyAttributes-Start");

            //Get all the distinct keys/placeholders that are required to send these notifications
            //  List<NotificationPlaceHolderDTO> requiredKeys = GetRequiredKeyAttributes(templateFetch);

            _logger.LogInformation($"InitialiseNotificationTemplates -> GetRequiredKeyAttributes-End");

            InitNotificationDTO payloads = new InitNotificationDTO();

            if (notificationPayloadDTO.action == NotificationTemplateTypes.SUPERCOACH_NOTIFICATION_OF_PENDING_TASK)
            {
                payloads = new InitNotificationDTO
                {
                    response_payload = notificationPayloadDTO.payload,
                    path = "",
                    request_payload = "",
                    token = notificationPayloadDTO.token,
                    userinfo = ""

                };
            }
            else
            {
                payloads = JsonConvert.DeserializeObject<InitNotificationDTO>(notificationPayloadDTO.payload);
            }

            SendNotificationRequest sendNotificationRequest = new SendNotificationRequest
            {
                meta = payloads != null ? JsonConvert.SerializeObject(payloads) : string.Empty,
                NotificationKey = notificationPayloadDTO.action
            };

            var response = await _configurationService.SendNotification(new List<SendNotificationRequest> { sendNotificationRequest }, notificationPayloadDTO.token);


            //TODO: - ask aayush can we send such a huge payload from gatway to sl

            _logger.LogInformation($"InitialiseNotificationTemplates -> GetKeyValuePairsByIndividualCases-Start");

            //Get the key value pairs to the required placeholders
            //List<Dictionary<string, string>> keyValuePairs = await GetKeyValuePairsByIndividualCases(notificationPayloadDTO.action, payloads, requiredKeys);

            //foreach (Dictionary<string, string> keyValuePairsItem in keyValuePairs)
            //{
            //    foreach (var item in templateFetch)
            //    {
            //        List<string> usersToSend = GetReceiversList(item.to, keyValuePairsItem);
            //        //If no user is found for the notification to be sent, throw exception
            //        if (usersToSend == null || usersToSend.Count == 0)
            //        {
            //            throw new Exception(Constants.Constants.NoUsersFoundToSendNotification);
            //        }
            //        var finalKeyValuePairs = GetClickLinkValue(keyValuePairsItem, item, requiredKeys);
            //        var modifiedTemplate = _configurationService.TransformMessageTemplateAccordingToPayloads(item.template, item.payload, finalKeyValuePairs);

            //        //Adding Notification as Push Notification
            //        if (item.notification_type == NotificationTypeDTO.PushNotificationType)
            //        {
            //            pushNewNotificationPayload.Add(
            //                new PostNewPushNotificationDTO
            //                {
            //                    type = notificationPayloadDTO.action,
            //                    message = modifiedTemplate,
            //                    meta = { },
            //                    users = usersToSend.ToArray(),
            //                    notification_template_id = item.Id,
            //                    link = finalKeyValuePairs[NotificationTemplatePayloads.CLICK_LINK]
            //                });
            //        }
            //        //Adding Notification as Email Notification
            //        else if (item.notification_type == NotificationTypeDTO.EmailNotificationType)
            //        {
            //            emailNewNotificationPayload.Add(new EmailMetaPayloadDTO
            //            {
            //                body = modifiedTemplate,
            //                to = usersToSend.ToArray(),
            //                cc = new string[] { },
            //                meta = { },
            //                subject = item.subject
            //            });
            //        }
            //    }
            //}
            //var tasks = new List<Task>();
            //if (pushNewNotificationPayload.Any())
            //{
            //    tasks.Add(_pushNotificationService.PostNewPushNotification(pushNewNotificationPayload, notificationPayloadDTO.token));
            //}
            //if (emailNewNotificationPayload.Any())
            //{
            //    tasks.Add(_emailService.SendEmail(emailNewNotificationPayload));
            //}
            //await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Get the list of the users to whom the notification is to be sent
        /// </summary>
        /// <param name="templateNotificationReceivers"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>

        public List<string> GetEmailIdFromMID(List<string> input)
        {
            List<string> EmailIds = new List<string>();
            if (input != null)
            {
                foreach (var item in input)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(item) && item.Contains("__"))
                        {
                            var spiltMail = item.Split("__");
                            if (spiltMail.Length > 1)
                            {
                                EmailIds.Add(spiltMail[1]);
                            }
                        }
                        else if (!string.IsNullOrEmpty(item) && !item.Contains("__"))
                        {
                            EmailIds.Add(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("---GetEmailIdFromMID---", ex);
                    }
                }
            }
            return EmailIds;
        }
        public static List<string> GetReceiversList(string templateNotificationReceivers, Dictionary<string, string> keyValuePairs)
        {
            List<string> receiversListResponse = new List<string>();
            //Split the roles seperated by comma
            var templateNotificationReceiversList = templateNotificationReceivers.Split(Constants.Constants.CommaSplitter);
            foreach (var item in templateNotificationReceiversList)
            {
                var value = keyValuePairs.Where(m => m.Key.Equals(item)).FirstOrDefault().Value;
                if (!String.IsNullOrEmpty(value))
                {
                    //Can be multiple users on a role, so add each of them which are seperated by comma
                    foreach (var user in value.Split(Constants.Constants.CommaSplitter))
                    {

                        receiversListResponse.Add(user);
                    }
                }
            }
            return receiversListResponse;
        }

        /// <summary>
        /// Get all the distinct keys required to send notifications in all templates
        /// </summary>
        /// <param name="templates"></param>
        /// <returns></returns>
        public static List<NotificationPlaceHolderDTO> GetRequiredKeyAttributes(List<NotificationTemplateDTO> templates)
        {
            ///check to get keys from template regex instead of another table
            var keys = new List<NotificationPlaceHolderDTO>();
            foreach (var template in templates)
            {
                //Add all the placeholder keys
                foreach (var payloadItem in template.payload)
                {
                    keys.Add(payloadItem);
                }
                //Add the roles it needs to be sent to as well as keys
                foreach (var item in template.to.Split(Constants.Constants.CommaSplitter))
                {
                    keys.Add(new NotificationPlaceHolderDTO
                    {
                        name = item
                    });
                }
                List<string> keysInLink = Helper.ExtractValuesByRegex(template.link);
                foreach (var item in keysInLink)
                {
                    keys.Add(new NotificationPlaceHolderDTO
                    {
                        name = item,
                        is_required = true,
                        link_payload = true
                    });
                }
            }
            return keys.DistinctBy(x => x.name).ToList();
        }

        /// <summary>
        /// Get Click Link Value for each template seperately
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public Dictionary<string, string> GetClickLinkValue(Dictionary<string, string> keyValuePairs, NotificationTemplateDTO template, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            var tempKeyValuePairs = keyValuePairs;
            var linkKeys = requiredKeys.Where(m => m.link_payload != null && m.link_payload == true).ToList();
            var modifiedUrl = _configurationService.TransformMessageTemplateAccordingToPayloads(template.link, linkKeys, keyValuePairs);
            var value = modifiedUrl;
            if (template.notification_type == NotificationTypeDTO.EmailNotificationType)
            {
                value = Helper.GetClickableLink(Environment.GetEnvironmentVariable("BaseUiUrl") + modifiedUrl, "Click");
            }
            if (tempKeyValuePairs.ContainsKey(NotificationTemplatePayloads.CLICK_LINK))
            {
                tempKeyValuePairs.Remove(NotificationTemplatePayloads.CLICK_LINK);
            }

            tempKeyValuePairs.Add(NotificationTemplatePayloads.CLICK_LINK, value);
            return tempKeyValuePairs;
        }

        /// <summary>
        /// Get Key Value Pairs By Individual Cases 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Dictionary<string, string>>> GetKeyValuePairsByIndividualCases(string action, InitNotificationDTO payload, List<NotificationPlaceHolderDTO> requiredKeys)
        {
            switch (action)
            {
                // 123,124 - UC018 - If a person is deactivated from the Roles & permission screen & this employee is assigned to a future/ current project - notification to be sent to Requestor as employee access to system is disabled
                case NotificationTemplateTypes.USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR:
                    return await _rolesAndPermission.USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR(payload, requiredKeys);
                case NotificationTemplateTypes.NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT:
                    return await _workflowNotificationHelper.NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT(payload, requiredKeys);
                case NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER:
                    return await _workflowNotificationHelper.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER(payload, requiredKeys);
                case NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR:
                    return await _workflowNotificationHelper.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR(payload, requiredKeys);
                //case NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REVIEWER:
                //    return await _workflowNotificationHelper.
                //case NotificationTemplateTypes.RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REQUESTOR:
                //    return await _workflowNotificationHelper.
                case NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT:
                    return await _workflowNotificationHelper.NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT(payload, requiredKeys);
                //// 43,44 - UC010 - Notification for allocation of a resources to a project (Notification directly to the employee for confirmation) for employee
                //case NotificationTemplateTypes.NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT:
                //    return await _workflowNotificationHelper.NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT(payload,requiredKeys);
                //
                //67,68 - UC025
                case NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE:
                    return await _workflowNotificationHelper.PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE(payload, requiredKeys);
                //69,70 - UC025
                case NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE:
                    return await _workflowNotificationHelper.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE(payload, requiredKeys);
                // 81,82 - UC025
                case NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON:
                    return await _workflowNotificationHelper.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON(payload, requiredKeys);
                // 83,84 - UC025
                case NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON:
                    return await _workflowNotificationHelper.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON(payload, requiredKeys);
                // 85,86 - UC025
                case NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION:
                    return await _workflowNotificationHelper.PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION(payload, requiredKeys);
                // 87,88 - UC025
                case NotificationTemplateTypes.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE:
                    return await _workflowNotificationHelper.PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE(payload, requiredKeys);
                //89,90 UC025
                case NotificationTemplateTypes.EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR:
                    return await _workflowNotificationHelper.EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR(payload, requiredKeys);
                //91,92 UC025
                case NotificationTemplateTypes.EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE:
                    return await _workflowNotificationHelper.EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE(payload, requiredKeys);
                //99,100 UC026
                case NotificationTemplateTypes.EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR:
                    return await _workflowNotificationHelper.EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR(payload, requiredKeys);
                //101,102 UC026
                case NotificationTemplateTypes.EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER:
                    return await _workflowNotificationHelper.EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER(payload, requiredKeys);
                //103,104 UC026
                case NotificationTemplateTypes.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER:
                    return await _workflowNotificationHelper.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER(payload, requiredKeys);
                //105,106 UC026
                case NotificationTemplateTypes.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR:
                    return await _workflowNotificationHelper.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR(payload, requiredKeys);
                //107,108 UC026
                case NotificationTemplateTypes.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE:
                    return await _workflowNotificationHelper.REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE(payload, requiredKeys);
                //130,131 UC028
                case NotificationTemplateTypes.EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH:
                    return await _workflowNotificationHelper.EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH(payload, requiredKeys);
                //132,133 UC028
                case NotificationTemplateTypes.SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST:
                    return await _workflowNotificationHelper.SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST(payload, requiredKeys);
                //134,135 UC028
                case NotificationTemplateTypes.SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST:
                    return await _workflowNotificationHelper.SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST(payload, requiredKeys);
                case NotificationTemplateTypes.NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT:
                    return await _marketPlace.NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT(payload, requiredKeys);
                case NotificationTemplateTypes.SUPERCOACH_NOTIFICATION_OF_PENDING_TASK:
                    return await _workflowNotificationHelper.SUPERCOACH_NOTIFICATION_OF_PENDING_TASK(payload, requiredKeys);

                default: return null;
            }
        }
        //action + status_wf =>  
        //create_wf
        //update_wf
        //module_type$action
    }
}
