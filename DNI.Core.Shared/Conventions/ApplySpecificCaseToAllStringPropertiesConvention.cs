using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Conventions
{
    public class ApplySpecificCaseToAllStringPropertiesConvention : IConvention
    {
        public ApplySpecificCaseToAllStringPropertiesConvention(CharacterCase characterCase)
        {
            CharacterCase = characterCase;
        }
        
        public CharacterCase CharacterCase { get; }

        public T Apply<T>(T model)
        {
            return (T)Apply(model.GetType(), model);
        }

        private object Apply(Type type, object model)
        {
            if(model != null && type == typeof(string))
            {
                var modelString = model.ToString();
                if(string.IsNullOrWhiteSpace(modelString))
                {
                    return model;
                }

                return SetCase(modelString);
            }

            return model;
        }

        private string SetCase(string value)
        {
            return CharacterCase switch
            {
                CharacterCase.None => value,
                CharacterCase.Lower => value.ToLower(),
                CharacterCase.Upper => value.ToUpper(),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}