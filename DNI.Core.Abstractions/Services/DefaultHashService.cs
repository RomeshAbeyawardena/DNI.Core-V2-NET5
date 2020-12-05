using DNI.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace DNI.Core.Abstractions.Services
{
    internal class DefaultHashService : HashServiceBase
    {
        public DefaultHashService(string algorithmName) 
            : base(algorithmName)
        {
        }

        public DefaultHashService(HashAlgorithmName hashAlgorithmName)
            : base(hashAlgorithmName.Name)
        {

        }

        public override string HashString(string value, Encoding encoding)
        {
            return Convert
                .ToBase64String(HashAlgorithm
                    .ComputeHash(encoding.GetBytes(value)));
        }

        private HashAlgorithm HashAlgorithm => HashAlgorithm.Create(AlgorithmName);
    }
}
