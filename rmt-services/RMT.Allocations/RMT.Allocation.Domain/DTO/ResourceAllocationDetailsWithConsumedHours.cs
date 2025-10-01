using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class ResourceAllocationDetailsWithConsumedHours : ResourceAllocationDetailsResponse
    {
        public Int64? consumedHours { get; set; }
    }
}
