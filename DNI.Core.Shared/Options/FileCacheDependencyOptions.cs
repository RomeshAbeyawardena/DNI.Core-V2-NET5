using DNI.Core.Shared.Contracts;
using System;

namespace DNI.Core.Shared.Options
{
    public class FileCacheDependencyOptions : ICacheDependencyOptions
    {
        public TimeSpan ElapsedPeriod { get; set; }
        public string DependencyFile { get; set; }
    }
}
