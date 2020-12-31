using System;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public interface ITriggerEventHandler<TEnum>
        where TEnum : Enum
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void Handle(object value);
        /// <summary>
        /// 
        /// </summary>
        TEnum State { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    /// <see cref="ITriggerEventHandler{TEnum}"/>
    public interface ITriggerEventHandler<T, TEnum>
        : ITriggerEventHandler<TEnum>
        where TEnum : Enum
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        void Handle(T value);
    }
}
