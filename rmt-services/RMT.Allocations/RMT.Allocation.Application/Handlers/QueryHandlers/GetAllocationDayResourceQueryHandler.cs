using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Repositories;
using RMT.Scheduler.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetAllocationDayResourceQuery : IRequest<List<AcutalAllocatedBudgetResourceDTO>>
    {
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class GetAllocationDayResourceQueryHandler : IRequestHandler<GetAllocationDayResourceQuery, List<AcutalAllocatedBudgetResourceDTO>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly WCGTResourceTimesheetHttpApi _wCGTesourceTimesheetHttpApi;
        private readonly ITokenService _tokenService;
        private readonly IdentityUserDetailsHttpApi _identityUserDetailsHttp;
        private readonly ProjectServiceHttpApi _projectServiceHttpApi;

        public GetAllocationDayResourceQueryHandler(IResourceAllocationRepository resourceAllocationRepository, WCGTResourceTimesheetHttpApi wCGTResourceTimesheetHttpApi
            , IdentityUserDetailsHttpApi identityUserDetailsHttpApi,
            ProjectServiceHttpApi projectServiceHttpApi,
            ITokenService tokenService)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _wCGTesourceTimesheetHttpApi = wCGTResourceTimesheetHttpApi;
            _tokenService = tokenService;
            _identityUserDetailsHttp = identityUserDetailsHttpApi;
            _projectServiceHttpApi = projectServiceHttpApi;
        }

        public async Task<List<AcutalAllocatedBudgetResourceDTO>> Handle(GetAllocationDayResourceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allocationResponse = await _resourceAllocationRepository.ResourceAllocationDayResourceGroup(request.StartDate, request.EndDate, request.PipelineCode, request.JobCode);
                //var timesheetResponse = await _wCGTesourceTimesheetHttpApi.GetResourceTimesheetDataByJobCode(request.JobCode, request.StartDate, request.EndDate);

                List<ResourceTimesheetDTO> timesheetResponse = await _resourceAllocationRepository.GetResourceTimesheetDataByJobCode(request.JobCode, request.StartDate, request.EndDate);

                if (allocationResponse != null)
                {
                    List<string> allocatedEmployeMail = allocationResponse.Select((a) => a.empemail).Distinct().ToList();
                    //  string token = await _tokenService.GetToken();
                    List<IdentityUserResponseDTO> identityUserResponse = new List<IdentityUserResponseDTO>();
                    if (allocatedEmployeMail.Count > 0)
                    {
                        identityUserResponse = await _identityUserDetailsHttp.GetEmployeesDataHttpApiQuery(allocatedEmployeMail);
                    }

                    var projectBudget = await _projectServiceHttpApi.GetProjectBudget(request.PipelineCode, request.JobCode);
                    Dictionary<string, double> allocationBudget = new Dictionary<string, double>();
                    foreach (var item in projectBudget)
                    {
                        allocationBudget.Add(item.Grade, item.RatePerHour);
                    }
                    var allocationIdentityResponse = allocationResponse.Join(identityUserResponse, a => a.empemail, i => i.emailId,
                            (allocation, identity) => new AcutalAllocatedBudgetResourceDTO
                            {
                                identityUserResponse = identity,
                                allocationtotaltime = allocation.totaltime,
                                empName = identity.name,
                                allocationtotalcost = allocation.cost == null ? 0 : (allocation.cost.Value),
                                //allocationtotalcost = allocation.totaltime * allocationBudget[identity.designation]

                            }).ToList();
                    foreach (var allocation in allocationIdentityResponse)
                    {
                        ResourceTimesheetDTO timesheetEntery = new();

                        //Monthly Quarterly 
                        timesheetEntery = timesheetResponse
                            .FirstOrDefault(t => 
                                t.email_id.ToLower().Trim() == allocation.identityUserResponse.emailId.ToLower().Trim()
                        );
                        var grade = allocation.identityUserResponse.grade;
                        if (timesheetEntery != null && grade != null && grade.Trim() != "")
                        {
                            var cost = allocationBudget.TryGetValue(grade, out double totalcost);

                            allocation.timesheettotaltime = timesheetEntery.totaltime;
                            allocation.timesheettotalCost = timesheetEntery.timesheetcost;
                            // allocation.timesheettotalCost = timesheetEntery.totaltime * allocationBudget[allocation.identityUserResponse.designation];
                        }

                    }
                    return allocationIdentityResponse;
                }
                else
                {
                    return new List<AcutalAllocatedBudgetResourceDTO>();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static double GetAllocationTotalCost(Dictionary<string, double> alllocationbudget, string key)
        {
            alllocationbudget.TryGetValue(key, out double totalcost);
            return totalcost;
        }
    }
}
