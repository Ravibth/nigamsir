using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class SuperCoachInformation
    {
        public string SuperCoachEmailId { get; set; }
        public string SuperCoachName { get; set; }
        public string? PrevAllocationSuperCoachDelegateName { get; set; }
        public string? NewAllocationSuperCoachDelegateName { get; set; }
        public string? PrevAllocationSuperCoachDelegateEmail { get; set; }
        public string? NewAllocationSuperCoachDelegateEmail { get; set; }
    }
    public class AddSuperCoachProjectRoleRequest
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public List<SuperCoachInformation> SuperCoachInformation { get; set; }
    }
}
