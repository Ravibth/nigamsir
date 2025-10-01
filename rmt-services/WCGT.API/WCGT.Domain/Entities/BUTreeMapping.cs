using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class BUTreeMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? id { get; set; }
        public string? bu { get; set; }
        public string? bu_id { get; set; }

        public string? offering { get; set; }
        public string? offering_id { get; set; }
        public string? offering_leader_mid { get; set; }

        public string? solution { get; set; }
        public string? solution_id { get; set; }
        public string? solution_leader_mid { get; set; }

        public Boolean? isactive { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
        public string? bu_leader_mid { get; set; }
        public string? bu_efficiency_leader_mid { get; set; }
    }
}
