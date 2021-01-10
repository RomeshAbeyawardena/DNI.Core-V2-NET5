using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using System.Security.Cryptography;

namespace DNI.Core.Abstractions.Factories
{
    internal class HashServiceFactory : IHashServiceFactory
    {
        public IHashService GetHashService(string algorithmName)
        {
            return new DefaultHashService(algorithmName);
        }

        public IHashService GetHashService(HashAlgorithmName hashAlgorithmName)
        {
            return new DefaultHashService(hashAlgorithmName);
        }
    }
}