using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Providers
{
    public interface ICacheProvider : IDisposable
    {
        CacheServiceType CacheServiceType { get; set; }
        SerializerType SerializerType { get; set; }
    }
}
