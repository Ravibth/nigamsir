using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class AllocationCommonView
    {        
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid UnPublishedResAllocId { get; set; }
        public string EmailId { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public DateOnly AllocationDate { get; set; }
        public Int64 Efforts { get; set; }      
        public double RatePerHour { get; set; }
        [DefaultValue(ConstantsDomain.RUPEES)]
        public string? Currency { get; set; } 
        //[ForeignKey("UnPublishedResAllocId")]
        //public virtual UnPublishedResAlloc UnPublishedResAlloc { get; set; }
        
        public string? Type {  get; set; }
        public string? AllocationStatus {  get; set; }
    }
}
