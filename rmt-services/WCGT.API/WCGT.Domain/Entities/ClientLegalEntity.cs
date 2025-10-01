using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class ClientLegalEntity
    {
        [Key]
        public string par_aid { get; set; }
        public string? para_desc { get; set; }

        //public virtual ICollection<Project>? projects { get; set; }

        public Boolean? isactive { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
