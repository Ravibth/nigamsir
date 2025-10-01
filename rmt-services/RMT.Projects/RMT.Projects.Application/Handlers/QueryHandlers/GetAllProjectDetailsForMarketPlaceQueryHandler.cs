using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetAllProjectDetailsForMarketPlaceQuery : IRequest<List<Project>>
    {
    }


    public class GetAllProjectDetailsForMarketPlaceQueryHandler : IRequestHandler<GetAllProjectDetailsForMarketPlaceQuery, List<Project>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetAllProjectDetailsForMarketPlaceQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<Project>> Handle(GetAllProjectDetailsForMarketPlaceQuery request, CancellationToken cancellationToken)
        {
             return await _ProjectRepo.GetAllProjectDetailsForMarketPlace();

            /*ProjectDetailsRequestorDto obj = null;
            if (result != null)
            {
                obj = ProjectMapper.Mapper.Map<ProjectDetailsRequestorDto>(result);
                if (obj is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
            }

            return await Task.FromResult(obj);*/

        }
    }
}










