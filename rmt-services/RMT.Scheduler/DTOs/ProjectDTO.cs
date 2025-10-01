using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class ProjectJobCodes
    {
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        public string? ProjectCode { get; set; }

        public string? PipelineCode { get; set; }

        public string? JobCode { get; set; }

        public string? JobName { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
    public class ProjectDTO
    {
        public Int64 Id { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobName { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? ClientName { get; set; }
        public string? Expertise { get; set; }
        public string? Sme { get; set; }//Recheck
        public string? Smeg { get; set; }//Recheck
        public string? bu { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public string? ProjectType { get; set; }
        public string? ChargableType { get; set; }
        public string? RevenueUnit { get; set; }//Recheck
        public string? BudgetStatus { get; set; }
        public bool IsRollover { get; set; }
        public string? ClientId { get; set; }
    }
}
