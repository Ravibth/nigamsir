using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class GetRateDesignationDTO
    {
        public string Designation { get; set; }
        public string Competency { get; set; }
        public Double RatePerHour { get; set; }
    }
}
