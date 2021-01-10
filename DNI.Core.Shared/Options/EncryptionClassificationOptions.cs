using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System;

namespace DNI.Core.Shared.Options
{
    internal class EncryptionClassificationOptions : IEncryptionClassificationOptions
    {
        public static IEncryptionClassificationOptions Create(IServiceProvider serviceProvider)
        {
            return new EncryptionClassificationOptions(serviceProvider);
        }

        public IEncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<IServiceProvider, EncryptionOptions> options)
        {
            return Configure(encryptionClassification, (opt) => options(serviceProvider, opt));
        }

        public IEncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<EncryptionOptions> options)
        {
            var encryptionOptions = new EncryptionOptions();
            options(encryptionOptions);
            EncryptionClassifications.Add(encryptionClassification, encryptionOptions);
            return this;
        }

        public ISwitch<EncryptionClassification, EncryptionOptions> EncryptionClassifications { get; }

        private EncryptionClassificationOptions(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            EncryptionClassifications = Switch.Create<EncryptionClassification, EncryptionOptions>();
        }

        private readonly IServiceProvider serviceProvider;
    }
}
