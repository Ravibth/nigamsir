using Azure.Core;
using Gateway.API.Dtos;
using Gateway.API.Helpers.IHttpServices;
using Gateway.API.Middlewares.MiddlewareHelpers.RolesAndPermissionsMiddlewareHelper;
using Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper;
using Gateway.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper.DTOs;
using Microsoft.IdentityModel.Tokens;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using Gateway.API.ServiceLayerHelper.WorkflowService;
using Gateway.API.ServiceLayerHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.MarketPlaceMiddlewareHelper;
using Microsoft.Extensions.Configuration;
using Gateway.API.Middlewares.MiddlewareHelpers.ProjectMiddlewareHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.MarketPlaceMiddlewareHelper.DTOs;
using Polly;
using Ocelot.RequestId;
using static Gateway.API.Constants;
using Gateway.API.Middlewares.MiddlewareHelpers.RolesAndPermissionsMiddlewareHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.ProjectMiddlewareHelper;
using System.Dynamic;

namespace Gateway.API.Middlewares
{
    public class NotificatiionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly IAzureServiceBusService _azureServiceBusService;
        private IConfigurationHttpService _configurationHttpService;
        private IProjectHttpService _projectHttpService;
        private IAllocationHttpService _allocationHttpService;
        private ILogger<NotificatiionMiddleware> _logger;
        private IAllocationMiddlewareHelper _allocationMiddlewareHelper;
        private IWorkflowService _workflowService;
        private IIdentityHttpServices _identityHttpServices;
        private ISkillsHttpService _skillsHttpService;
        public NotificatiionMiddleware(RequestDelegate next, IAzureServiceBusService azureServiceBusService, IConfiguration config, ILogger<NotificatiionMiddleware> logger)
        {
            _next = next;
            _azureServiceBusService = azureServiceBusService;
            _logger = logger;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation("--NotificatiionMiddleware--Invoke--Start");
                //_logger.LogDebug("Hellow this is Notification Middleware log Debug");
                //_logger.LogTrace("Hellow this is Notification Middleware log Trace");
                //_logger.LogCritical("Hellow this is Notification Middleware log Critical");
                //_logger.LogWarning("Hellow this is Notification Middleware log Warning");
                //_logger.LogError("Hellow this is Notification Middleware log Error");
                var statuses = new List<HttpStatusCode> { HttpStatusCode.Accepted, HttpStatusCode.Created, HttpStatusCode.OK };
                _configurationHttpService = context.RequestServices.GetService<IConfigurationHttpService>();
                _projectHttpService = context.RequestServices.GetService<IProjectHttpService>();
                _allocationHttpService = context.RequestServices.GetService<IAllocationHttpService>();
                _allocationMiddlewareHelper = context.RequestServices.GetService<IAllocationMiddlewareHelper>();
                _workflowService = context.RequestServices.GetService<IWorkflowService>();
                _identityHttpServices = context.RequestServices.GetService<IIdentityHttpServices>();
                _skillsHttpService = context.RequestServices.GetService<ISkillsHttpService>();


                LogRequestResponseData(context);

                if (context.Items != null)
                {
                    _logger.LogInformation("--NotificatiionMiddleware--Invoke--context.Items--Found-{0}", context.Items.Count);

                    bool initiateProcess = statuses.Any((item) => item == (context.Items.DownstreamResponse().StatusCode));

                    if (initiateProcess)
                    {
                        _logger.LogInformation("--NotificatiionMiddleware--Invoke--context.Items--initiateProcess-{0}", initiateProcess);

                        var request = context.Items.DownstreamRequest();

                        var temp1 = request.Headers.Where(m => m.Key.Equals(Constants.UserInfoCustomHeader)).FirstOrDefault();
                        if (temp1.Value != null)
                        {
                            var userAccessor = context.RequestServices.GetService<IUserAccessor>();
                            var response = context.Items.DownstreamResponse().Content.ReadAsStringAsync();

                            var request_body = context.Items.DownstreamRequest().Content.ReadAsStringAsync().Result;
                            var request_payload = new RequestPayloadDTO
                            {
                                query = JsonConvert.SerializeObject(QueryHelpers.ParseQuery(QueryString.FromUriComponent(request.Query).Value).ToList()),
                                body = request_body
                            };
                            var userinfo = temp1.Value.FirstOrDefault();
                            // var userinfo = new UserInfoDTO
                            // {
                            //     name = userAccessor.GetName(),
                            //     email = userAccessor.GetEmail(),
                            // };
                            InitNotificationDTO payload = new InitNotificationDTO
                            {
                                path = $"{request.Method}_{request.AbsolutePath}",
                                response_payload = response.Result.ToString(),
                                request_payload = (JsonConvert.SerializeObject(request_payload)).ToString(),
                                token = request.Headers.Authorization.ToString(),
                                userinfo = userinfo,
                            };
                            await initEmailNotificationProcess(payload, _logger);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("--NotificatiionMiddleware--Invoke--context.Items--initiateProcess-{0}", initiateProcess);
                    }
                }
                else
                {
                    _logger.LogInformation("--NotificatiionMiddleware--Invoke--context.Items--Not found.");
                    _logger.LogInformation("--NotificatiionMiddleware--Invoke--End");

                }
                _logger.LogInformation("--NotificatiionMiddleware--Invoke--End");
                await _next(context);
                _logger.LogInformation("--NotificatiionMiddleware--next Invoked");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "--NotificationMiddleware--Invoke--Failed");
                //throw;
            }
        }

        private async void LogRequestResponseData(HttpContext context)
        {
            try
            {
                var flag = _config.GetSection("MicroserviceApiSettings").GetSection("LogRequestResponseData").Value;
                if (flag == "true")
                {
                    var request = context.Items.DownstreamRequest();
                    var request_query = request.Query;
                    var request_body = context.Items.DownstreamRequest().Content.ReadAsStringAsync().Result;
                    var response = await context.Items.DownstreamResponse().Content.ReadAsStringAsync();
                    var responseStatusCode = context.Items.DownstreamResponse().StatusCode;
                    string endpoint = $"{request.Method}_{request.AbsolutePath}";

                    _logger.LogInformation("--LogRequestResponseData--Endpoint-{0}", endpoint);
                    _logger.LogInformation("--LogRequestResponseData--Request-{0}", request);
                    _logger.LogInformation("--LogRequestResponseData--Request_query-{0}", request_query);
                    _logger.LogInformation("--LogRequestResponseData--Request_body-{0}", request_body);
                    _logger.LogInformation("--LogRequestResponseData--ResponseStatusCode-{0}", responseStatusCode);
                    _logger.LogInformation("--LogRequestResponseData--Response-{0}", response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "--LogRequestResponseData--Error Occured");
            }
        }

        public async Task initEmailNotificationProcess(InitNotificationDTO notificationParams, ILogger<NotificatiionMiddleware> logger)
        {
            _logger.LogInformation("--initEmailNotificationProcess------Middleware--Start");
            //todo check to make it dynamic
            var workflowPayload = new List<NotificationPayload> { };
            var notificationPayload = new List<NotificationPayload> { };
            var projectServicePayload = new List<NotificationPayload> { };
            var tasks = new List<Task>();

            _logger.LogInformation("--initEmailNotificationProcess------Middleware--ApiUrl-{0}", notificationParams.path);

            //swith case comparision should be case insensitive
            switch (true)
            {

                // ************** For Notifications **************************
                // Roles and Permissions Update Case
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PutSwitchCasePath}{Constants.IdentitySwitchCasePath}user/update".ToLower()
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = RolesAndPermissionsMiddlewareHelper.UpdateRolesAndPermissionsCasePayloads(notificationParams);
                    tasks.Add(RemoveAllDraftAllocationsAfterUserIsDeactivated(notificationParams));
                    break;
                // ProjectActualBudgetOverShoot
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.GetSwitchCasePath}{Constants.ApiConstant}{Constants.ProjectSwitchCasePath}ProjectActualBudgetOverShoot"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = ProjectMiddlewareHelper.ProjectActualBudgetOverShoot(notificationParams);
                    break;
                // Workflow Create Case
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.WorkflowSwitchCasePath}{Constants.WorkflowVersion1SwitchCasePath}"
                    , StringComparison.InvariantCultureIgnoreCase):
                    tasks.Add(UpdateRespectiveTablesForCreateWorkflow(notificationParams, Constants.WorkflowConstants.CREATE_WORKFLOW_TITLE));
                    notificationPayload = WorkflowMiddlewareHelper.NewWorkflowCreated(notificationParams);
                    break;
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.WorkflowSwitchCasePath}{Constants.WorkflowVersion1SwitchCasePath}/bulk/update-workflow"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = WorkflowMiddlewareHelper.BulkApprovalUpdated(notificationParams);
                    tasks.Add(UpdateRespectiveTablesForWorkflowForBulkApproval(notificationParams, Constants.WorkflowConstants.UPDATE_WORKFLOW_TITLE));
                    break;
                //ROLLOVER
                case bool when notificationParams.path.Equals(
                    $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}ResourceAllocation/RolloverAllocationByPipelineCode/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = AllocationMiddlewareHelper.RollOVerAllocations(notificationParams);
                    break;
                //Ravi
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.MarketPlace}/SubmitEmpProjectInterest"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = MarketPlaceMiddlewareHelper.MarketPlaceEmployeeLikeSubmit(notificationParams);
                    //   tasks.Add(UpdateRespectiveTablesForUpdatedWorkflow(notificationParams, Constants.WorkflowConstants.UPDATE_WORKFLOW_TITLE));
                    break;

                //Create Requisition Api
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.RequisitionSwitchCasePath}SubmitRequisitionForProjectCode/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    //tasks.Add(_allocationMiddlewareHelper.AddUpdateProjectRequisitionAllocationForCreateRequisition(notificationParams));//added directly in allocation AddUpdateProjectRequisitionAllocation
                    projectServicePayload = MarketPlaceMiddlewareHelper.MarketPlaceRefreshEmployeeIntrestScoreForReqCreate(notificationParams);
                    break;
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PutSwitchCasePath}{Constants.ApiConstant}{Constants.RequisitionSwitchCasePath}UpdateRequisition/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    projectServicePayload = MarketPlaceMiddlewareHelper.MarketPlaceRefreshEmployeeIntrestScoreForReqUpdate(notificationParams);
                    break;
                //Delete Requisition Api
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.RequisitionSwitchCasePath}DeleteRequisitionById/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    //tasks.Add(_allocationMiddlewareHelper.AddUpdateProjectRequisitionAllocationForDeleteRequisitionById(notificationParams));//added directly in allocation AddUpdateProjectRequisitionAllocation
                    projectServicePayload = MarketPlaceMiddlewareHelper.MarketPlaceRefreshEmployeeIntrestScoreForReqDelete(notificationParams);
                    break;
                //Release Employee By Guid
                //RECHECK
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.ResourceAllocationSwitchCasePath}ReleaseResourceByGuid/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = AllocationMiddlewareHelper.ReleaseResource(notificationParams);
                    //tasks.Add(_allocationMiddlewareHelper.AddUpdateProjectRequisitionAllocationForReleaseResourceByGuid(notificationParams));//added directly in allocation AddUpdateProjectRequisitionAllocation
                    break;
                //Bulk Upload Requisition
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}BulkRequisition/BulkUploadRequisition"
                    , StringComparison.InvariantCultureIgnoreCase):
                    //tasks.Add(_allocationMiddlewareHelper.AddUpdateProjectRequisitionAllocationForBulkRequisitionUpload(notificationParams));//added directly in allocation AddUpdateProjectRequisitionAllocation
                    projectServicePayload = MarketPlaceMiddlewareHelper.MarketPlaceRefreshEmployeeIntrestScoreForReqBulkUpload(notificationParams);
                    break;
                //Admin assigned by Superadmin
                case bool flag when notificationParams.path.StartsWith($"{Constants.PutSwitchCasePath}{Constants.IdentitySwitchCasePath}user/v2/"):
                    notificationPayload = RolesAndPermissionsMiddlewareHelper.AddUpdateUserRoles(notificationParams);
                    break;
                //Job Code update- once all allocations have been moved to new code
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.ProjectSwitchCasePath}ChangeCodeForProject"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = ProjectMiddlewareHelper.JobCodeChangeToNewCode(notificationParams);
                    break;
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.ResourceAllocationSwitchCasePath}NewResourceAllocationMove/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = ProjectMiddlewareHelper.JobCodeChangeToNewCode(notificationParams);
                    break;
                // ************** For Workflow Initiation **************************
                //Not in use
                // case bool flag when notificationParams.path.Equals(
                //      "POST_/api/ResourceAllocation/UpdateAllocationByID/"
                //     , StringComparison.InvariantCultureIgnoreCase):
                //     workflowPayload = await CREATE_USER_ALLOCATION_WORKFLOW_BY_UPDATE_ALLOCATION_BY_ID(notificationParams);
                //     break;

                //Not in use
                //case bool flag when notificationParams.path.Equals(
                //     "POST_/api/ResourceAllocation/UpdateRollOverAllocations/"
                //    , StringComparison.InvariantCultureIgnoreCase):
                //    workflowPayload = await CREATE_USER_ALLOCATION_WORKFLOW_BY_UPDATE_ROLLOVER_ALLOCATIONS(notificationParams);
                //    break;

                //Not in use
                //case bool flag when notificationParams.path.Equals(
                //     "POST_/api/NamedAllocation/SubmitResourceAllocateToProject/"
                //    , StringComparison.InvariantCultureIgnoreCase):
                //    workflowPayload = await CREATE_USER_ALLOCATION_WORKFLOW_BY_SAME_TEAM_ALLOCATION(notificationParams, logger);
                //    break;
                // Not in use
                // case bool flag when notificationParams.path.Equals(
                //      "POST_/api/ResourceAllocation/CreateResourceAllocation"
                //     , StringComparison.InvariantCultureIgnoreCase):
                //     tasks.Add(_allocationMiddlewareHelper.AddUpdateProjectRequisitionAllocationForCreateResourceAllocation(notificationParams));
                //     workflowPayload = await CREATE_USER_ALLOCATION_WORKFLOW_BY_SYSTEM_SUGGESTION(notificationParams);
                //     break;
                case bool flag when notificationParams.path.Equals(
                     "POST_/api/ResourceAllocation/CommonResourceAllocation/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    tasks.Add(_allocationMiddlewareHelper.AddUpdateProjectRequisitionAllocationForCommonAllocation(notificationParams));
                    workflowPayload = await CREATE_USER_ALLOCATION_WORKFLOW_BY_COMMON_SCREEN(notificationParams, logger);
                    break;
                // Roles and Permissions Update Case
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.ProjectSwitchCasePath}UpdateProjectDetails"
                    , StringComparison.InvariantCultureIgnoreCase):
                    projectServicePayload = CreateProjectDetailsUpdateMessage(notificationParams, logger);
                    notificationPayload = ProjectMiddlewareHelper.ProjectUpdateNotification(notificationParams);
                    break;
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.UserSkillsSwitchCasePath}AddUpdateUserSkill"
                    , StringComparison.InvariantCultureIgnoreCase):
                    workflowPayload = await CREATE_USER_SKILL_ASSESSMENT_WORKFLOW(notificationParams);
                    break;
                // Project suspend
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.ProjectSwitchCasePath}UpdateProjectSuspensionStatus"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = CreateProjectSuspendMessage(notificationParams, logger);
                    break;
                // Job In active
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.ProjectSwitchCasePath}ProjectActivationStatusChange"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = CreateProjectSuspendMessage(notificationParams, logger);
                    break;
                // Project suspend allocation 
                case bool flag when notificationParams.path.Equals(
                     $"{Constants.PostSwitchCasePath}{Constants.ApiConstant}{Constants.ResourceAllocationSwitchCasePath}SuspendAllocation/"
                    , StringComparison.InvariantCultureIgnoreCase):
                    notificationPayload = CreateAllcationSuspendMessage(notificationParams, logger);
                    break;
                default:
                    return;
            }

            if (workflowPayload != null && workflowPayload.Count > 0)
            {
                _logger.LogInformation("--initEmailNotificationProcess------Middleware--{1}--publishWorkflow-Count-{0}", workflowPayload.Count, notificationParams.path);
                tasks.Add(publishWorkflow(workflowPayload, logger));
            }
            if (notificationPayload != null && notificationPayload.Count > 0)
            {
                _logger.LogInformation("--initEmailNotificationProcess------Middleware--{1}--publishNotification-Count-{0}", notificationPayload.Count, notificationParams.path);
                tasks.Add(publishNotification(notificationPayload, logger));
            }
            if (projectServicePayload != null && projectServicePayload.Count > 0)
            {
                _logger.LogInformation("--initEmailNotificationProcess------Middleware--{1}--publishProjectNotification-Count-{0}", projectServicePayload.Count, notificationParams.path);
                tasks.Add(publishProjectNotification(projectServicePayload, logger));
            }

            await Task.WhenAll(tasks);

            _logger.LogInformation("--initEmailNotificationProcess------Middleware--{0}--Task.WhenAll-executed", notificationParams.path);

            _logger.LogInformation("--initEmailNotificationProcess------Middleware--{0}--Completed", notificationParams.path);
        }

        /// <summary>
        /// publish the projectdetails messag to servicebus
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public async Task publishProjectNotification(List<NotificationPayload> payload, ILogger<NotificatiionMiddleware> logger)
        {
            ////using workflow subscription for testing 
            string type = Constants.ProjectTypeNotification;
            var topicName = _config.GetSection("AzureSBConfig").GetSection("AzureTopicInit").Value;

            //string type = Constants.WorkflowTypeNotification;
            //var topicName = _config.GetSection("AzureSBConfig").GetSection("AzureTopicInit").Value;

            foreach (var item in payload)
            {
                await _azureServiceBusService.PublishMessageOnAzureServiceBus(item, type, logger, topicName);
            }
        }

        /// <summary>
        /// Create projectdetails message for publishing to service bus
        /// </summary>
        /// <param name="notificationParams"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public List<NotificationPayload> CreateProjectDetailsUpdateMessage(InitNotificationDTO notificationParams, ILogger<NotificatiionMiddleware> logger)
        {
            logger.LogInformation("--initEmailNotificationProcess----inside CreateProjectDetailsUpdateMessage payload creation started");

            var requestDto = JsonConvert.DeserializeObject<UpdateProjectRequestDTO>(notificationParams.request_payload);
            var responseDto = JsonConvert.DeserializeObject<UpdateProjectResponseDTO>(notificationParams.response_payload);

            //if needed request response type can be modified after identifying the impacted areas, as of now only one instance is found 
            UpdateMarketPlaceProjectDTOPayload updateMarkeplaceProjectData = new UpdateMarketPlaceProjectDTOPayload()
            {
                PipelineCode = responseDto.PipelineCode,
                JobCode = responseDto.JobCode,
                JobName = responseDto.JobName,
                PipelineName = responseDto.PipelineName,
                ClientName = responseDto.ClientName,
                ClientGroup = responseDto.ClientGroup,
                StartDate = responseDto.StartDate,
                EndDate = responseDto.EndDate,
                Description = responseDto.Description,
                //ModifiedDate = responseDto.ModifiedDate,
                //ModifiedBy = responseDto.ModifiedBy,
                IsActive = responseDto.IsActive,

                ChargableType = responseDto.ChargableType,
                Location = responseDto.Location,
                Expertise = responseDto.Expertise,//Recheck
                BusinessUnit = responseDto.BusinessUnit,
                Smeg = responseDto.Smeg,//Recheck
                RevenueUnit = responseDto.RevenueUnit,//Recheck
                Offerings = responseDto.Offerings,
                Solutions = responseDto.Solutions,
                Industry = responseDto.Industry,
                Subindustry = responseDto.Subindustry,
            };

            var sbPayload = new List<NotificationPayload>
            {
                new NotificationPayload
                {
                    action = Constants.ServiceBusActions.UpdateMarketPlaceProjectDetails,
                    token = notificationParams.token,
                    payload = JsonConvert.SerializeObject(updateMarkeplaceProjectData)
                }
            };
            logger.LogInformation("--initEmailNotificationProcess----inside CreateProjectDetailsUpdateMessage payload creation completed");

            return sbPayload;

        }

        public  List<NotificationPayload> CreateProjectSuspendMessage(InitNotificationDTO notificationParams, ILogger<NotificatiionMiddleware> logger)
        {
            logger.LogInformation("--initEmailNotificationProcess----inside CreateProjectSuspendMessage payload creation started");

            var requestDto = JsonConvert.DeserializeObject<UpdateProjectRequestDTO>(notificationParams.request_payload);
            var responseDto = JsonConvert.DeserializeObject<UpdateProjectResponseDTO>(notificationParams.response_payload);
            var sbPayload = new List<NotificationPayload>();

            foreach (string action in responseDto.Actions)
            {
                InitNotificationDTO notificationDTO = new()
                {
                    path = notificationParams.path,
                    token = notificationParams.token,
                    request_payload = notificationParams.request_payload,
                    response_payload = notificationParams.response_payload
                };
                var payload = new NotificationPayload
                {
                    action = action,
                    token = notificationParams.token,
                    payload = JsonConvert.SerializeObject(notificationDTO)
                };
                sbPayload.Add(payload);
            }

            logger.LogInformation("--initEmailNotificationProcess----inside CreateProjectSuspendMessage payload creation completed");

            return sbPayload;

        }

        public List<NotificationPayload> CreateAllcationSuspendMessage(InitNotificationDTO notificationParams, ILogger<NotificatiionMiddleware> logger)
        {
            logger.LogInformation("--initEmailNotificationProcess----inside CreateAllcationSuspendMessage payload creation started");


            var sbPayload = new List<NotificationPayload>
            {
                new NotificationPayload
                {
                    action = Constants.NotificationTemplateTypes.EMPLOYEE_RELEASE_DUE_TO_SUSPENSION_OF_PROJECT,
                    token = notificationParams.token,
                    payload = JsonConvert.SerializeObject(notificationParams)
                }
            };
            logger.LogInformation("--initEmailNotificationProcess----inside CreateAllcationSuspendMessage payload creation completed");

            return sbPayload;

        }

        public async Task<CreateWorkflowRequestDTO> INITIATE_CREATE_USER_NEW_OR_UPDATE_ALLOCATION_WORKOW(ResourceAllocationDetailsResponse resourceAllocationDetails, string AllocationName, ILogger<NotificatiionMiddleware> logger, bool isUpdating, string token)
        {
            //var workflows = new List<NotificationPayload>();
            logger.LogInformation("--publishWorkflow----inside INITIATE_CREATE_USER_ALLOCATION_WORKOW");
            var pipelineCode = resourceAllocationDetails.PipelineCode;
            var jobCode = resourceAllocationDetails.JobCode;
            var projectDetails = await _projectHttpService.GetProjectDetailsByPipelineCode(pipelineCode, jobCode);
            var offeringName = projectDetails.Offerings;//Recheck
            var buName = projectDetails.bu;
            var buOfferingKey = $"{buName}{Constants.SeparatorPipe}{offeringName}";
            //config Data
            var reviewerConfigurationGroupDetails = await _configurationHttpService.GetConfigurationByConfigGroupConfigKeyAndConfigType(Constants.ConfigurationTypes.RESOURCE_ALLOCATION_REVIEW,
                                                                                                                                        Constants.ConfigurationTypes.RESOURCE_ALLOCATION_REVIEW,
                                                                                                                                        Constants.ConfigurationTypes.CONFIG_TYPE_OFFERINGS);
            var supercoachConfigurationGroupDetails = await _configurationHttpService.GetConfigurationByConfigGroupConfigKeyAndConfigType(Constants.ConfigurationTypes.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                                                                                                                                          Constants.ConfigurationTypes.ALLOCATION_SUPERCOACH_TO_ACCEPT_ALLOCATION,
                                                                                                                                          Constants.ConfigurationTypes.CONFIG_TYPE_OFFERINGS);
            var reviewerConfigDetails = reviewerConfigurationGroupDetails.FirstOrDefault();
            var supercoachConfigDetails = supercoachConfigurationGroupDetails.FirstOrDefault();
            if (reviewerConfigDetails == null || supercoachConfigurationGroupDetails == null)
            {
                logger.LogInformation("--publishWorkflow----inside CONFIGURATION_NOT_FOUND");
                throw new Exception("Configurationn db data not found!");
            }
            var reviewerProjectConfiguration = reviewerConfigDetails.ProjectConfigurations.Where((d) =>
                                                            !string.IsNullOrEmpty(d.AttributeName) &&
                                                            d.AttributeName.Equals(buOfferingKey, StringComparison.OrdinalIgnoreCase) &&
                                                            d.IsActive == true)
                                                            .FirstOrDefault();
            var supercoachProjectConfiguration = supercoachConfigDetails.ProjectConfigurations.Where((d) =>
                                                            !string.IsNullOrEmpty(d.AttributeName) &&
                                                            d.AttributeName.Equals(buOfferingKey, StringComparison.OrdinalIgnoreCase) &&
                                                            d.IsActive == true)
                                                            .FirstOrDefault();
            var reviewerConfigValueString = (reviewerProjectConfiguration != null) ? reviewerProjectConfiguration.AttributeValue : reviewerConfigDetails.AllValue;
            var supercoachConfigValueString = (supercoachProjectConfiguration != null ? supercoachProjectConfiguration.AttributeValue : supercoachConfigDetails.AllValue);
            if (!string.IsNullOrEmpty(reviewerConfigValueString) || !string.IsNullOrEmpty(supercoachConfigValueString))
            {
                var reviewerConfigValue = Int32.Parse(reviewerConfigValueString);
                var supercoachConfigValue = Int32.Parse(supercoachConfigValueString);
                //var configValue = JsonConvert.DeserializeObject<ReviewerAttributeValue>(configValueString);
                ResourceAllocationDetailsResponseForWorkflowMeta entityMeta = new()
                {
                    StartDate = resourceAllocationDetails.StartDate,
                    EndDate = resourceAllocationDetails.EndDate,
                    AllocationStatus = resourceAllocationDetails.AllocationStatus,
                    AllocationVersion = resourceAllocationDetails.AllocationVersion,
                    ConfirmedAllocationDate = resourceAllocationDetails.ConfirmedAllocationDate,
                    CreatedAt = resourceAllocationDetails.CreatedAt,
                    CreatedBy = resourceAllocationDetails.CreatedBy,
                    Description = resourceAllocationDetails.Description,
                    EmpEmail = resourceAllocationDetails.EmpEmail,
                    EmpName = resourceAllocationDetails.EmpName,
                    Id = resourceAllocationDetails.Id,
                    IsActive = resourceAllocationDetails.IsActive,
                    JobCode = resourceAllocationDetails.JobCode,
                    JobName = resourceAllocationDetails.JobName,
                    ModifiedAt = resourceAllocationDetails.ModifiedAt,
                    ModifiedBy = resourceAllocationDetails.ModifiedBy,
                    PipelineCode = resourceAllocationDetails.PipelineCode,
                    PipelineName = resourceAllocationDetails.PipelineName,
                    Requisition = resourceAllocationDetails.Requisition,
                    Grade = resourceAllocationDetails.Requisition.Grade,
                    Designation = resourceAllocationDetails.Requisition.Designation,
                    RequisitionId = resourceAllocationDetails.RequisitionId,
                    TotalEffort = resourceAllocationDetails.TotalEffort,
                    Type = resourceAllocationDetails.Type,

                };

                CreateWorkflowRequestDTO createWorkflow = new()
                {
                    name = AllocationName,
                    item_id = resourceAllocationDetails.Id, //Guid.NewGuid(),//resourceAllocationDetails.Guid
                    module = Constants.WorkflowConstants.WORKFLOW_MODULE_EMPLOYEE_ALLOCATION,
                    sub_module = isUpdating ? Constants.WorkflowConstants.WORKFLOW_SUB_MODULE_EMPLOYEE_ALLOCATION_UPDATE : Constants.WorkflowConstants.WORKFLOW_SUB_MODULE_EMPLOYEE_ALLOCATION,
                    status = Constants.WorkflowConstants.WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER,
                    entity_type = Constants.WorkflowConstants.WORKFLOW_ENTITY_TYPE_RESOURCE_ALLOCATION_RESPONSE,
                    entity_meta_data = entityMeta,
                    assigned_to = ""
                };

                List<NotificationPayload> notificationPayload = new();

                if (!isUpdating)
                {
                    if (reviewerConfigValue == -1 && supercoachConfigValue == -1)
                    {
                        //No Reviewer ,  No SC , TO Employee 
                        createWorkflow.status = Constants.WorkflowConstants.WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE;
                        return createWorkflow;
                    }
                    else if (reviewerConfigValue == -1 && supercoachConfigValue != -1)
                    {
                        //NO Reviewer , Yes SC
                        createWorkflow.status = Constants.WorkflowConstants.WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH;
                        return createWorkflow;
                    }
                }
                else if (isUpdating)
                {
                    if (reviewerConfigValue == -1 && supercoachConfigValue == -1)
                    {
                        createWorkflow.module = "";
                        createWorkflow.sub_module = "";
                        createWorkflow.status = "";
                        entityMeta.ModifiedAt = DateTime.Now;
                        var request = new UpdateAllocationRequestDTO()
                        {
                            guid = resourceAllocationDetails.Id.ToString(),
                            AllocationStatus = AllocationStatuses.EmployeeAllocationAcceptedByEmployee,
                            token = token
                        };
                        InitNotificationDTO payload = new InitNotificationDTO()
                        {
                            path = "",
                            request_payload = JsonConvert.SerializeObject(createWorkflow),
                            response_payload = JsonConvert.SerializeObject(entityMeta),
                            token = token,
                            userinfo = null
                        };
                        notificationPayload.Add(new NotificationPayload
                        {
                            action = NotificationTemplateTypes.NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE,
                            token = token,
                            payload = JsonConvert.SerializeObject(payload)
                        });
                        notificationPayload.Add(new NotificationPayload
                        {
                            action = NotificationTemplateTypes.PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE,
                            token = token,
                            payload = JsonConvert.SerializeObject(payload)
                        });
                        await publishNotification(notificationPayload, logger);
                        UpdateListOfAllocationStatusInResourceAllocationDetailsResponse successResponse = await _allocationHttpService.UpdateListOfAllocationStatus(new List<UpdateAllocationRequestDTO> { request });
                        return null;
                    }
                    else if (reviewerConfigValue == -1 && supercoachConfigValue != -1)
                    {
                        //NO Reviewer , Yes SC
                        createWorkflow.status = Constants.WorkflowConstants.WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_SUPERCOACH;
                        return createWorkflow;
                    }


                }
                logger.LogInformation("--publishWorkflow----inside INITIATE_CREATE_USER_ALLOCATION_WORKOW ", createWorkflow);
                return createWorkflow;
            }
            else
            {
                logger.LogInformation("--publishWorkflow----INITIATE_USER_ALLOCATION_WORKFLOW Configuration db data not found! ");
                throw new Exception("Configuration db data not found!");
            }

        }

        public async Task<List<NotificationPayload>> CREATE_USER_ALLOCATION_WORKFLOW_BY_COMMON_SCREEN(InitNotificationDTO notificationParams, ILogger<NotificatiionMiddleware> logger)
        {
            logger.LogInformation("--publishWorkflow----inside CREATE_USER_ALLOCATION_WORKFLOW_BY_COMMON_SCREEN");


            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationParams.request_payload);
            List<CommonAllocationRequestDTO> requests = JsonConvert.DeserializeObject<List<CommonAllocationRequestDTO>>(requestPayload.body);
            List<ResourceAllocationDetailsResponse> resourceAllocationDetailsResponses = JsonConvert.DeserializeObject<List<ResourceAllocationDetailsResponse>>(notificationParams.response_payload);
            var workflows = new List<NotificationPayload>();
            foreach (ResourceAllocationDetailsResponse allocationResponse in resourceAllocationDetailsResponses)
            {

                var requestItem = requests.Where(m => m.Email.ToLower().Trim() == allocationResponse.EmpEmail.ToLower().Trim()).FirstOrDefault();
                if (requestItem != null)
                {

                    if (allocationResponse.AllocationStatus == RequistionStatuses.DRAFT)
                    {
                        continue;
                    }
                    else if (allocationResponse.AllocationStatus == RequistionStatuses.PENDING_APPROVAL && requestItem.type == EAllocationType.UPDATE_ALLOCATION && !requestItem.isPreviouslyDraft)
                    {
                        string allocationName = GET_ALLOCATION_TYPE(allocationResponse.Requisition.RequisitionType.Type);
                        var createWorkflow = await INITIATE_CREATE_USER_NEW_OR_UPDATE_ALLOCATION_WORKOW(allocationResponse, allocationName, logger, true, notificationParams.token);
                        var workflowNotificationPayload = new NotificationPayload()
                        {
                            action = Constants.CreateUserAllocationWorkflowAction,
                            token = notificationParams.token,
                            payload = JsonConvert.SerializeObject(createWorkflow).ToString()
                        };
                        workflows.Add(workflowNotificationPayload);
                    }
                    else if (allocationResponse.AllocationStatus == RequistionStatuses.PENDING_APPROVAL)
                    {
                        string allocationName = GET_ALLOCATION_TYPE(allocationResponse.Requisition.RequisitionType.Type);
                        var createWorkflow = await INITIATE_CREATE_USER_NEW_OR_UPDATE_ALLOCATION_WORKOW(allocationResponse, allocationName, logger, false, notificationParams.token);
                        var workflowNotificationPayload = new NotificationPayload()
                        {
                            action = Constants.CreateUserAllocationWorkflowAction,
                            token = notificationParams.token,
                            payload = JsonConvert.SerializeObject(createWorkflow).ToString()
                        };
                        workflows.Add(workflowNotificationPayload);
                    }
                }
            }
            logger.LogInformation("--publishWorkflow----inside CREATE_USER_ALLOCATION_WORKFLOW_BY_COMMON_SCREEN ENDS ", workflows);
            return workflows;
        }

        public string GET_ALLOCATION_TYPE(string allocationType)
        {
            switch (allocationType)
            {
                case RequisitionTypeData.NamedAllocation:
                    return Constants.WorkflowConstants.WORKFLOW_NAME_SAME_TEAM_ALLOCATION;
                case RequisitionTypeData.SameTeamAllocation:
                    return Constants.WorkflowConstants.WORKFLOW_NAME_SAME_TEAM_ALLOCATION;
                case RequisitionTypeData.CreateRequisition:
                    return Constants.WorkflowConstants.WORKFLOW_NAME_SAME_TEAM_ALLOCATION;
                case RequisitionTypeData.BulkAllocation:
                    return Constants.WorkflowConstants.WORKFLOW_NAME_SAME_TEAM_ALLOCATION;
                default:
                    return string.Empty;
                    break;
            }
        }

        public async Task<bool> UpdateUserSkillStatusAfterWorkflow(InitNotificationDTO notificationParams, string workflow_type)
        {
            List<UpdateUserSkillStatusAfterWorkflowRequestDTO> requests = new List<UpdateUserSkillStatusAfterWorkflowRequestDTO>();
            switch (workflow_type)
            {
                //case Constants.WorkflowConstants.CREATE_WORKFLOW_TITLE:
                //    break;
                case Constants.WorkflowConstants.UPDATE_WORKFLOW_TITLE:
                    List<BulkWorkflowApprovalResponselDTO> workflowResponse = JsonConvert.DeserializeObject<List<BulkWorkflowApprovalResponselDTO>>(notificationParams.response_payload);
                    foreach (var workflow in workflowResponse)
                    {
                        if (!workflow.isError)
                        {
                            requests.Add(new()
                            {
                                Id = workflow.workflowResult.item_id,
                                ModifiedAt = DateTime.UtcNow,
                                ModifiedBy = workflow.result[0].updated_by,
                                ActionPerformed = workflow.result[0].status.ToLower().Trim() == Constants.WorkflowConstants.WORKFLOW_TASK_STATUS_APPROVED.ToLower().Trim() ? UpdateUserSkillWorkflowActions.APPROVED : UpdateUserSkillWorkflowActions.REJECTED
                            });
                        }
                    }
                    break;
            }
            if (!requests.IsNullOrEmpty())
            {
                //await _skillsHttpService.UpdateUserSkillStatusAfterWorkflow(requests);
                return true;
            }
            return true;
        }

        public async Task<bool> UpdateAllocationListStatus(InitNotificationDTO notificationParams, string workflow_type)
        {
            List<UpdateAllocationRequestDTO> requests = new List<UpdateAllocationRequestDTO>();
            switch (workflow_type)
            {
                case Constants.WorkflowConstants.CREATE_WORKFLOW_TITLE:
                    WorkflowDTO responseBody = JsonConvert.DeserializeObject<WorkflowDTO>(notificationParams.response_payload);
                    var workflowStatus = responseBody.status;
                    var allocationItemId = responseBody.item_id;
                    requests.Add(new UpdateAllocationRequestDTO()
                    {
                        AllocationStatus = workflowStatus,
                        guid = allocationItemId,
                        WorkflowModule = responseBody.module,
                        WorkflowSubModule = responseBody.sub_module,
                        token = notificationParams.token
                    });
                    break;
                case Constants.WorkflowConstants.UPDATE_WORKFLOW_TITLE:
                    List<BulkWorkflowApprovalResponselDTO> workflowResponse = JsonConvert.DeserializeObject<List<BulkWorkflowApprovalResponselDTO>>(notificationParams.response_payload);
                    foreach (var workflow in workflowResponse)
                    {
                        if (!workflow.isError)
                        {
                            requests.Add(new UpdateAllocationRequestDTO()
                            {
                                AllocationStatus = workflow.workflowResult.status,
                                guid = workflow.workflowResult.item_id,
                                WorkflowModule = workflow.workflowResult.module,
                                WorkflowSubModule = workflow.workflowResult.sub_module,
                                token = notificationParams.token
                            });
                        }
                    }
                    break;
                default:
                    break;
            }
            if (!requests.IsNullOrEmpty())
            {
                //await _allocationHttpService.UpdateListOfAllocationStatus(requests);
                return true;
            }
            return true;
        }

        public async Task<bool> UpdateRespectiveTablesForCreateWorkflow(InitNotificationDTO notificationParams, string workflow_type)
        {
            try
            {
                List<Task> taskList = new();
                WorkflowDTO responseBodyWorkflow = JsonConvert.DeserializeObject<WorkflowDTO>(notificationParams.response_payload);

                if (responseBodyWorkflow != null)
                {
                    switch (true)
                    {
                        case bool flag when responseBodyWorkflow.module
                            .Equals(Constants.WorkflowConstants.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT, StringComparison.InvariantCultureIgnoreCase):
                            taskList.Add(UpdateUserSkillStatusAfterWorkflow(notificationParams, workflow_type));
                            break;
                        case bool flag when responseBodyWorkflow.module
                            .Equals(Constants.WorkflowConstants.WORKFLOW_MODULE_EMPLOYEE_ALLOCATION, StringComparison.InvariantCultureIgnoreCase):
                            taskList.Add(UpdateAllocationListStatus(notificationParams, workflow_type));
                            break;
                        default:
                            break;
                    }
                }

                await Task.WhenAll(taskList);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> UpdateRespectiveTablesForWorkflowForBulkApproval(InitNotificationDTO notificationParams, string workflow_type)
        {
            try
            {
                List<Task> taskList = new();
                List<BulkWorkflowApprovalResponselDTO> responseBodyWorkflow = JsonConvert.DeserializeObject<List<BulkWorkflowApprovalResponselDTO>>(notificationParams.response_payload);
                if (responseBodyWorkflow.Count > 0)
                {
                    foreach (var item in responseBodyWorkflow)
                    {
                        switch (true)
                        {
                            case bool flag when item.workflowResult.module
                            .Equals(Constants.WorkflowConstants.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT, StringComparison.InvariantCultureIgnoreCase):
                                taskList.Add(UpdateUserSkillStatusAfterWorkflow(notificationParams, workflow_type));
                                break;
                            case bool flag when item.workflowResult.module
                            .Equals(Constants.WorkflowConstants.WORKFLOW_MODULE_EMPLOYEE_ALLOCATION, StringComparison.InvariantCultureIgnoreCase):
                                taskList.Add(UpdateAllocationListStatus(notificationParams, workflow_type));
                                break;
                            default:
                                break;
                        }
                    }
                }
                await Task.WhenAll(taskList);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Publish a workflow to topic
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task publishWorkflow(List<NotificationPayload> payload, ILogger<NotificatiionMiddleware> logger)
        {
            Console.WriteLine("--publishWorkflow--");
            logger.LogInformation("--publishWorkflow-- inside publish wf");
            string type = Constants.WorkflowTypeNotification;
            foreach (var item in payload)
            {
                //!TODO re-initialize service bus
                NotificationPayloadDTO payloadDTO = new()
                {
                    payload = item.payload,
                    action = item.action,
                    token = item.token,
                };

                string useServiceBusCalls = _config.GetSection("MicroserviceApiSettings").GetSection("UseServiceBusCalls").Value;

                if (useServiceBusCalls == "true")
                {
                    logger.LogInformation("--publishWorkflow-- publishing to service layer-{0},{1}", JsonConvert.SerializeObject(item), type);
                    await _azureServiceBusService.PublishMessageOnAzureServiceBus(item, type, logger);
                }
                else
                {
                    logger.LogInformation("--publishWorkflow-- publishing directly wf to service-{0}", payloadDTO);
                    await _workflowService.initWorkflow(payloadDTO);
                }
            }
        }

        /// <summary>
        /// Publush a notification can be push or email or both
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task publishNotification(List<NotificationPayload> payload, ILogger<NotificatiionMiddleware> logger)
        {
            string type = Constants.NotificationTypeNotification;
            foreach (var item in payload)
            {
                await _azureServiceBusService.PublishMessageOnAzureServiceBus(item, type, logger);
            }
        }

        public async Task<List<NotificationPayload>> CREATE_USER_SKILL_ASSESSMENT_WORKFLOW(InitNotificationDTO paramsPayload)
        {
            try
            {
                RequestPayloadDTO requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(paramsPayload.request_payload);
                List<AddUpdateUserSkillsRequestDTO> addedOrUpdatedSkillsRequest = JsonConvert.DeserializeObject<List<AddUpdateUserSkillsRequestDTO>>(requestPayload.body);
                List<AddUpdateUserSkillsResponseDTO> addedOrUpdatedSkills = JsonConvert.DeserializeObject<List<AddUpdateUserSkillsResponseDTO>>(paramsPayload.response_payload);
                UserInfoDTO userInfo = JsonConvert.DeserializeObject<UserInfoDTO>(paramsPayload.userinfo);
                string super_mid = !string.IsNullOrEmpty(userInfo.supercoach_mid) ? userInfo.supercoach_mid : userInfo.co_supercoach_mid;
                var supercoach_delegate = await _identityHttpServices.GetSupercoachDelegateByUserMid(super_mid);
                string assigned_to = !string.IsNullOrEmpty(userInfo.supercoach_mid) ? userInfo.supercoach_name.ToLower().Trim() : userInfo.co_supercoach_name.ToLower().Trim();
                dynamic obj = new ExpandoObject();
                obj.supercoach_email = assigned_to.ToLower().Trim();
                obj.skill_delegate_email = null;
                if (supercoach_delegate != null && supercoach_delegate.skill_delegate_email != null)
                {
                    assigned_to = $"{assigned_to},{supercoach_delegate.skill_delegate_email.ToLower().Trim()}";
                    obj.skill_delegate_email = supercoach_delegate.skill_delegate_email.ToLower().Trim();
                }
                var assign_to_json = new List<dynamic> { obj };
                var workflows = new List<NotificationPayload>();
                if (userInfo != null)
                {

                    if (!String.IsNullOrEmpty(userInfo.supercoach_name) || !String.IsNullOrEmpty(userInfo.co_supercoach_name))
                    {
                        foreach (var skillItem in addedOrUpdatedSkills)
                        {
                            var requestItem = addedOrUpdatedSkillsRequest.Where(m => m.skillCode.ToLower().Trim().Equals(skillItem.SkillCode.ToLower().Trim())).FirstOrDefault();

                            if (requestItem != null && (skillItem.Status == UserSkillsStatus.PENDING || skillItem.Status == UserSkillsStatus.PENDING_APPROVAL))
                            {
                                CreateWorkflowRequestDTO createWorkflowRequest = new CreateWorkflowRequestDTO
                                {
                                    name = Constants.WorkflowConstants.USER_SKILL_ASSESSMENT_WORKFLOW_FOR_SUPERCOACH,
                                    item_id = skillItem.Id, //Guid.NewGuid(),//resourceAllocationDetails.Guid
                                    module = Constants.WorkflowConstants.WORKFLOW_MODULE_USER_SKILL_ASSESSMENT,
                                    sub_module = Constants.WorkflowConstants.WORKFLOW_SUB_MODULE_USER_SKILL_ASSESSMENT,
                                    status = userInfo.supercoach_name != null
                                                    ? Constants.WorkflowConstants.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_SUPERCOACH_PENDING
                                                    : Constants.WorkflowConstants.WORKFLOW_STATUS_USER_SKILL_ASSESSMENT_CO_SUPERCOACH_PENDING,
                                    entity_type = Constants.WorkflowConstants.WORKFLOW_ENTITY_TYPE_RESOURCE_ALLOCATION_RESPONSE,
                                    entity_meta_data = skillItem,
                                    assigned_to = assigned_to,
                                    assigned_to_json = assign_to_json,
                                    comments = requestItem.comments
                                };
                                var workflowNotificationPayload = new NotificationPayload()
                                {
                                    action = Constants.CreateUserSkillAssessmentWorkflowAction,
                                    token = paramsPayload.token,
                                    payload = JsonConvert.SerializeObject(createWorkflowRequest).ToString()
                                };
                                workflows.Add(workflowNotificationPayload);
                            }
                        }
                        return workflows;
                    }
                    else
                    {
                        throw new Exception($"No Supercoach or co-supercoach assigned to user {userInfo.email}");
                    }
                }
                else
                {
                    throw new Exception("User Not found");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RemoveAllDraftAllocationsAfterUserIsDeactivated(InitNotificationDTO notificationParams)
        {
            var requestPayload = JsonConvert.DeserializeObject<RequestPayloadDTO>(notificationParams.request_payload);

            var requestQueryKeyValuePairs = JsonConvert.DeserializeObject<List<KeyValuePair<string, string[]>>>(requestPayload.query);
            var deactivatedUser = requestQueryKeyValuePairs.Find(m => m.Key.Equals("emailId")).Value.ToList();
            UpdateUserDto requestBody = JsonConvert.DeserializeObject<UpdateUserDto>(requestPayload.body);
            if (requestBody.status == false && deactivatedUser != null && deactivatedUser.Count > 0)
            {
                await _allocationHttpService.RemoveAllDraftAllocationsAfterUserIsDeactivated(deactivatedUser);
            }
            return true;
        }
    }
}
