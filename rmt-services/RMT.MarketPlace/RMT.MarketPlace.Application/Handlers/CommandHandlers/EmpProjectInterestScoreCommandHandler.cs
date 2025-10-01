using MediatR;
using Newtonsoft.Json;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.CommandHandlers
{
    public class EmpProjectInterestScoreRequest
    {
        public Int64? EmpProjectInterestId { get; set; }
        //public EmpProjectInterestScore? EmpProjectInterest { get; set; }
        public string? RequisitionId { get; set; }
        public string? RequisitionDesignation { get; set; }
        public string? RequisitionGrade { get; set; }
        public string? RequisitionBU { get; set; }
        public string? RequisitionOfferings { get; set; }
        public string? RequisitionSolutions { get; set; }
        public string? RequisitionCompetency { get; set; }
        public string? RequisionScore { get; set; }
        public string? Suggestion { get; set; }
        public List<RequisitionParameters>? RequisitionParameters { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsInterested { get; set; }
    }
    public class EmpProjectInterestScoreCommand : IRequest<List<EmpProjectInterestScoreResponse>>
    {
        public List<EmpProjectInterestScoreRequest> allRequistion { get; set; }
        public UserDecorator UserInfo { get; set; }

    }

    public class EmpProjectInterestScoreCommandHandler : IRequestHandler<EmpProjectInterestScoreCommand, List<EmpProjectInterestScoreResponse>>
    {
        private readonly IMarketPlaceRepository _MarketPlaceRepository;
        public EmpProjectInterestScoreCommandHandler(IMarketPlaceRepository MarketPlaceRepository)
        {
            _MarketPlaceRepository = MarketPlaceRepository;
        }

        public async Task<List<EmpProjectInterestScoreResponse>> Handle(EmpProjectInterestScoreCommand request, CancellationToken cancellationToken)
        {
            return await EmployeeProjectIntrestScoreUpdateOrCreate(request);
        }

        public async Task<List<EmpProjectInterestScoreResponse>> EmployeeProjectIntrestScoreUpdateOrCreate(EmpProjectInterestScoreCommand request)
        {
            List<EmpProjectInterestScoreResponse> EmpProjectInterestScoreList = new();
            for (int i = 0; i < request.allRequistion.Count; i++)
            {
                EmpProjectInterestScore entitiy = new()
                {
                    EmpProjectInterestId = request.allRequistion[i].EmpProjectInterestId,
                    RequisionScore = request.allRequistion[i].RequisionScore,
                    RequisitionId = request.allRequistion[i].RequisitionId,
                    Suggestion = request.allRequistion[i].Suggestion,
                    RequisitionParameters = JsonConvert.SerializeObject(request.allRequistion[i].RequisitionParameters),
                    RequisitionDesignation = request.allRequistion[i].RequisitionDesignation,
                    RequisitionGrade = request.allRequistion[i].RequisitionGrade,
                    RequisitionBU = request.allRequistion[i].RequisitionBU,
                    RequisitionOfferings = request.allRequistion[i].RequisitionOfferings,
                    RequisitionSolutions = request.allRequistion[i].RequisitionSolutions,
                    RequisitionCompetency = request.allRequistion[i].RequisitionCompetency,
                    CreatedDate = DateTime.UtcNow,
                };

                if (entitiy is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
                entitiy.IsActive = request.allRequistion[i].IsActive;
                entitiy.IsInterested = request.allRequistion[i].IsInterested;
                entitiy.CreatedBy = request.UserInfo.email;
                entitiy.ModifiedBy = request.UserInfo.email;
                entitiy.ModifiedDate = DateTime.UtcNow;
                var newEntity = await _MarketPlaceRepository.UpdateOrCreateEmpProjectInterestScore(entitiy);
                EmpProjectInterestScoreResponse ProjectResponse = MarketPlaceMapper.Mapper.Map<EmpProjectInterestScoreResponse>(newEntity);
                EmpProjectInterestScoreList.Add(ProjectResponse);
            }

            return EmpProjectInterestScoreList;
        }
    }
}
