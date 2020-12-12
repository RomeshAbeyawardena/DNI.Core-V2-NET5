using DNI.Core.Shared.Enumerations;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts
{
    public interface IAttemptedResponse
    {
        IAttempt Attempt { get; }
    }

    public interface IAttemptedResponse<T> : IAttemptedResponse
    {
        RequestQueryType Type { get; }
        new IAttempt<T> Attempt { get; }
        IAttempt<IEnumerable<T>> AttemptMany { get; }
    }
}
