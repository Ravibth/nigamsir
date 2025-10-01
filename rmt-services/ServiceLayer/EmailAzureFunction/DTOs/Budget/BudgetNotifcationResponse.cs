using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.Budget
{
    public class BudgetNotifcationResponse
    {
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public Double Limit { get; set; }
    }
}
