namespace RMT.Allocation.Application.Utils
{
    public class DatesUtils : IDatesUtils
    {
        public DatesUtils() { }

        public string[] weekendsDefined = new string[] { DayOfWeek.Saturday.ToString(), DayOfWeek.Sunday.ToString() };

        public async Task<int> GetNumberOfWeekends(DateTime? start_date, DateTime? end_date)
        {
            var weekends = 0;
            if (start_date != null && end_date != null)
            {
                TimeSpan span = (DateTime)end_date - (DateTime)start_date;

            for (int i = 0; i < span.Days; i++)
            {
                    DateTime currentDate = ((DateTime)start_date).AddDays(i);
                //if (currentDate.DayOfWeek == DayOfWeek.Sunday || currentDate.DayOfWeek == DayOfWeek.Saturday)
                if (weekendsDefined.Any(m => m == currentDate.DayOfWeek.ToString()))
                {
                    weekends++;
                }
            }
            }
            return weekends;
        }
    }
}
