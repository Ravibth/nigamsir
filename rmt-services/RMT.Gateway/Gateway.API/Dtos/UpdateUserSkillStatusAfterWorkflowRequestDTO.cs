using System;

namespace Gateway.API.Dtos
{
    public static class UpdateUserSkillWorkflowActions
    {
        public const string APPROVED = "APPROVED";
        public const string REJECTED = "REJECTED";
    }
    public class UpdateUserSkillStatusAfterWorkflowRequestDTO
    {
        public string Id { get; set; }
        public string ActionPerformed { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
