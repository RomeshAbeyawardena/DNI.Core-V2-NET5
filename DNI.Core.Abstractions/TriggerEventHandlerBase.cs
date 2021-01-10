using DNI.Core.Shared.Contracts;
using System;

namespace DNI.Core.Abstractions
{
    /// <see cref="ITriggerEventHandler{T, TEnum}" />
    /// <see cref="ITriggerEventHandler{T}" />
    public abstract class TriggerEventHandlerBase<T, TEnum> : ITriggerEventHandler<T, TEnum>
        where TEnum : Enum
    {
        protected TriggerEventHandlerBase(TEnum state)
        {
            State = state;
        }

        public TEnum State { get; }

        public abstract void Handle(T value);

        void ITriggerEventHandler<TEnum>.Handle(object value)
        {
            Handle((T)value);
        }
    }
}
