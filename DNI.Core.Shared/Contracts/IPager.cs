using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IPager<T>
    {
        IQueryable<T> Query { get; }
        Task<IEnumerable<T>> GetPagedItemsAsync(int pageIndex, int totalItemsPerPage, CancellationToken cancellationToken);
        IEnumerable<T> GetPagedItems(int pageIndex, int totalItemsPerPage);
        int GetTotalNumberOfPages(int length, int maximumRowsPerPage);
    }
}
