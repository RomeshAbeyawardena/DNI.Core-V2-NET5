using DNI.Core.Shared.Contracts.Services;
using System.Security.Cryptography;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents a hash service factory for getting an <see cref="IHashService"/> based on algorithm
    /// </summary>
    public interface IHashServiceFactory
    {
        /// <summary>
        /// Gets a specific <see cref="IHashService"/>
        /// </summary>
        /// <param name="algorithmName"></param>
        /// <returns>An instance of <see cref="IHashService"/></returns>
        IHashService GetHashService(string algorithmName);

        /// <summary>
        /// Gets a specific <see cref="IHashService"/>
        /// </summary>
        /// <param name="hashAlgorithmName"></param>
        /// <returns>An instance of <see cref="IHashService"/></returns>
        IHashService GetHashService(HashAlgorithmName hashAlgorithmName);
    }
}
