using System;

namespace DNI.Core.Shared.Contracts
{
    public interface ITriggerEventHandler<TEnum>
        where TEnum : Enum
    {
        void Handle(object value);
        TEnum State { get; }
    }

    public interface ITriggerEventHandler<T, TEnum>
        : ITriggerEventHandler<TEnum>
        where TEnum : Enum
    {
        void Handle(T value);
    }
}
