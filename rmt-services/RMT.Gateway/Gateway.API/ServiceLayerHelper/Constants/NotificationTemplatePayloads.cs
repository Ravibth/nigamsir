using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gateway.API.ServiceLayerHelper.Constants.Constants;

namespace Gateway.API.ServiceLayerHelper.Constants
{
    public static class NotificationTemplatePayloads
    {
        public const string RESOURCE_REQUESTOR_NAME = UserRoles.ResourceRequestor;
        public const string DELEGATE_NAME = UserRoles.Delegate;
        public const string ADDITIONAL_EL_NAME = UserRoles.AdditionalEl;
        public const string ADDITIONAL_DELEGATE_NAME = UserRoles.AdditionalDelegate;
        public const string REVIEWER_NAME = UserRoles.Reviewer;

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


        //For Links
        public const string ProjectCode = "ProjectCode";
        public const string RequisitionId = "RequisitionId";

    }
    public static class NotificationTemplatePayloadsConstants
    {
        public static readonly List<string> NotificationTemplatePayloadsArray = new List<string>
        {
            NotificationTemplatePayloads.RESOURCE_REQUESTOR_NAME,
            NotificationTemplatePayloads.DELEGATE_NAME,
            NotificationTemplatePayloads.USER_REQUESTING,
            NotificationTemplatePayloads.PROJECT_NAME,
            NotificationTemplatePayloads.DATE,
            NotificationTemplatePayloads.CLICK_LINK,
            NotificationTemplatePayloads.RMS_TEAM,
            NotificationTemplatePayloads.ADDITIONAL_EL_NAME,
            NotificationTemplatePayloads.ADDITIONAL_DELEGATE_NAME,
            NotificationTemplatePayloads.PROJECT_STATUS,
            NotificationTemplatePayloads.EMPLOYEE_NAME,
            NotificationTemplatePayloads.LIKE_COUNT,
            NotificationTemplatePayloads.START_DATE,
            NotificationTemplatePayloads.END_DATE,
            NotificationTemplatePayloads.CONFLICTED_EMPLOYEE_NAME,
            NotificationTemplatePayloads.SECONDARY_EMPLOYEE_NAME,
            NotificationTemplatePayloads.REVIEWER_NAME,
            NotificationTemplatePayloads.USER_ROLES,
            NotificationTemplatePayloads.EMPLOYEE_USERNAME,
            NotificationTemplatePayloads.PENDING_DAYS
        };
    }
}
