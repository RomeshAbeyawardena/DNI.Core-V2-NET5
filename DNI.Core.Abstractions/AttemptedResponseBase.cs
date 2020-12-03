using DNI.Core.Shared.Contracts;
using System.Collections.Generic;

namespace DNI.Core.Abstractions
{
    public class AttemptedResponseBase<T> : IAttemptedResponse<T>
    {
        public IAttempt<T> Attempt { get; set; }
        public IAttempt<IEnumerable<T>> AttemptMany { get; }
        IAttempt IAttemptedResponse.Attempt => Attempt ?? (IAttempt)AttemptMany;
    }
}
