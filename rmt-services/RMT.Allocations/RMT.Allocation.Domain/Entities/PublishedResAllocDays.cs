using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RMT.Allocation.Domain.Entities
{
    public class PublishedResAllocDays
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid PublishedResAllocId { get; set; }
        public Int64 Efforts { get; set; }
        public string EmailId { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? JobId { get; set; }
        public double RatePerHour { get; set; }
        [DefaultValue(ConstantsDomain.RUPEES)]
        public string? Currency { get; set; }
        public decimal? ActualEffort { get; set; }
        public DateOnly AllocationDate { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
        [ForeignKey("PublishedResAllocId")]
        public virtual PublishedResAlloc ResAlloc { get; set; }
    }
}
