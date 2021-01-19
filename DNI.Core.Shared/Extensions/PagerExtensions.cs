using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class PagerExtensions
    {
        public static Task<IEnumerable<T>> GetPagedItemsAsync<T>(
            this IPager<T> pager, 
            ISearchCriteria<T> searchCriteria, 
            CancellationToken cancellationToken,
            Expression<Func<T, bool>> whereExpression = default)
        {
            return pager.GetPagedItemsAsync(searchCriteria.PageIndex, 
                    searchCriteria.TotalItemsPerPage, cancellationToken,
                        whereExpression);
        }
    }
}
