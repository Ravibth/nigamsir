using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{
    public class GetAllActiveRequisitionDTO
    {
        public Int64? RequisionId { get; set; }
        public string PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobCode { get; set; }
        //public string ProjectCode { get; set; }//feb
        //public string ProjectName { get; set; }//feb
        public string RequisitionDescription { get; set; }
        public Boolean IsContinuousAllocation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int64 TotalHours { get; set; }
        public string RequisitionStatus { get; set; }
        public string Bu { get; set; }
        public string BuId { get; set; }
        public string Smeg { get; set; }//Recheck
        public string SmegId { get; set; }//Recheck
        public string Expertise { get; set; }//Recheck
        public string SME { get; set; }//Recheck

        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string OfferingsId { get; set; }
        public string SolutionsId { get; set; }

        public string Designation { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public string? Score { get; set; }
        public string? EffortsPerDay { get; set; }
        public bool? isPerDayHourAllocation { get; set; }
    }
}
