using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class resourceallocationrequistionview
    {

        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }

        //public string ProjectCode { get; set; }

        public string? PipelineName { get; set; }

        public string? JobName { get; set; }

        public string EmpEmail { get; set; }

        public string EmpName { get; set; }
        public Int64 RequisitionId { get; set; }

        public string RecordType { get; set; }


        public Guid guid { get; set; }


        //public Boolean IsContinuousAllocation { get; set; }

        public int TotalEffort { get; set; }
        public string AllocationStatus { get; set; }

        public bool IsActive { get; set; }

        public DateTime? AllocationStartDate { get; set; }
        public DateTime? AllocationEndDate { get; set; }


        public DateTime? SuspendedAt { get; set; }

        public string Designation { get; set; }


    }
}
