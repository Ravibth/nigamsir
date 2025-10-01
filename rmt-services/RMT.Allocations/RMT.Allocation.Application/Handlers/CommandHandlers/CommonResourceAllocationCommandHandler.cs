using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.DTOs.CommonResourceAllocationDTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Data;
using RMT.Allocation.Infrastructure.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using Constants_Infra = RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class CommonResourceAllocationCommand : IRequest<List<ResourceAllocationDetailsResponse>>
    {
        public AllResourceAllocationDTO[] resourceAllocationDTO { get; set; }
        public UserDecorator user { get; set; }
        public bool isDraft { get; set; }
    }
    public class CommonResourceAllocationCommandHandler : IRequestHandler<CommonResourceAllocationCommand, List<ResourceAllocationDetailsResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IIdentityUserDetailsHttpApi _identityUserDetailsHttpApi;
        private readonly IHolidayHttpService _holidayHttpService;

        public CommonResourceAllocationCommandHandler(
            IResourceAllocationRepository resourceAllocationRepository
            , IProjectServiceHttpApi projectServiceHttpApi
            , IRequisitionRepository requisitionRepository
            , IWCGTMasterHttpApi wCGTMasterHttpApi
            , IConfiguration configuration
            , HttpClient httpClient
            , IIdentityUserDetailsHttpApi identityUserDetailsHttpApi
            , IHolidayHttpService holidayHttpService
        )
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _requisitionRepository = requisitionRepository;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _configuration = configuration;
            _httpClient = httpClient;
            _identityUserDetailsHttpApi = identityUserDetailsHttpApi;
            _holidayHttpService = holidayHttpService;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> Handle(CommonResourceAllocationCommand request, CancellationToken cancellationToken)
        {


            var uniqueEmailIds = request.resourceAllocationDTO.Where(dto => !string.IsNullOrEmpty(dto.Email)).Select(dto => dto.Email).Distinct().ToList();

            // TODO Implement Tasks and check for validations

            // 1608 TBD Recheck 
            //1608 Holiday, leave and last day extraction must be done all in a single place and not different places
            // All should be called within a single function with a single API call
            var employeeDetails = await _identityUserDetailsHttpApi.GetEmployeesDataHttpApiQuery(uniqueEmailIds);
            var holidayResult = await _holidayHttpService.GetLocationSpecificHolidays(uniqueEmailIds, null, null);

            Dictionary<string, string> EmployeeEmailLocation = holidayResult.EmailLocationCollection;

            // 1608 Naming convention of GetHolidayLeaveResignedAbsconded , it represents a storage object and not a method to get data
            var leaveResults = new GetHolidayLeaveResignedAbsconded();
            var start_date_min = DateTime.MaxValue;
            var end_date_max = DateTime.MinValue;
            foreach (var item in request.resourceAllocationDTO)
            {
                var start_Date_comp = (DateTime)item.Allocations.MinBy(t => t.ConfirmedAllocationStartDate).ConfirmedAllocationStartDate;
                int res = DateTime.Compare(start_date_min, start_Date_comp);
                if (res >= 0)
                {
                    start_date_min = start_Date_comp;
                }
                var end_Date_comp = (DateTime)item.Allocations.MaxBy(t => t.ConfirmedAllocationEndDate).ConfirmedAllocationEndDate;
                int res1 = DateTime.Compare(end_date_max, end_Date_comp);
                if (res <= 0)
                {
                    end_date_max = end_Date_comp;
                }
            }
            if (uniqueEmailIds.Count > 0)
            {

                leaveResults = await Helper.GetHolidayLeaveResignedAbscondedByEmailIds(_configuration, uniqueEmailIds, _httpClient, _wCGTMasterHttpApi, start_date_min, end_date_max);
            }
            leaveResults.HolidayResponseTask = holidayResult.HolidayList;

            var response = new List<ResourceAllocationDetailsResponse>();
            request.resourceAllocationDTO = request.resourceAllocationDTO.OrderBy(x => x.type).ToArray();
            foreach (var allocationItem in request.resourceAllocationDTO)
            {
                // 1608 Open a transaction 
                try
                {
                    Requisition requisitionDetails =
                        allocationItem.RequisitionId != null
                            ? await _requisitionRepository.GetRequisitionDetailsByRequisitionId((Guid)allocationItem.RequisitionId)
                            : null;

                    if (requisitionDetails == null)
                    {
                        // If requisition does not exists, create new
                        var requisitionAdded = await CreateRequisition(allocationItem, request.user, request.isDraft);
                        requisitionDetails = requisitionAdded.FirstOrDefault();
                    }

                    //*************************************************************************
                    // Check if allocation exists for the user on the given dates
                    List<string> PipelineCodesForAllocationFetch = new();
                    if (allocationItem.ProjectInfo?.JobCode != null)
                    {
                        PipelineCodesForAllocationFetch.Add(allocationItem.ProjectInfo.JobCode);
                    }
                    else if (allocationItem.ProjectInfo?.PipelineCode != null)
                    {
                        PipelineCodesForAllocationFetch.Add(allocationItem.ProjectInfo.PipelineCode);
                    }

                    ResourceAllocationDetailsResponse allocationDetails = await _resourceAllocationRepository.GetAllocationByRequisitionId(requisitionDetails.Id);

                    ResourceAllocationDetailsResponse finalAllocationDetails = null;
                    if (allocationDetails != null)
                    {
                        // If allocation does exists, update allocation
                        var updatedAllocation = await UpdateAllocations(allocationItem, request.user, request.isDraft, requisitionDetails, leaveResults, allocationDetails);
                        finalAllocationDetails = updatedAllocation.FirstOrDefault();
                    }
                    else
                    {
                        // Else, Create new allocation
                        var addedAllocation = await CreateAllocation(allocationItem, request.user, request.isDraft, requisitionDetails, null, leaveResults, 1);
                        finalAllocationDetails = addedAllocation.FirstOrDefault();
                    }
                    //*************************************************************************

                    // Finally Update the Requisition
                    var requisitionUpdated = await UpdateRequisition(requisitionDetails, finalAllocationDetails, request.user, request.isDraft);
                    if (requisitionUpdated != null)
                    {
                        finalAllocationDetails.Requisition = requisitionUpdated.FirstOrDefault();
                    }
                    response.Add(finalAllocationDetails);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return response;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> UpdateAllocations(AllResourceAllocationDTO allocationItem, UserDecorator user, bool isDraft, Requisition requisition, GetHolidayLeaveResignedAbsconded leaveResults, ResourceAllocationDetailsResponse existingAllocationDetails)
        {
            if (existingAllocationDetails != null && requisition != null)
            {
                if (existingAllocationDetails.Type == AllocationType.PUBLISHED)
                {
                    // Create new unpublished records and mark this as updated
                    var addedUnPublishedAllocation = await CreateAllocation(allocationItem, user, isDraft, requisition, existingAllocationDetails.Id, leaveResults, (long)existingAllocationDetails.AllocationVersion + 1);
                    var publishedDetails = await _resourceAllocationRepository.GetPublishedResAllocDetailsById(existingAllocationDetails.Id);
                    if (publishedDetails != null)
                    {
                        publishedDetails.IsUpdated = true;
                        publishedDetails.ModifiedAt = DateTime.UtcNow;
                        await _resourceAllocationRepository.UpdatePublishedResAllocDetailsByEntity(publishedDetails);
                    }
                    return addedUnPublishedAllocation;
                }
                else
                {
                    // Update previous unpublished records
                    return await UpdatePreviousUnPublishedAllocations(existingAllocationDetails, allocationItem, user, isDraft, leaveResults);
                }
            }
            else
            {
                return new List<ResourceAllocationDetailsResponse>();
            }
        }

        public async Task<List<ResourceAllocationDetailsResponse>> UpdatePreviousUnPublishedAllocations(ResourceAllocationDetailsResponse allocation, AllResourceAllocationDTO allocationItem, UserDecorator user, bool isDraft, GetHolidayLeaveResignedAbsconded leaveResults)
        {
            List<UnPublishedResAlloc> allocationEntries = new();
            ResourceAllocationDetailsResponse entity = allocation;

            var previousDetails = await _resourceAllocationRepository.GetUnPublishedResAllocDetailsById(allocation.Id);
            if (previousDetails != null)
            {
                previousDetails.AllocationStatus = isDraft ? AllocationStatuses.DRAFT : AllocationStatuses.PENDING_APPROVAL;
                previousDetails.StartDate = DateOnly.FromDateTime((DateTime)allocationItem.Allocations.Min(m => m.ConfirmedAllocationStartDate));
                previousDetails.EndDate = DateOnly.FromDateTime((DateTime)allocationItem.Allocations.Max(m => m.ConfirmedAllocationEndDate));
                previousDetails.TotalEffort = allocationItem.TotalEfforts;
                previousDetails.CreatedAt = DateTime.UtcNow;
                previousDetails.ModifiedAt = DateTime.UtcNow;
                previousDetails.CreatedBy = user != null ? user.email : string.Empty;
                previousDetails.ModifiedBy = user != null ? user.email : string.Empty;
                previousDetails.Description = allocationItem.Description;
                previousDetails.AllocationVersion = previousDetails.AllocationVersion++;

                List<UnPublishedResAlloc> unPublishedResAllocations = new();
                foreach (var item in allocationItem.Allocations)
                {
                    unPublishedResAllocations.Add(new UnPublishedResAlloc()
                    {
                        StartDate = DateOnly.FromDateTime((DateTime)item.ConfirmedAllocationStartDate),
                        EndDate = DateOnly.FromDateTime((DateTime)item.ConfirmedAllocationEndDate),
                        Efforts = (long)item.ConfirmedPerDayHours,
                        IsPerDayAllocation = (bool)item.PerDayAllocation,
                        RequisitionId = previousDetails.RequisitionId,
                        // TODO 
                        // !Warning
                        TotalWorkingDays = 1,
                        UnPublishedResAllocDetailsId = previousDetails.Id,
                    });
                }
                previousDetails.ResourceAllocations = unPublishedResAllocations;
                var updatedAllocationDetails = await _resourceAllocationRepository.UpdateUnPublishedRecordsAsync(previousDetails, leaveResults, allocationItem?.ProjectInfo?.JobId);

                List<UnPublishedResAllocSkillEntity> skillsUpdated = await _resourceAllocationRepository.UpdateUnPublishedResourceAllocationSkillEntity(allocationItem.Skills.ToList(), allocation.RequisitionId, (user != null ? user.email : string.Empty), updatedAllocationDetails.Id);
                updatedAllocationDetails.Skills = await _resourceAllocationRepository.TransformUnPublishedResAllocSkillsIntoResourceAllocationSkillsResponse(skillsUpdated);
                return new List<ResourceAllocationDetailsResponse>() { updatedAllocationDetails };
            }
            return new List<ResourceAllocationDetailsResponse>() { };

        }

        public async Task<List<Requisition>> CreateRequisition(AllResourceAllocationDTO allocationItem, UserDecorator user, bool isDraft)
        {
            if (allocationItem.competency == null || allocationItem.competency.Competency == null || allocationItem.competency.CompetencyId == null)
            {
                throw new Exception("Details Missing");
            }
            // 1608 Do Mandatory validations check 
            var DtoData = new CreateRequisitionWithDemandRequestDTO
            {
                PipelineCode = allocationItem.ProjectInfo.PipelineCode,
                JobCode = allocationItem.ProjectInfo.JobCode,
                JobName = allocationItem.ProjectInfo.JobName,
                Description = allocationItem.Description,
                StartDate = DateOnly.FromDateTime((DateTime)allocationItem.Allocations.Min(a => a.ConfirmedAllocationStartDate)),
                EndDate = DateOnly.FromDateTime((DateTime)allocationItem.Allocations.Max(a => a.ConfirmedAllocationEndDate)),
                TotalHours = allocationItem.TotalEfforts,
                Status = RequisitionStatuses.PENDING,
                BusinessUnit = allocationItem.ProjectInfo.bu,
                Competency = allocationItem.competency.Competency,
                CompetencyId = allocationItem.competency.CompetencyId,
                Solutions = allocationItem.ProjectInfo.Solutions,
                Offerings = allocationItem.ProjectInfo.Offerings,
                //Expertise = allocationItem.ProjectInfo.Expertise,
                //SMEG = allocationItem.ProjectInfo.Sme,
                Designation = allocationItem.UserInfo.Designation,
                Grade = allocationItem.UserInfo.Grade,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                CreatedBy = user != null ? user.email : string.Empty,
                ModifiedBy = user != null ? user.email : string.Empty,
                IsActive = true,
                Skills = allocationItem.Skills.ToList(),
                IsAllResourcesSimilar = true,
                ClientName = allocationItem.ProjectInfo.ClientName,
                EffortsPerDay = allocationItem.TotalEfforts,
                IsPerDayHourAllocation = (bool)allocationItem.Allocations[0].PerDayAllocation,
                NumberOfResources = 1,
                PipelineName = allocationItem.ProjectInfo.PipelineName,
                RequisitionTypeId = (long)await GetRequisitionTypeIDByType(allocationItem.type),
            };
            return await _requisitionRepository.CreateRequisitionWithDemand(DtoData);
        }

        public async Task<List<Requisition>> UpdateRequisition(Requisition requisitionData, ResourceAllocationDetailsResponse allocationDetails, UserDecorator user, bool isDraft)
        {
            if (requisitionData != null)
            {
                requisitionData.StartDate = allocationDetails.StartDate;
                requisitionData.EndDate = allocationDetails.EndDate;
                requisitionData.TotalHours = allocationDetails.TotalEffort;
                requisitionData.EffortsPerDay = allocationDetails.TotalEffort;
                requisitionData.IsPerDayHourAllocation = false;
                requisitionData.RequisitionStatus = RequisitionStatuses.ALLOCATED;
                //requisitionData.RequisitionStatus = isDraft ? RequisitionStatuses.DRAFT : RequisitionStatuses.PENDING_APPROVAL;
                requisitionData.ModifiedAt = DateTime.UtcNow;
                requisitionData.ModifiedBy = user != null && !String.IsNullOrEmpty(user?.email) ? user?.email : string.Empty;
                return new List<Requisition>() { await _requisitionRepository.UpdateRequisitionByRequisitionEntity(requisitionData) };
            }
            throw new Exception("Requisition Details not found");
        }

        public async Task<Int64?> GetRequisitionTypeIDByType(string type)
        {
            var requisitionType = "";
            switch (type)
            {
                case EAllocationType.SAME_TEAM_ALLOCATION:
                    requisitionType = RequisitionTypeData.SameTeamAllocation;
                    break;
                case EAllocationType.NAME_ALLOCATION:
                    requisitionType = RequisitionTypeData.NamedAllocation;
                    break;
                case EAllocationType.BULK_ALLOCATION:
                    requisitionType = RequisitionTypeData.BulkAllocation;
                    break;
                default: return null;
            }
            if (!String.IsNullOrEmpty(requisitionType))
            {
                return (await _requisitionRepository.GetRequisitionTypeByType(requisitionType))?.Id;
            }
            return null;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> CreateAllocation(AllResourceAllocationDTO allocationItem, UserDecorator user, bool isDraft, Requisition requisitionData, Guid? parentPublishedAllocationDetailsId, GetHolidayLeaveResignedAbsconded leaveResults, Int64 allocationVersionToAdd)
        {
            // 1608 Do mandatory validation checks
            if (requisitionData != null)
            {
                var jobId = allocationItem?.ProjectInfo?.JobId;

                List<AddResourceAllocationSkillRequestDTO> addResourceAllocationSkillRequestDTOs = new();
                List<SkillsEntities> skills = new();

                if (allocationItem?.Skills != null && allocationItem.Skills.Length > 0)
                {
                    skills.AddRange(allocationItem.Skills);
                }

                addResourceAllocationSkillRequestDTOs.Add(new()
                {
                    RequisitionId = requisitionData.Id,
                    Skills = skills,
                });

                List<UnPublishedResAllocDetails> unPublishedResourceAllocationDetailsEntity = new()
                {
                    new ()
                    {
                        StartDate = DateOnly.FromDateTime((DateTime)allocationItem.Allocations.Min(m => m.ConfirmedAllocationStartDate)),
                        EndDate = DateOnly.FromDateTime((DateTime)allocationItem.Allocations.Max(m => m.ConfirmedAllocationEndDate)),
                        EmpName = allocationItem.UserInfo.EmpName,
                        EmpEmail = allocationItem.UserInfo.Email,
                        TotalEffort = allocationItem.TotalEfforts,
                        RequisitionId = requisitionData.Id,
                        AllocationStatus = isDraft ? AllocationStatuses.DRAFT : AllocationStatuses.PENDING_APPROVAL,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        CreatedBy = user != null ? user.email:string.Empty,
                        ModifiedBy =  user != null ? user.email:string.Empty,
                        Description = allocationItem !=null ? allocationItem?.Description : string.Empty,
                        AllocationVersion = allocationVersionToAdd,
                        IsActive = true,
                        ParentPublishedResAllocDetailsId = parentPublishedAllocationDetailsId,
                    }
                };
                List<AllocationObj> allocationObj = new();
                foreach (var allocationBreakup in allocationItem.Allocations)
                {
                    allocationObj.Add(new()
                    {
                        startDate = DateOnly.FromDateTime((DateTime)allocationBreakup.ConfirmedAllocationStartDate),
                        endDate = DateOnly.FromDateTime((DateTime)allocationBreakup.ConfirmedAllocationEndDate),
                        isPerDayHourAllocation = (bool)allocationBreakup.PerDayAllocation,
                        effort = (long)allocationBreakup.ConfirmedPerDayHours
                    });
                }
                Dictionary<string, double> resourceRateDict = new();

                var rateRequest = new List<GetRateDesignationRequestDTO>()
                {
                    new()
                    {
                        Designation = allocationItem.UserInfo.Designation,
                        Competency = allocationItem.UserInfo.Competency
                    }
                };
                List<GetRateDesignationDTO> rates = await _wCGTMasterHttpApi.GetRateByDesignation(rateRequest);

                var rateData = rates.FirstOrDefault();
                if (rateData != null)
                {
                    resourceRateDict.Add(allocationItem.Email, rateData.RatePerHour);
                }

                var allocationEntry = await _resourceAllocationRepository.AddAsync(unPublishedResourceAllocationDetailsEntity, allocationObj.ToArray(), resourceRateDict, addResourceAllocationSkillRequestDTOs, leaveResults, jobId);

                // 1608 move to service layer
                if (!isDraft)
                {
                    List<AddProjectUserRole> projectRoles = new()
                    {
                        new AddProjectUserRole
                        {
                            User = allocationItem.UserInfo.Email,
                            UserName = allocationItem.UserInfo.EmpName,
                            Role = Constants_Infra.UserRoles.Employee //Utils.Constants.Employee
                        }
                    };

                    await RMT.Allocation.Application.Utils.Helper.AddProjectRoles(projectRoles, (long)allocationItem.ProjectInfo.Id, allocationItem.ProjectInfo.PipelineCode, allocationItem.ProjectInfo.JobCode, _projectServiceHttpApi);
                }
                return allocationEntry;
            }
            else
            {
                throw new Exception("Requisition does not exist");
            }
        }
    }
}
