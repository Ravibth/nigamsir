using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectBudgetQuery : IRequest<List<ProjectBudgetDto>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
    public class GetProjectBudgetQueryHandler : IRequestHandler<GetProjectBudgetQuery, List<ProjectBudgetDto>>
    {

        private readonly IProjectRepository _ProjectRepo;
        public GetProjectBudgetQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<ProjectBudgetDto>> Handle(GetProjectBudgetQuery request, CancellationToken cancellationToken)
        {
            var response = await _ProjectRepo.GetProjectBudget(request.PipelineCode, request.JobCode);
            List<ProjectBudgetDto> result = new List<ProjectBudgetDto>();
            foreach (var item in response)
            {
                ProjectBudgetDto projectBudget = new();
                projectBudget.PipelineCode = item.PipelineCode;
                projectBudget.JobCode = item.JobCode;
                projectBudget.RatePerHour = item.RatePerHour;
                projectBudget.BudgetedHour = item.BudgetedHour;
                //projectBudget.Designation = item.Designation;
                projectBudget.Grade = item.Grade;
                projectBudget.OriginalBudgetedHour = item.OriginalBudgetedHour;
                projectBudget.OriginalRatePerHour = item.OriginalRatePerHour;
                result.Add(projectBudget);
            }
            return result;
        }
    }
}
