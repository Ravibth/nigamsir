using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class CreateWorkflowRequestDTO
    {
        public string name { get; set; }//WORKFLOW NAME
        public string status { get; set; }//TAKE PROPER WORKFLOW
        public string module { get; set; }//TAKE FROM MODULE
        public string sub_module { get; set; }//TAKE FORM SUB_MODULE
        public Guid item_id { get; set; }//UNIQUE ID
        public string assigned_to { get; set; }
        public string? entity_type { get; set; }
        public Object? entity_meta_data { get; set; }
    }
}
