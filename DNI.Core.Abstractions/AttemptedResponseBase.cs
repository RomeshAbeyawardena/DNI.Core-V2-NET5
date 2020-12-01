using DNI.Core.Shared.Contracts;

namespace DNI.Core.Abstractions
{
    public class AttemptedResponseBase<T> : IAttemptedResponse<T>
    {
        public IAttempt<T> Attempt { get; set; }
        IAttempt IAttemptedResponse.Attempt => Attempt;
    }
}
