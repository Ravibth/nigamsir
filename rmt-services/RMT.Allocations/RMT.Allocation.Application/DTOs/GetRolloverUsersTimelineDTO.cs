namespace RMT.Allocation.Application.DTOs
{
    public class GetRolloverUsersTimelineDTO
    {
        public List<GetUsersTimelineDTO> usersTimelineRequest { get; set; }

        public int RollOverDays { get; set; }

        public string PipelineCode { get; set; }
        public string JobCode { get; set; }

    }

}
