using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents a trigger factory used to get a list of <see cref="ITriggerEventHandler{TEnum}"/>
    /// </summary>
    public interface ITriggerFactory
    {
        IEnumerable<ITriggerEventHandler<T, TEnum>> GetTriggerEventHandlers<T, TEnum>() 
            where TEnum : Enum;

    }
}
