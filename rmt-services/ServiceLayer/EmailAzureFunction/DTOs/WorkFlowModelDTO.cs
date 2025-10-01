using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class WorkFlowModelDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string module { get; set; }
        public string sub_module { get; set; }
        public string item_id { get; set; }
        public string outcome { get; set; }
        public string status { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public string entity_type { get; set; }
        public object entity_meta_data { get; set; }
        public DateTime? updated_at { get; set; }
        public bool? is_active { get; set; }
    }
}
