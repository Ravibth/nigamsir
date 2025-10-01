using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SyncEventPayloadDTO
    {
        public string token { get; set; }
        public string action { get; set; }
        public string source_table_name { get; set; }
        public string source_table_row { get; set; }
        public string? destination_table_name { get; set; }
        public string? destination_table_row { get; set; }
    }
}
