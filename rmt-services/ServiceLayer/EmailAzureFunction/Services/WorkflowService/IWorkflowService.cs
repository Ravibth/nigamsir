using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService.DTOs;

namespace ServiceLayer.Services.WorkflowService
{
    public interface IWorkflowService
    {
        Task initWorkflow(NotificationPayloadDTO payload);

        Task TerminateWorkflow(List<TerminateWorkflowDTO> workflowRADGUID, string token);

        Task CreateWorkflowRollForward(NotificationPayloadDTO payload);
        Task RefreshAssignedTask(RefreshAssignedWorkflowTaskDTO request, string token);
        Task<List<WorkflowTaskDTO>> GetWorkflowSuperCoachTask(string employee_email, string supercoach_email, string token);
    }
}
