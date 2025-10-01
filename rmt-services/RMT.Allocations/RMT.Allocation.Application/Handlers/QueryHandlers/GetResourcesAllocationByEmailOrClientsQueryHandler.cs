using MediatR;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetResourcesAllocationByEmailOrClientsQuery : IRequest<List<ResourceAllocationResponse>>
    {
        public string EmpEmail { get; set; }
        public List<string>? clientName { get; set; }
        public List<string>? clientGroup { get; set; }
    }

    public class GetResourcesAllocationByEmailOrClientsQueryHandler : IRequestHandler<GetResourcesAllocationByEmailOrClientsQuery, List<ResourceAllocationResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
         
        public GetResourcesAllocationByEmailOrClientsQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IWCGTMasterHttpApi wCGTMasterHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
        }

        public async Task<List<ResourceAllocationResponse>> Handle(GetResourcesAllocationByEmailOrClientsQuery request, CancellationToken cancellationToken)
        {          
            var allocationResp = await _resourceAllocationRepository.GetResourceAllocationByEmail(request.EmpEmail);
            if (request.clientName != null && request.clientName.Any() || request.clientGroup != null && request.clientGroup.Any())
            {
                var uniqueJobCodes = allocationResp.Select(x => x.JobCode).Where(code => !string.IsNullOrEmpty(code)).Distinct().ToList();
                var clientresp = await _wCGTMasterHttpApi.GetClientListByJobCodes(uniqueJobCodes);
                
                if (request.clientName != null && request.clientName.Any())
                {
                    clientresp = clientresp.Where(a => request.clientName.Contains(a.client_id)).ToList();
                    var clientJobCodes = clientresp.Select(c => c.job_code).ToHashSet();
                    allocationResp = allocationResp.Where(a => clientJobCodes.Contains(a.JobCode)).ToList();
                }
                if (request.clientGroup != null && request.clientGroup.Any())
                {
                    clientresp = clientresp.Where(a => request.clientGroup.Contains(a.client_group_code)).ToList();
                    var clientJobCodes = clientresp.Select(c => c.job_code).ToHashSet();
                    allocationResp = allocationResp.Where(a => clientJobCodes.Contains(a.JobCode)).ToList();
                }                

            }
            List<ResourceAllocationResponse> response = AllocationMapper.Mapper.Map<List<ResourceAllocationResponse>>(allocationResp);
            //Console.WriteLine(a);
            return response;
        }
    }
}
