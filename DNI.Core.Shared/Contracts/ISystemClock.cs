using System;

namespace DNI.Core.Shared.Contracts
{
    /// <summary>
    /// Represents the system clock that supports overriding in test instances
    /// </summary>
    public interface ISystemClock
    {
        /// <summary>
        /// Represents the current date and time, may be overridden in a test instance
        /// </summary>
        DateTimeOffset Now { get; }
    }
}
