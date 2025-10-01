using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class WCGTDataLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? id { get; set; }

        public string? status { get; set; }

        public string? entity_type { get; set; }

        public string? error_message { get; set; }

        public string? error_stacktrace{ get; set; }

        public string? input_json { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
