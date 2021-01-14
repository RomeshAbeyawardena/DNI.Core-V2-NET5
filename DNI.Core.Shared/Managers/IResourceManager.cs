using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Managers
{
    public interface IResourceManager
    {
        IResourceManager AddExceptionErrorMessage<TException>(string resource)
            where TException : Exception;

        string Get<TException>(IDictionary<string, string> placeHolders = default);
       
        IReadOnlyDictionary<Type, string> Resources { get; }
    }
}
