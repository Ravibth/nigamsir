using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class RequestPayloadDTO
    {
        public string query { get; set; }
        public dynamic body { get; set; }
    }
}
