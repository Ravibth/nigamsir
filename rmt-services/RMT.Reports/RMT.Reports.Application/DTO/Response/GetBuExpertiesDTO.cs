using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.DTO.Response
{
    public class GetBuExpertiesDTO
    {
        public Dictionary<string, string> BU { get; set; }
        public Dictionary<string, string> Offerings { get; set; }
        public Dictionary<string, string> Solutions { get; set; }
    }
}
