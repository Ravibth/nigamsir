using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class WorkflowNotificationDTO
    {
        public string id { get; set; }
        public string assigned_to { get; set; }
        public string assigned_to_userName { get; set; }
        public string comment { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public string description { get; set; }
        public DateTime? due_date { get; set; }
        public string proxy_approval_by { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public WorkFlowModelDTO workflow { get; set; }
        public string workflow_id { get; set; }
    }
}
