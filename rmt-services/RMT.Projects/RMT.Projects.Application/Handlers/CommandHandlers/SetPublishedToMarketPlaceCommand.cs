using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class UpdatePublishedToMarketPlaceCommand : IRequest<List<ProjectResponse>>
    {
        public List<UpdatePublishedToMarketPlaceDTO> updatePublishedToMarketPlaceDTO { get; set; }
    }

    public class UpdatePublishedToMarketPlaceCommandHandler : IRequestHandler<UpdatePublishedToMarketPlaceCommand, List<ProjectResponse>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public UpdatePublishedToMarketPlaceCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<ProjectResponse>> Handle(UpdatePublishedToMarketPlaceCommand request, CancellationToken cancellationToken)
        {
            var newProject = await _ProjectRepo.UpdatePublishedToMarketPlace(request.updatePublishedToMarketPlaceDTO);
            List<ProjectResponse> ProjectResponse = ProjectMapper.Mapper.Map<List<ProjectResponse>>(newProject);
            return ProjectResponse;
        }
    }

}
