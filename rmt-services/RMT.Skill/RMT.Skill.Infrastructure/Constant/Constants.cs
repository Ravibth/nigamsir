namespace RMT.Skill.API.Constant
{
    public static class Constants
    {
        public const string idtoken_header_email = "preferred_username";
        public const string accesstoken_header_email = "unique_name";

        public const string SKILL_APPROVAL_STATUS = "Approved";
        public static class WorkflowTaskStatus
        {
            public const string APPROVED = "APPROVED";
            public const string PENDING = "PENDING";
            public const string REJECTED = "REJECTED";
        }
    }
}
