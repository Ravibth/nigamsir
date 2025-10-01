using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class Competency
    {
        [Key]
        public string CompetencyId { get; set; }

        public string CompetencyName { get; set; }

        public string? CompetencyLeaderMID { get; set; }

        public string BuId { get; set; }

        public Boolean isactive { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string createdby { get; set; }

        public string modifiedby { get; set; }
    }
}
