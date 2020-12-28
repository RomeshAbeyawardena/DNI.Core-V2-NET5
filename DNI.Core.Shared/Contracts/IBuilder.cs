﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents a builder that can create list and dictionary builders
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Creates a list builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IListBuilder<T> Create<T>();

        /// <summary>
        /// Creates a dictionary builder
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        IDictionaryBuilder<TKey, TValue> Create<TKey, TValue>();

        /// <summary>
        /// Creates a list builder using a declared build action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        IListBuilder<T> Create<T>(Action<IListBuilder<T>> buildAction);

        /// <summary>
        /// Creates a dictionary builder using a declared build action
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        IDictionaryBuilder<TKey, TValue> Create<TKey, TValue>(Action<IDictionaryBuilder<TKey, TValue>> buildAction);
    }
}
