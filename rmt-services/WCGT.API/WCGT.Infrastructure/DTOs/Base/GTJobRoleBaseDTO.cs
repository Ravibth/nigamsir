using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTJobRoleBaseDTO
    {
        public string user_mid { get; set; }
        public string user_empname { get; set; }
        public string user_emailid { get; set; }
        public string user_role { get; set; }
        public Boolean isactive { get; set; }
    }
}
