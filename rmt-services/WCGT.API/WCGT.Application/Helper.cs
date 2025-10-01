using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WCGT.Application.Services.IHttpServices;

namespace WCGT.Application
{
    public class Helper
    {
        public static string UrlBuilderByQuery(string baseUrl, Dictionary<string, dynamic> queries)
        {
            var urlBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            foreach (var item in queries)
            {
                if (item.Value is string)
                {
                    query.Add(item.Key, item.Value);
                }
                else if (item.Value is List<string>)
                {
                    foreach (var item1 in item.Value)
                    {
                        query.Add(item.Key, item1);
                    }
                }
                else if (item.Value is null)
                {
                    query.Add(item.Key, "null");
                }
                else
                {
                    query.Add(item.Key, item.Value);
                }
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }

        public static List<(DateOnly Start, DateOnly End)> GetWeeklyRanges(DateTime startDate, DateTime endDate)
        {
            var weeks = new List<(DateOnly Start, DateOnly End)>();
            var currentStart = DateOnly.FromDateTime(startDate.Date);

            var finalEnd = DateOnly.FromDateTime(endDate.Date);

            while (currentStart <= finalEnd)
            {
                var currentEnd = currentStart.AddDays(6);
                if (currentEnd > finalEnd)
                    currentEnd = finalEnd;

                weeks.Add((currentStart, currentEnd));
                currentStart = currentEnd.AddDays(1);
            }

            return weeks;
        }

        
        public static async Task<List<string>> GetSuperCoachMid(string DelegateMid, IIdentityHttpService _identityHttpService)
        {            
            var result =  await _identityHttpService.GetSuperCoachMid(DelegateMid);

            return result.Select(x => x.supercoach_mid).ToList();                        
        }
    }
}
