using DNI.Core.Abstractions.Services;
using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Factories
{
    internal class HashServiceFactory : IHashServiceFactory
    {
        public IHashService GetHashService(string algorithmName)
        {
            return new DefaultHashService(algorithmName);
        }
    }
}