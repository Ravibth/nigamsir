using RMT.MarketPlace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Domain.DTOs.Response
{
    public class MarketPlaceProjectDetaillsIntrestDTO
    {
        public Int64? ID { get; set; }

        //public string? ProjectCode { get; set; }
        //public string? ProjectName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? ClientName { get; set; }
        public string? ClientGroup { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public bool? IsPublishedToMarketPlace { get; set; }
        public DateTime? MarketPlacePublishDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public bool? IsActive { get; set; }
        //JsonData contain keyValue pair field ClientName, ClientGroup, nameOfProject, ProjectID
        public string? JsonData { get; set; }
        public string? ChargableType { get; set; }
        public string? Location { get; set; }
        public string? BusinessUnit { get; set; }
        public string BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }

        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
        public string? Csp { get; set; }
        public string? ProposedCsp { get; set; }
        public string? ElForJob { get; set; }
        public string? ElForPipeLine { get; set; }
        public string? JobManager { get; set; }
        public bool? IsInterested { get; set; }
        public bool? IsAllocated { get; set; }
        public DateTime? MarketPlaceExpirationDate { get; set; }
        public bool? IspipeLine { get; set; }

        public List<EmpProjectInterest>? EmployeeInterest { get; set; }
    }
}
