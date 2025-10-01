//Not in use
//using MediatR;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;
//using RMT.Allocation.Application.DTOs;
//using RMT.Allocation.Application.HttpServices.DTOs;
//using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http.Json;
//using System.Text;
//using System.Threading.Tasks;
//using static RMT.Allocation.Domain.ConstantsDomain;

//namespace RMT.Allocation.Application.Handlers.CommandHandlers
//{
//    public class RollOverAllocationDTO
//    {
//        public long RequisitionId { get; set; }

//    }

//    public class UpdateRollOverAllocationCommand : IRequest<RollOverResponse>
//    {
//        public string PipelineCode { get; set; }
//        public string JobCode { get; set; }

//        public bool ÌsRollOver { get; set; }

//        public int RollOverDays { get; set; }

//        public List<RollOverAllocationDTO> RollOverAllocations { get; set; }

//    }

//    public class UpdateRollOverAllocationCommandHandler : IRequestHandler<UpdateRollOverAllocationCommand, RollOverResponse>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        private readonly IConfiguration _configuration;
//        private readonly HttpClient _httpClient;
//        public UpdateRollOverAllocationCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IConfiguration configuration, HttpClient httpClient)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//            _configuration = configuration;
//            _httpClient = httpClient;
//        }

//        public async Task<RollOverResponse> Handle(UpdateRollOverAllocationCommand request, CancellationToken cancellationToken)
//        {

//            RollOverResponse rollOverResponse1 = new RollOverResponse();

//            List<ResourceAvailable> isAllocationsValid1 = new List<ResourceAvailable>();

//            List<ResourceAllocationDetails> resourceToBeAllocated1 = new List<ResourceAllocationDetails>();

//            ResourceAvailable chkResourceAvailable1 = null;
//            ResourceAllocationDetails chkResourceAllocation1 = null;
//            ResourceAvailable chkResourceAvailableResponse1 = null;

//            int resourceAllocationHours = 0;
//            string baseurl = _configuration.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
//            string getHolidaysLeaves = _configuration.GetSection("MicroserviceApiSettings").GetSection("GetHolidayPath").Value;
//            string getsLeaves = _configuration.GetSection("MicroserviceApiSettings").GetSection("GetEmployeeLeavePath").Value;

//            var holidayResponse = await _httpClient.GetAsync(baseurl + getHolidaysLeaves);
//            if (!holidayResponse.IsSuccessStatusCode)
//            {
//                string response = await holidayResponse.Content.ReadAsStringAsync();

//                throw new Exception("Unable to fetch holiday" + response);
//            }
//            List<GTHolidayDTO> holidayList = await holidayResponse.Content.ReadFromJsonAsync<List<GTHolidayDTO>>();

//            foreach (var item in request.RollOverAllocations)
//            {
//                chkResourceAllocation1 = await _resourceAllocationRepository.GetAllocationByRequisitionId(item.RequisitionId);

//                int reqHours = int.Parse(chkResourceAllocation1.Requisitions.TotalHours);
//                resourceAllocationHours = 0;

//                foreach (var _raItem in chkResourceAllocation1.ResourceAllocation)
//                {
//                    chkResourceAvailable1 = new ResourceAvailable()
//                    {
//                        EmailId = _raItem.EmpEmail,
//                        StartDate = _raItem.ConfirmedAllocationStartDate.Value.AddDays(request.RollOverDays),
//                        EndDate = _raItem.ConfirmedAllocationEndDate.Value.AddDays(request.RollOverDays),
//                        RequireWorkingHours = _raItem.ConfirmedPerDayHours,
//                        RequisitionId = _raItem.RequisitionId,
//                    };
//                    resourceAllocationHours += _raItem.ConfirmedPerDayHours;
//                    var employeeLeaveDto = new LeaveParamsDTO()
//                    {
//                        emp_emailid = new List<string>() { _raItem.EmpEmail },
//                    };

//                    var content = new StringContent(JsonConvert.SerializeObject(employeeLeaveDto), Encoding.UTF8, "application/json");
//                    var leavesResponse = await _httpClient.PostAsync(baseurl + getsLeaves, content);
//                    if (!leavesResponse.IsSuccessStatusCode)
//                    {
//                        string response = await leavesResponse.Content.ReadAsStringAsync();
//                        throw new Exception("Unable to fetch leaves" + response);
//                    }
//                    List<GTLeaveBaseDTO> leaveList = await leavesResponse.Content.ReadFromJsonAsync<List<GTLeaveBaseDTO>>();

//                    chkResourceAvailableResponse1 = await _resourceAllocationRepository.GetResourceAvailableHours(chkResourceAvailable1, holidayList, leaveList);
//                    chkResourceAvailableResponse1.EmailId = chkResourceAvailable1.EmailId;

//                    int totalAllocatedHours = chkResourceAllocation1.ResourceAllocation.Sum(x => x.ConfirmedPerDayHours);
//                    if (totalAllocatedHours > reqHours)
//                    {
//                        chkResourceAvailableResponse1.IsHoursAvialable = false;
//                        chkResourceAvailableResponse1.ErrorMsg += " Allocation of hours can not be greator than requisition hours.";
//                    }
//                    isAllocationsValid1.Add(chkResourceAvailableResponse1);
//                }

//                //check if available ahours are match and req hours are equal or gretor than the total allocated hours for that 
//                if (chkResourceAvailableResponse1.IsHoursAvialable && reqHours >= resourceAllocationHours)
//                    resourceToBeAllocated1.Add(chkResourceAllocation1);
//            }

//            if (isAllocationsValid1.Where(x => x.IsHoursAvialable == false).Any())
//            {
//                // not valid return invalid records
//                //return invalid allocation back to client
//                rollOverResponse1.InvalidAllocations = isAllocationsValid1.Where(x => x.IsHoursAvialable == false).ToList();
//            }
//            else
//            {
//                //valid action save the allocations
//                ResourceAllocation resourceAllocationUpdateObj1 = null;
//                List<ResourceAllocation> resourceAllocationCollUpdate = new List<ResourceAllocation>();
//                ResourceAllocationDetails updateResourceAllocated1 = null;

//                List<ResourceAllocationDetailsResponse> allocatedResponse = new List<ResourceAllocationDetailsResponse>();

//                foreach (var raItemDetails in isAllocationsValid1)
//                {
//                    updateResourceAllocated1 = await _resourceAllocationRepository.GetAllocationByRequisitionId(raItemDetails.RequisitionId);

//                    var entity = AllocationMapper.Mapper.Map<ResourceAllocationDetails>(updateResourceAllocated1);

//                    entity.RecordType = EAllocationRecordType.RolloverAllocation.ToString();
//                    entity.AllocationStatus = EAllocationStatus.RolloverAllocationPending.ToString();

//                    foreach (var _raItem in entity.ResourceAllocation)
//                    {
//                        _raItem.ConfirmedAllocationStartDate = (_raItem.ConfirmedAllocationStartDate.Value.AddDays(request.RollOverDays)).Date;
//                        _raItem.ConfirmedAllocationEndDate = (_raItem.ConfirmedAllocationEndDate.Value.AddDays(request.RollOverDays)).Date;
//                        _raItem.RecordType = EAllocationRecordType.RolloverAllocation.ToString();
//                        _raItem.AllocationStatus = EAllocationStatus.RolloverAllocationPending.ToString();
//                    }
//                    ResourceAllocationDetails newResourceAllocation1 = await _resourceAllocationRepository.UpdateAsync(entity);

//                    ResourceAllocationDetailsResponse resourceAllocationResponse = AllocationMapper.Mapper.Map<ResourceAllocationDetailsResponse>(newResourceAllocation1);
//                    if (!allocatedResponse.Where(a => a.Id == resourceAllocationResponse.Id).Any())
//                    {
//                        allocatedResponse.Add(resourceAllocationResponse);
//                    }
//                }

//                rollOverResponse1.UpdatedAllocations = allocatedResponse;

//            }

//            return rollOverResponse1;
//        }
//    }
//}
