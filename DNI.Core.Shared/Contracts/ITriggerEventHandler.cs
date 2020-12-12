using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
