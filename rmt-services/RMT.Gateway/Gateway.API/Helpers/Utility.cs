// <copyright file="Utility.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Gateway.API.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// Utility Class.
    /// </summary>
    public static class Utility
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
                    query.Add(item.Key, item.Value);
                }
            }

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            return url;
        }

        /// <summary>
        /// CheckIsValidJsonToken
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckIsValidJsonToken(string? str)
        {
            bool flag = false;
            if (CheckIsValidJsonArray(str) || CheckIsValidJsonObject(str))
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// CheckIsValidJsonObject
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckIsValidJsonObject(string? str)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(str) && str.Trim().StartsWith("{") && str.Trim().EndsWith("}"))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// CheckIsValidJsonArray
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckIsValidJsonArray(string? str)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(str) && str.Trim().StartsWith("[") && str.Trim().EndsWith("]"))
            {
                flag = true;
            }
            return flag;
        }
    }
}
