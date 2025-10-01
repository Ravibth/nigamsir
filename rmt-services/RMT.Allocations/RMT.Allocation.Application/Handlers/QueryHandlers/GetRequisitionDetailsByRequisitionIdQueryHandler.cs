using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static RMT.Allocation.Domain.ConstantsDomain;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{

    public class GetRequisitionDetailsByRequisitionIdQuery : IRequest<RequisitionResponse>
    {
        public Guid requisitionId { get; set; }
        public string? userEmail { get; set; }
        public bool? isRequsitionFilterByProjectRoles { get; set; } = false;
        public UserDecorator? UserInfo { get; set; }
    }

    public class GetRequisitionDetailsByRequisitionIdQueryHandler : IRequestHandler<GetRequisitionDetailsByRequisitionIdQuery, RequisitionResponse>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IConfigurationHttpService _configurationHttpService;
        public GetRequisitionDetailsByRequisitionIdQueryHandler(IRequisitionRepository requisitionRepository, IProjectServiceHttpApi projectServiceHttpApi, IConfigurationHttpService configurationHttpService)
        {
            _requisitionRepository = requisitionRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _configurationHttpService = configurationHttpService;
        }

        public async Task<RequisitionResponse> Handle(GetRequisitionDetailsByRequisitionIdQuery request, CancellationToken cancellationToken)
        {
            //Fetch Requisition Details
            var requisitionDetails = await _requisitionRepository.GetRequisitionDetailsByRequisitionId(request.requisitionId);

            //Helper.GetRequsitionListByCurrentUserRole(List);
            RequisitionResponse requisitionResponse = AllocationMapper.Mapper.Map<RequisitionResponse>(requisitionDetails);
            if ((bool)request.isRequsitionFilterByProjectRoles && requisitionResponse != null)
            {
                GetAllRequisitionByProjectCodeResponse checkerRequest = AllocationMapper.Mapper.Map<GetAllRequisitionByProjectCodeResponse>(requisitionDetails);

                List<GetAllRequisitionByProjectCodeResponse> reqRep = await Helper.GetRequsitionListByCurrentUserRole(new List<GetAllRequisitionByProjectCodeResponse> { checkerRequest }, requisitionResponse.PipelineCode, requisitionResponse.JobCode, request.userEmail, _projectServiceHttpApi, _configurationHttpService, request.UserInfo);

                RequisitionResponse resp = AllocationMapper.Mapper.Map<RequisitionResponse>(reqRep.FirstOrDefault());
                return resp;

                //var requisitionResp = await GetRequsitionAccessDetailsByProjectRoleAsync(requisitionResponse, requisitionResponse.PipelineCode, requisitionResponse.JobCode, request.userEmail, _projectServiceHttpApi, _configurationHttpService);
                //return requisitionResp;
            }
            return requisitionResponse;
        }

        //Not in use
        //private async Task<RequisitionResponse> GetRequsitionAccessDetailsByProjectRoleAsync(RequisitionResponse requisitionDetails, string PipelineCode, string JobCode, string userEmail, IProjectServiceHttpApi projectServiceHttpApi, IConfigurationHttpService configurationHttpService)
        //{
        //    ProjectDTO projectDTO = await projectServiceHttpApi.GetProjectDetailsByCode(PipelineCode, JobCode);
        //    var currentUserRoles = projectDTO.ProjectRoles.Where(d => !string.IsNullOrEmpty(d.User) && d.User.Equals(userEmail, StringComparison.OrdinalIgnoreCase) ||
        //                                            !string.IsNullOrEmpty(d.DelegateEmail) && d.DelegateEmail.Equals(userEmail, StringComparison.OrdinalIgnoreCase)).ToList();

        //    List<string> userRoles = currentUserRoles.Select(e => e.Role).ToList();
        //    if (Helper.IsResourceRequestor(userRoles))
        //    {
        //        requisitionDetails.hasPermissionToDelete = true;
        //        requisitionDetails.hasPermissionToEdit = true;
        //        return requisitionDetails;
        //    }
        //    else if (Helper.IsDelegate(userRoles))
        //    {
        //        requisitionDetails.hasPermissionToEdit = true;
        //        requisitionDetails.hasPermissionToDelete = true;
        //        return requisitionDetails;
        //    }
        //    else if (Helper.IsAdditionalElOrAdditionalDelegate(currentUserRoles))
        //    {
        //        var user = currentUserRoles
        //                        .Where((cur) => cur.Role.Equals(UserRolesConstant.AdditionalEl, StringComparison.OrdinalIgnoreCase))
        //                        .FirstOrDefault();
        //        string AdditionalElEmail = user.User == null ? string.Empty : user.User;
        //        string AdditionalDelegateEmail = user.DelegateEmail == null ? string.Empty : user.DelegateEmail;
        //        //TODO: GET CONFIG FOR ADDITIONAL EL
        //        //TODO: GET CONFIG FOR ADDITIONAL DELEGATE
        //        var configDataForAddEl = await configurationHttpService.GetProjectConfigurationByConfigGroupAndConfigType("Permission_for_Additional_EL", "GLOBAL");
        //        if (configDataForAddEl.Count == 0)
        //        {
        //            throw new Exception($"Configuration Group Permission_for_Additional_EL not found ");
        //        }
        //        var configDataForAddDel = await configurationHttpService.GetProjectConfigurationByConfigGroupAndConfigType("Permission_for_Additional_Delegate", "GLOBAL");
        //        if (requisitionDetails.CreatedBy.Equals(AdditionalElEmail, StringComparison.OrdinalIgnoreCase) || requisitionDetails.CreatedBy.Equals(AdditionalDelegateEmail, StringComparison.OrdinalIgnoreCase))
        //        {
        //            requisitionDetails.hasPermissionToDelete = true;
        //            requisitionDetails.hasPermissionToEdit = true;
        //        }
        //        else
        //        {
        //            requisitionDetails.hasPermissionToDelete = false;
        //            requisitionDetails.hasPermissionToEdit = false;
        //        }
        //        if (AdditionalElEmail.ToLower().Trim() == userEmail.ToLower().Trim() && Int32.Parse(configDataForAddEl.FirstOrDefault().AllValue) > 0)
        //        {
        //            return requisitionDetails;
        //        }
        //        if (AdditionalDelegateEmail.ToLower().Trim() == userEmail.ToLower().Trim() && Int32.Parse(configDataForAddDel.FirstOrDefault().AllValue) > 0)
        //        {
        //            return requisitionDetails;
        //        }
        //        if (requisitionDetails.CreatedBy.Equals(AdditionalElEmail, StringComparison.OrdinalIgnoreCase) || requisitionDetails.CreatedBy.Equals(AdditionalDelegateEmail, StringComparison.OrdinalIgnoreCase))
        //        {
        //            return requisitionDetails;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    return null;
        //}
    }
}
