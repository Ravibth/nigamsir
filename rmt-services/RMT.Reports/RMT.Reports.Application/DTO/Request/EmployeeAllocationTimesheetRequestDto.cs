using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace RMT.Reports.Application.DTO.Request
{
    public class EmployeeAllocationTimeSheetRequestDto
    {

        public string? designation_name { get; set; }

        public string? business_unit { get; set; }

        [Required]
        public DateTime start_date { get; set; }

        [Required]
        public DateTime end_date { get; set; }

    }
}