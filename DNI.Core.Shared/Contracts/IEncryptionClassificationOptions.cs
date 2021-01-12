using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;
using System;

namespace DNI.Core.Shared.Contracts
{
    public interface IEncryptionClassificationOptions
    {
        IEncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<IServiceProvider, EncryptionOptions> options);

        IEncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<EncryptionOptions> options);

        ISwitch<EncryptionClassification, EncryptionOptions> EncryptionClassifications { get; }
    }
}
