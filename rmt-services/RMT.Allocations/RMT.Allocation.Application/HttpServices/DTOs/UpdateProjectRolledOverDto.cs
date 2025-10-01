using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class UpdateProjectRolledOverDto
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }

        public Boolean IsRollover { get; set; }

        public int RolloverDays { get; set; }

        public string? ModifiedBy { get; set; }

    }
}
