using RMT.Employee.Domain.Entities;

namespace RMT.Employee.Application.Response
{
    public class EmployeePreferenceResponse
    {
        public Int64 Id { get; set; }
        public string EmployeeEmail { get; set; }
        public int PreferedValue { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public virtual PreferenceMaster PreferenceMaster { get; set; }
    }
}
