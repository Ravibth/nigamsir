using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;

namespace RMT.Allocation.Domain.Repositories
{
    public interface IRequisitionRepository
    {
        Task<Requisition> GetRequisitionDetailsByRequisitionId(Guid requisitionId, bool filterZeroWeightageParameters = false);
        Task<List<SystemSuggestionResponseDTO>> GetSystemSuggestions(int limit, int pagination, double pref_weightage_constraint, List<EmployeeDetailsDTO> employeeDetails, Requisition requisitionDetails, Guid requisitionId, int minimumPercentageForSystemSuggestions, Object[] filter, string[] parameter_value_pairs, string orderScoreBy);
        Task<List<Requisition>> GetAllRequisitionByProjectCode(string pipelineCode, string? jobCode);
        Task<List<Requisition>> GetAllRequisitionByProjectCodeForProjectDetails(string pipelineCode, string? jobCode);
        Task<Requisition> DeleteRequisitionById(Guid requisitionId);
        Task<Boolean> SuspendAllocationRequisition(List<KeyValuePair<string, string>> projectCodes);
        Task<List<Requisition>> AddRequisitionAsync(RequisitionRequest entity);
        Task<double> GetAllProjectRequisitionHoursByPipelineCode(string pipelineCode, string jobCode);
        Task<List<ProjectAllocatedHoursRatioDto>> GetAllProjectRequisitionHoursByPipelineCode(List<KeyValuePair<string, string>> pipelineCodes);
        Task<List<Requisition>> UpdateRequisition(UpdateRequisitionRequest entity
            , bool restrictIfPreviouslyAllocated = true
            , bool updateSkills = true
            , bool updateParameters = true
            , bool updateLocations = true
            , bool updateIndustries = true
            , bool updateSubIndustries = true
        );
        Task<BulkRequistionResponse> AddBulkRequisitions(List<BulkRequisition> entity, UserDecorator userDecorator);

        Task<List<Requisition>> CreateRequisitionWithDemand(CreateRequisitionWithDemandRequestDTO createRequisitionWithDemandRequestDTO);
        Task<RequisitionType> GetRequisitionTypeByType(string type);
        Task<List<RequisitionSkill>> GetSkillsByRequstionId(List<Guid> requistionId);
        Task<Requisition> UpdateRequisitionByRequisitionEntity(Requisition requisition);
        Task<Boolean> IsRequistionExistsInProject(string pipelineCode, string? jobCode);
        Task<List<Requisition>> GetRequistionByDate(DateTime CreatedAt, DateTime ModifiedAt);
        Task<List<PublishedResAllocDetails>> GetPublishedAllocationByDate(DateTime CreatedAt, DateTime ModifiedAt);
    }

}
