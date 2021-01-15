using DNI.Core.Shared.Extensions;
using DNI.Core.Shared.Managers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Managers
{
    internal class DefaultResourceManager : IResourceManager
    {
        public DefaultResourceManager()
        {
            resourceDictionary = new ConcurrentDictionary<Type, string>();
        }

        public IReadOnlyDictionary<Type, string> Resources => resourceDictionary;

        public IResourceManager AddExceptionErrorMessage<TException>(string resource) where TException : Exception
        {
            resourceDictionary.TryAdd(typeof(TException), resource);

            return this;
        }

        public string Get<TException>(IDictionary<string, string> placeHolders = null)
        {
            if(placeHolders == null)
            {
                placeHolders = new Dictionary<string, string>();
            }

            if(resourceDictionary.TryGetValue(typeof(TException), out var resourceText))
            { 
                return placeHolders
                    .Aggregate(resourceText, (s, kvp) => s.Replace("[" + kvp.Key + "]", kvp.Value));
            }

            return string.Empty;
        }

        
        private readonly ConcurrentDictionary<Type, string> resourceDictionary;
    }
}
