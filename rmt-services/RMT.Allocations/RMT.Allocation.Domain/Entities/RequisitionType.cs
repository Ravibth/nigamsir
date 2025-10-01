using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{

    public class RequisitionTypeData
    {
        public const string NamedAllocation = "Named Allocation";
        public const string SameTeamAllocation = "Same Team Allocation";
        public const string CreateRequisition = "Create Requisition";
        public const string RollForwardAllocation = "Roll Forward Allocation";
        public const string BulkAllocation = "Bulk Allocation";
        public const string BulkRequisition = "Bulk Requisition";
        public const int DefaultRequisitionTypeId = 3;
    }
    public class RequisitionType
    {
        [Key]
        public Int64 Id { get; set; }
        public string Type { get; set; }
    }
}
