using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
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
