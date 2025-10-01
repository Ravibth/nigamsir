using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.DTOs
{
    public class UserSkills
    {
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string Proficiency { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string EmpId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
