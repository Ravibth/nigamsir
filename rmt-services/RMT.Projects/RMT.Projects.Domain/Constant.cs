using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain
{
    public class Constant
    {
        public static class ProjectType
        {
            public const string Recurring = "Recurring";
            public const string NonRecurring = "Non-recurring";

        }
        public static class UserRoles
        {
            public const string Employee = "Employee";
            public const string ResourceRequestor = "ResourceRequestor";
            public const string Delegate = "Delegate";
            public const string Admin = "Admin";
            public const string CEOCOO = "CEOCOO";

            public const string Reviewer = "Reviewer";
            public const string Leaders = "Leaders";
            public const string SuperCoach = "SuperCoach";
            public const string SystemAdmin = "SystemAdmin";
            public const string AdditionalEl = "AdditionalEl";
            public const string AdditionalDelegate = "AdditionalDelegate";
            public const string EngagementLeader = "EngagementLeader";
            public const string CSL = "CSL";//tobe checked & discussed
            public const string AssignmentIncharge = "AssignmentIncharge";//tobe checked
            public const string LeadGenerator = "LeadGenerator";//tobe checked
            public const string JobManager = "JobManager";//tobe checked
            public const string SMEGLeader = "SMEGLeader";//tobe checked
            public const string ProposedCSP = "ProposedCSP";//tobe checked
            public const string ProposedEL = "ProposedEL";//tobe checked
            public const string FindingPartner = "FindingPartner";//tobe checked
            public const string FindingPartner1 = "FindingPartner1";//tobe checked
            public const string FindingPartner2 = "FindingPartner2";//tobe checked
            public const string EO = "EO";//tobe checked
            public const string CSP = "CSP";//tobe checked
        }


        public static class UserAdminRolesEnum
        {
            public const string ADMIN = UserRoles.Admin;
            public const string CEOCOO = UserRoles.CEOCOO;
            public const string SYSTEM_ADMIN = UserRoles.SystemAdmin;
        }

        public static List<string> UserAdminRolesArray = new List<string>() { UserAdminRolesEnum.ADMIN, UserAdminRolesEnum.SYSTEM_ADMIN, UserAdminRolesEnum.CEOCOO };

        public enum PROJECT_ALLOCATION_STATUS
        {
            PENDING_ALLOCATION,
            ALLOCATION_IN_PROGRESS,
            ALLOCATION_COMPLETED
        }

        public enum PipelineStatusType
        {
            IN_PROGRESS,
            PENDING_DS_TEAM,
            PENDING,
            APPROVED,
            WON

        }

    }
}
