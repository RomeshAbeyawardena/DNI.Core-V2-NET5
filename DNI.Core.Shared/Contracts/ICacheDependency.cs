using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface ICacheDependency
    {
        ICacheDependencyOptions Options { get; }
        /// <summary>
        /// Verifies whether the cache dependency object is valid for a cache entry
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsValid(string key, CancellationToken cancellationToken);

        /// <summary>
        /// Invalidates a cache entry, forcing the items to be retrieved from source
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> Invalidate(string key, CancellationToken cancellationToken);

        /// <summary>
        /// Sets a cache entry as valid and can be relied upon for a specific time period
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> Set(string key, CancellationToken cancellationToken);
    }
}
