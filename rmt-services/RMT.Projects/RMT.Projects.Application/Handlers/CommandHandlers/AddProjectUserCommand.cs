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

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class AddProjectUserRole
    {
        public Int64 ProjectId { get; set; }
        public string User { get; set; }
        public string? UserName { get; set; }
        public string Role { get; set; }
        //public string? DelegateRole { get; set; }
        public string? DelegateUserName { get; set; }
        public string? DelegateEmail { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        //[Timestamp]
        public DateTime? CreatedAt { get; set; }
        //[Timestamp]
        public DateTime? ModifiedAt { get; set; }
    }
    public class AddProjectUserCommand : IRequest<ProjectRoles[]>
    {
        public AddProjectUserRole[] AddProjectUserRoles { get; set; }
    }
    public class AddProjectUserCommandHandler : IRequestHandler<AddProjectUserCommand, ProjectRoles[]>
    {
        private readonly IProjectRepository _projectRepo;
        public AddProjectUserCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepo = projectRepository;
        }
        public async Task<ProjectRoles[]> Handle(AddProjectUserCommand request, CancellationToken cancellationToken)
        {

            ProjectRoles[] entities = ProjectMapper.Mapper.Map<ProjectRoles[]>(request.AddProjectUserRoles.ToArray());
            if (entities is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            return await _projectRepo.AddProjectUserWithRole(entities);
        }
    }
}
