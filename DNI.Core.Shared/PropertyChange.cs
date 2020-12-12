using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared
{
    public class PropertyChange
    {
        public PropertyInfo Property { get; }

        public PropertyChange(PropertyInfo property, object oldValue, object newValue = null)
        {
            Property = property;
            OldValue = oldValue;
            NewValue = newValue ?? oldValue;
        }

        public object OldValue { get; }
        public object NewValue { get; }
        public bool HasChanges => !OldValue.Equals(NewValue);
    }
}
