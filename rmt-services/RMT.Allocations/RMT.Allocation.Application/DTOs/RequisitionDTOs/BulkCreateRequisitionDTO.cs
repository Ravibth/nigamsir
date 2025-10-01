
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.RequisitionDTOs
{
    public class BulkCreateRequisitionDTO
    {
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? RequisitionDescription { get; set; }
        public Boolean? IsContinuousAllocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Range(Int64.MinValue, Int64.MaxValue)]
        public Int64? TotalHours { get; set; }
        public string? RequisitionStatus { get; set; }
        //public string? Expertise { get; set; }
        //public string? SME { get; set; }
        //public string? SMEG { get; set; }
        public string? SkillId { get; set; }
        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }
        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? CompetencyWeightage { get; set; }
        public string? OfferingsWeightage { get; set; }
        public string? SolutionsWeightage { get; set; }
        public string? BusinessUnit { get; set; }
        public bool? IsAllResourcesSimilar { get; set; }
        
        [Range(Int64.MinValue, Int64.MaxValue)]
        public Int64? NumberOfResources { get; set; }
        public string? ExpertiseId { get; set; }//Recheck
        //public string? SMEId { get; set; }
        //public string? smegId { get; set; }
        public string? locationCode { get; set; }
        public string? perDay { get; set; }
        public string? industryID { get; set; }
        public string? subIndustryID { get; set; }
        public string? subIndustry { get; set; }
        public string? Industry { get; set; }
        public string? subIndustryWeight { get; set; }
        public string? industryWeight { get; set; }
        public string? smegWeightage { get; set; }
        public string? locationWeightage { get; set; }
        public string? sameClientExperienceWeightage { get; set; }
        public string? skillWeightage { get; set; }
        public string? BUId { get; set; }
        public string? DesignationId { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }
        public string? Description { get; set; }
        public string? ClientName { get; set; }
        public string? EmailId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public Boolean? isUploaded { get; set; }
        public bool? status { get; set; }
        public List<string>? comments { get; set; }
        public string? errorMsg { get; set; }
        public DateTime? projectStartDate { get; set; }
        public DateTime? projectEndDate { get; set; }
        public string? skills { get; set; }
        public List<SkillCodeNameDTO>? SkillList { get; set; }
        public string? locations { get; set; }
        public List<ParametersEntities> Parameters { get; set; }
        public string? selectedOption { get; set; }
    }

}
