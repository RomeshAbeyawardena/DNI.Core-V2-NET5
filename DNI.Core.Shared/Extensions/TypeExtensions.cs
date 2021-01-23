using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetImplementatioType(Type interfaceType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new InvalidOperationException();
            }

            return interfaceType.Assembly
                .GetTypes().Where(a => a.GetInterfaces().Any(a => a == interfaceType));
        }

        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return type.GetProperties()
                .Where(property => property
                    .GetCustomAttribute<TAttribute>() != null);
        }
    }
}
