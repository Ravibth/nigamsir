using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{


    public class GetActiveAllocationByPipeLineCodeDTO
    {
        public Int64? Id { get; set; }
        public Guid guid { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string ProjectCode { get; set; }
        public string? JobName { get; set; }
        public string PipelineName { get; set; }
        public string EmpEmail { get; set; }
        public string? EmpName { get; set; }
        public Int64 RequisitionId { get; set; }
        public string? BU { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? SMEG { get; set; }//Recheck

        public string? BUId { get; set; }
        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public List<string>? Skill { get; set; }
        public string RecordType { get; set; }
        public Boolean IsContinuousAllocation { get; set; }
        public string Description { get; set; }
        public int TotalEffort { get; set; }
        public DateTime? AllocationStartDate { get; set; }
        public DateTime? AllocationEndDate { get; set; }
        public string AllocationStatus { get; set; }
        public string Designation { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? SuspendedAt { get; set; }
        public virtual List<ResourceAllocation>? ResourceAllocation { get; set; }


    }

    public class ResourceAllocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? ProjectCode { get; set; }
        public string? JobName { get; set; }
        public string? PipelineName { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpName { get; set; }
        public string? ClientName { get; set; }
        public DateTime? ConfirmedAllocationStartDate { get; set; }
        public DateTime? ConfirmedAllocationEndDate { get; set; }
        public int ConfirmedPerDayHours { get; set; }
        public Boolean isPerDayHourAllocation { get; set; }
        public Int64? ResAllocDetailsId { get; set; }
        public Int64 RequisitionId { get; set; }
        public string? RecordType { get; set; }
        public int TotalWorkingDays { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? SuspendedAt { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public bool isPublish { get; set; }
        public double? RatePerDay { get; set; }
    }


}
