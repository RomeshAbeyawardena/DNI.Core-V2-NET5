using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsDefault(this object value)
        {
            if( value == default)
            {
                return true;
            }

            if(value is int intValue && intValue == default)
            {
                return true;
            }

            if(value is long longValue && longValue == default)
            {
                return true;
            }

            if(value is decimal decimalValue && decimalValue == default)
            {
                return true;
            }

            if(value is DateTimeOffset dateTimeOffset && dateTimeOffset == default)
            {
                return true;
            }

            if(value is DateTime dateTime && dateTime == default)
            {
                return true;
            }

            
            if(value is float floatValue && floatValue == default)
            {
                return true;
            }

            return false;
        }
    }
}
