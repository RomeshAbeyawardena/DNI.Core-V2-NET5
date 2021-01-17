using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Contracts
{
    public interface IModelSanitizer
    {
        void SanitizeModel<T>(T model);
        void SanitizeModel(Type modelType, object model);
        void SanitizeModel(Type modelType, object model, IEnumerable<PropertyInfo> properties);
    }
}
