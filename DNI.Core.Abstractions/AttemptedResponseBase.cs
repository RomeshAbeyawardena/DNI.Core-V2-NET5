using DNI.Core.Shared.Contracts;
using DNI.Core.Shared.Enumerations;
using System.Collections.Generic;

namespace DNI.Core.Abstractions
{
    public abstract class AttemptedResponseBase<T> : IAttemptedResponse<T>
    {
        public RequestQueryType Type { get; }
        public IAttempt<T> Attempt { get; }
        public IAttempt<IEnumerable<T>> AttemptMany { get; }
        IAttempt IAttemptedResponse.Attempt => Attempt ?? (IAttempt)AttemptMany;
    }
}
