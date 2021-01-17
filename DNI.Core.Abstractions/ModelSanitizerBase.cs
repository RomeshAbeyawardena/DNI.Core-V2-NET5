using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Abstractions
{
    public abstract class ModelSanitizerBase : IModelSanitizer
    {
        private void CheckModelType(Type type, object model)
        {
            if(model.GetType() != type)
            {
                throw new ArgumentException("", nameof(model));
            }
        }

        public void SanitizeModel(Type modelType, object model)
        {
            CheckModelType(modelType, model);
            var modelProperties = modelType.GetProperties();
            SanitizeModel(modelType, model, modelProperties);
        }

        public void SanitizeModel(Type modelType, object model, IEnumerable<PropertyInfo> properties)
        {
            CheckModelType(modelType, model);

            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                if (propertyType == typeof(string) 
                    && property.CanWrite)
                {
                    var propertyValue = property.GetValue(model);
                    if(propertyValue == null)
                    {
                        continue;
                    }

                    property.SetValue(model, SanitizeString(propertyValue.ToString()));
                }
                else if (!(propertyType.IsPrimitive || propertyType.IsValueType))
                {
                    var value = property.GetValue(model);

                    if(value == null)
                    {
                        continue;
                    }

                    SanitizeModel(propertyType, value);
                }
            }
        }

        public void SanitizeModel<T>(T model)
        {
            var modelType = typeof(T);
            SanitizeModel(modelType, model);
        }

        public virtual string SanitizeString(string propertyValue)
        {
            if(string.IsNullOrWhiteSpace(propertyValue))
            {
                return string.Empty;
            }

            return propertyValue.Trim();
        }
    }


}
