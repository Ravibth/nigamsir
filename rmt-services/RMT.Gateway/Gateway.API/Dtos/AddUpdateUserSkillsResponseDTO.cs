using System;

namespace Gateway.API.Dtos
{
    public static class UserSkillsStatus
    {
        public const string PENDING = "Pending";
        public const string PENDING_APPROVAL = "Pending Approval";
        public const string APPROVED = "Approved";
        public const string REJECTED = "Rejected";
    }

    public class AddUpdateUserSkillsRequestDTO
    {
        public string? comments { get; set; }
        public string proficiency { get; set; }
        public string skillCode { get; set; }
        public string skillName { get; set; }
    }

    public class AddUpdateUserSkillsResponseDTO
    {
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string Proficiency { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string EmpId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string previousUpdatedLevel { get; set; }
    }
}
