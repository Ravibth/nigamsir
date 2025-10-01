using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.Entities;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTPipelineRoleBaseDTO
    {
        public string user_mid { get; set; }
        public string user_empname { get; set; }
        public string user_emailid { get; set; }
        public string user_role { get; set; }
        public Boolean isactive { get; set; }
    }
}
