using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using static RMT.Projects.Infrastructure.Constants;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectDetailsForRequestorQuery : IRequest<ProjectDetailsRequestorDto>
    {
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }


    public class GetProjectDetailsForRequestorQueryHandler : IRequestHandler<GetProjectDetailsForRequestorQuery, ProjectDetailsRequestorDto>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetProjectDetailsForRequestorQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<ProjectDetailsRequestorDto> Handle(GetProjectDetailsForRequestorQuery request, CancellationToken cancellationToken)
        {
            Project result = await _ProjectRepo.GetProjectDetailsForRequestor(request.PipelineCode, request.JobCode);

            ProjectDetailsRequestorDto obj = null;
            if (result != null)
            {
                obj = ProjectMapper.Mapper.Map<ProjectDetailsRequestorDto>(result);
                if (obj is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }

                bool IsJobType = false;
                if (!string.IsNullOrEmpty(result.JobCode))
                {
                    IsJobType = true;
                }
                string projectType = result.ChargableType + string.Empty;

                if (IsJobType == true && projectType.ToLower().Trim() == ProjectChargeType.CHARGEABLE.ToLower().Trim())
                {
                    obj.ResourceRequestorNames = result.ProjectRolesView.Where(r => EmployeeChargableRR.Any(d => d.ToLower().Trim().Equals(r.Role.ToLower().Trim()))).Select(a => a.UserName).ToList();
                }
                else if (IsJobType == true && projectType.ToLower().Trim() == ProjectChargeType.NON_CHARGABLE.ToLower().Trim())
                {
                    obj.ResourceRequestorNames = result.ProjectRolesView.Where(r => EmployeeNonChargableRR.Any(d => d.ToLower().Trim().Equals(r.Role.ToLower().Trim()))).Select(a => a.UserName).ToList();
                }
                else if (IsJobType == false)
                {
                    obj.ResourceRequestorNames = result.ProjectRolesView.Where(r => EmployeePipelineRR.Any(d => d.ToLower().Trim().Equals(r.Role.ToLower().Trim()))).Select(a => a.UserName).ToList();
                }
                else
                {
                    obj.ResourceRequestorNames = new List<string>();
                }
            }

            return await Task.FromResult(obj);

        }
    }
}









