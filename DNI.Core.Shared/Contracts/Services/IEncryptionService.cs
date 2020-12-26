using DNI.Core.Shared.Options;

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
