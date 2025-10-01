
using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.RequisitionDTOs
{
    public class CreateRequisitionDTO
    {
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? RequisitionDescription { get; set; }
        public string? Description { get; set; }
        public Boolean? IsContinuousAllocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Int64? TotalHours { get; set; }
        public string? RequisitionStatus { get; set; }
        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }
        public string BU { get; set; }
        public string? Industry { get; set; }
        public string? SubIndustry { get; set; }
        public bool? IsAllResourcesSimilar { get; set; }
        public Int64? NumberOfResources { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }
        public List<ResourceEntities>? ResourceEntities { get; set; }
        public virtual ICollection<RequisitionLocationDTO>? requisitionLocations { get; set; }
        public virtual ICollection<RequisitionDurationDTO>? requisitionDuration { get; set; }
        public virtual ICollection<RequisitionParametersDTO>? requisitionParameters { get; set; }
        public virtual ICollection<RequisitionSkillDTO>? requisitionSkills { get; set; }
        public string? ClientName { get; set; }
    }
}