using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Responses
{
    public class GTBUEfficiencyLeaderResponse
    {
        public string? bu_id { get; set; }
        public string? bu_efficiency_mid { get; set; }
        public Boolean isfailed { get; set; }
        public string? failed_message { get; set; }
    }
}
