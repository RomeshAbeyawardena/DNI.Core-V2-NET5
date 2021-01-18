using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Conventions
{
    public abstract class StringConventionBase : IConvention
    {
        protected StringConventionBase(Func<string,string> applyFunction)
        {
            this.applyFunction = applyFunction;
        }

        public T Apply<T>(T model)
        {
            if(model == null)
            {
                return model;
            }

            return (T)Apply(model.GetType(), model);
        }

        protected object Apply(Type type, object model)
        {
            if(model != null && type == typeof(string))
            {
                var modelString = model.ToString();
                if(string.IsNullOrWhiteSpace(modelString))
                {
                    return model;
                }

                return applyFunction(modelString);
            }

            return model;
        }

        private readonly Func<string, string> applyFunction;
    }
}
