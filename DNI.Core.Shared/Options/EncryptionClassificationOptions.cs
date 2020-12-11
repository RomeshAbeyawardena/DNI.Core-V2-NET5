using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Options
{
    public class EncryptionClassificationOptions
    {
        public EncryptionClassificationOptions(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            EncryptionClassifications = Switch.Create<EncryptionClassification, EncryptionOptions>();
        }

        public EncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<IServiceProvider, EncryptionOptions> options)
        {
            return Configure(encryptionClassification, (opt) => options(serviceProvider, opt));
        }

        public EncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<EncryptionOptions> options)
        {
            var encryptionOptions = new EncryptionOptions();
            options(encryptionOptions);
            EncryptionClassifications.Add(encryptionClassification, encryptionOptions);
            return this;
        }

        public ISwitch<EncryptionClassification, EncryptionOptions> EncryptionClassifications { get; }

        private readonly IServiceProvider serviceProvider;
    }
}
