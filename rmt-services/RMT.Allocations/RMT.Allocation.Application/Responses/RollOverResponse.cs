using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Responses
{
    public class RollOverResponse
    {
        public List< ResourceAvailable> InvalidAllocations { get; set; }

        public List<ResourceAllocationDetailsResponse> UpdatedAllocations { get; set; }
        
    }
}
