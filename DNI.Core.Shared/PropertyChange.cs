﻿using DNI.Core.Shared.Extensions;
using System.ComponentModel.DataAnnotations;
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
        //public bool HasChanges => !NewValue.IsDefault() && !OldValue.Equals(NewValue);

        public bool HasChanges { 
            get { 

                var keyAttribute = Property.GetCustomAttribute<KeyAttribute>();

                if(OldValue == null && NewValue != null
                    || OldValue != null && NewValue == null)
                    return true;

                if(OldValue.IsDefault() && !NewValue.IsDefault()
                    || keyAttribute == null && !OldValue.IsDefault() && NewValue.IsDefault()
                    || OldValue != NewValue)
                    return true;

                return false;
            }
        }
    }
}
