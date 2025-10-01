using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class UserSkillDto
    {

        public Guid id { get; set; }
        public string skillName { get; set; }
        public string skillCode { get; set; }
        public string proficiency { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string empId { get; set; }
        public bool isActive { get; set; }
        public DateTime createdAt { get; set; }
        public string createdBy { get; set; }
        public DateTime modifiedAt { get; set; }
        public string modifiedBy { get; set; }
    }
}
