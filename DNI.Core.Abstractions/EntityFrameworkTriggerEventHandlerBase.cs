using DNI.Core.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class EntityFrameworkTriggerEventHandlerBase<T> : TriggerEventHandlerBase<T, EntityState>
    {
        protected EntityFrameworkTriggerEventHandlerBase(EntityState state)
            : base(state)
        {
            
        }

        public abstract override void Handle(T value);
    }
}
