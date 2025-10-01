using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class WCGTPipelineRoles
    {
        public Int64? id { get; set; }
        public string? pipeline_code { get; set; }
        public string? job_id { get; set; }
        public string? job_code { get; set; }
        public string? user_mid { get; set; }
        public string? user_empname { get; set; }
        public string? user_emailid { get; set; }
        public string? user_role { get; set; }
        public bool? isactive { get; set; }
        public string? user_with_empid { get; set; }
        public string? user_transformed_role { get; set; }
        public string? user_application_role { get; set; }
    }
}
