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
    public class EmpProjectInterestCommand : IRequest<EmpProjectInterestResponse>
    {
        public int? MarketPlaceId { get; set; }

        public bool? IsInterested { get; set; }
        public DateTime? InterestDate { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public class EmpProjectinterestCommandHandler : IRequestHandler<EmpProjectInterestCommand, EmpProjectInterestResponse>
    {
        private readonly IMarketPlaceRepository _MarketPlaceRepository;
        public EmpProjectinterestCommandHandler(IMarketPlaceRepository MarketPlaceRepository)
        {
            _MarketPlaceRepository = MarketPlaceRepository;
        }

        public async Task<EmpProjectInterestResponse> Handle(EmpProjectInterestCommand request, CancellationToken cancellationToken)
        {
            var entitiy = MarketPlaceMapper.Mapper.Map<EmpProjectInterest>(request);
            if (entitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            EmpProjectInterestResponse ProjectResponse = null;
            var newEntity = await _MarketPlaceRepository.UpdateOrCreateEmpProjectInterest(request.MarketPlaceId, entitiy);
            if (newEntity != null)
            {
                ProjectResponse = MarketPlaceMapper.Mapper.Map<EmpProjectInterestResponse>(newEntity);
                if (!string.IsNullOrEmpty(ProjectResponse.JobCode))
                {
                    ProjectResponse.ProjectName = ProjectResponse.JobName;
                }
                else
                {
                    ProjectResponse.ProjectName = ProjectResponse.PipelineName;
                }
                var lst = await _MarketPlaceRepository.GetEmpProjectInterestList(newEntity);
                if (lst != null)
                {
                    ProjectResponse.NoOfInterested = lst.Count;
                }
            }
            else
            {
                throw new Exception("Marketplace interest is not saved");
            }
            return ProjectResponse;
        }
    }
}
