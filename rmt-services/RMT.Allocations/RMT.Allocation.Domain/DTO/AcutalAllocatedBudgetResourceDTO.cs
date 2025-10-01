using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class AcutalAllocatedBudgetResourceDTO 
    {
        public Double? allocationtotaltime { get; set; }
        public Double? allocationtotalcost { get; set; }
        public string empName { get; set; }
        public Double? timesheettotaltime { get; set; }
        public Double? timesheettotalCost { get; set; }
        public IdentityUserResponseDTO identityUserResponse { get; set; }   
    }
}
