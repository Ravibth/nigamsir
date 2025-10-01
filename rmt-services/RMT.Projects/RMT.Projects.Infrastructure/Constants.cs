using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Projects.Domain.Constant;

namespace RMT.Projects.Infrastructure
{
    public class Constants
    {
        public const string NULL_STR = "null";

        public static class ProjectRequisitionAllocationStatus
        {
            public const string PENDING = "Pending";
            public const string Completed = "Completed";
            public const string ToBeStarted = "To be started";
        }
        public static class ProjectChargeType
        {
            public const string CHARGEABLE = "Chargeable";
            public const string NON_CHARGABLE = "NonChargeable";
        }
        public static class ViewTypes
        {
            public const string EMPLOYEE_VIEW = "EMPLOYEE_VIEW";
            public const string DELEGATE_VIEW = "DELEGATE_VIEW";
            public const string REQUESTOR_VIEW = "REQUESTOR_VIEW";
        }
        public static class ProjectActivationStatus
        {
            public const string ACTIVE = "Active";
            public const string IN_ACTIVE = "In Active";
        }
        public static class ProjectClosureStatus
        {
            public const string CLOSED = "Closed";
            public const string OPEN = "Open";
        }
        public static string LowerString(string? str)
        {
            try
            {
                return string.IsNullOrEmpty(str) ? string.Empty : str.Trim().ToLower();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        //Project role and Application Role mapping data for temporary fix
        public static Dictionary<string, string> MappingRoleApplication = new Dictionary<string, string>()
        {
            { UserRoles.EO, UserRoles.ResourceRequestor },
            { UserRoles.EngagementLeader, UserRoles.ResourceRequestor },
            { UserRoles.ProposedEL, UserRoles.ResourceRequestor },
            { UserRoles.CSP, UserRoles.Reviewer },
            { UserRoles.ProposedCSP, UserRoles.Reviewer },
            { UserRoles.SMEGLeader, UserRoles.Reviewer },
            { UserRoles.JobManager, UserRoles.ResourceRequestor },
            { UserRoles.Delegate, UserRoles.Delegate },
            { UserRoles.AdditionalEl, UserRoles.AdditionalEl },
            { UserRoles.AdditionalDelegate, UserRoles.AdditionalDelegate },
            { UserRoles.Employee, UserRoles.Employee },
            { UserRoles.SuperCoach, UserRoles.SuperCoach }
        };

        public static List<string> EmployeeChargableRR = new List<string>() { UserRoles.EngagementLeader };
        public static List<string> EmployeeNonChargableRR = new List<string>() { UserRoles.JobManager };
        public static List<string> EmployeePipelineRR = new List<string>() { UserRoles.EO, UserRoles.ProposedEL };

    }
}
