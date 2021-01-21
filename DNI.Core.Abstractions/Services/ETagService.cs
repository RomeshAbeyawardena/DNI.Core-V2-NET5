using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Meta;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
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
            return Generate(model, new ETagServiceOptions { 
                Separator = separator, 
                HashAlgorithmName = HashAlgorithmName.SHA512, 
                Encoding = encoding 
            });
        }

        public string Generate<T>(T model, string separator, Encoding encoding)
        {
            return Generate((object)model, separator, encoding);
        }

        public string Generate(object model, ETagServiceOptions options)
        {
            var separator = options.Separator;
            var hashAlgorithmName = options.HashAlgorithmName;
            var encoding = options.Encoding;

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

        public string Generate<T>(T model, ETagServiceOptions options)
        {
            return Generate((object)model, options);
        }

        public bool Validate<T>(T sourcemodel, T model, ETagServiceOptions options) where T : IETag
        {
            var modelETag = Generate(model, options);

            return sourcemodel.ETag == modelETag;
        }

        private readonly IHashServiceFactory hashServiceFactory;

    }
}
