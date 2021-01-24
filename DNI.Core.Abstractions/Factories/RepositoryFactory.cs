using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions.Factories
{
    internal class RepositoryFactory : IRepositoryFactory
    {
        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IAsyncRepository<TEntity> GetAsyncRepository<TEntity>() where TEntity : class
        {
            return serviceProvider.GetRequiredService<IAsyncRepository<TEntity>>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return serviceProvider.GetRequiredService<IRepository<TEntity>>();
        }

        private readonly IServiceProvider serviceProvider;
    }
}
