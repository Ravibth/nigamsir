using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class UpdateRequisitionCommand : IRequest<List<RequisitionResponse>>
    {
        public string? token { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public List<UpdateResourceEntities>? ResourceEntities { get; set; }
        public UserDecorator userInfo { get; set; }
    }

    public class UpdateRequisitionCommandHandler : IRequestHandler<UpdateRequisitionCommand, List<RequisitionResponse>>
    {
        private readonly IRequisitionRepository _repository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        public UpdateRequisitionCommandHandler(IRequisitionRepository repository, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _repository = repository;
            _projectServiceHttpApi = projectServiceHttpApi;
        }
        public async Task<List<RequisitionResponse>> Handle(UpdateRequisitionCommand command, CancellationToken cancellationToken)
        {
            //Allow these user to allow update requisition based on lgged in user 
            var rolesAllowed = new List<string> { UserRoles.Delegate, UserRoles.ResourceRequestor, UserRoles.AdditionalEl, UserRoles.AdditionalDelegate };
            var requisitionIds = command.ResourceEntities.Select(m => m.id).Distinct().ToArray();

            var projectCodes = new List<KeyValuePair<string, string>>();
            foreach (var item in requisitionIds)
            {
                var reqDetails = await _repository.GetRequisitionDetailsByRequisitionId(item);
                projectCodes.Add(new KeyValuePair<string, string>(reqDetails.PipelineCode, reqDetails.JobCode));
            }

            var projectDetails = await _projectServiceHttpApi.GetProjectDetailsByProjectCode(projectCodes.Distinct().ToList(), command.token);
            if (projectDetails.Count() != command.ResourceEntities.Count())
            {
                throw new Exception("Project Not found");
            }
            else
            {
                if (command.userInfo != null && command?.userInfo?.roles?.ToList().Where(m => m?.Trim().ToLower() == UserRoles.SystemAdmin.Trim().ToLower()).Count() > 0)
                {
                    var requisitionRequest = AllocationMapper.Mapper.Map<UpdateRequisitionRequest>(command);
                    var requisitionResponse = await _repository.UpdateRequisition(requisitionRequest);
                    return AllocationMapper.Mapper.Map<List<RequisitionResponse>>(requisitionResponse);
                }
                else
                {
                    foreach (var item in projectDetails)
                    {
                        if ((item.ProjectRolesView.Where(m => m.User.Trim().ToLower().Equals(command.ModifiedBy.Trim().ToLower())
                            && (!string.IsNullOrEmpty(m.Role) && rolesAllowed.Any(p => p.Trim().ToLower().Equals(m.Role.ToLower().Trim()))
                            || !string.IsNullOrEmpty(m.ApplicationRole) && rolesAllowed.Any(p => p.Trim().ToLower().Equals(m.ApplicationRole.ToLower().Trim())))
                            )).Count() < 1
                           )
                        {
                            throw new Exception("Permission Not found");
                        }
                    }
                }
            }
            var requisition = AllocationMapper.Mapper.Map<UpdateRequisitionRequest>(command);
            var response = await _repository.UpdateRequisition(requisition);
            return AllocationMapper.Mapper.Map<List<RequisitionResponse>>(response);
        }
    }
}
