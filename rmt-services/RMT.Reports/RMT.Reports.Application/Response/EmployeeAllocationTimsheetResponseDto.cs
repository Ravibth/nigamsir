namespace RMT.Reports.Application.Response
{
    public class EmployeeAllocationTimeSheetDto
    {
        public Int64 PreferenceMasterId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
        //public DateTime ModifiedAt { get; set; }
        //public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}