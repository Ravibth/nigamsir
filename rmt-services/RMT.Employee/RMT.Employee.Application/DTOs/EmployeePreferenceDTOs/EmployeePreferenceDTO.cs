using RMT.Employee.Domain.DTOs;

namespace RMT.Employee.Application.DTOs.EmployeePreferenceDTOs
{
    public class EmployeePreferenceDTO
    {
        public Int64? Id { get; set; }
        public string EmployeeEmail { get; set; }
        public int PreferedValue { get; set; }
        public string Category { get; set; }
        public BusinessUnit? businessUnit { get; set; }
        public Offering? offering { get; set; }
        public Solution? solution { get; set; }
    }
}
