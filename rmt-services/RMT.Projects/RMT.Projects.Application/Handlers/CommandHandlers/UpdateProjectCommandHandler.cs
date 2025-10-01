using MediatR;
using Newtonsoft.Json;
using RMT.Projects.Application.Handlers.QueryHandlers;
using RMT.Projects.Application.HttpServices;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Projects.Application.Constants.Constants;
using static RMT.Projects.Domain.Constant;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class UpdateProjectCommand : IRequest<ProjectResponse>
    {
        public Int64 Id { get; set; }

        public string? ProjectCode { get; set; }
        public string? PipelineCode { get; set; }
        public string? jobCode { get; set; }

        //public string? ProjectName { get; set; }

        //public string? JobCode { get; set; }

        //public string? PipelineCode { get; set; }

        //public string? ClientName { get; set; }

        //public string? Expertise { get; set; }

        //public string? SME { get; set; }

        //public DateTime? StartDate { get; set; }

        //public DateTime? EndDate { get; set; }

        public string? Description { get; set; }

        //public string? AllocationStatus { get; set; }

        //public string? ResourceRequestor { get; set; }
        public bool? IsConfidential { get; set; }

        public string? ModifiedBy { get; set; }

        //[Timestamp]
        public DateTime? ModifiedAt { get; set; }

        public ICollection<ProjectDemand>? ProjectDemands { get; set; }

        public ICollection<ProjectRoles>? ProjectRoles { get; set; }

        public ICollection<ProjectSkills>? ProjectSkills { get; set; }
        public DateTime? ProjectEndDate { get; set; }

        public bool? IsEndDateChanged { get; set; } = false;

    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ProjectResponse>
    {
        private readonly IProjectRepository _ProjectRepo;
        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;
        private readonly IWorkflowHttpService _workflowHttpService;

        public UpdateProjectCommandHandler(IProjectRepository ProjectRepository, IResourceAllocationHttpApi resourceAllocationHttpApi, IWorkflowHttpService workflowHttpService)
        {
            _ProjectRepo = ProjectRepository;
            _resourceAllocationHttpApi = resourceAllocationHttpApi;
            _workflowHttpService = workflowHttpService;
        }

        public async Task<ProjectResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var ProjectEntitiy = ProjectMapper.Mapper.Map<Project>(request);
            if (ProjectEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
            keyValuePairs.Add(new KeyValuePair<string, string>("PipelineCode", request.PipelineCode));
            keyValuePairs.Add(new KeyValuePair<string, string>("JobCode", request.jobCode));

            var currentProjectDetails = await _ProjectRepo.GetProjectByCode(ProjectEntitiy.PipelineCode, ProjectEntitiy.JobCode);


            var tempRoles = ProjectMapper.Mapper.Map<List<ProjectRolesResponseDTO>>(currentProjectDetails.ProjectRoles);

            //List<ProjectRolesResponseDTO> tempRoles = null;

            //var currentProjectDetailsString = JsonConvert.SerializeObject(currentProjectDetails.ProjectRoles);

            //List<ProjectRoles> previousProjectRolesDetails = JsonConvert.DeserializeObject<List<ProjectRoles>>(currentProjectDetailsString);

            DateTime? previousEndDate;
            if (currentProjectDetails.EndDate.HasValue)
                previousEndDate = currentProjectDetails.EndDate.Value.Date;
            else
                previousEndDate = currentProjectDetails.EndDate;

            string previousDelegateUser = null;

            List<string> previousAdditionalElUser = new();

            List<string> previousAdditionalDelegateUser = new();

            if (tempRoles != null)
            {
                var previousDelegateUser1 = tempRoles.Where(d => d.IsActive == true && d.Role.ToLower().Trim() == UserRoles.Delegate.ToLower().Trim());
                if (previousDelegateUser1 != null && previousDelegateUser1.Count() > 0)
                {
                    previousDelegateUser = previousDelegateUser1.FirstOrDefault().User;
                }
                var previousAdditionalEl = tempRoles.Where(d => d.IsActive == true && d.Role.ToLower().Trim() == UserRoles.AdditionalEl.ToLower().Trim());
                if (previousAdditionalEl != null && previousAdditionalEl.Count() > 0)
                {
                    previousAdditionalElUser = previousAdditionalEl.Select(e => e.User.ToLower().Trim()).ToList();
                }
                var previousAdditionalDelegate = tempRoles.Where(d => d.IsActive == true && d.Role.ToLower().Trim() == UserRoles.AdditionalEl.ToLower().Trim() && string.IsNullOrEmpty(d.DelegateEmail) == false);
                if (previousAdditionalDelegate != null && previousAdditionalDelegate.Count() > 0)
                {
                    previousAdditionalDelegateUser = previousAdditionalDelegate.Select(e => e.DelegateEmail.ToLower().Trim()).ToList();
                }

            }

            if (request.IsEndDateChanged == true && request.ProjectEndDate != null)
            {
                var requisitionApi = await _resourceAllocationHttpApi.GetAllRequisitionByProjectCodeForProjectDetailsQuery(keyValuePairs);
                if (requisitionApi.All(d => d.EndDate.Date <= request.ProjectEndDate.Value.Date))
                {
                    ProjectEntitiy.EndDate = request.ProjectEndDate.Value.Date;
                }
                else
                {
                    throw new ApplicationException("InvalidProjectEndDateException");
                }
            }

            if (request.ProjectEndDate.HasValue)
                ProjectEntitiy.EndDate = request.ProjectEndDate.Value.Date;
            else
                ProjectEntitiy.EndDate = request.ProjectEndDate;

            if (request.IsConfidential != currentProjectDetails.IsConfidential)
            {
                ProjectEntitiy.IsConfidential = request.IsConfidential;
            }
            List<string> actions = new List<string>();

            string newRolesDelegate = null;

            var newProject = await _ProjectRepo.UpdateAsync(ProjectEntitiy);

            DateTime? newEndDate;

            if (newProject.EndDate.HasValue)
            {
                newEndDate = newProject.EndDate.Value.Date;
            }
            else
            {
                newEndDate = newProject.EndDate;
            }


            List<string> newRolesAdditionalEl = new();
            List<string> newRolesAdditionalDelegate = new();

            if (newProject.ProjectRoles != null)
            {
                var newRolesDelegate1 = newProject.ProjectRoles.Where(e => e.Role.ToLower().Trim() == UserRoles.Delegate.Trim().ToLower() && e.IsActive == true);
                if (newRolesDelegate1 != null && newRolesDelegate1.Count() > 0)
                {
                    newRolesDelegate = newRolesDelegate1.FirstOrDefault().User;
                }

                var newRolesAdditionalEl1 = newProject.ProjectRoles.Where(e => e.Role.ToLower().Trim() == UserRoles.AdditionalEl.Trim().ToLower() && e.IsActive == true);
                if (newRolesAdditionalEl1 != null && newRolesAdditionalEl1.Count() > 0)
                {
                    newRolesAdditionalEl = newRolesAdditionalEl1.Select(e => e.User.ToLower().Trim()).ToList();
                }
                var newRolesAdditionalDelegate1 = newProject.ProjectRoles.Where(e => e.Role.ToLower().Trim() == UserRoles.AdditionalEl.Trim().ToLower() && e.IsActive == true && string.IsNullOrEmpty(e.DelegateEmail) == false);
                if (newRolesAdditionalDelegate1 != null && newRolesAdditionalDelegate1.Count() > 0)
                {
                    newRolesAdditionalDelegate = newRolesAdditionalDelegate1.Select(e => e.DelegateEmail.ToLower().Trim()).ToList();
                }
            }


            if (currentProjectDetails != null)
            {
                if (previousEndDate == null && newEndDate != null)
                {
                    actions.Add(NotificationTemplateTypes.PROJECT_END_DATE_UPDATE_NOTIFICATION);
                }
                if ((previousEndDate != null && newEndDate != null) && (DateOnly.FromDateTime((DateTime)previousEndDate) != DateOnly.FromDateTime((DateTime)newEndDate)))
                {
                    actions.Add(NotificationTemplateTypes.PROJECT_END_DATE_UPDATE_NOTIFICATION);
                }
            }

            var newlyAddedAdditionalEL = newRolesAdditionalEl.Except(previousAdditionalElUser);

            var newlyAddedAdditionalDelegate = newRolesAdditionalDelegate.Except(previousAdditionalDelegateUser);

            ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);
            if (newProject.EndDate.HasValue)
                ProjectResponse.EndDate = newProject.EndDate.Value.Date;
            ProjectResponse.DelegateAssignedBy = request.ModifiedBy;
            ProjectResponse.DelegateAssignmentDate = DateOnly.FromDateTime(DateTime.Now.Date);
            ProjectResponse.UserAssignmentDate = DateOnly.FromDateTime(DateTime.Now.Date);
            ProjectResponse.UserRequesting = request.ModifiedBy;

            if ((string.IsNullOrEmpty(previousDelegateUser) && previousDelegateUser != newRolesDelegate) || (!string.IsNullOrEmpty(previousDelegateUser) && !previousDelegateUser.Equals(newRolesDelegate, StringComparison.OrdinalIgnoreCase)))
            {
                if (!string.IsNullOrEmpty(previousDelegateUser))
                {
                    //include task title
                    RefreshAssignedWorkflowTask req = new()
                    {
                        previousAssignTo = previousDelegateUser,
                        currentAssignTo = string.IsNullOrEmpty(newRolesDelegate) ? string.Empty : newRolesDelegate,
                        type = "employee_allocation_resource_requestor",
                        pipelineCode = request.PipelineCode,
                        jobCode = string.IsNullOrEmpty(request.jobCode) ? null : request.jobCode,
                    };
                    await _workflowHttpService.RefreshAssignedTask(req);
                }
                else
                {
                    var requestorList = tempRoles.Where(e => e.ApplicationRole == UserRoles.ResourceRequestor && e.IsActive == true).Select(e => e.User).ToList();
                    var requestor = requestorList.FirstOrDefault();
                    if (requestor != null)
                    {
                        //include task title
                        RefreshAssignedWorkflowTask req = new()
                        {

                            previousAssignTo = requestor,
                            currentAssignTo = string.IsNullOrEmpty(newRolesDelegate) ? string.Concat(string.Empty, ",", requestor) : string.Concat(newRolesDelegate , ",", requestor) ,
                            type = "employee_allocation_resource_requestor",
                            pipelineCode = request.PipelineCode,
                            jobCode = string.IsNullOrEmpty(request.jobCode) ? null : request.jobCode,
                        };
                        await _workflowHttpService.RefreshAssignedTask(req);

                    }
                }
                actions.Add(NotificationTemplateTypes.PROJECT_DELEGATE_UPDATE_NOTIFICATION);
            }
            if (newlyAddedAdditionalEL != null && newlyAddedAdditionalEL.Any())
            {
                actions.Add(NotificationTemplateTypes.PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION);
                ProjectResponse.NewAdditionalEls = String.Join(",", newlyAddedAdditionalEL);
            }
            if (newlyAddedAdditionalDelegate != null && newlyAddedAdditionalDelegate.Any())
            {
                actions.Add(NotificationTemplateTypes.PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION);
                ProjectResponse.NewAdditionalDelegates = String.Join(",", newlyAddedAdditionalDelegate);
            }
            if (actions.Count() > 0)
            {
                ProjectResponse.Actions = actions;
            }
            return ProjectResponse;
        }
    }

}