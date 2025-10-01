using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gateway.API.Dtos
{
    public class ProjectCompetency
    {
        public Int64? Id { get; set; }

        public Int64 ProjectId { get; set; }

        public string Competency { get; set; }
        public string CompetencyId { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
    public class ProjectInfoDTO
    {
        public Int64? Id { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? ClientName { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }

        public string? Expertise { get; set; }//Recheck
        public string? Sme { get; set; }//Recheck
        public string? Smeg { get; set; }//Recheck
        public string? bu { get; set; }
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
        public string? RevenueUnit { get; set; }//Recheck
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public string? BudgetStatus { get; set; }
        public int? ProjectFulFilledDemands { get; set; }
        public bool? MarketClosed { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public bool IsRollover { get; set; }
        public int RolloverDays { get; set; }
        public List<ProjectCompetency>? ProjectCompetencies { get; set; }


        //[Timestamp]
        //public byte[]? CreatedAt { get; set; }

        //[Timestamp]
        //public byte[]? ModifiedAt { get; set; }

        //public virtual ICollection<ProjectDemand> ProjectDemands { get; set; }

        //public virtual ICollection<ProjectJobCodes> ProjectJobCodes { get; set; }

        //public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }
    }
}
