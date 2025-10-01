using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RMT.MarketPlace.Application
{
    public class Helper
    {
        public static string UrlBuilderByQuery(string baseUrl, Dictionary<string, dynamic> queries)
        {
            var urlBuilder = new UriBuilder(baseUrl);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            //query.
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
                    query.Add(item.Key, Convert.ToString(item));
                }
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }
    }
}
