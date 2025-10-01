using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTProjectJobCodeBaseDTO
    {
        public string? job_id { get; set; }
        public string? job_code { get; set; }
        public string? pipeline_code { get; set; }
        public string? job_name { get; set; }
        public string? job_description { get; set; }
        public string? job_client { get; set; }
    }
}
