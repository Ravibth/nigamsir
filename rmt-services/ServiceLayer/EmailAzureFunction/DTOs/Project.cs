using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace ServiceLayer.DTOs
{
    public class Project
    {
        public Project()
        {
            ProjectDemands = new List<ProjectDemand>();
            ProjectJobCodes = new List<ProjectJobCodes>();
            ProjectRoles = new List<ProjectRoles>();
            //ProjectSkills = new List<ProjectSkills>();
        }
        
        public Int64? Id { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? bu { get; set; }
        //public string? sector { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public string? ClientGroup { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? Sme { get; set; }//Recheck

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Description { get; set; }
        public string? ProjectAllocationStatus { get; set; }
        public string? Location { get; set; }
        public string? PipelineStage { get; set; }
        public string? ProjectType { get; set; }
        public string? PipelineStatus { get; set; }
        public string? ChargableType { get; set; }
        public string? LegalEntity { get; set; }
        public string? JobLocation { get; set; }
        public string? DeliveryLocation { get; set; }
        public string? GtRefferenceCountry { get; set; }
        public string? RevenueUnit { get; set; }//Recheck
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public string? BudgetStatus { get; set; }
        public int? ProjectFulFilledDemands { get; set; }
        public bool? IsPublishedToMarketPlace { get; set; }
        public string? JustificationToAllocate { get;set; }
        public DateTime? PublishedToMarketPlaceDate { get; set; }
        // ToDo : use this flag to disable the requistion /allocation button in projectlisting-page 
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public bool IsRollover { get; set; }
        public bool? ProjectActivationStatus { get; set; }
        public bool? ProjectClosureState { get; set; }//Open:-true , Closed:-false

        public int RolloverDays { get; set; }
        public bool? IsRequisitionCreationallowed { get; set; }
        //[Timestamp]
        public DateTime? CreatedAt { get; set; }
        //[Timestamp]
        public DateTime? ModifiedAt { get; set; }

        public DateTime? SuspendedModifyAt { get; set; }

        public bool? IsSuspended { get; set; } = false;

        public virtual ICollection<ProjectDemand> ProjectDemands { get; set; }

        public virtual ICollection<ProjectJobCodes> ProjectJobCodes { get; set; }

        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }

        //public virtual ICollection<ProjectRolesView> ProjectRolesView { get; set; }

        //public virtual ICollection<ProjectSkills> ProjectSkills { get; set; }

    }
}
