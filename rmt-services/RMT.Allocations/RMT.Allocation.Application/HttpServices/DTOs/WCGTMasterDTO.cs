using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class WCGTDesigantionDTO
    {
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
        public string? designation_id { get; set; }
        public string? designation_name { get; set; }
        public string? grade { get; set; }
        public string? description { get; set; }
        public bool? isactive { get; set; }
    }

    public class WCGTMasterDataDTO
    {
        public string bu { get; set; }
        public string bu_id { get; set; }
        public string offering { get; set; }
        public string offering_id { get; set; }
        public string? offering_leader_mid { get; set; }

        public string solution { get; set; }
        public string solution_id { get; set; }
        public string? solution_leader_mid { get; set; }
        public bool isactive { get; set; }
        public string? bu_leader_mid { get; set; }
        public string? bu_efficiency_leader_mid { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }

    public class WCGTLocationDTO
    {
        public string? location_id { get; set; }
        public string? location_mid { get; set; }
        public string? location_name { get; set; }
        public bool? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }

    public class WCGTIndustryDTO
    {
        public string? industry_id { get; set; }
        public string? industry_name { get; set; }
        public string? sub_industry_id { get; set; }
        public string? sub_industry_name { get; set; }
        public bool? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }


}
