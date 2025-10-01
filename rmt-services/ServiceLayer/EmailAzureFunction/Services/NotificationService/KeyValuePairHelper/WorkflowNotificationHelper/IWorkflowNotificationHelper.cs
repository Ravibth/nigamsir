using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper
{
    public interface IWorkflowNotificationHelper
    {
        Task<List<Dictionary<string, string>>> NOTIFICATION_TO_REVIEWER_ALLOCATION_OF_RESOURCE_TO_PROJECT(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> NOTIFICATION_TO_EMPLOYEE_ALLOCATION_OF_RESOURCE_TO_PROJECT(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> RESOURCE_ALLOCATION_REVIEW_NOTIFICATION_APPROVED_BY_REVIEWER_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_APPROVED_BY_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_EMPLOYEE_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_REJECTED_BY_EMPLOYEE_CONFIGURATION_FOR_REVIEW_BY_REQUESTOR_IS_ON(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_WITHDRAWAL_BY_EMPLOYEE_POST_REJECTION_OFF_ALLOCATION(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_APPROVED_BY_REQUESTOR_NOTIFICATION_TO_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> PROJECT_ALLOCATION_REVIEW_RESOURCE_NOTIFICATION_TO_RESOURCE_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> EMPLOYEE_REJECTION_REJECTED_BY_REQUESTOR_ESCALATION_NOTIFICATION_TO_REVIEWER(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REVIEWER(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> REVIEWER_TO_ACCEPT_REQUESTOR_RESPONSE_NOTIFICATION_TO_EMPLOYEE(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> EMPLOYEE_SHALL_BE_NOTIFIED_FOR_SUBMISSION_OF_THEIR_SKILL_UPDATE_REQUEST_SENT_TO_SUPRERCOACH(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
        Task<List<Dictionary<string, string>>> SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);

        Task<List<Dictionary<string, string>>> SUPERCOACH_NOTIFICATION_OF_PENDING_TASK(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);

    }
}
