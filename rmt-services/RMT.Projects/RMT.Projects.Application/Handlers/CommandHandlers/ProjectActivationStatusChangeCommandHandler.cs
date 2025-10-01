using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class ProjectActivationStatusChangeCommand : IRequest<ProjectResponse>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public bool IsJobClosed { get; set; }

    }
    public class ProjectActivationStatusChangeCommandHandler : IRequestHandler<ProjectActivationStatusChangeCommand, ProjectResponse>
    {
        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;
        private readonly IWorkflowHttpService _workflowHttpService;
        private readonly IProjectRepository _projectRepository;
        public ProjectActivationStatusChangeCommandHandler(IResourceAllocationHttpApi resourceAllocationHttpApi, IWorkflowHttpService workflowHttpService, IProjectRepository projectRepository)
        {
            _resourceAllocationHttpApi = resourceAllocationHttpApi;
            _workflowHttpService = workflowHttpService;
            _projectRepository = projectRepository;
        }
        public async Task<ProjectResponse> Handle(ProjectActivationStatusChangeCommand request, CancellationToken cancellationToken)
        {
            
            List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string?> (request.PipelineCode, string.IsNullOrEmpty( request.JobCode) ? null : request.JobCode),
            };
            SuspendAllocationCommand suspendReq = new SuspendAllocationCommand()
            {
                ProjectCode = keyValuePairs
            };
            var suspendAllocationResponse = await _resourceAllocationHttpApi.SuspendAllocationHttpApiQuery(suspendReq);
            var newProject = await _projectRepository.GetProjectByPipelineCode(request.PipelineCode, string.IsNullOrEmpty(request.JobCode) ? null : request.JobCode);
            ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);
            TerminateWorkflowByPipelineCodeAndJobCodeRequestDTO terminateReq = new()
            {
                PipelineCode = request.PipelineCode,
                JobCode = string.IsNullOrEmpty(request.JobCode) ? null : request.JobCode,
            };
            if (suspendAllocationResponse != null && suspendAllocationResponse.Count > 0)
            {
                ProjectResponse.Actions = new List<string> { Constants.Constants.NotificationTemplateTypes.JOB_INACTIVE_ALLOCATION_RELEASE_NOTIFICATION };
                ProjectResponse.EmployeeReleasedDueToProjectSuspend = suspendAllocationResponse.Select(e => e.EmpEmail).Distinct().ToList();
                ProjectResponse.SuspendedAt = DateTime.Now;
            }
            if (request.IsJobClosed)
            {
                newProject.EndDate = DateTime.UtcNow;
                var projectUpdateResult = await _projectRepository.UpdateAsync(newProject);
            }

            await _workflowHttpService.TerminateWorkflowByPipelineCodeAndJobCode(terminateReq);

            return ProjectResponse;
        }
    }
}
