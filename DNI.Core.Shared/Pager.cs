using DNI.Core.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    internal class Pager<T> : IPager<T>
    {
        public IQueryable<T> Query { get; }

        public IEnumerable<T> GetPagedItems(int pageIndex, int totalItemsPerPage)
        {
            return GetPagedItems(pageIndex, totalItemsPerPage, Query.Count()).ToArray();
        }

        public async Task<IEnumerable<T>> GetPagedItemsAsync(int pageIndex, int totalItemsPerPage, CancellationToken cancellationToken)
        {
            return await GetPagedItems(pageIndex, totalItemsPerPage,
                await Query.CountAsync(cancellationToken))
                .ToArrayAsync(cancellationToken);
        }


        int IPager<T>.GetTotalNumberOfPages(int length, int maximumRowsPerPage)
        {
            return maximumRowsPerPage == 0 || length == 0
            ? 0
            : Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(length) / Convert.ToDecimal(maximumRowsPerPage)));
        }

        public Pager(IQueryable<T> query)
        {
            Query = query;
        }

        private IQueryable<T> Filter(int skipAmount, int takeAmount)
        {
            var query = Query;

            if (skipAmount > 0)
                query = query.Skip(skipAmount);

            if (takeAmount > 0)
                query = query.Take(takeAmount);

            return query;
        }

        private IQueryable<T> GetPagedItems(int pageIndex, int totalItemsPerPage, int totalItems)
        {
            if (totalItems < totalItemsPerPage)
            {
                totalItemsPerPage = totalItems;
            }

            var rowsToSkip = CalculateRowsToSkip(pageIndex, totalItemsPerPage);

            return Filter(rowsToSkip, totalItemsPerPage);
        }

        private int CalculateRowsToSkip(int pageNumber, int maximumRowsPerPage) => (pageNumber - 1) * maximumRowsPerPage;
    }
}
