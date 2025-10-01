using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper.DTOs
{
    public class BulkUpdateWorkflowResponseDTO
    {
        public BulkWorkflowApprovalRequestDTO requestPayload { get; set; }
        public WorkflowDTO workflowResult { get; set; }
        public string[] actions { get; set; }
        public List<WorkflowTaskDTO> result { get; set; }
        public dynamic error { get; set; }
        public bool isError { get; set; }
    }
    public class BulkWorkflowApprovalRequestDTO
    {
        public string workflow_id { get; set; }
        public string item_id { get; set; }
        public string workflow_task_id { get; set; }
        public string assigned_to { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string workflow_task_title { get; set; }
    }
}