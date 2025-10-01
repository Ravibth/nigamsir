using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.RequisitionDTOs
{
    public class UpdateRequisitionDTO
    {
        public List<UpdateResourceEntities>? ResourceEntities { get; set; }
    }
}