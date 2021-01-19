using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Extensions
{
    public static class PagerExtensions
    {
        public static Task<IEnumerable<T>> GetPagedItemsAsync<T>(
            IPager<T> pager, 
            ISearchCriteria<T> searchCriteria, 
            CancellationToken cancellationToken)
        {
            return pager.GetPagedItemsAsync(searchCriteria.PageIndex, 
                searchCriteria.TotalItemsPerPage, cancellationToken);
        }
    }
}
