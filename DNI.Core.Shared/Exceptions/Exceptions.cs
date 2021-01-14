using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Exceptions
{
    public static class Exceptions
    {
        public static string EntityNotFound<T>(bool isMultiple = false) 
        { 
            var entityTypeName = typeof(T).Name;

            var entityTypeNameToUse = isMultiple 
                ? entityTypeName.Pluralize() 
                : entityTypeName.Singularize();

            return $"{entityTypeNameToUse} not found";
        }

        public static string DuplicateEntityFound<T>(bool isMultiple = false) 
        { 
            var entityTypeName = typeof(T).Name;

            var entityTypeNameToUse = isMultiple 
                ? entityTypeName.Pluralize() 
                : entityTypeName.Singularize();

            return $"{entityTypeNameToUse} already exists";
        }

        public static TException Create<TException>(string message)
            where TException : Exception
        {
            return Activator.CreateInstance(typeof(TException), message) as TException;
        }

        public static string EntityNotFound<T>(string details, bool isMultiple = false, params object[] args) 
            => $"{EntityNotFound<T>(isMultiple)}. {string.Format(details, args)}";

        public const string InvalidOperation = "The requested operation with specified parameters is invalid";
    }
}
