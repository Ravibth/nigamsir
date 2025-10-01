using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetAllProjectsByBUandExpertiseQuery : IRequest<List<ProjectFullDetailsResponse>>
    {
        public string BU { get; set; }
        public string Expertise { get; set; }//Recheck
        public string Offerings { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class GetAllProjectsByBUandExpertiseQueryHandler : IRequestHandler<GetAllProjectsByBUandExpertiseQuery, List<ProjectFullDetailsResponse>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetAllProjectsByBUandExpertiseQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<ProjectFullDetailsResponse>> Handle(GetAllProjectsByBUandExpertiseQuery request, CancellationToken cancellationToken)
        {
            var result = await _ProjectRepo.GetAllProjectByBUandExpertiseAsync(request.BU, request.Offerings, request.EndDate);//Recheck
            var response = ProjectMapper.Mapper.Map<List<ProjectFullDetailsResponse>>(result);
            return response;
            //return await Task.FromResult(new List<Project>());
        }
    }
}