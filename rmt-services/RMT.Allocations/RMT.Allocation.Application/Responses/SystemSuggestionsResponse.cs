using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Responses
{
    public class SystemSuggestionsResponse
    {
        public string empName { get; set; }
        public string email { get; set; }
        public string designation { get; set; }
        public string location { get; set; }
        public string expertise { get; set; }//Recheck
        public string sme { get; set; }//Recheck

        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string OfferingsId { get; set; }
        public string SolutionsId { get; set; }

        public string supercoach { get; set; }
        public string score { get; set; }
        public object scoreBreakup { get; set; }
        public string allocations { get; set; }

    }
}
