using DNI.Core.Shared;
using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    /// <inheritdoc />
    internal class DefaultChangeTracker<T> : IChangeTracker<T>
    {
        public bool HasChanges(T source, T value, out IEnumerable<PropertyChange> propertyChanges)
        {
            propertyChanges = Scan(source, value);
            return propertyChanges.Any(pc => pc.HasChanges);
        }

        public int MergeChanges(T source, T value)
        {
            var propertyChanges = Scan(source, value);
            return MergeChanges(source, value, propertyChanges);
        }

        public int MergeChanges(T source, T value, IEnumerable<PropertyChange> propertyChanges)
        {
            var changeCount = 0;
            foreach (var propertyChange in propertyChanges.Where(pc => pc.HasChanges))
            {
                propertyChange.Property.SetValue(source, propertyChange.NewValue);
                changeCount++;
            }

            return changeCount;
        }

        private static IEnumerable<PropertyChange> Scan(T source, T value)
        {
            var valueType = typeof(T);
            var propertyChangeList = new List<PropertyChange>();
            foreach (var property in valueType.GetProperties())
            {
                var sourceValue = property.GetValue(source);
                var destinationValue = property.GetValue(value);
                propertyChangeList.Add(new PropertyChange(property, sourceValue, destinationValue));
            }

            return propertyChangeList.ToArray();
        }
    }
}
