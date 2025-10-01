using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class EmailMetaPayloadDTO
    {
        public string[] to { get; set; }
        public string[] cc { get; set; }
        public string meta { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
    }
}
