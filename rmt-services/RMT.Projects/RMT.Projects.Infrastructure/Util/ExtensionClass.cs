using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Infrastructure.Util
{
    public static class ExtensionClass
    {
        public static string? TrimLower(this string? str)
        {
            try
            {
                return string.IsNullOrEmpty(str) ? string.Empty : str.Trim().ToLower();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
