using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;

namespace DNI.Core.Abstractions.Factories
{
    internal class EncryptionClassificationFactory : IEncryptionClassificationFactory
    {
        public EncryptionClassificationFactory(ISwitch<EncryptionClassification, EncryptionOptions> encryptionClassificationSwitch)
        {
            this.encryptionClassificationSwitch = encryptionClassificationSwitch;
        }

        public EncryptionOptions GetEncryptionOptions(EncryptionClassification encryptionClassification)
        {
            if(encryptionClassificationSwitch.TryGetValue(encryptionClassification, out var encryptionOptions))
            {
                return encryptionOptions;
            }

            return default;
        }

        private readonly ISwitch<EncryptionClassification, EncryptionOptions> encryptionClassificationSwitch;
    }
}
