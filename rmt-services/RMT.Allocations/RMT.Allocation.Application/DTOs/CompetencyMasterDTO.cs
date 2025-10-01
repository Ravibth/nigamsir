using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class CompetencyMasterDTO
    {
        public string? CompetencyId { get; set; }
        public string? CompetencyMID { get; set; }
        public string? CompetencyName { get; set; }
        public string? Competency { get; set; }
        public string? CompetencyLeaderMID { get; set; }
        public string? BuId { get; set; }
        public Boolean? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
