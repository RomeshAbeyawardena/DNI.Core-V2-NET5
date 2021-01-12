using DNI.Core.Shared.Enumerations;
using System;

namespace DNI.Core.Shared.Contracts.Providers
{
    public interface ICacheProvider : IDisposable
    {
        CacheServiceType CacheServiceType { get; set; }
        SerializerType SerializerType { get; set; }
    }
}
