using ServiceLayer.Services.AllocationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
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
