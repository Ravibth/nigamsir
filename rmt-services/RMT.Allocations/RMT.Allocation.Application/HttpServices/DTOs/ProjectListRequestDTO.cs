using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class ProjectListRequestDTO
    {
        public string userEmail { get; set; }
        public int limit { get; set; }
        public int pagination { get; set; }
        public string? searchQuery { get; set; }
        public List<string>? searchRoles { get; set; }
        public string? orderBy { get; set; }
        public FilterQueryParameters filterQueryParameters { get; set; }
    }

    public class FilterQueryParameters
    {
        public List<string>? Bu { get; set; }
        public List<string>? Expertises { get; set; }//Recheck
        public List<string>? Smes { get; set; }//Recheck
        public List<string>? Smegs { get; set; }//Recheck

        public List<string>? Offerings { get; set; }
        public List<string>? Solutions { get; set; }

        public List<string>? Industry { get; set; }
        public List<string>? SubIndustry { get; set; }
        public List<string>? ClientNames { get; set; }
        public List<string>? PipelineCodes { get; set; }
        public List<string>? JobCodes { get; set; }
        public List<string>? JobName { get; set; }
        public List<string>? ProjectStatus { get; set; }
        public List<string>? RevenueUnit { get; set; }//Recheck
        public string? ProjectType { get; set; }
        public bool? MarketPlace { get; set; }
        public string? ProjectChargeType { get; set; }
        public bool? IsAllocatedHoursRequired { get; set; }

    }

    public class ProjectRequisitionAllocationRequestDTO
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public int requisitionCountAdded { get; set; }
        public int allocationCountAdded { get; set; }
    }
}
