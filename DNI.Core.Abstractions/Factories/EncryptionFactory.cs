using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.Options;

namespace DNI.Core.Abstractions.Factories
{
    internal class EncryptionFactory : IEncryptionFactory
    {
        public EncryptionFactory(IHashServiceFactory hashServiceFactory,
            IOptions<EncryptionOptions> options)
        {
            this.hashServiceFactory = hashServiceFactory;
            encryptionOptions = options?.Value;
        }

        public IEncryptionService GetEncryptionService(string algorithName)
        {
            if(encryptionOptions == null)
            {
                return new DefaultEncryptionService(hashServiceFactory, algorithName);
            }

            return new DefaultEncryptionService(hashServiceFactory, algorithName, encryptionOptions);
        }

        private readonly IHashServiceFactory hashServiceFactory;
        private readonly EncryptionOptions encryptionOptions;
    }
}
