using RMT.Projects.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class ProjectDetailsRequestorDto
    {
        public Int64 Id { get; set; }
        //public string? ProjectCode { get; set; }//feb
        //public string? ProjectName { get; set; }//feb
        public string? JobCode { get; set; }
        public string? JobId { get; set; }
        public string? JobName { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? ClientName { get; set; }
        public string? ClientGroup { get; set; }
        public string? bu { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? Sme { get; set; }//Recheck
        public string? Smeg { get; set; }//Recheck

        public string? BUId { get; set; }
        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
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
        public string? JustificationToAllocate { get; set; }
        public DateTime? PublishedToMarketPlaceDate { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? ProjectClosureState { get; set; }//Open:-true , Closed:-false
        public bool? ProjectActivationStatus { get; set; } //Active , In - Active
        public DateTime? ModifiedAt { get; set; }
        public bool IsRollover { get; set; }
        public int RolloverDays { get; set; }
        public bool? isRequisitionCreationallowed { get; set; }
        public List<ProjectRoles> ProjectRoles { get; set; }
        public List<ProjectRolesView> ProjectRolesView { get; set; }
        //public List<ProjectJobCodes> ProjectJobCodes { get; set; }
        public List<ProjectDemand> ProjectDemands { get; set; }
        public List<ProjectSkills> ProjectSkills { get; set; }
        public List<ProjectCompetency> ProjectCompetencies { get; set; }
        public List<string> ResourceRequestorNames { get; set; }
        public string? ClientId { get; set; }
        public bool? IsConfidential { get; set; }
    }
}
