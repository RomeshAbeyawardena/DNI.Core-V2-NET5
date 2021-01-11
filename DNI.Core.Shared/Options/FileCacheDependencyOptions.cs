using DNI.Core.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Shared.Options
{
    public class FileCacheDependencyOptions : ICacheDependencyOptions
    {
        public TimeSpan ElapsedPeriod { get; set; }
        public string DependencyFile { get; set; }
    }
}
