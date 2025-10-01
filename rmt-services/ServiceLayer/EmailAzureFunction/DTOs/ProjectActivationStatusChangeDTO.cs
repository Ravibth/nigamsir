using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ProjectActivationStatusChangeDTO
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public bool IsJobClosed { get; set; }
    }
}
