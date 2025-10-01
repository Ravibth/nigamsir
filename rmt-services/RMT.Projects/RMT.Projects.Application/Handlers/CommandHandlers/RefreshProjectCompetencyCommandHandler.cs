using MediatR;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using RMT.Projects.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class RefreshProjectCompetencyCommand : IRequest<List<ProjectCompetency>>
    {
        public List<RefreshProjectCompetencyRequestDTO> competencyRequest { get; set; }
    }
    public class RefreshProjectCompetencyCommandHandler : IRequestHandler<RefreshProjectCompetencyCommand, List<ProjectCompetency>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;

        public RefreshProjectCompetencyCommandHandler(IProjectRepository projectRepository, IResourceAllocationHttpApi resourceAllocationHttpApi)
        {
            _projectRepository = projectRepository;
            _resourceAllocationHttpApi = resourceAllocationHttpApi;
        }
        public async Task<List<ProjectCompetency>> Handle(RefreshProjectCompetencyCommand request, CancellationToken cancellationToken)
        {
            var distinctPipelineCodeJobCode = request.competencyRequest.GroupBy(r => new { r.PipelineCode, r.JobCode }).Select(g => new RefreshProjectCompetencyRequestDTO
            {
                PipelineCode = g.Key.PipelineCode,
                JobCode = g.Key.JobCode
            })
            .ToList();
            await _projectRepository.RemoveProjectCompetency(distinctPipelineCodeJobCode);
            List<KeyValuePair<string, string?>> req = new();
            foreach (var item in distinctPipelineCodeJobCode)
            {
                req.Add(new KeyValuePair<string, string?>
                (
                     item.PipelineCode,
                     string.IsNullOrEmpty(item.JobCode) ? null : item.JobCode
                ));
            }
            var resposne = await _resourceAllocationHttpApi.GetActivePublishedAllocationByPipeLineCode(req);
            var projectCompetency = resposne.Select(e => new AddCompetencyRequestDTO
            {
                PipelineCode = e.PipelineCode,
                JobCode = string.IsNullOrEmpty(e.JobCode) ? null : e.JobCode,
                Competency = e.Requisition.Competency,
                CompetencyId = e.Requisition.CompetencyId
            })
                .GroupBy(t => new { t.PipelineCode, t.JobCode, t.Competency, t.CompetencyId })
                .Select(t => new AddCompetencyRequestDTO
                {
                    PipelineCode = t.Key.PipelineCode,
                    JobCode = t.Key.JobCode,
                    Competency = t.Key.Competency,
                    CompetencyId = t.Key.CompetencyId
                })
                .ToList();
            return await _projectRepository.AddProjectCompetencies(projectCompetency);
        }
    }
}
