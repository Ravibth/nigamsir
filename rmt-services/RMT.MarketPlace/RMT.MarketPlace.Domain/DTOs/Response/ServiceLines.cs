using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Domain.DTOs.Response
{

    public class Offerings
    {
        public string BU { get; set; }
        public string Name { get; set; }
    }

    public class Solutions
    {
        public string Offerings { get; set; }
        public string Name { get; set; }
    }

    public class SubIndustry
    {
        public string Industry { get; set; }
        public string Name { get; set; }
    }

}
