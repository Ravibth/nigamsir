using MediatR;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetBasicProjectDetailRequestorByPipelineCodeQuery : IRequest<BasicProjectDetailsRequestorResponse>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
    public class GetBasicProjectDetailRequestorByPipelineCodeQueryHandler : IRequestHandler<GetBasicProjectDetailRequestorByPipelineCodeQuery, BasicProjectDetailsRequestorResponse>
    {
        private readonly IProjectRepository _projectRepository;
        public GetBasicProjectDetailRequestorByPipelineCodeQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<BasicProjectDetailsRequestorResponse> Handle(GetBasicProjectDetailRequestorByPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            var projectDetail = await _projectRepository.GetProjectByPipelineCode(request.PipelineCode, request.JobCode);
            if (projectDetail != null)
            {
                return new BasicProjectDetailsRequestorResponse()
                {
                    Id = (long)(projectDetail.Id),
                    PipelineName = projectDetail.PipelineName,
                    Sme = projectDetail.Sme,//Recheck
                    Smeg = projectDetail.Smeg,//Recheck
                    BudgetStatus = projectDetail.BudgetStatus,
                    ChargableType = projectDetail.ChargableType,
                    ClientName = projectDetail.ClientName,
                    EndDate = projectDetail.EndDate,
                    Expertise = projectDetail.Expertise,//Recheck

                    Offerings = projectDetail.Offerings,
                    Solutions = projectDetail.Solutions,
                    OfferingsId = projectDetail.OfferingsId,
                    SolutionsId = projectDetail.SolutionsId,

                    Location = projectDetail.Location,
                    //ProjectCode = projectDetail.ProjectCode,
                    PipelineCode = projectDetail.PipelineCode,
                    JobCode = projectDetail.JobCode,
                    JobName = projectDetail.JobName,
                    StartDate = projectDetail.StartDate,
                    ProjectType = projectDetail.ProjectType,
                    RevenueUnit = projectDetail.RevenueUnit,//Recheck
                    //ProjectJobCodes = (List<Domain.Entities.ProjectJobCodes>)projectDetail.ProjectJobCodes
                };
            }
            else
            {
                return null;
            }
        }
    }
}
