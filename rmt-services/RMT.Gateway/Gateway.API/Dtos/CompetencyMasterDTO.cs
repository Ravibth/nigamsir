namespace Gateway.API.Dtos
{
    public class CompetencyMasterDTO
    {
        public string CompetencyId { get; set; }

        public string CompetencyName { get; set; }

        public string? CompetencyLeaderMID { get; set; }

        public string BuId { get; set; }

        public bool isactive { get; set; }
    }
}
