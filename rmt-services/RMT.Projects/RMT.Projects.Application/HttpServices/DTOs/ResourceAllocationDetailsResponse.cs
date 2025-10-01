using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{
    public class Requisition
    {
        public Guid Id { get; set; }
        public Guid RequisitionDemand { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Int64 EffortsPerDay { get; set; }
        public Int64 TotalHours { get; set; }
        public string RequisitionStatus { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public bool IsPerDayHourAllocation { get; set; }
        public string BusinessUnit { get; set; }
        //public string Expertise { get; set; }
        //public string SMEG { get; set; }
        public string CompetencyId { get; set; }
        public string Competency { get; set; }
        public string Offerings { get; set; } = "1";
        public string Solutions { get; set; } = "1";

        public string OfferingsId { get; set; } = "1";
        public string SolutionsId { get; set; } = "1";

        public string BUId { get; set; } = "1";

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64 RequisitionTypeId { get; set; }
    }
    public class ResourceAllocationDetailsResponse
    {
        public Guid Id { get; set; }
        public Guid guid { get; set; }
        public Guid? ParentPublishedResAllocDetailsId { get; set; }
        public Guid RequisitionId { get; set; }
        public string EmpEmail { get; set; }
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string EmpName { get; set; }
        public string Description { get; set; }
        public Int64 TotalEffort { get; set; }
        public string AllocationStatus { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime? ConfirmedAllocationDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64? AllocationVersion { get; set; }
        public Requisition? Requisition { get; set; }
        public string Type { get; set; }
        public bool? IsUpdated { get; set; }
    }
}
