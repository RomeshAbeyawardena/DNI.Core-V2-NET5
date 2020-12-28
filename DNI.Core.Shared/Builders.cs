using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    public static class Builders
    {
        /// <summary>
        /// Creates a dictionary builder
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IDictionaryBuilder<TKey, TValue> Dictionary<TKey, TValue>()
        {
            return GetBuilder().Create<TKey, TValue>();
        }

        /// <summary>
        /// Creates a list builder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IListBuilder<T> List<T>()
        {
            return GetBuilder().Create<T>();
        }

        /// <summary>
        /// Creates a dictionary builder with a declared build action
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public static IDictionaryBuilder<TKey, TValue> Dictionary<TKey, TValue>(Action<IDictionaryBuilder<TKey, TValue>> buildAction)
        {
            return GetBuilder().Create(buildAction);
        }

        /// <summary>
        /// Creates a list builder with a declared build action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public static IListBuilder<T> List<T>(Action<IListBuilder<T>> buildAction)
        {
            return GetBuilder().Create(buildAction);
        }

        /// <summary>
        /// Gets or creates a builder to create lists or dictionary builders
        /// </summary>
        /// <returns></returns>
        public static IBuilder GetBuilder()
        {
            return builder ??= new Builder();
        }

        private static IBuilder builder;
    }
}
