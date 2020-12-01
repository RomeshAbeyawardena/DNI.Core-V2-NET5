using System;
using System.Reflection;
using DNI.Core.Shared.Contracts;

namespace DNI.Core.Shared.Extensions
{
    public static class DefinitionExtensions
    {
        public static IDefinition<Type> GetType<T>(this IDefinition<Type> definition)
        {
            return definition.Add(typeof(T));
        }   

        public static IDefinition<Assembly> GetAssembly(this IDefinition<Assembly> assemblies, Type type)
        {
            return assemblies.Add(Assembly.GetAssembly(type));
        }

        public static IDefinition<Assembly> GetAssembly<T>(this IDefinition<Assembly> assemblies)
        {
            return assemblies.GetAssembly(typeof(T));
        }
    }
}
