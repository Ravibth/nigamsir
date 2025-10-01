using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper.DTOs
{
    public class UpdateUserDto
    {
        public string[]? roles { get; set; }
        public string? role_ids { get; set; }
        public string? emp_code { get; set; }
        public bool? status { get; set; }
    }
    public class UpdateUserRequestQueryDTO
    {
        public string emailId { get; set; }
    }
}
