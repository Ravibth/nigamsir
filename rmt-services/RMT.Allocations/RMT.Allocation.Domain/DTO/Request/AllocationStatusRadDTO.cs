namespace RMT.Allocation.Domain.DTO.Request
{
    public class AllocationStatusRadDTO
    {
        public string AllocationStatus { get; set; }
        public Guid Guid { get; set; }
        public string? WorkflowModule { get; set; }
        public string? WorkflowSubModule { get; set; }
        public string token { get; set; }
    }
}
