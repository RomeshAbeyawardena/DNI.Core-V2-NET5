using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceAll(this string value, string newValue, params string[] oldStrings)
        {
            return ReplaceAll(value, oldStrings, newValue);
        }

        public static string ReplaceAll(this string value, IEnumerable<string> oldStrings, string newValue)
        {
            foreach (var oldString in oldStrings)
            {
                value = value.Replace(oldString, newValue);
            }

            return value;
        }
    }
}
