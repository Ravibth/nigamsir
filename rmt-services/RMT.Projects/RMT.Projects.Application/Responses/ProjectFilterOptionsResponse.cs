using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Responses
{
    public class ProjectFilterOptionsResponse
    {
        public List<string>? DistinctClientNames { get; set; }
        public List<string>? DistinctBUSet { get; set; }
        public List<Expertises> DistinctExpertises { get; set; }//Recheck
        public List<Smes>? DistinctSmes { get; set; }//Recheck
        public List<Smegs>? DistinctSmegs { get; set; }//Recheck
        public List<Offerings> DistinctOfferings { get; set; }
        public List<Solutions> DistinctSolutions { get; set; }

        public List<string>? DistinctPipelines { get; set; }
        public List<string>? DistinctJobs { get; set; }
        public List<string>? DistinctJobNames { get; set; }
        public List<string>? Status { get; set; }
        public List<string>? ProjectType { get; set; }
        public List<string>? MarketPlaceType { get; set; }
        public List<string>? DistinctIndustry { get; set; }
        public List<SubIndustry>? DistinctSubIndustry { get; set; }
        public List<RevenueUnit>? RevenueUnit { get; set; }//Recheck
    }
    
    public class Offerings
    {
        public string BU { get; set; }
        public string Name { get; set; }
    }

    public class Solutions
    {
        public string Offerings { get; set; }
        public string Name { get; set; }
    }

    public class Expertises//Recheck
    {
        public string  BU { get; set; }
        public string Name { get; set; }
    }
    public class Smegs//Recheck
    {
        public string Expertise { get; set; }
        public string Name { get; set; }       
    }

    public class RevenueUnit//Recheck
    {
        public string Expertise { get; set; }
        public string Name { get; set; }
    }
    public class Smes//Recheck
    {
        public string Expertise { get; set; }
        public string Name { get; set; }       
    }

    public class SubIndustry
    {
        public string Industry { get; set; }
        public string Name { get; set; }
    }

}
