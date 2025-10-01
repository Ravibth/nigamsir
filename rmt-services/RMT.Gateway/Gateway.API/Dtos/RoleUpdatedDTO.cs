using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gateway.API.Dtos
{
    public class RoleUpdatedDTO
    {
    
        public int? Id { get; set; }

        public string? RoleIds { get; set; }

  
        public string? EmailId { get; set; }


        public string? Name { get; set; }

        public string? UEmailId { get; set; }

        
        public string? Entity { get; set; }

       
        public string? EmployeeId { get; set; }

        public string? EmpCode { get; set; }

        public string? FName { get; set; }

        public string? LName { get; set; }

        public string? Designation { get; set; }

        public string? Location { get; set; }

        public string? RegionName { get; set; }

        public string? Smeg { get; set; }//Recheck

        public string? Expertise { get; set; }//Recheck

        public string? BusinessUnit { get; set; }
        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public string? CoSupercoachName { get; set; }

        public string? SupercoachName { get; set; }

        public string? ServiceLine { get; set; }

        public string? Roles { get; set; }

        public bool? Status { get; set; }

        public string? ReportingPartnerMid { get; set; }
        public List<string>? ActionList { get; set; }

    }
}
