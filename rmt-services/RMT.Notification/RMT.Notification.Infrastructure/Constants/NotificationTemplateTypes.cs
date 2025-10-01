using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Infrastructure.Constants
{
    public static class NotificationTemplateSubject
    {
        // 130,131 - UC028 - Employee shall be  notified for submission of their skill update request sent to Supercoach.
        public const string EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH = "Skill Assessment Request submitted for Review";
        // 132,133 - UC028 - ACCEPT Once skill assessment request is actioned by Supercoach, employee will get a notification for skill assessment request status along with reviewer comments (for mail notification)
        public const string SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST = "Skill Assessment Review Updates";
        // 134,135 - UC028 - REJECT- Once skill assessment request is actioned by Supercoach, employee will get a notification for skill assessment request status along with reviewer comments (for mail notification)
        public const string SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST = "Skill Assessment Review Updates";
        // 136,137 - UC028 - Consolidated Reminder
        public const string CONSOLIDATED_REMINDER = "RMS Tasks pending for your action";

    }
    public static class NotificationTemplateTypes
    {
        // 1,2 - UC002 - New Job/ Pipeline Notification (Added from WCGT) to Resource Requestor
        public const string NEW_JOB_ADDED_FROM_WCGT = "NEW_JOB_ADDED_FROM_WCGT";

        // 3,4 - UC007 - Assignment of Delegate by Requestor
        public const string ASSIGNMENT_OF_DELEGATE_BY_REQUESTOR = "ASSIGNMENT_OF_DELEGATE_BY_REQUESTOR";

        // 5,6 - UC040 - Assignment of Additional EL by Requestor
        public const string ASSIGNMENT_OF_ADDITIONAL_EL_BY_REQUESTOR = "ASSIGNMENT_OF_ADDITIONAL_EL_BY_REQUESTOR";

        // 7,8 - UC041 - Assignment of Additional Delegate by Additional EL
        public const string ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL = "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL";

        // 9,10 - UC002 - X days before Project start date if Project status is not WON
        public const string X_DAYS_BEFORE_PROJECT_START_DATE = "X_DAYS_BEFORE_PROJECT_START_DATE";

        // 11-12 - UC002 - Job/ Pipeline Update Notification(Update in project details from WCGT)
        public const string JOB_UPDATED_FROM_WCGT = "JOB_UPDATED_FROM_WCGT";

        // 13,14 - UC039 - Project End Date Update (done in RMS)
        public const string PROJECT_END_DATE_UPDATE = "PROJECT_END_DATE_UPDATE";

        // 15,16 - UC039 - Project End Date Update - End Date rolled back, allocations updated accordingly
        public const string PROJECT_END_DATE_UPDATE_ROLLED_BACK = "PROJECT_END_DATE_UPDATE_ROLLED_BACK";

        // 17,18 - UC038 - Assignment of Additional EL by Delegate
        public const string ASSIGNMENT_OF_ADDITIONAL_EL_BY_DELEGATE = "ASSIGNMENT_OF_ADDITIONAL_EL_BY_DELEGATE";

        // 19,20 - UC004 - Notification to Employee for Project Listing in Marketplace (Fixed Frequency)
        public const string NOTIFICATION_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE = "NOTIFICATION_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE";

        // 21,22 - UC004 - Project being no longer available in the Marketplace 
        public const string PROJECT_NO_LONGER_AVAILABLE_IN_MARKETPLACE = "PROJECT_NO_LONGER_AVAILABLE_IN_MARKETPLACE";

        // 23,24 - UC043 - Notification for interests in marketplace against their project  
        public const string NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT = "NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT";

        // 25,26 - UC043 - MP Interests Summary Notification  post removal of project from MP
        public const string MP_INTERESTS_SUMMARY_NOTIFICATION_POST_REMOVAL_OF_PROJECT_FROM_MP = "MP_INTERESTS_SUMMARY_NOTIFICATION_POST_REMOVAL_OF_PROJECT_FROM_MP";

        // 27,28 - UC020 - Project roll forward notification
        public const string PROJECT_ROLL_FORWARD_NOTIFICATION = "PROJECT_ROLL_FORWARD_NOTIFICATION";

        // 29,30 - UC020 - Roll Forward of allocations done by the user
        public const string ROLL_FORWARD_OF_ALLOCATIONS_DONE_BY_THE_USER = "ROLL_FORWARD_OF_ALLOCATIONS_DONE_BY_THE_USER";

        // 31,32 - UC024 - Project Suspension Notification
        public const string PROJECT_SUSPENSION_NOTIFICATION = "PROJECT_SUSPENSION_NOTIFICATION";

        // 33,34 - UC024 - Employee release due to suspension of project
        public const string EMPLOYEE_RELEASE_DUE_TO_SUSPENSION_OF_PROJECT = "EMPLOYEE_RELEASE_DUE_TO_SUSPENSION_OF_PROJECT";

        // 35,36 - UC028 - Resource Requestor to be notified for any allocation conflicts within last 30 working days of employee in the organization.
        public const string RESOURCE_REQUESTOR_TO_BE_NOTIFIED_FOR_ANY_ALLOCATION_CONFLICTS_WITHIN_LAST_30_WORKING_DAYS_OF_EMPLOYEE_IN_THE_ORGANIZATION = "RESOURCE_REQUESTOR_TO_BE_NOTIFIED_FOR_ANY_ALLOCATION_CONFLICTS_WITHIN_LAST_30_WORKING_DAYS_OF_EMPLOYEE_IN_THE_ORGANIZATION";

        // 37,38 - UC028 - Resource Requestor to be notified  if Employee on Leave/ Absconding during Project Allocation
        public const string RESOURCE_REQUESTOR_TO_BE_NOTIFIED__IF_EMPLOYEE_ON_LEAVE_OR_ABSCONDING_DURING_PROJECT_ALLOCATION = "RESOURCE_REQUESTOR_TO_BE_NOTIFIED__IF_EMPLOYEE_ON_LEAVE_OR_ABSCONDING_DURING_PROJECT_ALLOCATION";

        // 39,40 - UC005 - Requisition created
        public const string REQUISITION_CREATED = "REQUISITION_CREATED";

        // 41,42 - UC010 - Notification for allocation of resources to a project (Reviewer if configuration is enabled) for reviewer 
        public const string NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT = "NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT";

        // 43,44 - UC010 - Notification for allocation of a resources to a project (Notification directly to the employee for confirmation) for employee
        public const string NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT = "NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT";

        // 45,46 - UC012 - Notification for allocation update of resources to a project (Reviewer if configuration is enabled) for reviewer 
        public const string NOTIFICATION_TO_REVIEWER_UPDATE_ALLOCATION_OF_RESOURCE_TO_PROJECT = "NOTIFICATION_TO_REVIEWER_UPDATE_ALLOCATION_OF_RESOURCE_TO_PROJECT";

        // 47,48 - UC029 - Notification to employee for updating time-sheet once RMS sends daily allocation update to RMS 
        public const string NOTIFICATION_TO_EMPLOYEE_FOR_UPDATING_TIME_SHEET_ONCE_RMS_SENDS_ALLOCATION_UPDATE_TO_RMS = "NOTIFICATION_TO_EMPLOYEE_FOR_UPDATING_TIME_SHEET_ONCE_RMS_SENDS_ALLOCATION_UPDATE_TO_RMS";

        // 49,50 - UC021 - User being onboarded
        public const string USER_BEING_ONBOARDED = "USER_BEING_ONBOARDED";

        // 51,52 - UC018 - User roles & permissions update saved
        public const string USER_ROLES_PERMISSIONS_UPDATE_SAVED = "USER_ROLES_PERMISSIONS_UPDATE_SAVED";

        // 53,54 - UC017 - Reminder to Reviewer to take action pending request (for no action - auto-approval)
        public const string REMINDER_TO_REVIEWER_TO_TAKE_ACTION_PENDING_REQUEST_FOR_NO_ACTION_AUTO_APPROVAL = "REMINDER_TO_REVIEWER_TO_TAKE_ACTION_PENDING_REQUEST_FOR_NO_ACTION_AUTO_APPROVAL";

        // 55,56 - UC017 - Resource Allocation Review Notification - Approved by Reviewer
        public const string RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER";

        // 57,58 - UC017 - Resource Allocation Review Notification - Approved by Reviewer- Notification to Requestor
        public const string RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR";

        // 59,60 - UC017 - Resource Allocation Review Notification - Auto-approved - Notification to Reviewer
        public const string RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REVIEWER = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REVIEWER";

        // 61,62 - UC017 - Resource Allocation Review Notification - Auto-approved - Notification to Requestor
        public const string RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REQUESTOR = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_AUTO_APPROVED_NOTIFICATION_TO_REQUESTOR";

        // 63,64 - UC017 - Resource Allocation Review Notification - Reject -  Notification to Reviewer
        public const string RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECT_NOTIFICATION_TO_REVIEWER = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECT_NOTIFICATION_TO_REVIEWER";

        // 65,66 - UC017 - Resource Allocation Review Notification - Rejected - Notification to Requestor
        public const string RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECTED_NOTIFICATION_TO_REQUESTOR = "RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_REJECTED_NOTIFICATION_TO_REQUESTOR";

        // 67,68 - UC025 - Project Allocation Review (Resource) - Approved by Employee
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE";

        // 69,70 - UC025 - Project Allocation Review (Resource) - Notification to Resource Requestor once Approved by Employee
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE";

        // 71,72 - UC025 - Project Allocation Review (Resource) - Reminder to Employee
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_REMINDER_TO_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_REMINDER_TO_EMPLOYEE";

        // 73,74 - UC025 - Project Allocation Review (Resource)- Auto Approve for employee
        public const string PROJECT_ALLOCATION_REVIEWER_RESOURCE_AUTO_APPROVE_FOR_EMPLOYEE = "PROJECT_ALLOCATION_REVIEWER_RESOURCE_AUTO_APPROVE_FOR_EMPLOYEE";

        // 75,76 - UC025 - Project Allocation Review (Resource)-  Notification to Resource Requestor once Auto-Approved for employee
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_AUTO_APPROVED_FOR_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_AUTO_APPROVED_FOR_EMPLOYEE";

        // 77,78 - UC025 - Project Allocation Review (Resource) - Rejected by Employee ( Configuration for review by Requestor is OFF) TO EMPLOYEE
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF";

        // 79,80 - UC025 - Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is OFF)
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_OFF";

        // 81,82 - UC025 - Project Allocation Review (Resource) - Rejected by Employee ( Configuration for review by Requestor is ON)
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON";

        // 83,84 - UC025 - Project Allocation Review (Resource) -  Notification to Resource Requestor once Rejected by Employee ( Configuration for review by Requestor is ON)
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON";

        // 85,86 - UC025 - Project Allocation Review (Resource) - Withdrawal by Employee post rejection of allocation
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION = "PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION";

        // 87,88 - UC025 - Project Allocation Review (Resource) -  Notification to Resource Requestor once withdrawal by Employee
        public const string PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE = "PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE";

        // 89,90 - UC025 - Employee Rejection approved by Requestor - Notification to Requestor
        public const string EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR = "EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR";

        // 91,92 - UC025 - Employee Rejection approved by  Requestor  - Notification to Employee
        public const string EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE = "EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE";

        // 93,94 - UC026 - Reminder to Requestor to review rejection from employee
        public const string REMINDER_TO_REQUESTOR_TO_REVIEW_REJECTION_FROM_EMPLOYEE = "REMINDER_TO_REQUESTOR_TO_REVIEW_REJECTION_FROM_EMPLOYEE";

        // 95,96 - UC026 - Auto-approve Employee Rejection - Notification to Requestor
        public const string AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_REQUESTOR = "AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_REQUESTOR";

        // 97,98 - UC026 - Auto-approve Employee Rejection - Notification to Employee
        public const string AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_EMPLOYEE = "AUTO_APPROVE_EMPLOYEE_REJECTION_NOTIFICATION_TO_EMPLOYEE";

        // 99,100 - UC026 - Employee Rejection rejected by Requestor - Notification to Requestor
        public const string EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR = "EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR";

        // 101,102 - UC026 - Employee Rejection rejected by Requestor - Escalation Notification to Reviewer
        public const string EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER = "EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER";

        // 103,104 - UC026 - Reviewer to Accept Requestor Response -  Notification to Reviewer
        public const string REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER = "REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER";

        // 105,106 - UC026 - Reviewer to Accept Requestor Response -  Notification to Requestor
        public const string REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR = "REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR";

        // 107,108 - UC026 - Reviewer to Accept Requestor Response -  Notification to Employee
        public const string REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE = "REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE";

        // 109,110 - UC026 - Reminder to Reviewer to take action on pending requests for requestor response
        public const string REMINDER_TO_REVIEWER_TO_TAKE_ACTION_ON_PENDING_REQUEST_FOR_REQUESTOR_RESPONSE = "REMINDER_TO_REVIEWER_TO_TAKE_ACTION_ON_PENDING_REQUEST_FOR_REQUESTOR_RESPONSE";

        // 111,112 - UC026 - Auto- approval of requests escalated to Reviewer - notification to Reviewer
        public const string AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REVIEWER = "AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REVIEWER";

        // 113,114 - UC026 - Auto- approval of requests escalated to Reviewer -notification to Requestor
        public const string AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REQUESTOR = "AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_REQUESTOR";

        // 115,116 - UC026 - Auto- approval of requests escalated to Reviewer -notification to Employee
        public const string AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_EMPLOYEE = "AUTO_APPROVAL_OF_REQUESTS_ESCALATED_TO_EMPLOYEE";

        // 117,118 - UC026 - Reviewer to Reject Requestor Response - Notification to Reviewer
        public const string REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER = "REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER";

        // 119,120 - UC026 - Reviewer to Reject Requestor Response -  Notification to Requestor
        public const string REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR = "REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR";

        // 121,122 - UC026 - Reviewer to Reject Requestor Response -  Notification to Employee
        public const string REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE = "REVIEWER_TO_REJECT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE";

        // 123,124 - UC018 - If a person is deactivated from the Roles & permission screen & this employee is assigned to a future/ current project - notification to be sent to Requestor as employee access to system is disabled
        public const string USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR = "USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR";
        // 130,131 - UC028 - Employee shall be  notified for submission of their skill update request sent to Supercoach.
        public const string EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH = "EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH";
        // 132,133 - UC028 - ACCEPT Once skill assessment request is actioned by Supercoach, employee will get a notification for skill assessment request status along with reviewer comments (for mail notification)
        public const string SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST = "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST";
        // 134,135 - UC028 - REJECT- Once skill assessment request is actioned by Supercoach, employee will get a notification for skill assessment request status along with reviewer comments (for mail notification)
        public const string SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST = "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST";
        // 136,137 - UC028 - Consolidated Reminder
        public const string SUPERCOACH_NOTIFICATION_OF_PENDING_TASK = "SUPERCOACH_NOTIFICATION_OF_PENDING_TASK";
        // 159,160 - UC018 - User roles & permissions update - Admin assigned by Superadmin
        public const string ADMIN_ASSIGNED_BY_SYSTEM_ADMIN = "ADMIN_ASSIGNED_BY_SYSTEM_ADMIN";
        // 163,164 - UC036 - Job Code update- once all allocations have been moved to new code 
        public const string JOB_CODE_UPDATE_TO_NEW_CODE = "JOB_CODE_UPDATE_TO_NEW_CODE";




    }
}
