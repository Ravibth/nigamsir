using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Projects.Infrastructure.Repositories;
using RMT.Projects.Application.Responses;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.HttpServices.DTOs;
using static RMT.Projects.Domain.Constant;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class ReplaceProjectsSuperCoachRoleCommand : IRequest<ProjectRoles[]>
    {
        public string PreviouseUser { get; set; }
        public string User { get; set; }
        public List<ProjectPipelineCodeJobCode> ProjectCodes { get; set; } 

    }

    public class ReplaceProjectsSuperCoachRoleCommandHandler : IRequestHandler<ReplaceProjectsSuperCoachRoleCommand, ProjectRoles[]>
    {
        private readonly IIdentityHttpService _identityHttpService;
        private readonly IProjectRepository _projectRepository;
        public ReplaceProjectsSuperCoachRoleCommandHandler(IIdentityHttpService identityHttpService, IProjectRepository projectRepository)
        {
            _identityHttpService = identityHttpService;
            _projectRepository = projectRepository;
        }       

        public async Task<ProjectRoles[]> Handle(ReplaceProjectsSuperCoachRoleCommand request, CancellationToken cancellationToken)
        {
            List<EmployeeResponseDTO> identityEmployees = await _identityHttpService.GetEmployeesListByInputEmail(request.User);
            if (identityEmployees != null && identityEmployees.Count > 0) {
                EmployeeResponseDTO user = identityEmployees[0];
                List<ProjectRoles> addProjectrole = new List<ProjectRoles>();
                List<ProjectRoles> removeProjectrole = new List<ProjectRoles>();

                foreach (ProjectPipelineCodeJobCode projetcode  in request.ProjectCodes)
                {
                    Project project = await _projectRepository.GetProjectByCode(projetcode.PiplelineCode, projetcode.PiplelineCode);
                    addProjectrole.Add(new ProjectRoles
                    {
                        ProjectId = (long)project.Id,
                        User = user.emailId,
                        UserName = user.name,
                        Role = UserRoles.SuperCoach,
                        DelegateEmail = user?.delegate_details?.allocation_delegate_email == null ? null : user.delegate_details.allocation_delegate_email,
                        DelegateUserName = user?.delegate_details?.allocation_delegate_name == null ? null : user.delegate_details.allocation_delegate_name,
                        IsActive = true
                    }) ;
                }                
                return await _projectRepository.AddProjectUserWithRole(addProjectrole.ToArray());                
            };
            return null;
         }
    }
}
