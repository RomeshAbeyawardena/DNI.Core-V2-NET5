namespace DNI.Core.Shared.Contracts
{
    public interface IAttemptedResponse
    {
        IAttempt Attempt { get; }
    }

    public interface IAttemptedResponse<T> : IAttemptedResponse
    {
        new IAttempt<T> Attempt { get; }
    }
}
