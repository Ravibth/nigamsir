using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Helpers
{
    public static class Helper
    {
        /// <summary>
        /// SanitizeInputData
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SanitizeInputData(string? str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string strEncode = HtmlEncoder.Default.Encode(str);
                return strEncode;
            }
            else
            {
                return str;
            }
        }
    }
}
