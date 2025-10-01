using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Responses
{
    public enum ProjectType
    {
        JobCode,
        PipleCode
    };
    public class EmployeeProjects
    {
        public int Id { get; set; } = 0;
        //public string ProjectCode { get; set; }//feb = string.Empty;
        public string ProjectPiplineCode { get; set; } = string.Empty;
        //public string ProjectName { get; set; }//feb = string.Empty;
        public string ProjectDescription { get; set; } = string.Empty;

        public string EngagementLeader { get; set; }
        public string Expertise { get; set; }
        public string ProjectStatus { get; set; } = string.Empty;
        public string JobManager { get; set; }
        public string ClientName { get; set; }

        public DateOnly ProjectStartDate { get; set; }
        public DateOnly ProjectEndDate { get; set; }
        public string ProjectChargeType { get; set; } = string.Empty;
        public ProjectType ProjectPipelineType { get; set; } = ProjectType.PipleCode;
        public string EmployeeEmail { get; set; } = string.Empty;
        public DateOnly EmployeeAllocationStartDate { get; set; }
        public DateOnly EmployeeAllocationEndDate { get; set; }
        public string EmployeeAllocationHours { get; set; } = string.Empty;


    }

}
