using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts.Factories
{
    public interface ITriggerFactory
    {
        IEnumerable<ITriggerEventHandler<T, TEnum>> GetTriggerEventHandlers<T, TEnum>() 
            where TEnum : Enum;

    }
}
