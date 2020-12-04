using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
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
