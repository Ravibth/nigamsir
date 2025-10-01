using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.DTOs.Request
{
    public class RefreshProjectCompetencyRequestDTO
    {
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
}
