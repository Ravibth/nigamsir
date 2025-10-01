using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTCompetencyBaseDTO
    {
        public string CompetencyId { get; set; }

        public string CompetencyName { get; set; }

        public string? CompetencyLeaderMID { get; set; }

        public string BuId { get; set; }

        public bool isactive { get; set; }

    }
}
