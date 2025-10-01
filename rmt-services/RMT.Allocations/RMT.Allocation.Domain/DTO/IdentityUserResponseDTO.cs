using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
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
    public class IdentityUserResponseDTO
    {
        public string name { get; set; }
        public string emailId { get; set; }
        public string employeeId { get; set; }

        public string entity { get; set; }
        public string empCode { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string designation { get; set; }
        public string grade { get; set; }
        public string location { get; set; }
        public string region_name { get; set; }

        public string competency { get; set; }
        public string competencyId { get; set; }

        public string employee_id { get; set; }
        public string uemail_id { get; set; }
        public string supercoach_mid { get; set; }
        public string co_supercoach_mid { get; set; }

        public string Supercoach { get; set; }
        public string supercoach_name { get; set; }
        public string co_supercoach_name { get; set; }
        public string serviceLine { get; set; }
        public string BusinessUnit { get; set; }
        public string? business_unit { get; set; }

        public string roles { get; set; }
        public SupercoachDelegate? delegate_details { get; set; }

    }
}
