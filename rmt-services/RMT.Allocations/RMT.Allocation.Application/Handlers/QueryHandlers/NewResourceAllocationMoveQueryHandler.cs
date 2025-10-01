using MediatR;
using RMT.Allocation.Application.DTOs.CommonResourceAllocationDTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class NewResourceAllocationMoveQuery : IRequest<NewJobCodeMoveResponseDTO>
    {
        public NewJobCodeMoveDTO newJobCodeMoveDTO { get; set; }
    }
    public class NewResourceAllocationMoveQueryHandler : IRequestHandler<NewResourceAllocationMoveQuery, NewJobCodeMoveResponseDTO>
    {
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        public NewResourceAllocationMoveQueryHandler(IProjectServiceHttpApi projectServiceHttpApi)
        {
            _projectServiceHttpApi = projectServiceHttpApi;
        }
        async Task<NewJobCodeMoveResponseDTO> IRequestHandler<NewResourceAllocationMoveQuery, NewJobCodeMoveResponseDTO>.Handle(NewResourceAllocationMoveQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectServiceHttpApi.GetProjectDetailsByCode(request.newJobCodeMoveDTO.PipelineCode, request.newJobCodeMoveDTO.JobCode);
            List<string> previousroles = null;
            if (result != null)
            {
                if (result.ProjectRolesView != null && result.ProjectRolesView.Any())
                {
                    var rolesAllowed = new List<string> {
                        UserRoles.EngagementLeader,
                        UserRoles.SMEGLeader,
                        UserRoles.ProposedCSP,
                        UserRoles.ProposedEL,
                        UserRoles.Delegate,
                        UserRoles.ResourceRequestor,
                        UserRoles.AdditionalEl,
                        UserRoles.AdditionalDelegate,
                        UserRoles.Reviewer,
                        UserRoles.JobManager,
                        UserRoles.CSP,
                        UserRoles.EO,
                    };
                    var previousRoles1 = result.ProjectRolesView.Where(e => !string.IsNullOrEmpty(e.User) && rolesAllowed.Any(p => p.Trim().ToLower().Equals(e.Role.ToLower().Trim())));
                    if (previousRoles1 != null && previousRoles1.Count() > 0)
                    {
                        previousroles
                         = previousRoles1.Select(e => e.User).ToList();
                    }
                }
            }
            NewJobCodeMoveResponseDTO response = new()
            {
                employee_jobcode_change = request.newJobCodeMoveDTO.AllResourceAllocation.Select(e => e.Email).ToList(),
                projectname = !String.IsNullOrEmpty(request.newJobCodeMoveDTO.JobName) ? request.newJobCodeMoveDTO.JobName : (request.newJobCodeMoveDTO.PipelineName),
                oldprojectcode = request.newJobCodeMoveDTO.JobCode,
                newprojectcode = request.newJobCodeMoveDTO.NewJobCode,
                newpipelinecode = request.newJobCodeMoveDTO.NewPipelineCode,
                newjobcode = request.newJobCodeMoveDTO.NewJobCode,
                previousrolesemails = previousroles
            };
            return response;
        }
    }
}
