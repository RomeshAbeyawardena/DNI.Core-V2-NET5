using DNI.Core.Shared.Contracts.Services;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IEncryptionFactory
    {
        IEncryptionService GetEncryptionService(string algorithName);
    }
}
