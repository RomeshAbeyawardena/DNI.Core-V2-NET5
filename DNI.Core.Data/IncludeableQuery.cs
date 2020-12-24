using System.Linq;
using DNI.Core.Abstractions;

namespace DNI.Core.Data
{
    internal class IncludeableQuery<T> : IncludeableQueryBase<T>
        where T : class
    {
        public IncludeableQuery(IQueryable<T> query)
            : base(query)
        {

        }
    }
}
