using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Gateway.API.Dtos
{
    public class RequestPayloadDTO
    {
        public string query { get; set; }
        public dynamic body { get; set; }
    }
}
