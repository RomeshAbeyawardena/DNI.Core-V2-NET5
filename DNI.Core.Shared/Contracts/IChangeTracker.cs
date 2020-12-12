using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents a simple object change tracker
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChangeTracker<T>
    {
        /// <summary>
        /// Determines whether there are changes between the source and value parameters
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool HasChanges(T source, T value, out IEnumerable<PropertyChange> propertyChanges);

        /// <summary>
        /// Merges any changes from the value parameter to the source parameter
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int MergeChanges(T source, T value);

        /// <summary>
        /// Merges any changes from the value parameter to the source parameter, using pre-determined property-changes.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="propertyChanges"></param>
        /// <returns></returns>
        int MergeChanges(T source, T value, IEnumerable<PropertyChange> propertyChanges);
    }
}
