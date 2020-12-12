using DNI.Core.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IEncryptionService
    {
        string EncryptionMethod { get; }
        EncryptionOptions EncryptionOptions { get; }
        string Encrypt(string value, EncryptionOptions encryptionOptions = default);
        string Decrypt(string value, EncryptionOptions encryptionOptions = default);
    }
}
