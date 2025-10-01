using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public static class UpdateUserSkillWorkflowActions
    {
        public const string APPROVED = "APPROVED";
        public const string REJECTED = "REJECTED";
    }
    public class UpdateUserSkillStatusAfterWorkflowRequestDTO
    {
        public Guid Id { get; set; }
        public string ActionPerformed { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
