using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class JobAllocationMappingDTO
    {
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpMID { get; set; }

        public DateTime? AllocationConfirmationDate { get; set; }
        //public Dictionary<string, List<string>> ProjectRoles { get; set; }
        //public List<JobAllocationRoleDTO> ProjectRoles { get; set; }
    }

    //public class JobAllocationRoleDTO
    //{
    //    public string RoleName { get; set; }
    //    public string EmpEmailId { get; set; }

    //}
}
