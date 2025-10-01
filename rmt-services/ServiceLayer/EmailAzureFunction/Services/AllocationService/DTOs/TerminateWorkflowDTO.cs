using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.AllocationService.DTOs
{
    public class TerminateWorkflowDTO
    {
        public string ItemId { get; set; }

        public string WorkflowStatus { get; set; }

    }
}
