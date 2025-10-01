using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RMT.Scheduler.Helper
{
    public class Helper
    {
        public static string QueryStringBuilderByRegex(string sqlQuery, DataRow dataRow)
        {
            string pattern = @"\{\{(.+?)\}\}";
            MatchCollection matchCollection = Regex.Matches(sqlQuery, pattern);
            List<string> dataList = new List<string>();
            foreach (Match match in matchCollection)
            {
                string data = match.Groups[1].Value;
                string data1 = data.Split("=")[1].Trim();

                if (!dataList.Contains(data1))
                {
                    dataList.Add(data1);
                }
                string replaceString = " {{" + data + "}}";
                string replaceValue = string.Empty;
                if (dataRow.IsNull(data1))
                {
                    replaceValue = " IS NULL ";
                }
                else
                {
                    replaceValue = " = " + "\'" + ReplaceInvalidChar(Convert.ToString(dataRow[data1])) + "\'";
                }
                sqlQuery = sqlQuery.Replace(replaceString, replaceValue);

            }
            //foreach (string data in dataList)
            //{
            //    //project_code , job_code
            //    //= project_code
            //    string replaceString = " {{" + data + "}}";
            //    string replaceValue = "\'" + Convert.ToString(dataRow[data]) + "\'";
            //    if (dataRow.IsNull(data))
            //    {
            //        replaceValue = " NULL ";
            //    }
            //    sqlQuery = sqlQuery.Replace(replaceString , replaceValue);
            //}
            return sqlQuery;
        }

        public static string ReplaceInvalidChar(object obj)
        {
            string val = Convert.ToString(obj);

            if (!string.IsNullOrEmpty(val))
            {
                val = Convert.ToString(obj).Replace("'", "''");
            }
            return val;
        }

    }
}
