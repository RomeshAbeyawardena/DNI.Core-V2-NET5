using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Contracts.Services
{
    /// <summary>
    /// Represents a cache service used to retrieve and save items to a caching store
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// The type of <see cref="ICacheService"/> this instance implements
        /// </summary>
        CacheServiceType Type { get; }

        /// <summary>
        /// Method to retrieve a value of <typeparamref name="T"/> from the caching store
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> value to be returned</typeparam>
        /// <param name="cacheKeyName">The key of the item stored</param>
        /// <param name="serializer">The <see cref="SerializerType"/> that should be used to convert the byte representation to <typeparamref name="T"/></param>
        /// <returns>An instance of <see cref="IAttempt{T}"/></returns>
        IAttempt<T> TryGet<T>(string cacheKeyName, SerializerType serializer);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> value to be stored</typeparam>
        /// <param name="cacheKeyName">The key of the item to be stored</param>
        /// <param name="value">The value to be stored against the <paramref name="cacheKeyName"/></param>
        /// <param name="serializer">The <see cref="SerializerType"/> that should be used to convert the byte representation to <typeparamref name="T"/></param>
        /// <returns>An instance of <see cref="IAttempt"/></returns>
        IAttempt TrySet<T>(string cacheKeyName, T value, SerializerType serializer);

        
        /// <summary>
        /// Method to retrieve a value of <typeparamref name="T"/> from the caching store
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> value to be returned</typeparam>
        /// <param name="cacheKeyName">The key of the item stored</param>
        /// <param name="serializer">The <see cref="SerializerType"/> that should be used to convert the byte representation to <typeparamref name="T"/></param>
        /// <param name="cancellationToken">Notification to cancel asynchronous task</param>
        /// <returns>An awaitable instance of <see cref="IAttempt{T}"/></returns>
        Task<IAttempt<T>> TryGetAsync<T>(string cacheKeyName, SerializerType serializer, CancellationToken cancellationToken);

        
        /// <summary>
        /// Method to retrieve a value of <typeparamref name="T"/> from the caching store
        /// </summary>
        /// <typeparam name="T">The <typeparamref name="T"/> value to be returned</typeparam>
        /// <param name="cacheKeyName">The key of the item stored</param>
        /// <param name="value">The key of the item stored</param>
        /// <param name="serializer">The <see cref="SerializerType"/> that should be used to convert the byte representation to <typeparamref name="T"/></param>
        /// <param name="cancellationToken">Notification to cancel asynchronous task</param>
        /// <returns>An awaitable instance of <see cref="IAttempt{T}"/></returns>
        Task<IAttempt> TrySetAsync<T>(string cacheKeyName, T value, SerializerType serializer, CancellationToken cancellationToken);
    }
}
