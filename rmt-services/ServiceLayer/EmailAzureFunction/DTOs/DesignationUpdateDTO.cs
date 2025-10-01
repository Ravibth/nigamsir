using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class DesignationUpdateRequestDTO
    {
        public List<DesignationUpdateDTO> UpdateDesignationCostDTO { get; set; }
    }
    public class DesignationUpdateDTO
    {
        public string EmpEmail { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }
        public int? RatePerHour { get; set; }
        public DateTime UpdateDate { get; set; }
    }

}
