using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTClientLegalEntityBaseDTO
    {
        public string par_aid { get; set; }
        public string para_desc { get; set; }
        public bool isactive { get; set; }

    }
}
