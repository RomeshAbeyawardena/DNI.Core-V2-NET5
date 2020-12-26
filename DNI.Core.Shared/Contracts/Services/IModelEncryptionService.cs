namespace DNI.Core.Shared.Contracts.Services
{
    public interface IModelEncryptionService<T> 
    {
        void Encrypt(T model);
        void Decrypt(T model);
    }
}
