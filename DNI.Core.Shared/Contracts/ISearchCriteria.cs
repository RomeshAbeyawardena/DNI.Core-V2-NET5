namespace DNI.Core.Shared.Contracts
{
    public interface ISearchCriteria<T> : IPagingCriteria
    {
        T Parameters { get; set; }
    }
}
