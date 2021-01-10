using Microsoft.EntityFrameworkCore;

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
