using DNI.Core.Shared.Abstractions;
using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Conventions
{
    public class TrimStringConvention : StringConventionBase
    {
        public TrimStringConvention(TrimMode trimMode)
            : base(s => Trim(s, trimMode))
        {
            TrimMode = trimMode;
        }

        public TrimMode TrimMode { get; }

        private static string Trim(string value, TrimMode trimMode)
        {
            switch (trimMode)
            {
                case TrimMode.Start:
                    return value.TrimStart();
                case TrimMode.End:
                    return value.TrimEnd();
                case TrimMode.Both:
                    return value.Trim();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
