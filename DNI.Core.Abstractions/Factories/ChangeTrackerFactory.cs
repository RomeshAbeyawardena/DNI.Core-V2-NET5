using DNI.Core.Shared;
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
    internal class ChangeTrackerFactory : IChangeTrackerFactory
    {
        public ChangeTrackerFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IChangeTracker<T> GetChangeTracker<T>()
        {
            return serviceProvider.GetRequiredService<IChangeTracker<T>>();
        }

        public bool HasChanges<T>(T source, T value, out IEnumerable<PropertyChange> propertyChanges)
        {
            return GetChangeTracker<T>()
                .HasChanges(source, value, out propertyChanges);
        }

        public int MergeChanges<T>(T source, T value)
        {
            return GetChangeTracker<T>()
                .MergeChanges(source, value);
        }

        public int MergeChanges<T>(T source, T value, IEnumerable<PropertyChange> propertyChanges)
        {
            return GetChangeTracker<T>()
                .MergeChanges(source, value, propertyChanges);
        }
        
        private readonly IServiceProvider serviceProvider;
    }
}
