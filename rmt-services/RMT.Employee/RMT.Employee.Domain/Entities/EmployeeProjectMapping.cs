using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Domain.Entities
{
    public class EmployeeProjectMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        public string EmpMID { get; set; }

        [Required]
        public string Offering { get; set; }

        public string? OfferingId { get; set; }

        [Required]
        public string Solution { get; set; }

        public string? SolutionId { get; set; }


        public DateTime ModifiedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

    }
}
