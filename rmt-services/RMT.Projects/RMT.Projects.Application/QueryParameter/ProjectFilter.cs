using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.QueryParameter
{
    public class ProjectFilter
    {
        public List<string>? bu { get; set; }
        // public List<string>? experties { get; set; }//Recheck
        // public List<string>? smeg { get; set; }//Recheck

        public List<string>? Offerings { get; set; }
        public List<string>? Solutions { get; set; }

        public List<string>? clientName { get; set; }
        public List<string>? pipelineCode { get; set; }
        public List<string>? jobCode { get; set; }
        public List<string>? projectStatus { get; set; }
        public string? projectType { get; set; }
        public string? marketplace { get; set; }
    }
}
