using RMT.Notification.Application.HttpServices.DTO;
using RMT.Notification.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ConfigurationService
{
    public interface IConfigurationService
    {
        Task<List<NotificationTemplateDTO>> GetNotificationTemplate(string[] type, string token);
        Task<string> GetResourceAllocationDetailsByGuid(string guid);
        string TransformMessageTemplateAccordingToPayloads(string template, List<NotificationPlaceHolderDTO> payload, Dictionary<string, string> data);
        Task<string> GetRequestorEmailsForAllocationWorkflow(string pipelineCode, string jobCode, string workflowCreatedBy);
        Task<string> GetUserInfoByUserEmailId(string emailId);
        Task<string> GetResourceReviewerEmailsByPipelineCode(string pipelineCode, string jobCode);
        Task<string> GetProjectDetailsByPipelineCodeAndJobCode(string pipelineCode, string? jobCode);
        Task<List<WorkflowDTO>> GetWorkflowPendingTasks();
        Task<List<RoleEmailsByPipelineCodeResponse>> GetRolesEmailByPipelineCodesAndRoles(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto);
        Task<string> GetResourceRequestorEmailsByPipelineCode(string pipelineCode, string? jobCode);
        Task<List<MarketPlaceProjectDetailDTO>> GetMarketPlaceProjectListByPublishDate(DateOnly publishDate);
        Task<List<ProjectFullDetailsResponse>> GetAllProjectByCreationDate(DateOnly currentDate);
        Task<string> GetUsersByUsersEmail(List<string> users);
        Task<List<IdentityUserResponseDTO>> GetUserDetailsByUserEmailId(List<string> emailId);
        Task<List<WorkflowDTO>> GetWorkflowByModuleOutcomeAndUpdateDate(string module, string outcome, DateTime date);
    }
}
