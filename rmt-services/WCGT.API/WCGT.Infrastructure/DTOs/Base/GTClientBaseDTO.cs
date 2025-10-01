using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTClientBaseDTO
    {
        public string client_id { get; set; }
        public string client_group_code { get; set; }
        public string job_client { get; set; }
        public string client_group_name { get; set; }
        public string legal_entity { get; set; }
        public bool isactive { get; set; }
    }
}
