using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class GetUsersByEmailDTO
    {
        public int count { get; set; }
        public List<UserDTO> rows { get; set; }

    }
    
    public class UserDTO
    {
        public int? Id { get; set; }
        public string? RoleIds { get; set; }
        public string email_id { get; set; }
        public string? Name { get; set; }
        public string uemail_id { get; set; }
        public string? Entity { get; set; }
        public string employee_id { get; set; }
        public string emp_code { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Designation { get; set; }
        public string? Location { get; set; }
        public string? RegionName { get; set; }
        public string? Smeg { get; set; }//Recheck
        public string? Expertise { get; set; }//Recheck
        public string? BusinessUnit { get; set; }
        public string? business_unit { get; set; }
        public string? supercoach_name { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? supercoach_mid { get; set; }
        public string? co_supercoach_mid { get; set; }
        public string? competency { get; set; }
        public string? competencyId { get; set; }

        public string? CoSupercoachName { get; set; }
        public string? SupercoachName { get; set; }
        public string? ServiceLine { get; set; }
        public string? Roles { get; set; }
        public bool? Status { get; set; }
        public string? ReportingPartnerMid { get; set; }
        public string? EmployeeStatus { get; set; }
        public DateTime? EmployeeResignationDate { get; set; }
        public DateTime? EmployeeLastWorkingDate { get; set; }
        public string? SupercoachMid { get; set; }
        public string? CoSupercoachMid { get; set; }
        public bool is_active { get; set; }
        public SupercoachDelegate? delegate_details { get; set; }
    }

}




