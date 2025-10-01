using RMT.Reports.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RMT.Reports.Application
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
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }

        public static async Task<List<string>> GetSuperCoachMid(string DelegateMid, IIdentityHttpService _identityHttpService)
        {
            var result = await _identityHttpService.GetSuperCoachMid(DelegateMid);

            return result.Select(x => x.supercoach_mid).ToList();
        }
    }
}
