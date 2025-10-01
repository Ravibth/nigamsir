using Newtonsoft.Json;
using RMT.Employee.Domain.DTOs;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMT.Employee.Application.DTOs.EmployeePreferenceDTOs
{
    public class UpdateEmployeePreferenceDTO
    {
        public Int64? Id { get; set; }
        public string? EmployeeEmail { get; set; }
        public string? Category { get; set; }
        public string? PreferenceInfo { get; set; }
        public int? PreferenceOrder { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public BusinessUnit? businessUnit { get; set; }
        public Offering? offering { get; set; }
        public Solution? solution { get; set; }
        public Location? location { get; set; }
        public Industry? industry { get; set; }
        public SubIndustry? subIndustry { get; set; }
    }
}
