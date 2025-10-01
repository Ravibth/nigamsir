using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTProjectBaseDTO
    {
        public string? pipeline_code { get; set; }
        public string? project_code { get; set; }
        public string? location { get; set; }
        public string? enquiry_owner { get; set; }
        public string? recurring { get; set; }
        public string? sector { get; set; }
        public string? pipeline_status { get; set; }
        public string? sme { get; set; }
        public string? pipeline_description { get; set; }
        public string? service_line { get; set; }
        public string? sme_group { get; set; }
        public string? revenue { get; set; }

        public bool? isactive { get; set; }

    }
}
