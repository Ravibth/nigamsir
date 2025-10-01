using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SuperCoachProjectRole
    { 
        public string PreviouseUser { get;set; }
        public string User { get; set; }
        public List<ProjectCode> ProjectCodes { get; set;}        
    }
    public class ProjectCode
    {
        public string JobCode { get; set; }
        public string PiplelineCode { get; set; }
    }
}
