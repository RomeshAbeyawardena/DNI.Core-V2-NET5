using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface ITriggerFactory
    {
        IEnumerable<ITriggerEventHandler<T, TEnum>> GetTriggerEventHandlers<T, TEnum>() 
            where TEnum : Enum;

    }
}
