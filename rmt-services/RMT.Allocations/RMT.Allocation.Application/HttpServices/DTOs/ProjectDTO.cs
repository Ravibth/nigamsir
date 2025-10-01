using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class ProjectRoles
    {
        public Int64? Id { get; set; }
        public Int64? ProjectId { get; set; }
        public string? User { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public string? ApplicationRole { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? DelegateUserName { get; set; }
        public string? DelegateEmail { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public class ProjectRolesView
    {
        public Int64? Id { get; set; }

        public Int64? ProjectId { get; set; }

        public string? User { get; set; }

        public string? UserName { get; set; }

        public string Role { get; set; }

        public string? ApplicationRole { get; set; }
        public string? Description { get; set; }

        public string? ParentEmail { get; set; }

        public string? ParentName { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }

    //public class ProjectJobCodes
    //{
    //    public Int64? Id { get; set; }
    //    public Int64? ProjectId { get; set; }
    //    //public string? ProjectCode { get; set; }
    //    public string? PipelineCode { get; set; }
    //    public string? JobCode { get; set; }
    //    public string? JobName { get; set; }
    //    public bool? IsActive { get; set; }
    //    public string? CreatedBy { get; set; }
    //    public string? ModifiedBy { get; set; }
    //    public DateTime? CreatedAt { get; set; }
    //    public DateTime? ModifiedAt { get; set; }
    //}
    public class ProjectDemand
    {
        public Int64? Id { get; set; }
        public Int64? ProjectId { get; set; }
        public string? Designation { get; set; }
        public int? NoOfResources { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
    public class ProjectDTO
    {
        public Int64? Id { get; set; }
        //public string? ProjectCode { get; set; }
        //public string? ProjectName { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobId { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public string? bu { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }
        public string? Offerings { get; set; } = "OfferingTest1";
        public string? Solutions { get; set; } = "SolutionsTest1";

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }

        public string? Sme { get; set; }//Recheck
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
        public string? RevenueUnit { get; set; }
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public string? BudgetStatus { get; set; }
        public int? ProjectFulFilledDemands { get; set; }
        public bool? MarketClosed { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsRollover { get; set; }
        public int? RolloverDays { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public List<ProjectDemand>? ProjectDemands { get; set; }
        //public List<ProjectJobCodes> ProjectJobCodes { get; set; }
        public List<ProjectRoles>? ProjectRoles { get; set; }

        public List<ProjectRolesView>? ProjectRolesView { get; set; }

    }

    public class AddProjectUserRole
    {
        public Int64 ProjectId { get; set; }
        public string User { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        //[Timestamp]
        public DateTime? CreatedAt { get; set; }

        //[Timestamp]
        public DateTime? ModifiedAt { get; set; }
    }

    public class PipelineCodeAndRolesDto
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public List<string>? roles { get; set; }
    }

    public class RoleEmailsByPipelineCodeResponse
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public Dictionary<string, List<string>> RolesEmails { get; set; }
    }

}
