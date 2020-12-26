using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IEncryptionClassificationFactory
    {
        EncryptionOptions GetEncryptionOptions(EncryptionClassification encryptionClassification);
    }
}
