using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Contracts.Builders;
using System;

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
        /// Creates a switch with a declared build action
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="buildAction"></param>
        /// <returns></returns>
        public static ISwitch<TKey, TValue> Switch<TKey, TValue>(Action<ISwitch<TKey, TValue>> buildAction)
        {
            return GetBuilder().Create(buildAction);
        }

        /// <summary>
        /// Gets or creates a builder to create list, switch or dictionary builders
        /// </summary>
        /// <returns></returns>
        public static IBuilder GetBuilder()
        {
            return builder ??= new Builder();
        }

        /// <summary>
        /// Gets the default builder
        /// </summary>
        public static IBuilder Default => GetBuilder();

        private static IBuilder builder;
    }
}
