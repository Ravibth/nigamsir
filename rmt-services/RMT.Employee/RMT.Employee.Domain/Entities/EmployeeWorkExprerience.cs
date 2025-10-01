using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Domain.Entities
{
    public class EmployeeWorkExprerience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string name_of_employer { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string last_designation_held { get; set; }
        public string employee_mid { get; set; }
        public Int64 employee_profile_id { get; set; }
        [ForeignKey("employee_profile_id")]
        public virtual EmployeeProfile? employee_profile { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string created_by { get; set; }
        public string? updated_by { get; set; }
    }
}
