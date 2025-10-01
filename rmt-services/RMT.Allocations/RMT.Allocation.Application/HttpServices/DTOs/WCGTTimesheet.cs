using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class WCGTTimesheet
    {
        public Int64? id { get; set; }

        public string employeecode { get; set; }
        public string employeename { get; set; }
        public DateOnly datelog { get; set; }

        public Double totaltime { get; set; }
        public string client { get; set; }
        public string jobcode { get; set; }
        public string status { get; set; }
        public string designation { get; set; }
        public string gradename { get; set; }
        public string chargeableflag { get; set; }
        public double rate { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
