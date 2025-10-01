using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.Entities
{
    public class EmployeeProject
    {
        public Int64 Id { get; set; } = 0;
        //public string ProjectCode { get; set; }//feb 
        //public string ProjectName { get; set; }//feb 
        //public string ProjectDescription { get; set; } 
        public string? EngagementLeader { get; set; }
        public string Expertise { get; set; }//Recheck

        public string Offerings { get; set; }
        public string Solutions { get; set; }

        public string ProjectStatus { get; set; }
        public string? JobManager { get; set; }
        public string ClientName { get; set; }
        public string Sme { get; set; }//Recheck

        public string? Smeg { get; set; }//Recheck

        //public DateOnly ProjectStartDate { get; set; }
        //public DateOnly ProjectEndDate { get; set; }
        public string ProjectChargeType { get; set; }
        public string ProjectPipelineType { get; set; }
        public string EmployeeEmail { get; set; }
        public DateOnly ProjectStartDate { get; set; }
        public DateOnly ProjectEndDate { get; set; }
        public DateTime EmployeeAllocationStartDateTime { get; set; }
        public DateTime EmployeeAllocationEndDateTime { get; set; }
        public string EmployeeAllocationHours { get; set; }

    }
}
