using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Extensions;
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

        public void Encrypt<T>(T model)
        {
            GetModelEncryptionService<T>()
                .Encrypt(model);
        }

        public void Decrypt<T>(T model)
        {
            GetModelEncryptionService<T>()
                .Decrypt(model);
        }

        public void Encrypt<T>(IEnumerable<T> model)
        {
            model.ForEach(m => Encrypt(m));
        }

        public void Decrypt<T>(IEnumerable<T> model)
        {
            model.ForEach(m => Decrypt(m));
        }

        private readonly IServiceProvider serviceProvider;
    }
}
