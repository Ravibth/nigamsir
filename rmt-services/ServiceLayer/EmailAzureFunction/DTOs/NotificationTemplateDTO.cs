using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class NotificationPlaceHolderDTO
    {
        public Int64? id { get; set; }
        public string name { get; set; }
        public Int64? notification_template_id { get; set; }
        public bool? is_active { get; set; }
        public bool? is_required { get; set; }
        public bool? link_payload { get; set; }
    }
    public class NotificationTemplateDTO
    {
        public Int64 Id { get; set; }
        public string module { get; set; }
        public string sub_module { get; set; }
        public string subject { get; set; }
        public string type { get; set; }
        public bool is_active { get; set; }
        public string notification_type { get; set; }
        public string to { get; set; }
        public string template { get; set; }
        public string link { get; set; }
        public List<NotificationPlaceHolderDTO> payload { get; set; }
    }
}
