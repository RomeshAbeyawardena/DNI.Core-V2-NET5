using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class ModelEncryptionServiceExtensions
    {
        public static string Encrypt<T>(
            this IModelEncryptionService<T> modelEncryptionService, 
            string value,
            Expression<Func<T, string>> modelKeySelector,
            params object[] args)
        {
            var instance = CreateInstance(value,
                modelKeySelector,
                out var property,
                args);

            modelEncryptionService.Encrypt(instance);

            return (string)property.GetValue(instance);
        }

        public static string Decrypt<T>(
            this IModelEncryptionService<T> modelEncryptionService, 
            string value,
            Expression<Func<T, string>> modelKeySelector,
            params object[] args)
        {
            var instance = CreateInstance(value,
                modelKeySelector,
                out var property,
                args);

            modelEncryptionService.Decrypt(instance);

            return (string)property.GetValue(instance);
        }

        private static T CreateInstance<T>(
            string value,
            Expression<Func<T, string>> modelKeySelector,
            out PropertyInfo property,
            params object[] args)
        {
            var instance = Activator.CreateInstance(typeof(T), args);
            property = modelKeySelector.GetProperty();
            property.SetValue(instance, value);
            return (T)instance;
        }
    }
}
