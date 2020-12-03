using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents a definition collection of <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDefinition<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets items of <typeparamref name="T"/> in definition
        /// </summary>
        IEnumerable<T> Items { get; }

        /// <summary>
        /// Adds item of <typeparamref name="T"/> to definition
        /// </summary>
        /// <param name="item"></param>
        /// <returns>The current instance of <see cref="IDefinition{T}"/></returns>
        IDefinition<T> Add(T item);

        /// <summary>
        /// Adds items of <typeparamref name="T"/> to definition
        /// </summary>
        /// <param name="items"></param>
        /// <returns>The current instance of <see cref="IDefinition{T}"/></returns>
        IDefinition<T> AddRange(IEnumerable<T> items);
    }
}
