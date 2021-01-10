using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface ILookupServiceFactory
    {
        ILookupService GetLookupService<TEntity>();
    }
}
