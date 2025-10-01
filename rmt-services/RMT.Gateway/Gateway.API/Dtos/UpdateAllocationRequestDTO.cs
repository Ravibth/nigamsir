namespace Gateway.API.Dtos
{
    public class UpdateAllocationRequestDTO
    {
        public string guid { get; set; }
        public string AllocationStatus { get; set; }
        public string? WorkflowModule { get; set; }
        public string? WorkflowSubModule { get; set; }
        public string token { get; set; }

    }
}
    