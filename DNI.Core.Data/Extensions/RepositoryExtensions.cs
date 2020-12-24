using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Data.Extensions
{
    public static class RepositoryExtensions
    {
        public static Task<int> AddOrUpdate<T, TSelector>(this IAsyncRepository<T> asyncRepository, 
            Func<T, TSelector> keySelector, 
            T item, CancellationToken cancellationToken)
            where T : class
        {
            var key = keySelector(item);

            if (key.IsDefault())
            {
                asyncRepository.Add(item);
            }
            else
            {
                asyncRepository.Update(item);
            }

            return asyncRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
