namespace DNI.Core.Shared.Contracts.Services
{
    /// <summary>
    /// Represents a model encryption service to encrypt and decrypt a model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IModelEncryptionService<T> 
    {
        /// <summary>
        /// Encrypts a model in the <paramref name="model"/> instance
        /// </summary>
        /// <param name="model"></param>
        void Encrypt(T model);

        /// <summary>
        /// Decrypts a model in the <paramref name="model"/> instance
        /// </summary>
        /// <param name="model"></param>
        void Decrypt(T model);

        /// <summary>
        /// Decrypts a model in a new instance of <typeparamref name="T"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        T DecryptAsClone(T model, params object[] arguments);

        /// <summary>
        /// Encrypts a model in a new instance of <typeparamref name="T"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        T EncryptAsClone(T model, params object[] arguments);
    }
}
