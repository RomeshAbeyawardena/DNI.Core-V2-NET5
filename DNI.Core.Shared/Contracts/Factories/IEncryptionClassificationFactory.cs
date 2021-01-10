using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents a factory for getting encrpytion options for a specific encryption classification configured by the consumer
    /// </summary>
    public interface IEncryptionClassificationFactory
    {
        /// <summary>
        /// Gets the encryption options for a specific encryption classification
        /// </summary>
        /// <param name="encryptionClassification"></param>
        /// <returns></returns>
        EncryptionOptions GetEncryptionOptions(EncryptionClassification encryptionClassification);
    }
}
