using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.DTOs.Request
{
    public class GetBuExpertiesDTO
    {
        public Dictionary<string, string> BU { get; set; }
        //public Dictionary<string, string> Experties { get; set; }//Recheck
        //public Dictionary<string, string> SMEG { get; set; }//Recheck
        public Dictionary<string, string> Offerings { get; set; }
        public Dictionary<string, string> Solutions { get; set; }

    }
}
