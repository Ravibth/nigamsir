using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Responses
{
    public class BudgetResponse : GTBudgetDTO
    {
        public Boolean isfailed { get; set; }
        public string? failed_message { get; set; }
    }
}
