using MediatR;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.CommandHandlers
{
    public class UpdateExpiredMarketPlaceProjectsCommand : IRequest<List<MarketPlaceProjectDetailResponse>>
    {
        public DateTime ExpiryDate { get; set; }
        public int DaysAdjustment { get; set; }
    }

    public class UpdateExpiredMarketPlaceProjectsCommandHandler : IRequestHandler<UpdateExpiredMarketPlaceProjectsCommand, List<MarketPlaceProjectDetailResponse>>
    {
        private readonly IMarketPlaceRepository _MarketPlaceRepository;
        public UpdateExpiredMarketPlaceProjectsCommandHandler(IMarketPlaceRepository MarketPlaceRepository)
        {
            _MarketPlaceRepository = MarketPlaceRepository;
        }

        public async Task<List<MarketPlaceProjectDetailResponse>> Handle(UpdateExpiredMarketPlaceProjectsCommand request, CancellationToken cancellationToken)
        {
            List<MarketPlaceProjectDetail> newEntity = await _MarketPlaceRepository.UpdateExpiredMarketPlaceProjects(request.ExpiryDate, request.DaysAdjustment);
            List<MarketPlaceProjectDetailResponse> ProjectResponse = MarketPlaceMapper.Mapper.Map<List<MarketPlaceProjectDetailResponse>>(newEntity);
            foreach (var project in ProjectResponse)
            {
                var employeesWithIntrest = await _MarketPlaceRepository.GetListOfUsersInterestedByPipelineCode(project.PipelineCode , project.JobCode);
                project.EmployeeShowedInterestCount = employeesWithIntrest.Count;
            }
            return ProjectResponse;
        }
    }
}
