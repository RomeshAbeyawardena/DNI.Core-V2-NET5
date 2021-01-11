using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface IChangeTrackerFactory
    {
        IChangeTracker<T> GetChangeTracker<T>();
        bool HasChanges<T>(T source, T value, out IEnumerable<PropertyChange> propertyChanges);
        int MergeChanges<T>(T source, T value);
        int MergeChanges<T>(T source, T value, IEnumerable<PropertyChange> propertyChanges);
    }
}
