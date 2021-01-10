using System.Linq;
using DNI.Core.Abstractions;
using DNI.Core.Shared.Contracts;

namespace DNI.Core.Data
{
    internal class IncludeableQuery<T> : IncludeableQueryBase<T>
        where T : class
    {
        public static IIncludeableQuery<T> Create(IQueryable<T> query)
        {
            return new IncludeableQuery<T>(query);
        }

        private IncludeableQuery(IQueryable<T> query)
            : base(query)
        {

        }
    }
}
