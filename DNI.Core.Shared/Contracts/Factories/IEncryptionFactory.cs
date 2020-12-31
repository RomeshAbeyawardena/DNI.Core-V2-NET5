using DNI.Core.Shared.Contracts.Services;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents an implementation factory for getting the encryption service based on algorithm
    /// </summary>
    public interface IEncryptionFactory
    {
        /// <summary>
        /// Gets the encryption service to be used based on algorithm
        /// </summary>
        /// <param name="algorithName"></param>
        /// <returns>An instance of <see cref="IEncryptionService"/></returns>
        IEncryptionService GetEncryptionService(string algorithName);
    }
}
