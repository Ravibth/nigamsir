using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain
{
    public class ConstantsDomain
    {
        public const string RUPEES = "Rupees";
        public enum EAllocationStatus
        {
            //ApprovalPending,
            //Approved,
            //Not Used anymore as the roll forward requirement chnaged 
            RolloverAllocationPending,
            //Etc
        }

        public enum EAllocationRecordType
        {
            //Allocation,
            //Leave,
            //Holiday,
            //Not Used anymore as the roll forward requirement chnaged 
            RolloverAllocation,
            //Etc
        }

        public enum RequisitionStatus
        {
            AllocationPending,
            //AllocationPendingApproval,
            //PendingResourceConfirmation,
            //Allocated,
            //Pending
        }


        //public enum AllocationStatus
        //{
        //    Open = 0,
        //    InProgress = 1,
        //    Completed = 3
        //}

        //public const string Employee = "Employee";
        //public const string ResourceRequestor = "Resource Requestor";
        //public const string Delegate = "Delegate";
        //public const string Admin = "Admin";
        //public const string Reviewer = "Reviewer";
        //public const string Leaders = "Leaders";
        //public const string SystemAdmin = "System Admin";
        //public const int WorkingHours = 8;//duplicate workinghourPerDay

        //public enum UserRolesMaster
        //{
        //    //Employee,
        //    //ResourceRequestor,
        //    //Delegate,
        //    //Admin,
        //    //Reviewer,
        //    //Leaders,
        //    //SystemAdmin,
        //}

        //public static class AllocationStatuses
        //{
        //    //public const string CompletedStatus = "Allocation Complete";
        //    //public const string DraftStatus = "Draft";
        //    //public const string InProgressStatus = "Pending";
        //}

        public static class PipelineStatuses
        {
            public const string Suspended = "Suspended";
        }
        public static class UserRolesConstant
        {
            public const string Employee = "Employee";
            public const string ResourceRequestor = "ResourceRequestor";
            public const string Delegate = "Delegate";
            public const string Admin = "Admin";
            public const string CEOCOO = "CEOCOO";
            public const string Reviewer = "Reviewer";
            public const string Leaders = "Leaders";
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
            public const string EO = "EO";//tobe checked
            public const string CSP = "CSP";//tobe checked
        }

        public static class Requisition_Parameter_type
        {
            public const string Location = "Location";
            public const string Industry = "Industry";
            public const string SubIndustry = "SubIndustry";
            public const string Null = "null";


        }

        public static string UserMidSplitter = "__";

        public static class ServiceBusActions
        {
            public const string REFRESH_PROJECT_COMPETENCY = "REFRESH_PROJECT_COMPETENCY";
            public const string REFRESH_PROJECT_BUDGET_STATUS = "REFRESH_PROJECT_BUDGET_STATUS";
        }

    }

}
