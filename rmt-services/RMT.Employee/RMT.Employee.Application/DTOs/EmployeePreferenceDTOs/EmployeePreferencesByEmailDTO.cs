namespace RMT.Employee.Application.DTOs.EmployeePreferenceDTOs
{
    public class EmployeePrefDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string PreferenceId { get; set; }

    }
    public class EmployeePreferencesByEmailDTO
    {
        public string Email { get; set; }

        public List<EmployeePrefDTO> EmployeePreference { get; set; }

    }
}
