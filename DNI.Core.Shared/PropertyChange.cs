using DNI.Core.Shared.Extensions;
using System.Reflection;

namespace DNI.Core.Shared
{
    public class PropertyChange
    {
        public PropertyInfo Property { get; }

        public PropertyChange(PropertyInfo property, object oldValue, object newValue = null)
        {
            Property = property;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public object OldValue { get; }
        public object NewValue { get; }
        public bool HasChanges => !NewValue.IsDefault() && !OldValue.Equals(NewValue);
    }
}
