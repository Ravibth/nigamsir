using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Util
{
    public class DateRange
    {
        private readonly DateTime startDate;
        private readonly DateTime endDate;
        public DateRange(DateTime startDate, DateTime endDate)
        {
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                yield return date;
            }
        }

       

        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
   public class Util
    {
        //public DateOnly getDateOnly(DateTime eDate)
        //{
        //    return eDate.Date;
        //}
    } 
}
