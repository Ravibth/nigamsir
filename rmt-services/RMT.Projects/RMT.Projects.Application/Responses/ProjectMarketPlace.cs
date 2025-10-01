using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Responses
{
   
    public class ProjectMarketPlace
    {
        public int Id { get; set; } = 0;       
        //public string ProjectCode { get; set; }//feb = string.Empty;       
        public string Flag { get; set; } = string.Empty;
        public DateOnly PublishDate { get; set; }
        public DateOnly EmployeeAllocationEndDate { get; set; }
        public string EmployeeAllocationHours { get; set; } = string.Empty;

    }

}
