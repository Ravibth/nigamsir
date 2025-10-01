using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Allocation.Domain.DTO;

namespace RMT.Allocation.Application.DTOs
{
    public class ProjectFullDetailsResponse
    {
        public ProjectFullDetailsResponse()
        {
            ProjectDemands = new List<ProjectDemand>();
            ProjectRoles = new List<ProjectRoles>();
        }

        public Int64? Id { get; set; }
        //public string? ProjectCode { get; set; }//feb
        //public string? ProjectName { get; set; }//feb

        public string? JobName { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? ClientName { get; set; }
        public string? bu { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? Sme { get; set; }//Recheck
        public string? Smeg { get; set; }//Recheck

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
        public string? RevenueUnit { get; set; }//Recheck
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public string? BudgetStatus { get; set; }
        public int? ProjectFulFilledDemands { get; set; }
        public bool? IsPublishedToMarketPlace { get; set; }
        public bool? MarketClosed { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

        public bool IsRollover { get; set; }
        public int RolloverDays { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<ProjectDemand> ProjectDemands { get; set; }

        //public virtual ICollection<ProjectJobCodes> ProjectJobCodes { get; set; }

        public virtual ICollection<ProjectRoles> ProjectRoles { get; set; }
        public virtual ICollection<ProjectRolesView> ProjectRolesView { get; set; }

        public ProjectAllocatedHoursRatioDto? ProjectAllocatedHoursRatio { get; set; }
        public ProjectRequisitionAllocation? ProjectRequisitionAllocations { get; set; }

        public string? ClientId { get; set; }
    }
}
