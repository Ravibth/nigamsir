using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs.Responses
{
    public class GetUserSkillsByEmailResponse
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
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
        public string Basic { get; set; }
        public string Intermediate { get; set; }
        public string Advanced { get; set; }
        public string Expert { get; set; }
        public string? Approver { get; set; }
        public string? ApproverEmail{ get; set; }
        public DateTime? ApprovedOn { get; set; }
    }
}
