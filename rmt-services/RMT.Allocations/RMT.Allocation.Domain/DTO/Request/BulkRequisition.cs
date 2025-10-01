using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO.Request
{
    public class BulkRequisition : RequisitionRequest
    {
        public DateTime? projectStartDate { get; set; }
        public DateTime? projectEndDate { get; set; }
        public string? skills { get; set; }
        public string? locations { get; set; }
        public string? Industry { get; set; }
        public string? SubIndustry { get; set; }
        public string Competency { get; set; }
        public string CompetencyId { get; set; }
        public List<ParametersEntities> parameters { get; set; }
        public string? EmailId { get; set; }
        public string? EmpName { get; set; }
        public Boolean? isUploaded { get; set; }
        public string? errorMsg { get; set; }
        public long? totalNumberRequisition { get; set; }
        public List<SkillCodeNameDTO>? SkillList { get; set; }
    }

    public class BulkRequistionResponse
    {
        public List<BulkRequisition> bulkRequisition { get; set; }
        public long? totalNumberRequisition { get; set; }
    }
}
