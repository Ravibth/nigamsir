using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WCGT.Domain.Entities;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTSectorIndustryBaseDTO
    {
        public string industry_id { get; set; }
        public string industry_name { get; set; }
        public string sub_industry_id { get; set; }
        public string sub_industry_name { get; set; }
        public bool isactive { get; set; }

    }
}
