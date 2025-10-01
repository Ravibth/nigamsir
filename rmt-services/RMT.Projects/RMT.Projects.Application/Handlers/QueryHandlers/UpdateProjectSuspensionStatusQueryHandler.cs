using MediatR;
using RMT.Projects.Application.Constants;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.HttpServices;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class UpdateProjectSuspensionStatusQuery : IRequest<ProjectResponse>
    {
        public List<KeyValuePair<string, string>> ProjectCodes { get; set; }

        public Boolean IsSuspended { get; set; }
    }
    public class UpdateProjectSuspensionStatusQueryHandler : IRequestHandler<UpdateProjectSuspensionStatusQuery, ProjectResponse>
    {
        private readonly IProjectRepository _ProjectRepo;
        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;
        public UpdateProjectSuspensionStatusQueryHandler(IProjectRepository ProjectRepository, IResourceAllocationHttpApi resourceAllocationHttpApi)
        {
            _ProjectRepo = ProjectRepository;
            _resourceAllocationHttpApi = resourceAllocationHttpApi;
        }

        public async Task<ProjectResponse> Handle(UpdateProjectSuspensionStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Project> projectEntities = new List<Project>();
                foreach (var code in request.ProjectCodes)
                {
                    Project projectEntitiy = new Project();
                    //projectEntitiy.ProjectCode = code;
                    projectEntitiy.PipelineCode = code.Key;
                    projectEntitiy.JobCode = code.Value;
                    projectEntitiy.IsSuspended = request.IsSuspended;
                    projectEntities.Add(projectEntitiy);
                }
                if (projectEntities is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
                var newProject = await _ProjectRepo.UpdateProjectSuspensionStatus(projectEntities);
                ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);

                SuspendAllocationCommand suspendAllocationCommand = new SuspendAllocationCommand();
                suspendAllocationCommand.ProjectCode = request.ProjectCodes;
                var allocationHttpResonse = await _resourceAllocationHttpApi.SuspendAllocationHttpApiQuery(suspendAllocationCommand);
                if(allocationHttpResonse != null && allocationHttpResonse.Count > 0)
                {
                    ProjectResponse.Actions = new List<string> { Constants.Constants.NotificationTemplateTypes.PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION };
                    ProjectResponse.EmployeeReleasedDueToProjectSuspend = allocationHttpResonse.Select(e => e.EmpEmail).Distinct().ToList();
                    ProjectResponse.SuspendedAt = DateTime.Now;
                }

                return ProjectResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
