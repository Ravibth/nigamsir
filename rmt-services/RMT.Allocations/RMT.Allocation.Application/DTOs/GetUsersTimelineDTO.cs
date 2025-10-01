namespace RMT.Allocation.Application.DTOs
{
    public class GetUsersTimelineDTO
    {
        public string emails { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
