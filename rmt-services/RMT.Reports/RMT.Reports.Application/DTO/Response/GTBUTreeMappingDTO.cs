using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.DTO.Response
{
    public class GTBUTreeMappingDTO
    {
        public string bu { get; set; }
        public string bu_id { get; set; }
        public string offering { get; set; }
        public string offering_id { get; set; }
        public string? offering_leader_mid { get; set; }

        public string solution { get; set; }
        public string solution_id { get; set; }
        public string? solution_leader_mid { get; set; }
        public bool isactive { get; set; }
        public string? bu_leader_mid { get; set; }
        public string? bu_efficiency_leader_mid { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
