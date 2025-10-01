using MediatR;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO.Request;
//using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Migrations;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class AddProjectRoleCommand : IRequest
    {
        public List<ProjectRoles> ProjectRoles { get; set; }
        public long? ProjectId { get; set; }
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
    }

    public class AddProjectRoleCommandHandler : IRequestHandler<AddProjectRoleCommand>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;

        public AddProjectRoleCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Handle(AddProjectRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //AddProjectUserCommand obj = new AddProjectUserCommand();
                //ProjectDTO projectDetail = await _projectServiceHttpApi.GetProjectDetailsByCode(resourceAllocationResponse.ProjectCode);
                //List<AddProjectUserRole> projectRoles = new List<AddProjectUserRole>();
                //projectRoles.Add(new AddProjectUserRole
                //{
                //    ProjectId = (long)projectDetail.Id,
                //    User = resourceAllocationResponse.EmpEmail,
                //    UserName = resourceAllocationResponse.EmpName,
                //    Role = Utils.Constants.Employee
                //});
                //obj.AddProjectUserRoles = projectRoles.ToArray();
                //var projectRolesResponse = await _projectServiceHttpApi.AddProjectUserWithRole(obj);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
    public class AddProjectUserCommand
    {
        public AddProjectUserRole[] AddProjectUserRoles { get; set; }
    }
}
