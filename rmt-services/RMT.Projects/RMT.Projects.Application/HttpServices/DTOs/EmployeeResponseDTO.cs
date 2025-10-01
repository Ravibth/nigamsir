using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{
    public class SupercoachDelegate
    {
        public Guid? Id { get; set; }
        public string supercoach_mid { get; set; }
        public string? allocation_delegate_name { get; set; }
        public string? allocation_delegate_mid { get; set; }
        public string? allocation_delegate_email { get; set; }
        public string? skill_delegate_name { get; set; }
        public string? skill_delegate_mid { get; set; }
        public string? skill_delegate_email { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
    public class EmployeeResponseDTO
    {
        public string? id { get; set; }
        public string? role_ids { get; set; }
        public string emailId { get; set; }
        public string name { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string? designation { get; set; }
        public string? BusinessUnit { get; set; }
        //public int roles { get; set; }
        public bool? status { get; set; }
        public bool? is_existing { get; set; }
        public List<string>? role_list { get; set; }
        public string? Supercoach { get; set; }
        public string? smeg { get; set; }//Recheck
        public string? expertise { get; set; }//Recheck

        public string? BUId { get; set; }
        public string? Offerings { get; set; }
        public string? Solutions { get; set; }
        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public string location { get; set; }
        public string empCode { get; set; }
        public string serviceLine { get; set; }
        public SupercoachDelegate? delegate_details { get; set; }
    }
}
