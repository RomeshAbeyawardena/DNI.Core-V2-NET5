namespace DNI.Core.Shared.Contracts.Services
{
    /// <summary>
    /// Represents a model encryption service to encrypt and decrypt a model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IModelEncryptionService<T> 
    {
        /// <summary>
        /// Encrypts a model
        /// </summary>
        /// <param name="model"></param>
        void Encrypt(T model);

        /// <summary>
        /// Decrypts a model
        /// </summary>
        /// <param name="model"></param>
        void Decrypt(T model);
    }
}
