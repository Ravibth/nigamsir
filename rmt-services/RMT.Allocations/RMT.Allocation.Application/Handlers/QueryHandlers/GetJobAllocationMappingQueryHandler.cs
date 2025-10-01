using MediatR;
using Newtonsoft.Json.Linq;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetJobAllocationMappingQuery : IRequest<List<JobAllocationMappingDTO>>
    {
        public List<DateTime> AllocationConfirmedDate { get; set; }

        //public string? BearerToken { get; set; }

        //public bool? GetProjectRoles { get; set; }

        //public bool? GetEmployeeIds { get; set; }

    }

    public class GetJobAllocationMappingQueryHandler : IRequestHandler<GetJobAllocationMappingQuery, List<JobAllocationMappingDTO>>
    {
        private readonly IResourceAllocationRepository _allocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IIdentityUserDetailsHttpApi _identityServiceHttpApi;

        public GetJobAllocationMappingQueryHandler(IResourceAllocationRepository allocationRepository, IProjectServiceHttpApi projectServiceHttpApi, IIdentityUserDetailsHttpApi identityServiceHttpApi)
        {
            _allocationRepository = allocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _identityServiceHttpApi = identityServiceHttpApi;
        }

        public async Task<List<JobAllocationMappingDTO>> Handle(GetJobAllocationMappingQuery request, CancellationToken cancellationToken)
        {
            bool GetEmployeeIds = false;
            bool GetProjectRoles = false;

            List<JobAllocationMappingDTO> resourceAllocationDetails = null;
            if (request.AllocationConfirmedDate != null && request.AllocationConfirmedDate.Count() > 0)
            {
                //Get only date part and distinct values
                List<DateTime> allocatenDatetime = request.AllocationConfirmedDate.Select(a => a.Date).Distinct().ToList();

                //Get All allocation confirmed on the provided date
                resourceAllocationDetails = await _allocationRepository.GetJobAllocationMapping(allocatenDatetime);

                if (resourceAllocationDetails != null && resourceAllocationDetails.Count > 0)
                {
                    List<IdentityUserResponseDTO> identityUserResponse = new List<IdentityUserResponseDTO>();
                    if (GetEmployeeIds == true)
                    {
                        //get all distinct emp email ids
                        List<string> allEmployees = resourceAllocationDetails.Select(m => m.EmpEmail?.ToLower().Trim()).Distinct().ToList();

                        //get all employee_id for all distinct employees email_ids
                        if (allEmployees != null && allEmployees.Count > 0)
                        {
                            identityUserResponse = await _identityServiceHttpApi.GetEmployeesDataHttpApiQuery(allEmployees);
                        }
                    }

                    List<RoleEmailsByPipelineCodeResponse> projectWithRolesResponse = new List<RoleEmailsByPipelineCodeResponse>();
                    if (GetProjectRoles == true)
                    {
                        //get all pipeline/job codes ditinct values to get project roles for these projects
                        List<PipelineCodeAndRolesDto> _pipelineCodeAndRoles = resourceAllocationDetails
                            .Select(x => new PipelineCodeAndRolesDto { pipelineCode = x.PipelineCode, jobCode = x.JobCode, roles = null })
                            .DistinctBy(d => new { d.pipelineCode, d.jobCode })
                            .ToList();

                        //get projectroles for the all distinct pipeline/job codes filtered for allocation records
                        projectWithRolesResponse = await _projectServiceHttpApi.GetEmployeeRoleByByPipelineJobCodes(_pipelineCodeAndRoles);

                    }

                    ////Populate the object with all properties and return the same
                    //foreach (var allocationItem in resourceAllocationDetails)
                    //{
                    //    //allocationItem.PipelineCode
                    //    //allocationItem.PipelineName
                    //    //allocationItem.JobCode
                    //    //allocationItem.JobName
                    //    //allocationItem.EmpEmail
                    //    //allocationItem.EmpMID
                    //    //allocationItem.AllocationConfirmationDate
                    //    //allocationItem.ProjectRoles
                    //    allocationItem.EmpMID = identityUserResponse.AsEnumerable().Where(x => string.Equals(x.emailId, allocationItem.EmpEmail, StringComparison.OrdinalIgnoreCase)).Select(u => u.employeeId).FirstOrDefault();
                    //    //allocationItem.ProjectRoles = projectWithRolesResponse.AsEnumerable().Where(a =>
                    //    //                                string.Equals(a.PipelineCode, allocationItem.PipelineCode, StringComparison.OrdinalIgnoreCase)
                    //    //                                && string.Equals(a.JobCode, allocationItem.JobCode, StringComparison.OrdinalIgnoreCase)
                    //    //                                ).Select(pr => pr.RolesEmails).FirstOrDefault();

                    //}
                }
            }

            return resourceAllocationDetails;
        }
    }
}
