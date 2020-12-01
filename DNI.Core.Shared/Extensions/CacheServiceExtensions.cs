using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Services;
using DNI.Core.Shared.Enumerations;

namespace DNI.Core.Shared.Extensions
{
    public static class CacheServiceExtensions
    {
        public static IAttempt<T> TryGetCachedEntryItem<T>(
            this ICacheService cacheService,
            string cacheKeyName,
            SerializerType serializerType,
            Func<T, bool> firstOrDefaultExpression = null)
        {
            var attempt = cacheService.TryGet<IEnumerable<T>>(cacheKeyName, serializerType);

            if (attempt.Successful)
            {
                if (firstOrDefaultExpression == null)
                {
                    return Attempt.Success(attempt.Result.FirstOrDefault());
                }

                return Attempt.Success(attempt.Result.FirstOrDefault(firstOrDefaultExpression));
            }

            return Attempt.Failed<T>(attempt.Exception, attempt.ValidationFailures);
        }

        public static async Task<IAttempt<T>> TryGetCachedEntryItemAsync<T>(
            this ICacheService cacheService,
            string cacheKeyName,
            SerializerType serializerType,
            Func<T, bool> firstOrDefaultExpression = null,
            CancellationToken cancellationToken = default)
        {
            var attempt = await cacheService.TryGetAsync<IEnumerable<T>>(cacheKeyName, serializerType, cancellationToken);

            if (attempt.Successful)
            {
                if (firstOrDefaultExpression == null)
                {
                    return Attempt.Success(attempt.Result.FirstOrDefault());
                }

                return Attempt.Success(attempt.Result.FirstOrDefault(firstOrDefaultExpression));
            }

            return Attempt.Failed<T>(attempt.Exception, attempt.ValidationFailures);
        }

        public static IAttempt<T> TryGetSet<T>(
            this ICacheService cacheService,
            string cacheKeyName,
            SerializerType serializerType,
            Func<T> retrievalFromSourceAction,
            CancellationToken cancellationToken)
        {
            var attempt = cacheService.TryGet<T>(cacheKeyName, serializerType);

            if (attempt.Successful)
            {
                return attempt;
            }

            var value = retrievalFromSourceAction();
            var setAttempt = cacheService.TrySet(cacheKeyName, value, serializerType);

            if (setAttempt.Successful)
            {
                return Attempt.Success(value);
            }

            return attempt;
        }

        public static async Task<IAttempt<T>> TryGetSetAsync<T>(
            this ICacheService cacheService,
            string cacheKeyName,
            SerializerType serializerType,
            Func<CancellationToken, Task<T>> retrievalFromSourceAction,
            CancellationToken cancellationToken)
        {
            var attempt = await cacheService.TryGetAsync<T>(cacheKeyName, serializerType, cancellationToken);

            if (attempt.Successful)
            {
                return attempt;
            }

            var value = await retrievalFromSourceAction(cancellationToken);
            var setAttempt = await cacheService.TrySetAsync(cacheKeyName, value, serializerType, cancellationToken);

            if (setAttempt.Successful)
            {
                return Attempt.Success(value);
            }

            return attempt;
        }

        public static async Task<IAttempt> TrySetCachedEntryItemAsync<T>(
            this ICacheService cacheService,
            string cacheKeyName, SerializerType serializerType, T item, CancellationToken cancellationToken)
        {
            var attempt = await cacheService.TryGetAsync<IEnumerable<T>>(cacheKeyName, serializerType, cancellationToken);

            List<T> itemList = new List<T>();

            if (attempt.Successful)
            {
                var items = attempt.Result;

                if (items.Any())
                {
                    itemList = items.ToList();
                }

            }

            itemList.Add(item);

            return await cacheService.TrySetAsync(cacheKeyName, itemList.ToArray(), serializerType, cancellationToken);


        }
    }
}
