using RMT.Projects.Domain.Entities;

namespace RMT.Projects.Application.Responses
{
    public class BasicProjectDetailsRequestorResponse
    {
        public Int64 Id { get; set; }
        //public string ProjectCode { get; set; }//feb
        public string? PipelineName { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobName { get; set; }
        public string? JobId { get; set; }
        public string? JobCode { get; set; }
        public string? ClientName { get; set; }
        public string? Expertise { get; set; }//Recheck
        public string? Sme { get; set; }//Recheck
        public string? Smeg { get; set; }//Recheck

        public string? bu { get; set; }
        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Location { get; set; }
        public string? ProjectType { get; set; }
        public string? ChargableType { get; set; }
        public string? RevenueUnit { get; set; }//Recheck
        public string? BudgetStatus { get; set; }
        public bool IsRollover { get; set; }
        //public List<ProjectJobCodes>? ProjectJobCodes { get; set; }
        public string? ClientId { get; set; }
    }
}
