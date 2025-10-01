using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.CommandHandlers
{
    public class RefreshEmpProjectInterestScoreCommand : IRequest<string>
    {
        public List<KeyValuePair<string, string?>> PipelineJobCodes { get; set; }
        public string RequisitionActionType { get; set; }
        public UserDecorator? userDecorator { get; set; }
    }
    public class RefreshEmpProjectInterestScoreCommandHandler : IRequestHandler<RefreshEmpProjectInterestScoreCommand, string>
    {
        private readonly IMarketPlaceRepository _marketPlaceRepository;
        private readonly IAllocationServiceHttpApi _allocationServiceHttpApi;
        public RefreshEmpProjectInterestScoreCommandHandler(IMarketPlaceRepository marketPlaceRepository, IAllocationServiceHttpApi allocationServiceHttpApi)
        {
            _marketPlaceRepository = marketPlaceRepository;
            _allocationServiceHttpApi = allocationServiceHttpApi;
        }
        public async Task<string> Handle(RefreshEmpProjectInterestScoreCommand request, CancellationToken cancellationToken)
        {
            List<EmpProjectInterestScore> requestDtos = new();
            foreach (var item in request.PipelineJobCodes)
            {
                var employeeProjectIntrestScores = await _marketPlaceRepository.GetAllEmpProjectInterestScoreByProject(item.Key, string.IsNullOrEmpty(item.Value) ? null : item.Value);
                List<string> employeeEmails = employeeProjectIntrestScores.Where(x => x.IsActive == true).Select(t => t.EmpEmail).Distinct().ToList();
                List<GetAllRequisitionByProjectCodeResponse> completeRequisitionList = new();
                foreach (var employeeEmail in employeeEmails)
                {
                    List<GetAllRequisitionByProjectCodeResponse> allRequisitionList = await _allocationServiceHttpApi.GetAllRequisitionByProjectCode(item.Key, item.Value, 100, 1, employeeEmail, true, false);
                    completeRequisitionList.AddRange(allRequisitionList);
                }
                
                switch (request.RequisitionActionType)
                {
                    case "CREATE_REQUISITION":
                        var newRequisitionAddition = completeRequisitionList.Where(e => employeeProjectIntrestScores.Any(t => t.RequisitionId == Convert.ToString(e.Id)) == false).ToList();
                        var employeeProjectIntrestIdList = employeeProjectIntrestScores.Where(e => e.EmpProjectInterest != null && e.EmpProjectInterest.IsActive == true).Select(e => e.EmpProjectInterest.ID).Distinct().ToList();
                        foreach (var newReq in newRequisitionAddition)
                        {
                            foreach (var employeeProjectInterestId in employeeProjectIntrestIdList)
                            {
                                EmpProjectInterestScore requestDto = new()
                                {
                                    RequisitionId = Convert.ToString(newReq.Id),
                                    EmpProjectInterestId = employeeProjectInterestId,
                                    Suggestion = newReq.Suggestion,
                                    RequisionScore = newReq.Score,
                                    RequisitionSolutions = newReq.Solutions,
                                    RequisitionBU = newReq.BusinessUnit,
                                    RequisitionCompetency = newReq.Competency,
                                    RequisitionDesignation = newReq.Designation,
                                    RequisitionGrade = newReq.Grade,
                                    RequisitionOfferings = newReq.Offerings,
                                    RequisitionParameters = JsonConvert.SerializeObject(newReq.RequisitionParameters),
                                    IsActive = true,
                                    CreatedBy = request.userDecorator != null ? request.userDecorator.email : string.Empty,
                                    ModifiedBy = request.userDecorator != null ? request.userDecorator.email : string.Empty,
                                    CreatedDate = DateTime.UtcNow,
                                    ModifiedDate = DateTime.UtcNow,
                                };
                                requestDtos.Add(requestDto);
                            }
                        }
                        break;
                    case "UPDATE_OR_DELETE_REQUISITION":
                        //var updateRequisition = completeRequisitionList.Where(e => employeeProjectIntrestScores.Any(t => t.RequisitionId == Convert.ToString(e.Id)) == true).ToList();
                        foreach (var empProjectInterestScore in employeeProjectIntrestScores)
                        {
                            var requisitionInfo = completeRequisitionList.Where(e => Convert.ToString(e.Id) == empProjectInterestScore.RequisitionId).FirstOrDefault();
                            if(requisitionInfo != null)
                            {
                                EmpProjectInterestScore requestDto = new()
                                {
                                    RequisitionId = Convert.ToString(requisitionInfo.Id),
                                    EmpProjectInterestId = empProjectInterestScore.EmpProjectInterestId,
                                    Suggestion = requisitionInfo.Suggestion,
                                    RequisionScore = requisitionInfo.Score,
                                    RequisitionSolutions = requisitionInfo.Solutions,
                                    RequisitionBU = requisitionInfo.BusinessUnit,
                                    RequisitionCompetency = requisitionInfo.Competency,
                                    RequisitionDesignation = requisitionInfo.Designation,
                                    RequisitionGrade = requisitionInfo.Grade,
                                    RequisitionOfferings = requisitionInfo.Offerings,
                                    RequisitionParameters = JsonConvert.SerializeObject(requisitionInfo.RequisitionParameters),
                                    IsActive = empProjectInterestScore.IsActive,
                                    ModifiedBy = request.userDecorator != null ? request.userDecorator.email : string.Empty,
                                    ModifiedDate = DateTime.UtcNow,
                                    CreatedBy = empProjectInterestScore.CreatedBy,
                                    CreatedDate = empProjectInterestScore.CreatedDate,
                                    ID = empProjectInterestScore.ID
                                };
                                requestDtos.Add(requestDto);
                            }
                            else
                            {
                                EmpProjectInterestScore requestDto = new()
                                {
                                    RequisitionId = Convert.ToString(empProjectInterestScore.RequisitionId),
                                    EmpProjectInterestId = empProjectInterestScore.EmpProjectInterestId,
                                    Suggestion = empProjectInterestScore.Suggestion,
                                    RequisionScore = empProjectInterestScore.RequisionScore,
                                    RequisitionSolutions = empProjectInterestScore.RequisitionSolutions,
                                    RequisitionBU = empProjectInterestScore.RequisitionBU,
                                    RequisitionCompetency = empProjectInterestScore.RequisitionCompetency,
                                    RequisitionDesignation = empProjectInterestScore.RequisitionDesignation,
                                    RequisitionGrade = empProjectInterestScore.RequisitionGrade,
                                    RequisitionOfferings = empProjectInterestScore.RequisitionOfferings,
                                    RequisitionParameters = empProjectInterestScore.RequisitionParameters,
                                    IsActive = false,
                                    ModifiedBy = request.userDecorator != null ? request.userDecorator.email : string.Empty,
                                    ModifiedDate = DateTime.UtcNow,
                                    CreatedBy = empProjectInterestScore.CreatedBy,
                                    CreatedDate = empProjectInterestScore.CreatedDate,
                                    ID = empProjectInterestScore.ID
                                };
                                requestDtos.Add(requestDto);
                            }
                            
                        }
                        break;
                    default:
                        break;
                }

            }
            foreach (var item in requestDtos)
            {
                await _marketPlaceRepository.UpdateOrCreateEmpProjectInterestScore(item);
            }
            return "Hi";
        }
    }
}
