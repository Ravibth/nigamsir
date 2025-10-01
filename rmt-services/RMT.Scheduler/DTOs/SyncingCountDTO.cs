using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class SyncingCountDTO
    {
        public int TotalRecords { get; set; } = 0;
        public int ValidRecords { get; set; } = 0;
        public int InValidRecords { get; set; } = 0;
    }
}
