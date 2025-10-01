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
    public class GetProjectDetailsForEmployeeQuery : IRequest<ProjectDetailsEmployeeDto>
    {
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
    public class GetProjectDetailsForEmployeeQueryHandler : IRequestHandler<GetProjectDetailsForEmployeeQuery, ProjectDetailsEmployeeDto>
    {
        private readonly IProjectRepository _ProjectRepo;

        public GetProjectDetailsForEmployeeQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<ProjectDetailsEmployeeDto> Handle(GetProjectDetailsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            Project result = await _ProjectRepo.GetProjectDetailsForEmployee(request.PipelineCode, request.JobCode);

            var obj = ProjectMapper.Mapper.Map<ProjectDetailsEmployeeDto>(result);
            if (obj is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            return await Task.FromResult(obj);
        }
    }
}






