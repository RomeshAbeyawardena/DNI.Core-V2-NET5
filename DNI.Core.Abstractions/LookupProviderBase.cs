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
            var instance = ModelEncryptionServiceExtensions.CreateInstance<TKey, T>(value,
                getProperty, 
                out var property,
                args);

            modelEncryptionFactory.Encrypt(instance);
            var val = (TKey)property.GetValue(instance);

            return getResult(val);
        }

        private readonly IModelEncryptionFactory modelEncryptionFactory;
    }
}
