using MediatR;
using RMT.Projects.Domain.DTOs.Response;
using RMT.Projects.Domain.Repositories;
using RMT.Projects.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetOfferingSolutionsByJobCodeQuery : IRequest<List<GetOfferingSolutionsByJobCodeResponseDTO>>
    {
        public List<string> jobCodes { get; set; }
    }

    public class GetOfferingSolutionsByJobCodeQueryHandler : IRequestHandler<GetOfferingSolutionsByJobCodeQuery, List<GetOfferingSolutionsByJobCodeResponseDTO>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetOfferingSolutionsByJobCodeQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<GetOfferingSolutionsByJobCodeResponseDTO>> Handle(GetOfferingSolutionsByJobCodeQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetOfferingSolutionsByJobCode(request.jobCodes);
        }
    }
}
