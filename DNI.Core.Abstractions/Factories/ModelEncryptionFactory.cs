using DNI.Core.Shared.Contracts.Factories;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

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

        public void Encrypt<T>(ICollection<T> model)
        {
            model.ForEach(m => Encrypt(m));
        }

        public void Decrypt<T>(ICollection<T> model)
        {
            model.ForEach(m => Decrypt(m));
        }

        public void Encrypt<T, TSelector>(T model, Func<T, TSelector> propertySelector)
        {
            var propertyValue = propertySelector(model);
            Encrypt(propertyValue);
        }

        public void Encrypt<T, TSelector>(T model, Func<T, IEnumerable<TSelector>> propertySelector)
        {
            var propertyValue = propertySelector(model);
            Encrypt(propertyValue);
        }

        public void Encrypt<T, TSelector>(T model, Func<T, ICollection<TSelector>> propertySelector)
        {
            var propertyValue = propertySelector(model);
            Encrypt(propertyValue);
        }

        
        public void Decrypt<T, TSelector>(T model, Func<T, TSelector> propertySelector)
        {
            var propertyValue = propertySelector(model);
            Decrypt(propertyValue);
        }

        public void Decrypt<T, TSelector>(T model, Func<T, IEnumerable<TSelector>> propertySelector)
        {
            var propertyValue = propertySelector(model);
            Decrypt(propertyValue);
        }

        public void Decrypt<T, TSelector>(T model, Func<T, ICollection<TSelector>> propertySelector)
        {
            var propertyValue = propertySelector(model);
            Decrypt(propertyValue);
        }

        private readonly IServiceProvider serviceProvider;
    }
}
