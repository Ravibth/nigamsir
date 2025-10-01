using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Utils
{
    public interface IDatesUtils
    {
        Task<int> GetNumberOfWeekends(DateTime? start_date, DateTime? end_date);
    }
}
