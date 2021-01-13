using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;
using System;

namespace DNI.Core.Shared.Contracts.Providers
{
    /// <summary>
    /// Represents an ICacheProvider that exposes methods available to an instance of <see cref="ICacheService"/>
    /// </summary>
    public interface ICacheProvider : IDisposable
    {
        /// <summary>
        /// Gets or sets the <see cref="CacheServiceType"/> this instance exposes
        /// </summary>
        CacheServiceType CacheServiceType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SerializerType"/> this instance uses to serialize and deserialize data
        /// </summary>
        SerializerType SerializerType { get; set; }
    }
}
