using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Factories
{
    internal class ModelEncryptionFactory : IModelEncryptionFactory
    {
        public ModelEncryptionFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IModelEncryptionService<T> GetModelEncryptionService<T>()
        {
            return serviceProvider.GetRequiredService<IModelEncryptionService<T>>();
        }

        private readonly IServiceProvider serviceProvider;
    }
}
