using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WCGT.Infrastructure.DTOs.Base
{
        public class GTBUTreeMappingBaseDTO
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
        }
}
