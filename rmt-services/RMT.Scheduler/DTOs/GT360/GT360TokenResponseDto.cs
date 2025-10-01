using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs.GT360
{
    public class GT360TokenResponseDto
    {
        public int statusCode { get; set; }
        public string statusMsg { get; set; }
        public GT360TokenResponseDataDto data { get; set; }
    }

    public class GT360TokenResponseDataDto
    {
        public string stoken { get; set; }
        public string strMsg { get; set; }
        public string strCode { get; set; }
        public string strExpery { get; set; }

    }
}
