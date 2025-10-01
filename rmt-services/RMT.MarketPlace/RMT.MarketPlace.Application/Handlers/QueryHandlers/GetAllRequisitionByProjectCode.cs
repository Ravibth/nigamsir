using MediatR;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Application.Mappers;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RMT.MarketPlace.Application.Handlers.QueryHandlers
{
    public class GetAllRequisitionByProjectCodeQuery : IRequest<List<GetAllRequisitionByProjectCodeResponse>>
    {
        //TODO : new to remove limit and PAgination 
        public int limit { get; set; }
        public int pagination { get; set; }
        public int id { get; set; }
        public string? currentEmp { get; set; }
        public bool? ScoreCalculationForRequisitionIdsAllowed { get; set; } = false;
        public bool? IsRequsitionFilterByProjectRoles { get; set; } = false;
    }

    public class GetAllRequisitionByProjectCodeQueryHandler : IRequestHandler<GetAllRequisitionByProjectCodeQuery, List<GetAllRequisitionByProjectCodeResponse>>
    {
        private readonly IMarketPlaceRepository _Repo;
        private readonly IAllocationServiceHttpApi _allocationServiceHttpApi;

        public GetAllRequisitionByProjectCodeQueryHandler(IMarketPlaceRepository repository, IAllocationServiceHttpApi allocationServiceHttpApi)
        {
            _Repo = repository;
            _allocationServiceHttpApi = allocationServiceHttpApi;
        }

        public async Task<List<GetAllRequisitionByProjectCodeResponse>> Handle(GetAllRequisitionByProjectCodeQuery request, CancellationToken cancellationToken)
        {
            MarketPlaceProjectDetail MpProject = await _Repo.GetMarketPlaceProjectById(request.id);

            List<GetAllRequisitionByProjectCodeResponse> result = await _allocationServiceHttpApi.GetAllRequisitionByProjectCode(MpProject.PipelineCode, MpProject.JobCode, request.limit, request.pagination, request.currentEmp, request.ScoreCalculationForRequisitionIdsAllowed, request.IsRequsitionFilterByProjectRoles);

            if (result != null && result.Count > 0)
            {
                //Sort data first by designation
                result = result.OrderBy(x => x.Designation).ToList();

                //check if interest already submitted ?
                //List<EmpProjectInterestScore> projectIntrestScore = await _Repo.GetAllEmpProjectInterestScoreByProject(MpProject.PipelineCode, MpProject.JobCode);

                //List<EmpProjectInterestScore> currentEmpScore = new List<EmpProjectInterestScore>();

                //if (projectIntrestScore != null && projectIntrestScore.Count > 0)
                //{
                //    currentEmpScore = projectIntrestScore.Where(m => string.Compare(m.EmpEmail, request.currentEmp, true) == 0 && m.IsActive == true).ToList();

                //    foreach (var item in result)
                //    {
                //        var _submittedScore = currentEmpScore.Where(e => string.Compare(Convert.ToString(item.Id), e.RequisitionId, true) == 0).FirstOrDefault()?.RequisionScore;
                //        item.Score = _submittedScore != null ? _submittedScore : item.Score;
                //    }
                //}
            }

            return result;
        }
    }
}
