using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Infrastructure.Constants.Constants;

namespace RMT.Notification.Infrastructure.Constants
{
    public static class NotificationTemplatePayloads
    {
        public const string RESOURCE_REQUESTOR_NAME = UserRoles.ResourceRequestor;
        public const string DELEGATE_NAME = UserRoles.Delegate;
        public const string ADDITIONAL_EL_NAME = UserRoles.AdditionalEl;
        public const string ADDITIONAL_DELEGATE_NAME = UserRoles.AdditionalDelegate;
        public const string REVIEWER_NAME = UserRoles.Reviewer;
        public const string SUPRERCOACH_NAME = UserRoles.SuperCoach;

        //todo:- check for user requesting condition & ckeck updated by spelling all-over
        public const string USER_REQUESTING = "UpdatedBy";
        public const string PROJECT_NAME = "ProjectName";
        public const string DATE = "Date";
        public const string CLICK_LINK = "click_link";
        public const string PROJECT_STATUS = "ProjectStatus";
        public const string EMPLOYEE_NAME = "employee_name";
        public const string LIKE_COUNT = "like_count";
        public const string START_DATE = "StartDate";
        public const string END_DATE = "EndDate";
        public const string CONFLICTED_EMPLOYEE_NAME = "conflicted_employee_name";
        public const string SECONDARY_EMPLOYEE_NAME = "secondary_employee_name";
        public const string USER_ROLES = "UserRoles";
        public const string EMPLOYEE_USERNAME = "employee_username";
        public const string PENDING_DAYS = "pending_days";

        public const string RMS_TEAM = "RMS Team";
        public const string GeneralizedAll = "Team";

        public const string REJECTION_REASON = "RejectionReason";

        public const string TABLE_DESIGN_FOR_ALLOCATION = "TABLE_DESIGN_FOR_ALLOCATION";
        public const string TABLE_DESIGN_FOR_SKILL = "TABLE_DESIGN_FOR_SKILL";


        //For Links
        public const string ProjectCode = "ProjectCode";
        public const string RequisitionId = "RequisitionId";

    }
    public static class MicroServicesNames
    {
        public const string SKILLS = "SKILLS";
        public const string ALLOCATION = "ALLOCATION";
        public const string CREATEWORKFLOW = "CREATEWORKFLOW";
        public const string UPDATEWORKFLOW = "UPDATEWORKFLOW";

    }
}
