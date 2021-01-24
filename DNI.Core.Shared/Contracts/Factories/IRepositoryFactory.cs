using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IRepositoryFactory
    {
        IAsyncRepository<TEntity> GetAsyncRepository<TEntity>()
            where TEntity : class;

        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
    }
}
