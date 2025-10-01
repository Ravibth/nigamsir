using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class AddResourceAllocationSkillRequestDTO
    {
        public Guid RequisitionId { get; set; }
        public List<SkillsEntities> Skills { get; set; }
    }
}
