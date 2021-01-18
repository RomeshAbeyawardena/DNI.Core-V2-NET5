using System;

namespace DNI.Core.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsDefault(this object value)
        {
            if(value == null || value == default)
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
        
        public static T Clone<T>(this T value, params object[] arguments)
        {
            return (T)Clone(value as object, arguments);
        }

        public static object Clone(this object source, params object[] arguments)
        {
            var cloneType = source.GetType();
            var instance = Activator.CreateInstance(cloneType, arguments);

            foreach (var property in cloneType.GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(source);

                    if(value != null)
                    {
                        property.SetValue(instance, value);
                    }
                }
            }

            return instance;
        }
    }
}
