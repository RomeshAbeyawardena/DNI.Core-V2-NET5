using DNI.Core.Abstractions;
using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Tests.Assets
{
    public class TestModelSanitizer : ModelSanitizerBase
    {
        public override string SanitizeString(string propertyValue)
        {
            return base.SanitizeString(propertyValue);
        }
    }
}
