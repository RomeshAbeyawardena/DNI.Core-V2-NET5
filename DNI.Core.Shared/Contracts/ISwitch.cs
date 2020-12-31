using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents a multi-purpose and re-useable switch
    /// </summary>
    /// <typeparam name="TKey">The <typeparamref name="TKey"/> used  </typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ISwitch<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Retrieves the switch value for <paramref name="key"/>
        /// </summary>
        /// <param name="key">The key for the switch value to be returned</param>
        /// <returns>Instance of <typeparamref name="TValue"/></returns>
        IAttempt<TValue> Case(TKey key);

        /// <summary>
        /// Attempts to add the specified key and value to this i <see cref="ISwitch{TKey, TValue}"/>
        /// </summary>
        /// <param name="item">A <see cref="KeyValuePair{TKey, TValue}" /> to add to the Switch"</param>
        /// <returns>This instance of <see cref="ISwitch{TKey, TValue}"/></returns>
        new ISwitch<TKey, TValue> Add(KeyValuePair<TKey, TValue> item);

        /// <summary>
        /// Attempts to add the specified key and value to the <see cref="ISwitch{TKey, TValue}"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>This instance of <see cref="ISwitch{TKey, TValue}"/></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        new ISwitch<TKey, TValue> Add(TKey key, TValue value);
    }
}
