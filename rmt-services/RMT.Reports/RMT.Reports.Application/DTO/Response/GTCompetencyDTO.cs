using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.DTO.Response
{
    public class GTCompetencyDTO
    {
        public string CompetencyId { get; set; }

        public string CompetencyName { get; set; }

        public string? CompetencyLeaderMID { get; set; }

        public string BuId { get; set; }

        public bool isactive { get; set; }
    }
}
