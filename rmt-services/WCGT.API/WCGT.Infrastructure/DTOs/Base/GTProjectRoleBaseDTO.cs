using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    //public enum ProjectUserRole
    //{
    //    enquiry_owner=0,
    //    finding_partner2=1,
    //    finding_partner3 = 2,

    //}
    public class GTProjectRoleBaseDTO
    {
        public string? user_email { get; set; }
        public string? user_mid { get; set; }
        public string? user_empname { get; set; }
        public string? user_role { get; set; }
    }
}
