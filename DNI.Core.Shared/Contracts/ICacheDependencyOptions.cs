using System;

namespace DNI.Core.Shared.Contracts
{
    public interface ICacheDependencyOptions
    {
        TimeSpan ElapsedPeriod { get; } 
    }
}
