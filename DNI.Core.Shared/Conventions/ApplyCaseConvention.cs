using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Conventions
{
    public class ApplyCaseConvention : StringConventionBase
    {
        public ApplyCaseConvention(CharacterCase characterCase)
            : base(s => SetCase(s, characterCase))
        {
            CharacterCase = characterCase;
        }
        
        public CharacterCase CharacterCase { get; }

        private static string SetCase(string value, CharacterCase characterCase)
        {
            return characterCase switch
            {
                CharacterCase.None => value,
                CharacterCase.Lower => value.ToLower(),
                CharacterCase.Upper => value.ToUpper(),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}