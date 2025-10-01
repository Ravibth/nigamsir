using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Responses
{
    public class RequisitionParameters
    {
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public string Category { get; set; }
        public Int64 RequisitionWeight { get; set; }
        public bool IsChecked { get; set; }
        //public virtual Requisition Requisition { get; set; }

    }

    public class GetAllRequisitionByProjectCodeResponse
    {
        public string PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string RequisitionDescription { get; set; }
        public Boolean IsContinuousAllocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int64 TotalHours { get; set; }
        public string RequisitionStatus { get; set; }
        public string Bu { get; set; }
        public string BUId { get; set; }//Recheck
        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }
        public string Designation { get; set; }
        public string? Grade { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string? Score { get; set; }
        public List<RequisitionSkill>? RequisitionSkill { get; set; }
        public Guid Id { get; set; }
        public List<RequisitionParameters> RequisitionParameters { get; set; }
        public Guid RequisitionDemand { get; set; }
        public string? ClientName { get; set; }
        public Int64 EffortsPerDay { get; set; }
        public bool IsPerDayHourAllocation { get; set; }
        public string BusinessUnit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64 RequisitionTypeId { get; set; }
        public bool? hasPermissionToEdit { get; set; }
        public bool? hasPermissionToDelete { get; set; }
        public string? Suggestion { get; set; }
    }
    public class RequisitionSkill
    {
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public string? SkillName { get; set; }
        public string? SkillCode { get; set; }
        public string? Type { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public bool IsActive { get; set; }

    }
}
