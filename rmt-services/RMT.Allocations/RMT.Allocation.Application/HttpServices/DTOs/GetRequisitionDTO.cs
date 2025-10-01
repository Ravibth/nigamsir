using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class GetRequisitionDTO
    {

        public string? pipelineCode { get; set; }
        public string? jobcode { get; set; }
        public string emailId { get; set; }
        //public string projectCode { get; set; }
    }

}