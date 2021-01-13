using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents a change tracker factory to resolve <see cref="IChangeTracker{T} instances" /> 
    /// </summary>
    public interface IChangeTrackerFactory
    {
        /// <summary>
        /// Resolves a <see cref="IChangeTracker{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>An instance of a <see cref="IChangeTracker{T}"/></returns>
        IChangeTracker<T> GetChangeTracker<T>();
        /// <summary>
        /// Determines whether changes exist between <paramref name="source"/> and  <paramref name="value"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="propertyChanges"></param>
        /// <returns></returns>
        bool HasChanges<T>(T source, T value, out IEnumerable<PropertyChange> propertyChanges);

        /// <summary>
        /// Commits changes between <paramref name="source"/> and <paramref name="value"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        int MergeChanges<T>(T source, T value);
        /// <summary>
        /// Commits changes between <paramref name="source"/> and <paramref name="value"/>
        /// using an existing <paramref name="propertyChanges"/> list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="propertyChanges"></param>
        /// <returns></returns>
        int MergeChanges<T>(T source, T value, IEnumerable<PropertyChange> propertyChanges);
    }
}
