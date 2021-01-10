using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IEncryptionClassificationOptions
    {
        IEncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<IServiceProvider, EncryptionOptions> options);

        IEncryptionClassificationOptions Configure(EncryptionClassification encryptionClassification, Action<EncryptionOptions> options);

        ISwitch<EncryptionClassification, EncryptionOptions> EncryptionClassifications { get; }
    }
}
