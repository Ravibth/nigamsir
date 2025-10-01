using MediatR;
using Newtonsoft.Json;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.Mappers;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.QueryHandlers
{
    public class GetAllEmpProjectInterestScoreByPipelineCodeQuery : IRequest<List<EmpProjectInterestScoreDTO>>
    {
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }

    public class GetAllEmpProjectInterestScoreByPipelineCodeQueryHandler : IRequestHandler<GetAllEmpProjectInterestScoreByPipelineCodeQuery, List<EmpProjectInterestScoreDTO>>
    {
        private readonly IMarketPlaceRepository _Repo;
        public GetAllEmpProjectInterestScoreByPipelineCodeQueryHandler(IMarketPlaceRepository ProjectRepository)
        {
            _Repo = ProjectRepository;
        }

        public async Task<List<EmpProjectInterestScoreDTO>> Handle(GetAllEmpProjectInterestScoreByPipelineCodeQuery request, CancellationToken cancellationToken)
        {

            var result = await _Repo.GetAllEmpProjectInterestScoreByProject(request.PipelineCode, request.JobCode);

            List<EmpProjectInterestScoreDTO> obj = new();
            if (result != null)
            {
                foreach (var item in result)
                {
                    obj.Add(new()
                    {
                        ID = item.ID,
                        EmpName = item.EmpName,
                        Suggestion = String.IsNullOrEmpty(item?.Suggestion) ? null : JsonConvert.DeserializeObject<SystemSuggestionResponseDTO>(item.Suggestion),
                        RequisitionParameters = String.IsNullOrEmpty(item?.RequisitionParameters) ? null : JsonConvert.DeserializeObject<List<RequisitionParameters>>(item.RequisitionParameters),
                        RequisionScore = item.RequisionScore,
                        EmpProjectInterestId = item.EmpProjectInterestId,
                        IsActive = item.IsActive,
                        IsInterested = item.IsInterested,
                        RequisitionDesignation = item.RequisitionDesignation,
                        RequisitionGrade = item.RequisitionGrade,
                        RequisitionBU = item.RequisitionBU,
                        RequisitionOfferings = item.RequisitionOfferings,
                        RequisitionSolutions = item.RequisitionSolutions,
                        RequisitionCompetency = item.RequisitionCompetency,
                        RequisitionId = item.RequisitionId,
                        EmpEmail = item.EmpEmail
                    });
                }
            }

            return obj;
        }
    }
}










