using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Responses
{
    public class ProjectResponse
    {
        public Int64 ProjectId { get; set; }

        //public string ProjectCode { get; set; }//feb

        //public string ProjectName { get; set; }//feb

        public string? JobCode { get; set; }

        public string PipelineCode { get; set; }
        public string? JobId { get; set; }

        public string ClientName { get; set; }
        public string? ClientGroup { get; set; }

        public string? Expertise { get; set; }//Recheck

        public string SME { get; set; }//Recheck

        public string? bu { get; set; }
        public string? BUId { get; set; }
        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public string AllocationStatus { get; set; }

        public string ResourceRequestor { get; set; }

        public bool IsRollover { get; set; }
        public int RolloverDays { get; set; }
        public bool? IsPublishedToMarketPlace { get; set; }

        public string? JobName { get; set; }
        public string PipelineName { get; set; }
        public bool? IsActive { get; set; }
        public string? ChargableType { get; set; }
        public string? Location { get; set; }
        public string? BusinessUnit { get; set; }
        public string? Smeg { get; set; }//Recheck
        public string? RevenueUnit { get; set; }//Recheck
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public DateTime? SuspendedAt { get; set; }

        public DateOnly? DelegateAssignmentDate { get; set; }
        public string? DelegateAssignedBy { get; set; }
        public string? UserRequesting { get; set; }
        public DateOnly? UserAssignmentDate { get; set; }
        public string? NewAdditionalEls { get; set; }
        public string? NewAdditionalDelegates { get; set; }
        //ACTIONS
        public List<string>? Actions { get; set; }
        public List<string>? EmployeeReleasedDueToProjectSuspend { get; set; }
        public string? ClientId { get; set; }
    }
}
