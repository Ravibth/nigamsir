using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.ResourceAllocationDTOs
{
    public class GetResourceNameResponseDTO
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Designation { get; set; }
        public List<string> Skills { get; set; }

        public string Location { get; set; }
        public string Supercoach { get; set; }
        public string RevenueUnit { get; set; }//Recheck
        public string Expertise { get; set; }//Recheck
        public string SME { get; set; }//Recheck

        public string? supercoach_name { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? supercoach_mid { get; set; }
        public string? co_supercoach_mid { get; set; }

        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }
        public string? Offerings { get; set; } 
        public string? Solutions { get; set; } 

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? BUId { get; set; }

        public string BusinessUnit { get; set; }

    }
}
