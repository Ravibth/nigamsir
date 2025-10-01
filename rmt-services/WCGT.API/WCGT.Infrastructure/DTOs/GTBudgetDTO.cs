using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs
{
    public class GTBudgetDTO
    {
        public string? PipelineCode { get; set; }
        public string JobCode { get; set; }
        public string Grade { get; set; }
        public double Hour { get; set; }
        public double Rate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
    }
}
