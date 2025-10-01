using Gateway.API.ServiceLayerHelper.DTOs;
using System.Collections.Generic;

namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs
{
    public class AllocationRolloverResponseDTO
    {
        public ProjectRolloverRequestDTO projectRolloverRequest { get; set; }
        public List<NotificationPayloadDTO> allWorkflowToStart { get; set; }
        public List<TerminateWorkflowDTO> allWorkflowToTerminate { get; set; }
        public List<string> NotificationActions { get; set; }
        public List<string> EmployeeEmailInDraft { get; set; }
        public List<string> EmployeeEmailTerminated { get; set; }
    }
}
