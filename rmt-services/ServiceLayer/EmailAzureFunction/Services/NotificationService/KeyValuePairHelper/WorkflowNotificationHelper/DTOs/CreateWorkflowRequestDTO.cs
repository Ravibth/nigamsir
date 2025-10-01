using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.WorkflowNotificationHelper.DTOs
{
    public class CreateWorkflowRequestDTO
    {
        public string? name { get; set; }
        public string? module { get; set; }
        public string? sub_module { get; set; }
        public string? item_id { get; set; }
        public string? created_by { get; set; }
        public string? assigned_to { get; set; }
        public string? status { get; set; }
    }
}
