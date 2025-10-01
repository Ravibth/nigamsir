using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.DTOs;

namespace RMT.Allocation.Domain.Repositories
{
    public interface IResourceAllocationRepository
    {
        Task DeletePublishedResAllocDetailsByReqId(Guid requisitionId);
        Task<List<ResourceAllocationResponse>> GetResourceAllocationByEmail(string EmpEmail);
        Task<List<ResourceAllocationDetailsResponse>> AddAsync(List<UnPublishedResAllocDetails> entity, AllocationObj[] allocations, Dictionary<string, double> resourceRate, List<AddResourceAllocationSkillRequestDTO> addResourceAllocationSkillRequestDTOs, GetHolidayLeaveResignedAbsconded leaveResults, string jobId);
        Task<List<ResourceAllocationResponse>> GetProjectsByEmployeeEmailAndPipelineCode(string email, string pipelineCode, string jobCode, string? allocationType);
        Task<ResourceAllocationDetailsResponse> GetAllocationByRequisitionId(Guid RequisitionId);
        Task<Int64> GetWithConsumedHours(Guid requisitionId);
        Task<ResourceAllocationDetailsResponse> UpdateUnPublishedRecordsAsync(UnPublishedResAllocDetails unPublishedResAllocDetailsToUpdate, GetHolidayLeaveResignedAbsconded leaveResults, string jobId, bool? updateAllocationDates = false);
        Task<ResourceAllocationDetailsResponse> UpdateAsync(ResourceAllocationDetailsResponse resourceAllocationDetails, bool? updateAllocationDates = false);
        Task<List<UsersAvailability>> isUserAvailable(UsersAvailabilityCheckDTO usersAvailabilityCheck);
        Task<List<ResourceAllocationResponse>> GetUserAllocationsByEmailAndDates(List<string> emails, DateTime startdate, DateTime enddate);
        Task<List<ResourceAllocationDetailsResponse>> GetUserAllocationDetailsByEmailAndDates(List<string> emails, DateTime? startdate, DateTime? enddate, List<string>? PipelineCodes, bool? CheckUnPublishedAsWell);
        Task<ResourceAvailable> GetResourceAvailableHours(ResourceAvailable resourceAvailable, List<GTHolidayDTO> holidayList, List<GTLeaveBaseDTO> leaveList, string? pipelineCode, string? jobCode);
        Task<List<ResourceAllocationDetailsResponse>> GetAllocationByJobCodeHandler(List<string> jobCodes);
        Task<List<ProjectAllocatedHoursRatioDto>> GetAllProjectAllocationHoursByPipelineCode(List<KeyValuePair<string, string>> pipelineCodes);
        Task<Boolean> SuspendAllocationPerDay(List<KeyValuePair<string, string>> projectCodes);
        Task<Boolean> SuspendAllocations(List<KeyValuePair<string, string>> projectCodes);
        Task<List<SuspendAllocationResponse>> SuspendAllocationsDetails(List<KeyValuePair<string, string>> projectCodes);
        Task<List<BudgetOverviewDto>> GetBudgetOverview(BudgetOverviewRequest request);
        Task<List<ResourceAllocationDesignation>> GetDesignationBudget(string pipelineCode, string jobCode);
        Task<ResourceAllocationDetailsResponse> UpdateAllocationStatus(UnPublishedResAllocDetails resourceAllocationDetails);
        Task<List<ResourceAllocationDetailsResponse>> GetActiveAllocationByPipeLineCode(string pipelineCode, string jobCode);
        Task<List<ResourceAllocationDetailsResponse>> GetApprovedAllocationByPipeLineCode(string pipelineCode, string? jobCode);
        Task<ResourceAllocationDetailsResponse> GetAllocationByGuidHandler(Guid guid, bool? discardInactiveRecords = false);
        Task<ResourceAllocationDetailsResponse> ReleaseResourceByGuid(Guid guid, string ModifiedBy);
        Task<ResourceAllocationDetailsResponse> ReleaseResourceActiveAllocation(Guid guid, string ModifiedBy);
        Task<List<ResourceAllocationDaysResponse>> GetUserPerDayAllocationsByEmailAndDates(List<string> emails, DateTime startdate, DateTime enddate);
        Task<List<ResourceAllocationResponse>> GetAllocationByEmailAndLeaveStartDateAndEndDate(string email, DateTime leave_start_date, DateTime leave_end_date);
        Task<List<AllocationDayGroup>> ResourceAllocationDayGroup(string TimeOption, string pipelineCode, string jobCode, DateTime startDate, DateTime endDate);
        Task<List<AllocationDayResourceGroup>> ResourceAllocationDayResourceGroup(DateTime startDate, DateTime endDate, string pipelineCode, string jobCode);
        Task<UpdateListOfAllocationDetailsStatusResponse> UpdateListOfAllocationDetailsStatus(List<UpdateListOfAllocationDetailsStatusRequest> request, GetHolidayLeaveResignedAbsconded leaveResult = null);
        Task<List<UpdateDesignationCost>> UpdateDesignationCost(List<UpdateDesignationCostDTO> request);
        Task<List<JobAllocationMappingDTO>> GetJobAllocationMapping(List<DateTime> confirmedDate);
        Task<List<ResourceAllocationDaysResponse>> GetConfirmedPerDayHoursByDate(DateTime startDate, DateTime endDate);
        Task<Boolean> UpdateAllocationWithNewJobCode(string pipelineCode, string jobCode, string new_pipelineCode, string newJobCode, string newJobName, string updatedBy);
        Task<List<UnPublishedResAllocSkillEntity>> UpdateUnPublishedResourceAllocationSkillEntity(List<SkillsEntities> finalSkills, Guid requisitionId, string userEmail, Guid unPublishedResAllocDetailsId);
        Task<List<string>> DoesUserHaveAnyFutureOrOngoingAllocations(List<string> emails);
        Task<List<Guid>> GetAllDraftAllocationForEmployeeForRemoval(List<string> emails);
        Task<UnPublishedResAllocDetails?> GetUnPublishedResAllocDetailsById(Guid id);
        Task<List<ResourceAllocationDetailsResponse>> TransformPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(List<PublishedResAllocDetails> publishedResAllocDetails);
        Task<List<ResourceAllocationDaysResponse>> TransformPublishedResAllocDaysIntoResourceAllocationDaysResponse(List<PublishedResAllocDays> publishedResAllocDays);
        Task<List<ResourceAllocationResponse>> TransformPublishedResAllocIntoResourceAllocationResponse(List<PublishedResAlloc> publishedResAlloc);
        Task<List<ResourceAllocationSkillsResponse>> TransformPublishedResAllocSkillsIntoResourceAllocationSkillsResponse(List<PublishedResAllocSkillEntity> publishedResAllocSkillEntity);
        Task<List<ResourceAllocationDetailsResponse>> TransformUnPublishedResAllocDetailsIntoResourceAllocationDetailsResponse(List<UnPublishedResAllocDetails> unPublishedResAllocDetails);
        Task<List<ResourceAllocationDaysResponse>> TransformUnPublishedResAllocDaysIntoResourceAllocationDaysResponse(List<UnPublishedResAllocDays> UnpublishedResAllocDays);
        Task<List<ResourceAllocationResponse>> TransformUnPublishedResAllocIntoResourceAllocationResponse(List<UnPublishedResAlloc> UnpublishedResAlloc);
        Task<List<ResourceAllocationSkillsResponse>> TransformUnPublishedResAllocSkillsIntoResourceAllocationSkillsResponse(List<UnPublishedResAllocSkillEntity> UnpublishedResAllocSkillEntity);
        Task<PublishedResAllocDetails> UpdatePublishedResAllocDetailsByEntity(PublishedResAllocDetails publishedResAllocDetails);
        Task<PublishedResAllocDetails> GetPublishedResAllocDetailsById(Guid id);
        Task<List<ResourceAllocationDetailsResponse>> GetAllAllocationByPipelineOrJobCode(string pipelineCode, string? jobCode);
        Task<List<ResourceAllocationDetailsResponse>> UpdateResourcesAllocations(ResourceAllocationDetailsResponse[] allResourceAllocationDTO);
        Task<List<ResourceAllocationDaysResponse>> GetResAllocDaysRespFromPubResAllocId(Guid resAllocId);
        Task<List<ResourceAllocationDetailsResponse>> GetActivePublishedAllocationByPipeLineCode(List<KeyValuePair<string, string?>> requests);
        Task<List<GetUserAvailabilitiesForSystemSuggestionResponse>> GetAvailabilityForSystemSuggestion(
            List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion> users
            , Int64 required_hours
            , DateTime start_date
            , DateTime end_date
            , string job_code
            , string? pipeline_code
            );
        Task<bool> IsUserAlreadyAllocatedForSameProjectInBetweenDates(string email, string pipelineCode, string? jobCode, DateOnly startDate, DateOnly endDate, Guid? requisitionIdToAvoid);
        Task DeletePublishedAllocationByDetails(PublishedResAllocDetails PublishedResourceAllocationDetails);
        Task<List<ResourceAllocationDetailsResponse>> GetListOfAllocationByGuidHandler(List<Guid> guidRequest, bool? discardInactiveRecords = true);
        Task<List<DraftTimesheetResponse>> GetDraftTimesheet(List<DateTime> dates);
        Task<bool> UpdatePublishAllocationActualEfforts(List<UpdatePublishAllocationActualEffortsRequestDTO> req);
        Task<List<TimesheetDaysReponseDTO>> GetTimesheetDaysReponse(string jobCode, string timeOption, DateTime startDate, DateTime endDate);
        Task<List<TimesheetResponseDTO>> GetProjectDesignationTimesheet(string jobCode);
        Task<List<ResourceTimesheetDTO>> GetResourceTimesheetDataByJobCode(string jobCode, DateTime startDate, DateTime endDate);
        Task<List<AllocationDayResourceGroup>> PublishedResouceAllocationDaysGroup();
        Task<List<ResourceAllocationDaysResponse>> GetPublishedResourceAllocationDays(DateTime startDate, DateTime endDate);
        Task<List<ResourceAllocationDaysResponse>> GetPublishedResourceAllocationDays(List<string> employeeEmails, DateTime startDate, DateTime endDate);
        Task<int> GetAllocationCount(string pipelinecode, string jobCode);
        Task<int> GetRequisitionCount(string pipelinecode, string jobCode);
    }
}
