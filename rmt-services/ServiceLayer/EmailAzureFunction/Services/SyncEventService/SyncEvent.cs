using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;
using Azure.Core;
using ServiceLayer.Services.AllocationService.DTOs;
using ServiceLayer.Services.IdentityService;
using ServiceLayer.Services.WorkflowService;
using ServiceLayer.Services.NotificationService;
using ServiceLayer.Constants;
using ServiceLayer.Services.ProjectService;

namespace ServiceLayer.Services.SyncEventService
{

    public class SyncEvent : ISyncEvent
    {
        private readonly ILogger<SyncEvent> _logger;
        private readonly IAllocationService _allocationService;
        private readonly IIdentityService _identityHttpServices;
        private readonly IWorkflowService _workflowService;
        private readonly INotificationService _notificationService;
        private readonly IProjectService _projectService;
        public SyncEvent(ILogger<SyncEvent> log, IAllocationService allocationService, IIdentityService identityHttpServices, IWorkflowService workflowService, INotificationService notificationService, IProjectService projectService)
        {
            _logger = log;
            _allocationService = allocationService;
            _identityHttpServices = identityHttpServices;
            _workflowService = workflowService;
            _notificationService = notificationService;
            _projectService = projectService;
        }

        public async Task initSyncEvent(SyncEventPayloadDTO payload)
        {
            _logger.LogInformation($"SyncEvent--initSyncEvent--Start action message: {payload.action}");
            List<string> affectedRoles = new() { UserRoles.Reviewer, UserRoles.ResourceRequestor };
            var wcgtEmployee = new WcgtEmployeeDTO();
            var user = new User();
            var rmsProject = new Project();
            var wcgtJobs = new WcgtJobDTO();
            switch (payload.action)
            {
                case ServiceBusActions.EMPLOYEE_SUPERCOACH_CHANGE:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.EMPLOYEE_SUPERCOACH_CHANGE, payload);
                    wcgtEmployee = JsonConvert.DeserializeObject<WcgtEmployeeDTO>(payload.source_table_row);
                    user = JsonConvert.DeserializeObject<User>(payload.destination_table_row);
                    if (wcgtEmployee != null && (!(string.IsNullOrEmpty(wcgtEmployee.supercoach_mid) && string.IsNullOrEmpty(user.supercoach_mid)) || (string.IsNullOrEmpty(wcgtEmployee.supercoach_mid) && !wcgtEmployee.supercoach_mid.Equals(user.supercoach_mid))))
                    {
                        var _previousAssignTo = string.IsNullOrEmpty(user.supercoach_mid) ? string.Empty : user.supercoach_name;
                        var _currentAssignTo = string.IsNullOrEmpty(wcgtEmployee.supercoach_mid) ? string.Empty : wcgtEmployee.supercoach_name;

                        RefreshAssignedWorkflowTaskDTO req = new()
                        {
                            employeeEmail = user.email_id,
                            previousAssignTo = _previousAssignTo,
                            currentAssignTo = _currentAssignTo,
                            type = "workflow_module_user_skill_assessment"
                        };
                        await _workflowService.RefreshAssignedTask(req, payload.token);
                        if (!string.IsNullOrEmpty(_previousAssignTo))
                        {
                            List<WorkflowTaskDTO> workflowTasks = await _workflowService.GetWorkflowSuperCoachTask(user.email_id, _previousAssignTo, payload.token);
                            if (workflowTasks.Count > 0)
                            {
                                RefreshAssignedWorkflowTaskDTO reqTask = new()
                                {
                                    employeeEmail = user.email_id,
                                    previousAssignTo = _previousAssignTo,
                                    currentAssignTo = _currentAssignTo,
                                    type = "workflow_module_user_supercoach_assessment"
                                };
                                await _workflowService.RefreshAssignedTask(reqTask, payload.token);
                            }
                            SuperCoachProjectRole superCoachProjectRolePayload = new SuperCoachProjectRole();
                            superCoachProjectRolePayload.PreviouseUser = _previousAssignTo;
                            superCoachProjectRolePayload.User = _currentAssignTo;
                            HashSet<(string PipelineCode, string JobCode)> uniqueProjects = new();
                            List<ProjectCode> projectcode = new List<ProjectCode>();
                            foreach (WorkflowTaskDTO task in workflowTasks)
                            {

                                var projectKey = (task.Workflow.EntityMetaData.PipelineCode, task.Workflow.EntityMetaData.JobCode);
                                if (!uniqueProjects.Contains(projectKey))
                                {
                                    uniqueProjects.Add(projectKey);
                                    projectcode.Add(new ProjectCode
                                    {
                                        PiplelineCode = projectKey.PipelineCode,
                                        JobCode = projectKey.JobCode
                                    });
                                }

                            }

                            if (projectcode.Any())
                            {
                                superCoachProjectRolePayload.ProjectCodes = projectcode;
                                await _projectService.ReplaceProjectsSuperCoachRole(superCoachProjectRolePayload, payload.token);
                            }
                        }

                    }
                    break;
                case ServiceBusActions.EMPLOYEE_CO_SUPERCOACH_CHANGE:
                    //_logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.EMPLOYEE_CO_SUPERCOACH_CHANGE, payload);
                    //wcgtEmployee = JsonConvert.DeserializeObject<WcgtEmployeeDTO>(payload.source_table_row);
                    //user = JsonConvert.DeserializeObject<User>(payload.destination_table_row);
                    ////if (!wcgtEmployee.supercoach_mid.Equals(user.supercoach_mid))
                    //if (wcgtEmployee != null && (!(string.IsNullOrEmpty(wcgtEmployee.reporting_partner_mid) && string.IsNullOrEmpty(user.co_supercoach_mid)) || (string.IsNullOrEmpty(wcgtEmployee.reporting_partner_mid) && !wcgtEmployee.reporting_partner_mid.Equals(user.co_supercoach_mid))))
                    //{
                    //    RefreshAssignedWorkflowTaskDTO req = new()
                    //    {
                    //        employeeEmail = user.email_id,
                    //        previousAssignTo = string.IsNullOrEmpty(user.co_supercoach_mid) ? string.Empty : user.co_supercoach_name,
                    //        currentAssignTo = string.IsNullOrEmpty(wcgtEmployee.reporting_partner_mid) ? string.Empty : wcgtEmployee.co_supercoach_name,
                    //        type = "workflow_module_user_skill_assessment"
                    //    };
                    //    await _workflowService.RefreshAssignedTask(req, payload.token);
                    //}
                    break;
                case ServiceBusActions.PROJECT_ROLE_UPDATE:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.PROJECT_ROLE_UPDATE, payload);
                    var wcgtJobRoles = JsonConvert.DeserializeObject<WCGTJobRoles>(payload.source_table_row);
                    var projectRoles = JsonConvert.DeserializeObject<ProjectRoles>(payload.destination_table_row);
                    if (wcgtJobRoles.user_transformed_role.ToLower().Trim() == projectRoles.Role.ToLower().Trim()
                        && wcgtJobRoles.user_with_empid.ToLower().Trim() != projectRoles.User.ToLower().Trim()
                        && affectedRoles.Any(e => e.ToLower().Trim() == projectRoles.ApplicationRole.ToLower().Trim())
                    )
                    {
                        RefreshAssignedWorkflowTaskDTO req = new()
                        {
                            pipelineCode = wcgtJobRoles.pipeline_code,
                            jobCode = wcgtJobRoles.job_code,
                            previousAssignTo = projectRoles.User,
                            currentAssignTo = wcgtJobRoles.user_with_empid,
                            type = "Employee Allocation"
                        };
                        await _workflowService.RefreshAssignedTask(req, payload.token);
                    }
                    break;
                case ServiceBusActions.PROJECT_PIPELINE_ROLE_UPDATE:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.PROJECT_PIPELINE_ROLE_UPDATE, payload);
                    var wcgtPipelineRoles = JsonConvert.DeserializeObject<WCGTPipelineRoles>(payload.source_table_row);
                    var projectAssignedRoles = JsonConvert.DeserializeObject<ProjectRoles>(payload.destination_table_row);
                    if (wcgtPipelineRoles.user_transformed_role.ToLower().Trim() == projectAssignedRoles.Role.ToLower().Trim()
                        && wcgtPipelineRoles.user_with_empid.ToLower().Trim() != projectAssignedRoles.User.ToLower().Trim()
                        && affectedRoles.Any(e => e.ToLower().Trim() == projectAssignedRoles.ApplicationRole.ToLower().Trim())
                    )
                    {
                        RefreshAssignedWorkflowTaskDTO req = new()
                        {
                            pipelineCode = wcgtPipelineRoles.pipeline_code,
                            jobCode = null,
                            previousAssignTo = projectAssignedRoles.User,
                            currentAssignTo = wcgtPipelineRoles.user_with_empid,
                            type = "Employee Allocation"
                        };
                        await _workflowService.RefreshAssignedTask(req, payload.token);
                    }
                    break;
                case ServiceBusActions.PROJECT_ROLL_FORWARD_EVENT:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.PROJECT_ROLL_FORWARD_EVENT, payload);
                    //call rollforward related api

                    var wcgtJob = JsonConvert.DeserializeObject<WcgtJobDTO>(payload.source_table_row);
                    Project rmsProjectJob = JsonConvert.DeserializeObject<Project>(payload.destination_table_row);
                    AllocationRolloverResponseDTO result = await _allocationService.RolloverAllocationByPipelineCode(wcgtJob, rmsProjectJob, payload.token);
                    if (result != null)
                    {
                        _logger.LogInformation("--ServiceBus--RolloverAllocationByPipelineCode-- Start " + JsonConvert.SerializeObject(result));
                        await this.RolloverAllocationWorkflowProcessing(result, payload);
                        _logger.LogInformation("--ServiceBus--RolloverAllocationByPipelineCode-- End " + JsonConvert.SerializeObject(result));
                    }
                    else
                    {
                        _logger.LogInformation("--ServiceBus--RolloverAllocationByPipelineCode-- result is null");
                    }
                    _logger.LogInformation("--ServiceBus--SyncEvent-- End" + ServiceBusActions.PROJECT_ROLL_FORWARD_EVENT, payload);

                    break;
                case ServiceBusActions.PROJECT_ACTIVATION_STATUS_EVENT:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- PROJECT_ACTIVATION_STATUS_EVENT " + ServiceBusActions.PROJECT_STATUS_CHANGE_EVENT, payload);
                    wcgtJob = JsonConvert.DeserializeObject<WcgtJobDTO>(payload.source_table_row);
                    rmsProject = JsonConvert.DeserializeObject<Project>(payload.destination_table_row);
                    if (wcgtJob.isactive != null && rmsProject.ProjectActivationStatus != null && wcgtJob.isactive == false && rmsProject.ProjectActivationStatus == true)
                    {
                        await _projectService.ProjectActivationStatusChange(rmsProject.PipelineCode, string.IsNullOrEmpty(rmsProject.JobCode) ? null : rmsProject.JobCode, payload.token, false);
                    }
                    break;
                case ServiceBusActions.PROJECT_CLOSURE_STATUS_EVENT:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- PROJECT_ACTIVATION_STATUS_EVENT " + ServiceBusActions.PROJECT_CLOSURE_STATUS_EVENT, payload);
                    wcgtJob = JsonConvert.DeserializeObject<WcgtJobDTO>(payload.source_table_row);
                    rmsProject = JsonConvert.DeserializeObject<Project>(payload.destination_table_row);
                    if (wcgtJob.closed_job != null && rmsProject.ProjectClosureState != null && wcgtJob.closed_job == true && rmsProject.ProjectClosureState == false)
                    {
                        await _projectService.ProjectActivationStatusChange(rmsProject.PipelineCode, string.IsNullOrEmpty(rmsProject.JobCode) ? null : rmsProject.JobCode, payload.token, true);
                    }
                    break;
                case ServiceBusActions.PROJECT_STATUS_CHANGE_EVENT:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.PROJECT_STATUS_CHANGE_EVENT, payload);
                    var wcgt = JsonConvert.DeserializeObject<WCGTPipelineDTO>(payload.source_table_row);

                    rmsProject = JsonConvert.DeserializeObject<Project>(payload.destination_table_row);
                    if (wcgt.pipeline_status.ToLower().Trim() == SUSPEND_STATUS.Trim().ToLower() || wcgt.pipeline_status.ToLower().Trim() == LOST_STATUS.Trim().ToLower())
                    {
                        Project projectPipeline = JsonConvert.DeserializeObject<Project>(payload.destination_table_row);
                        projectPipeline.PipelineStatus = wcgt.pipeline_status;
                        projectPipeline.SuspendedModifyAt = DateTime.Now;
                        InitNotificationDTO notificationDto = new InitNotificationDTO
                        {
                            path = "",
                            request_payload = null,
                            response_payload = JsonConvert.SerializeObject(projectPipeline),
                            token = payload.token,
                            userinfo = null
                        };

                        NotificationPayloadDTO notificationPayload = new NotificationPayloadDTO
                        {
                            action = PROJECT_SUSPENSION_NOTIFICATION,
                            token = payload.token,
                            payload = JsonConvert.SerializeObject(notificationDto),
                        };
                        await _notificationService.InitialiseNotificationTemplates(notificationPayload);
                        List<string> projectPipelines = new List<string>() { rmsProject.PipelineCode };
                        await _projectService.SuspendProjects(projectPipelines, payload.token);
                        _logger.LogInformation("--ServiceBus--SyncEvent-- End " + ServiceBusActions.PIPELINE_SUSPEND_EVENT, payload);
                    }
                    break;
                case ServiceBusActions.DESIGNATION_CHANGE_EVENT:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.DESIGNATION_CHANGE_EVENT, payload);
                    wcgtEmployee = JsonConvert.DeserializeObject<WcgtEmployeeDTO>(payload.source_table_row);
                    var request = new DesignationUpdateDTO
                    {
                        EmpEmail = wcgtEmployee.employee_mid + "__" + wcgtEmployee.email_id,
                        Designation = wcgtEmployee.designation_name,
                        UpdateDate = (DateTime)wcgtEmployee.modifiedat,
                        Grade = wcgtEmployee.grade,
                        RatePerHour = (int?)wcgtEmployee.rateperhour
                    };
                    await _allocationService.UpdateDesignation(request, payload.token);
                    _logger.LogInformation("--ServiceBus--SyncEvent-- End " + ServiceBusActions.DESIGNATION_CHANGE_EVENT, payload);
                    break;

                case ServiceBusActions.EMPLOYEE_STATUS_CHANGE_EVENT:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.EMPLOYEE_STATUS_CHANGE_EVENT, payload);
                    var wcgtEmployeeData = JsonConvert.DeserializeObject<WcgtEmployeeDTO>(payload.source_table_row);
                    if (wcgtEmployeeData != null
                            && !string.IsNullOrEmpty(wcgtEmployeeData.employee_status)
                            && (wcgtEmployeeData.employee_status.Equals(Absconder, StringComparison.OrdinalIgnoreCase))
                        )
                    {
                        //ABSCONDING

                        var allocations = await _allocationService.GetActiveAllocationByEmail(wcgtEmployeeData.employee_mid + "__" + wcgtEmployeeData.email_id, payload.token);
                        var activeAllocations = allocations.Where(a => a.EndDate >= DateOnly.FromDateTime(DateTime.Now)).ToList();
                        List<string> uniquePipelineCode = new List<string>();
                        foreach (var allocation in activeAllocations)
                        {
                            if (!uniquePipelineCode.Contains(allocation.PipelineCode))
                            {
                                uniquePipelineCode.Add(allocation.PipelineCode);
                                InitNotificationDTO initNotificationDTO = new InitNotificationDTO
                                {
                                    path = "",
                                    request_payload = null,
                                    response_payload = JsonConvert.SerializeObject(allocation),
                                    token = payload.token,
                                    userinfo = null,
                                };
                                NotificationPayloadDTO notificationPayload = new NotificationPayloadDTO
                                {
                                    action = NotificationTemplateTypes.EMPLOYEE_ABSCONDING_NOTIFCATION,
                                    token = payload.token,
                                    payload = JsonConvert.SerializeObject(initNotificationDTO),
                                };
                                await _notificationService.InitialiseNotificationTemplates(notificationPayload);
                                _logger.LogInformation("--ServiceBus--SyncEvent-- End " + ServiceBusActions.EMPLOYEE_STATUS_CHANGE_EVENT, payload);
                            }
                        }
                    }
                    else if (wcgtEmployeeData != null && !string.IsNullOrEmpty(wcgtEmployeeData.employee_status) &&
                              (wcgtEmployeeData.employee_status.Equals(Voluntary, StringComparison.OrdinalIgnoreCase)
                              || wcgtEmployeeData.employee_status.Equals(Involuntary, StringComparison.OrdinalIgnoreCase)
                              || wcgtEmployeeData.employee_status.Equals(Termination, StringComparison.OrdinalIgnoreCase)))
                    {
                        var allocations = await _allocationService.GetActiveAllocationByEmail(wcgtEmployeeData.email_id, payload.token);
                        var activeAllocations = allocations.Where(a => a.EndDate >= DateOnly.FromDateTime(DateTime.Now)).ToList();
                        List<string> uniquePipelineCode = new List<string>();
                        foreach (var allocation in activeAllocations)
                        {
                            if (!uniquePipelineCode.Contains(allocation.PipelineCode))
                            {
                                uniquePipelineCode.Add(allocation.PipelineCode);
                                InitNotificationDTO initNotificationDTO = new InitNotificationDTO
                                {
                                    path = "",
                                    request_payload = null,
                                    response_payload = JsonConvert.SerializeObject(allocation),
                                    token = payload.token,
                                    userinfo = null,
                                };
                                NotificationPayloadDTO notificationPayload = new NotificationPayloadDTO
                                {
                                    action = NotificationTemplateTypes.ALLOCATION_CONFLICT_NOTIFICATION,
                                    token = payload.token,
                                    payload = JsonConvert.SerializeObject(initNotificationDTO),
                                };
                                await _notificationService.InitialiseNotificationTemplates(notificationPayload);
                                _logger.LogInformation("--ServiceBus--SyncEvent-- End " + ServiceBusActions.EMPLOYEE_STATUS_CHANGE_EVENT, payload);
                            }
                        }
                    }

                    break;
                case ServiceBusActions.EMPLOYEE_LEAVE_EVENT:
                    _logger.LogInformation("--ServiceBus--SyncEvent-- Start " + ServiceBusActions.EMPLOYEE_LEAVE_EVENT, payload);
                    var employeeLeave = JsonConvert.DeserializeObject<WCGTLeaves>(payload.source_table_row);
                    if (employeeLeave != null)
                    {

                        DateTime startDate = employeeLeave.leave_start_date;
                        DateTime endDate = employeeLeave.leave_end_date;
                        string enployeeEmailWithMid = employeeLeave.emp_mid + "__" + employeeLeave.emp_email;
                        var allocations = await _allocationService.GetAllocationInformationByLeaves(enployeeEmailWithMid, startDate, endDate, payload.token);
                        if (allocations != null && allocations.Count > 0)
                        {
                            var groupedAllocations = allocations.GroupBy(g => new { g.PipelineCode, g.JobCode });
                            //List <PipelineCodeAndRolesDto> pipelineCodesAndRoles = new();
                            //foreach (var group in groupedAllocations)
                            //{
                            //    PipelineCodeAndRolesDto pipelineCodeAndRole = new()
                            //    {
                            //        pipelineCode = group.Key.PipelineCode,
                            //        jobCode = group.Key.JobCode != null ? group.Key.JobCode : null
                            //    };
                            //}
                            //var projectRoles = await _projectService.GetRolesEmailByPipelineCodesAndRoles(pipelineCodesAndRoles , payload.token);
                            //LEAVES
                            foreach (var group in groupedAllocations)
                            {
                                NotificationLeavePayload leavePayload = new NotificationLeavePayload()
                                {
                                    PipelineCode = group.Key.PipelineCode,
                                    JobCode = group.Key.JobCode != null ? group.Key.JobCode : null,
                                    LeaveStartDate = Convert.ToDateTime(employeeLeave.leave_start_date),
                                    LeaveEndDate = Convert.ToDateTime(employeeLeave.leave_end_date),
                                    EmployeeEmail = enployeeEmailWithMid
                                };
                                InitNotificationDTO initNotificationDTO = new InitNotificationDTO()
                                {
                                    path = "",
                                    token = payload.token,
                                    request_payload = null,
                                    response_payload = JsonConvert.SerializeObject(leavePayload)
                                };
                                NotificationPayloadDTO notificationPayload = new NotificationPayloadDTO
                                {
                                    action = NotificationTemplateTypes.EMPLOYEE_LEAVE_NOTIFCATION,
                                    token = payload.token,
                                    payload = JsonConvert.SerializeObject(initNotificationDTO)
                                };
                                await _notificationService.InitialiseNotificationTemplates(notificationPayload);
                            }
                            _logger.LogInformation("--ServiceBus--SyncEvent-- End  " + ServiceBusActions.EMPLOYEE_LEAVE_EVENT, payload);
                        }
                    }
                    break;
                default:
                    break;
            }

            _logger.LogInformation($"SyncEvent--initSyncEvent--End");

        }

        public async Task RolloverAllocationWorkflowProcessing(AllocationRolloverResponseDTO request, SyncEventPayloadDTO payload)
        {
            try
            {
                _logger.LogInformation("RolloverAllocationWorkflowProcessing-Started");
                List<NotificationPayloadDTO> allWorkflowToStart = request.allWorkflowToStart;
                List<TerminateWorkflowDTO> allWorkflowToTerminate = request.allWorkflowToTerminate;

                //requestMessage += "GetUserInfo-Execute-Start";
                //UserInfoDTO userInfo = await _identityHttpServices.GetUserInfo(request.UserEmail);
                //requestMessage += "GetUserInfo-Execute-Start";

                //Process all the terminating workflows object for terminating old workflows
                if (allWorkflowToTerminate != null && allWorkflowToTerminate.Count > 0)
                {
                    try
                    {
                        _logger.LogInformation("TerminateWorkflow-Execute-Start");
                        await _workflowService.TerminateWorkflow(allWorkflowToTerminate, payload.token);
                        _logger.LogInformation("TerminateWorkflow-Execute-End");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("TerminateWorkflow-Exception-" + ex.Message + ",StackTrace-" + ex.StackTrace);
                        //log the error out records and move to next one
                    }
                }
                else
                {
                    _logger.LogInformation("allWorkflowToTerminate-Count is 0");
                }

                if (allWorkflowToStart != null && allWorkflowToStart.Count > 0)
                {
                    //Process all the new workflows object for creating new workflows
                    foreach (var allocationWorkflowObj in allWorkflowToStart)
                    {
                        try
                        {
                            _logger.LogInformation("CreateWorkflow-Execute-Start");
                            await _workflowService.CreateWorkflowRollForward(allocationWorkflowObj);
                            _logger.LogInformation("CreateWorkflow-Execute-End");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("CreateWorkflow-Exception-" + ex.Message + ",StackTrace-" + ex.StackTrace);
                            //log the error out records and move to next one
                        }
                    }
                }
                else
                {
                    _logger.LogInformation("allWorkflowToStart-Count is 0");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("RolloverAllocationWorkflowProcessing-Exception-" + ex.Message + ",StackTrace-" + ex.StackTrace);
            }

            _logger.LogInformation("RolloverAllocationWorkflowProcessing-End");

        }

    }
}
