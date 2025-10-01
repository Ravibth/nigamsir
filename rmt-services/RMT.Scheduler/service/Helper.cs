using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RMT.Scheduler.service
{
    public static class Helper
    {
        public static string UrlBuilderByQuery(string baseUrl, Dictionary<string, dynamic> queries)
        {
            var urlBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            foreach (var item in queries)
            {
                if (item.Value is string)
                {
                    query.Add(item.Key, HttpUtility.UrlEncode(item.Value));
                }
                else if (item.Value is List<string>)
                {
                    foreach (var item1 in item.Value)
                    {
                        query.Add(item.Key, HttpUtility.UrlEncode(item1));
                    }
                }
                else if (item.Value is null)
                {
                    query.Add(item.Key, "null");
                }
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }
    }
}
