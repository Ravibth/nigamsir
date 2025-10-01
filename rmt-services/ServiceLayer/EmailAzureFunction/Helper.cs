using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ServiceLayer
{
    public static class Helper
    {
        public static string GetClickableLink(string url, string placeholder)
        {
            return $"<a href='{url}' alt={placeholder}>{placeholder}</a>";
        }

        public static List<string> ExtractValuesByRegex(string input)
        {
            // Define the regular expression pattern
            string pattern = @"<([^>]+)>";

            // Match the pattern in the input string
            MatchCollection matches = Regex.Matches(input, pattern);

            // Extract values from matches
            List<string> extractedValues = new List<string>();
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    extractedValues.Add(match.Groups[1].Value);
                }
            }

            return extractedValues;
        }

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
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }

        public static ServiceBusClient CreateServiceBusClient(ILogger logger)
        {
            var fullyQualifiedNamespace = Environment.GetEnvironmentVariable("AzureServiceBus__fullyQualifiedNamespace");
            var clientId = Environment.GetEnvironmentVariable("SBClientId");
            var tenantId = Environment.GetEnvironmentVariable("SBTenantId");
            var clientSecret = Environment.GetEnvironmentVariable("SBClientSecret");

            logger.LogInformation("--ServiceBus---URL---{0}", fullyQualifiedNamespace);
            logger.LogInformation("--ServiceBus---ClientId---{0}", clientId);

            var tokenCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            return new ServiceBusClient(fullyQualifiedNamespace, tokenCredential);
        }

    }
}

