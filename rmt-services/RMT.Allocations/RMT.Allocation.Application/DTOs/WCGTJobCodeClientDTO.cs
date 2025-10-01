using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class WCGTJobCodeClientDTO
    {
        public string? job_code { get; set; }
        public string client_id { get; set; }
        public string? client_group_code { get; set; }
        public string? job_client { get; set; }
        public string? client_group_name { get; set; }
        public string? legal_entity { get; set; }

        //public virtual ICollection<Project>? projects { get; set; }

        public Boolean? isactive { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
