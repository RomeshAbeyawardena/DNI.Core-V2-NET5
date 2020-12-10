using DNI.Core.Shared.Enumerations;
using DNI.Core.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IEncryptionClassificationFactory
    {
        EncryptionOptions GetEncryptionOptions(EncryptionClassification encryptionClassification);
    }
}
