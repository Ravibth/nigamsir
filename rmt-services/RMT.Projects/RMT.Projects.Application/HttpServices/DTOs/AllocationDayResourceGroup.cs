using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{
    public class AllocationDayResourceGroup
    {
        public string? empemail { get; set; }
        public string? empname { get; set; }
        public Double? totaltime { get; set; }
        public Double? cost { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public Double? TotalActualEfforts { get; set; }
    }
}
