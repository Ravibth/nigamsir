using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.AllocationService.DTOs
{
    public class UserInfoDTO
    {
        public int? id { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
        public string? emp_code { get; set; }
        public string? designation { get; set; }
        public string[]? roles { get; set; }
        public string? role { get; set; }

    }
}
