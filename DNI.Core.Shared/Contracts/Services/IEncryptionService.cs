using DNI.Core.Shared.Options;

namespace DNI.Core.Shared.Contracts.Services
{
    public interface IEncryptionService
    {
        /// <summary>
        /// The implemented encryption method to encrypt and decrypt values
        /// </summary>
        string EncryptionMethod { get; }

        /// <summary>
        /// The encryption keys used to encrpyt and decrypt values
        /// </summary>
        EncryptionOptions EncryptionOptions { get; }

        /// <summary>
        /// Encrypts a value using the default or specified <see cref="Options.EncryptionOptions"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encryptionOptions"></param>
        /// <returns></returns>
        string Encrypt(string value, EncryptionOptions encryptionOptions = default);
        /// <summary>
        /// Decrypts a value using the default or specified <see cref="Options.EncryptionOptions"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encryptionOptions"></param>
        /// <returns></returns>
        string Decrypt(string value, EncryptionOptions encryptionOptions = default);
    }
}
