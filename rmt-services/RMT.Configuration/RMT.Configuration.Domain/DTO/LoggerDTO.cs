using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain.DTO
{
    public class LoggerDTO
    {
        public LogLevel LogLevel { get; set; }
        public string? Category { get; set; }
        public string? Function { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public object[]? LogObjects { get; set; }

    }
}
