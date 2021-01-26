using DNI.Core.Shared.Extensions;
using DNI.Core.Shared.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DNI.Core.Abstractions
{
    public abstract class LookupProviderBase
    {
        public LookupProviderBase(IModelEncryptionFactory modelEncryptionFactory)
        {
            this.modelEncryptionFactory = modelEncryptionFactory;
        }

        protected T GetResult<TKey, T>(TKey value,
            Expression<Func<T, TKey>> getProperty,
            Func<TKey, T> getResult, params object[] args)
        {
            var property = getProperty.GetProperty();

            var instance = (T)Activator.CreateInstance(typeof(T), args);

            property.SetValue(instance, value);

            modelEncryptionFactory.Encrypt(instance);

            var val = getProperty
                .Compile()
                .Invoke(instance);

            return getResult(val);
        }

        private readonly IModelEncryptionFactory modelEncryptionFactory;
    }
}
