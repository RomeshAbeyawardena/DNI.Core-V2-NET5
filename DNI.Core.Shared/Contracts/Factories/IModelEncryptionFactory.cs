using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IModelEncryptionFactory
    {
        IModelEncryptionService<T> GetModelEncryptionService<T>();
        void Encrypt<T>(T model);
        void Decrypt<T>(T model);
        void Encrypt<T>(IEnumerable<T> model);
        void Decrypt<T>(IEnumerable<T> model);

        void Encrypt<T, TSelector>(T model, Func<T, TSelector> propertySelector);
        void Encrypt<T, TSelector>(T model, Func<T, IEnumerable<TSelector>> propertySelector);
        void Encrypt<T, TSelector>(T model, Func<T, ICollection<TSelector>> propertySelector);
        void Decrypt<T, TSelector>(T model, Func<T, TSelector> propertySelector);
        void Decrypt<T, TSelector>(T model, Func<T, IEnumerable<TSelector>> propertySelector);
        void Decrypt<T, TSelector>(T model, Func<T, ICollection<TSelector>> propertySelector);
    }
}
