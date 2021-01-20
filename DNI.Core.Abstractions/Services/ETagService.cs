using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Services
{
    public class ETagService : IETagService
    {
        public ETagService(IHashServiceFactory hashServiceFactory)
        {
            this.hashServiceFactory = hashServiceFactory;
        }

        public string Generate(object model, string separator, Encoding encoding)
        {
            return Generate(model, separator, HashAlgorithmName.SHA512, encoding);
        }

        public string Generate<T>(T model, string separator, Encoding encoding)
        {
            return Generate((object)model, separator, encoding);
        }

        public string Generate(object model, string separator, HashAlgorithmName hashAlgorithmName, Encoding encoding)
        {
            var modelType = model.GetType();
            var stringBuilder = new StringBuilder();
            foreach (var property in modelType.GetProperties())
            {
                var propertyType = property.PropertyType;
                var value = property.GetValue(model);

                if (value == null)
                {
                    continue;
                }

                if (propertyType == typeof(string)
                    || propertyType.IsPrimitive
                    || propertyType.IsValueType)
                {
                    stringBuilder.AppendFormat("{0}{1}", value, separator);
                }
                else
                {
                    stringBuilder.Append(Generate(value, separator, encoding));
                }
            }

            var hashService = hashServiceFactory.GetHashService(hashAlgorithmName);

            return hashService
                .HashString(stringBuilder.ToString().TrimEnd(separator.ToCharArray()), encoding);
        }

        public string Generate<T>(T model, string separator, HashAlgorithmName hashAlgorithmName, Encoding encoding)
        {
            return Generate((object)model, separator, hashAlgorithmName, encoding);
        }

        private readonly IHashServiceFactory hashServiceFactory;

    }
}
