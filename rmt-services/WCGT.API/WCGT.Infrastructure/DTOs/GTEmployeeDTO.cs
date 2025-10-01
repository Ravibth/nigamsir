using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.Entities;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.Infrastructure.DTOs
{

    public class GetEmployeesForPortfolioResponse
    { 
      public List<GTEmployeeDTO> employees { get; set; }
        public List<string> role { get; set; } = new List<string>();
        public string emp_mid { get; set; }
    }

    public class GTEmployeeDTO : GTEmployeeBaseDTO, IEntityBase
    {
        [ForeignKey("location_id")]
        public virtual Location? location { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }        
    }
}

