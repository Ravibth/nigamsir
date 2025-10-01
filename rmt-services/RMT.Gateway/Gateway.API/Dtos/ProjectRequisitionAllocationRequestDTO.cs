namespace Gateway.API.Dtos
{
    public class ProjectRequisitionAllocationRequestDTO
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public int requisitionCountAdded { get; set; }
        public int allocationCountAdded { get; set; }
    }
}
