using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gateway.API.Middlewares.MiddlewareHelpers.ProjectMiddlewareHelper.DTOs
{
    //ProjectResponse classname in project service 
    public class UpdateProjectRequestDTO
    {
        public Int64 Id { get; set; }

        public string? Description { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public ICollection<ProjectDemand>? ProjectDemands { get; set; }

        public ICollection<ProjectRoles>? ProjectRoles { get; set; }

        public ICollection<ProjectSkills>? ProjectSkills { get; set; }

        public DateTime? ProjectEndDate { get; set; }

    }

    public class ProjectDemand
    {
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        public string Designation { get; set; }

        public int NoOfResources { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

    }

    public class ProjectRoles
    {
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        public string User { get; set; }
        public string UserName { get; set; }

        public string Role { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }

    public class ProjectSkills
    {
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        public string SkillName { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
