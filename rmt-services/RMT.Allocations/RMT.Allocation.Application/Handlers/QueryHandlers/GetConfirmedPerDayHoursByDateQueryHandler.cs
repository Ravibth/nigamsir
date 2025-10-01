using MediatR;
using Microsoft.Extensions.Logging;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetConfirmedPerDayHoursByDateQuery : IRequest<List<EmployeePerDayAllocationDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool GetClientIds { get; set; }
        public bool GetEmployeeIds { get; set; }

    }
    public class GetConfirmedPerDayHoursByDateQueryHandler : IRequestHandler<GetConfirmedPerDayHoursByDateQuery, List<EmployeePerDayAllocationDto>>
    {
        private readonly IResourceAllocationRepository _allocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IIdentityUserDetailsHttpApi _identityServiceHttpApi;

        public GetConfirmedPerDayHoursByDateQueryHandler(IResourceAllocationRepository allocationRepository, IProjectServiceHttpApi projectServiceHttpApi, IIdentityUserDetailsHttpApi identityServiceHttpApi)
        {
            _allocationRepository = allocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _identityServiceHttpApi = identityServiceHttpApi;
        }

        public async Task<List<EmployeePerDayAllocationDto>> Handle(GetConfirmedPerDayHoursByDateQuery request, CancellationToken cancellationToken)
        {
            List<EmployeePerDayAllocationDto> perDayAllocationData = null;
            if (request.StartDate != null && request.EndDate != null)
            {
                ////Get only date part and distinct values
                //List<DateTime> allocatenDatetime = request.AllocationConfirmedDate.Select(a => a.Date).Distinct().ToList();

                //Get All allocation confirmed per daya on the provided date range
                List<ResourceAllocationDaysResponse> raDays = await _allocationRepository.GetConfirmedPerDayHoursByDate(request.StartDate, request.EndDate);

                if (raDays != null && raDays.Count > 0)
                {

                    List<IdentityUserResponseDTO> identityUserResponse = new List<IdentityUserResponseDTO>();
                    if (request.GetEmployeeIds == true)
                    {
                        //get all distinct emp email ids
                        List<string> allEmployees = raDays.Select(m => m.EmailId?.ToLower().Trim()).Distinct().ToList();

                        //get all employee_id for all distinct employees email_ids
                        if (allEmployees != null && allEmployees.Count > 0)
                        {
                            identityUserResponse = await _identityServiceHttpApi.GetEmployeesDataHttpApiQuery(allEmployees);
                        }
                    }

                    List<ProjectFullDetailsResponse> projectFullDetailsColl = new List<ProjectFullDetailsResponse>();
                    if (request.GetClientIds == true)
                    {
                        //get all pipeline/job codes ditinct values to get project data for these projects
                        List<KeyValuePair<string, string?>> _pipelineCodeAndRoles =
                            raDays.Select(x =>
                            new KeyValuePair<string, string?>(x.PipelineCode, x.JobCode))
                            .DistinctBy(d => new { d.Key, d.Value })
                            .ToList();

                        //get projectroles for the all distinct pipeline/job codes filtered for allocation records
                        projectFullDetailsColl = await _projectServiceHttpApi.GetProjectFullDetailsByUniqueCodes(_pipelineCodeAndRoles);

                    }

                    EmployeePerDayAllocationDto perDayRecord = new EmployeePerDayAllocationDto();
                    foreach (var raItem in raDays)
                    {
                        perDayRecord = new EmployeePerDayAllocationDto()
                        {
                            PipelineCode = raItem.PipelineCode,
                            JobCode = raItem.JobCode,
                            PipelineName = raItem.PipelineName,
                            JobName = raItem.JobName,
                            EmpEmail = raItem.EmailId,
                            AllocationDate = raItem.AllocationDate.ToDateTime(TimeOnly.MinValue),
                            AllocatedPerDayHour = raItem.Efforts,
                            EmpMID = identityUserResponse.AsEnumerable()
                            .Where(x => string.Equals(x.emailId, raItem.EmailId, StringComparison.OrdinalIgnoreCase))
                            .Select(u => u.employeeId)
                            .FirstOrDefault(),
                            ClientId = projectFullDetailsColl.AsEnumerable().Where(a =>
                                                        string.Equals(a.PipelineCode, raItem.PipelineCode, StringComparison.OrdinalIgnoreCase)
                                                        && string.Equals(a.JobCode, raItem.JobCode, StringComparison.OrdinalIgnoreCase)
                                                        ).Select(pr => pr.ClientId).FirstOrDefault()
                        };
                    }

                    perDayAllocationData.Add(perDayRecord);
                }

            }
            else
            {
                Console.WriteLine("Invalid Dates-{0}-{1}", request.StartDate, request.EndDate);
            }

            return perDayAllocationData;
        }
    }
}