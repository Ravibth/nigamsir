using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class Holiday
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? id { get; set; }

        public string? holiday_name { get; set; }
        public string? holiday_type { get; set; }

        public string? location_id { get; set; }

        [ForeignKey("location_id")]
        public virtual Location? location { get; set; }

        public string? location_name { get; set; }
        public DateOnly? holiday_date { get; set; }
        public DateOnly? cr_date { get; set; }

        public Boolean? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
