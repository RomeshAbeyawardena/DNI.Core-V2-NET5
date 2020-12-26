using DNI.Core.Shared.Contracts.Services;
using System.Security.Cryptography;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IHashServiceFactory
    {
        IHashService GetHashService(string algorithmName);
        IHashService GetHashService(HashAlgorithmName hashAlgorithmName);
    }
}
