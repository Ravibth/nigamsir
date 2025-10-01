using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.DTOs.Response;
using RMT.Projects.Domain.Entities;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync();
        Task<List<Project>> GetAllProjectForBudget();
        Task<List<Project>> GetAllProjectForBudgetByJobCodes(List<string> JobCodes);
        Task<List<Project>> GetAllProjectByBUandExpertiseAsync(string BU, string Expertise, DateTime EndDate);
        Task<Project> AddAsync(Project entity);
        Task<Project> UpdateAsync(Project entity);
        Task<List<Project>> GetAllProjectsByCreationDate(DateOnly creationDate);
        Task<Project> UpdateProjectRolledOver(Project entity);
        Task<Project> UpdateProjectSuspensionStatus(List<Project> entity);
        Task<Project> UpdateProjectBudgetStatus(Project entity, string budgetStatus);
        //custom operations here
        Task<Project> GetProjectById(Int64 id);
        Task<Project> GetProjectByCode(string pipelineCode, string? jobCode);

        Task<List<Project>> GetProjectsByRequestorEmail(UserDecorator userDecorator, string requestorEmail, List<string>? bu, List<string>? offerings, List<string>? solutions,
            List<string> roles,
            string projectChargeType, List<string>? industry, List<string>? subIndustry, List<string>? clientNames, List<string>? pipelineCodes, List<string>? jobCodes, List<string>? jobNames,
           List<string>? projectStatus, string? projectType, bool? marketPlace, string? searchQuery, int pagination, int limit,
           GetBuExpertiesDTO? buMappingList, List<CompetencyMasterDTO>? competencyMasters);
        Task<List<Project>> GetProjectsDetailsByEmail(string emailId, int pagination, int limit, string role);
        Task<Project> GetProjectDetailsForRequestor(string pipelineCode, string? jobCode);
        Task RemoveProjectCompetency(List<RefreshProjectCompetencyRequestDTO> request);

        Task<Project> GetProjectDetailsForEmployee(string pipelineCode, string? jobCode);

        ////Move this method to Alllocation
        //Task<List<EmployeeProject>> GetProjectsByEmail(string employeeEmail, string? projectChargeType, List<string>? expertises, List<string>? smes, List<string>? clientNames, List<string>? projectCodes, List<string>? projectNames);

        ////Move this method to Alllocation Service
        //Task<List<EmployeeProject>> GetEmployeesByProjectCode(string pipelineCode,string? jobCode)
        Task<Project> GetProjectByPipelineCode(string pipelineCode, string? jobCode);

        Task<Project> GetProjectDetailsByCode(string pipelineCode, string? jobCode);
        /*        Task<Project> GetProjectDetailsForRequestor(string projectCode);

                Task<Project> GetProjectDetailsForEmployee(string projectCode);*/
        Task<ProjectRoles[]> AddProjectUserWithRole(ProjectRoles[] entity);
        Task<List<Project>> GetEmployeeListingProjectData(List<KeyValuePair<string, string>> projectCodes);
        Task<List<ProjectRolesView>> GetReviewerEmailsByPipelineCode(string pipelineCode, string? jobCode);
        Task<List<ProjectRolesView>> GetAllProjectRolesByCodes(KeyValuePair<string, string?> pipelineCode);
        Task<List<ProjectRolesView>> GetRequestorEmailsByPipelineCode(string pipelineCode, string? jobCode);
        Task<List<Project>> GetMultipleProjectByCodes(List<KeyValuePair<string, string>> projectCodes);
        Task<List<Project>> GetRequestorEmailsListByPipelineCode(List<KeyValuePair<string, string?>> pipelineCodes);
        Task<List<Project>> GetAllProjectDetailsForMarketPlace();
        Task<ProjectRoles[]> RemoveProjectUserWithRole(ProjectRoles[] entity);
        Task<Project> SetIsRequisitionCreationAllowed(bool IsRequisitionCreationallowed, string pipelineCode, string? jobCode);
        Task<List<Project>> UpdatePublishedToMarketPlace(List<UpdatePublishedToMarketPlaceDTO> updatePublishedToMarketPlaceDTO);
        Task<Dictionary<string, List<ProjectRolesView>>> GetProjectRolesByPipelineCodeAndRoles(List<KeyValuePair<string, string?>> pipelineCode, List<string>? roles);

        Task<Dictionary<string, List<ProjectRolesView>>> GetProjectRolesByPipelineCodeAndAppRoles(List<KeyValuePair<string, string?>> pipelineCodes, List<string>? roles);
        Task<List<GetProjectRolesByEmailsResponse>> GetProjectRolesByEmails(List<string> emails);
        Task<List<FieldForMarketPlace>> GetFieldForMarketPlace();
        Task<List<ProjectRolesView>> GetOnlyEmployeesOfProject(string pipelineCode, string? jobCode);
        Task<FieldForMarketPlace> CreateOrUpdateActiveFieldForMarketPlace(string InternalName, string DisplayName, bool IsActive);
        Task<List<PublishedFieldForMarketPlace>> GetPublishedFieldForMarketPlace(string PipelineCode, string? jobCode);
        Task<PublishedFieldForMarketPlace> CreateOrUpdatePublishedFieldForMarketPlace(string PipelineCode, string? jobCode, string FieldName, bool IsActive);
        Task<List<GetMembersOfAllProjectsOfUserResponse>> GetMembersOfAllProjectsOfUser(List<string> userEmail, List<string>? rolesProvided);
        Task<GetProjectDetailsByUserRole> GetProjectDetailsByPipelineCodeAndUserRole(string pipelineCode, string? jobCode, string? userEmailId, List<string>? applicationRoles);
        Task<List<ProjectRoles>> GetRequestorEmailForAllocationWorkflow(string pipelineCode, string? jobCode, string workflowStartedBy);
        Task<Boolean> AddUpdateProjectRequisitionAllocation(List<ProjectRequisitionAllocationRequestDTO> projectRequisitionAllocationRequestDTOs, string updatedBy);
        Task<List<ProjectBudget>> GetProjectBudget(string pipelineCode, string? jobCode);
        Task<Project> AddJusticiationText(Project entity, string justificationText);
        Task<List<string>> GetAllJobCodesForPipelineCode(string pipelineCode, string jobCode, bool? SameTeamAllocation);

        Task<List<Project>> GetProjectsByUniqueCodes(List<KeyValuePair<string, string?>> projectUniqueCode);
        Task<List<ProjectCompetency>> AddProjectCompetencies(List<AddCompetencyRequestDTO> request);
        Task<Boolean> UpdateProjectJobCode(string pipelineCode, string? jobCode, string? newJobCode, string newJobName, string updatedBy);
        Task<List<GetOfferingSolutionsByJobCodeResponseDTO>> GetOfferingSolutionsByJobCode(List<string> jobCodes);
        Task<List<Project>> GetAllProjectBudgetList();
        Task<Project> AddSuperCoachRole(AddSuperCoachProjectRoleRequestDTO req);
        Task<List<ProjectRoles>> UpdateProjectRolesForSupercoachDelegate(string supercoach_email, string? prev_supercoach_delegate_email, string? new_supercoach_delegate_email, string? new_supercoach_delegate_name);
    }
}
