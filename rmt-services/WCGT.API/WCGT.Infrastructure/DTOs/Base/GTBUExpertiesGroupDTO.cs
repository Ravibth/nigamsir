using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTBUExpertiesGroupDTO
    {

        public Dictionary<string,string> BU { get; set; }
        public Dictionary<string, string> Offerings { get; set; }
        public Dictionary<string, string> Solutions { get; set; }

    }
 
}
