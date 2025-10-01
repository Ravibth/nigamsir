using MediatR;
using Microsoft.Extensions.Logging;
using RMT.Projects.Application.DTOs;
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

    public class GetEmployeeRoleByByPipelineJobCodesQuery : IRequest<List<RoleEmailsByPipelineCodeResponse>>
    {
        public List<PipelineCodeAndRolesDto> PipelineCodeAndRolesDto { get; set; }
    }
    public class GetEmployeeRoleByByPipelineJobCodesQueryHandler : IRequestHandler<GetEmployeeRoleByByPipelineJobCodesQuery, List<RoleEmailsByPipelineCodeResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<GetEmployeeRoleByByPipelineJobCodesQueryHandler> _logger;
        public GetEmployeeRoleByByPipelineJobCodesQueryHandler(IProjectRepository projectRepository, ILogger<GetEmployeeRoleByByPipelineJobCodesQueryHandler> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }
        public async Task<List<RoleEmailsByPipelineCodeResponse>> Handle(GetEmployeeRoleByByPipelineJobCodesQuery request, CancellationToken cancellationToken)
        {
            List<RoleEmailsByPipelineCodeResponse> response = new List<RoleEmailsByPipelineCodeResponse>();
            string result = string.Join("\n", request.PipelineCodeAndRolesDto.Select(x =>
               $"PipelineCode: {x.pipelineCode}, JobCode: {x.jobCode}, Roles: [{string.Join(", ", x.roles ?? new List<string>())}]"
               ));
            _logger.LogInformation("AllocationSummary>>" + result);
            foreach (var item in request.PipelineCodeAndRolesDto)
            {
                try
                {
                    Dictionary<string, List<ProjectRolesView>> respResult = await _projectRepository.GetProjectRolesByPipelineCodeAndRoles(
                        new List<KeyValuePair<string, string?>>() { new KeyValuePair<string, string?>(item.pipelineCode, item.jobCode) }, item.roles);
                    Dictionary<string, List<string>> respResultWithEmailOnly = new Dictionary<string, List<string>>();
                    foreach (var kVp in respResult)
                    {
                        respResultWithEmailOnly[kVp.Key] = kVp.Value.Select(d => d.User).ToList();
                    }
                    RoleEmailsByPipelineCodeResponse respObj = new RoleEmailsByPipelineCodeResponse()
                    {
                        PipelineCode = item.pipelineCode,
                        JobCode = item.jobCode,
                        RolesEmails = respResultWithEmailOnly
                    };
                    response.Add(respObj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Message-{0}, StackTrace-{1}", ex.Message, ex.StackTrace);
                }
            }
            return response;
        }
    }

}
