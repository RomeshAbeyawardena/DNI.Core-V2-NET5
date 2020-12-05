using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Factories
{
    internal class EncryptionFactory : IEncryptionFactory
    {
        public EncryptionFactory(IOptions<EncryptionOptions> options)
        {
            encryptionOptions = options?.Value;
        }

        public IEncryptionService GetEncryptionService(string algorithName)
        {
            if(encryptionOptions == null)
            {
                return new DefaultEncryptionService(algorithName);
            }

            return new DefaultEncryptionService(algorithName, encryptionOptions);
        }

        private readonly EncryptionOptions encryptionOptions;
    }
}
