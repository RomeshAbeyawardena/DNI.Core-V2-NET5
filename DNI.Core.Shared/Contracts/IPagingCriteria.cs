namespace DNI.Core.Shared.Contracts
{
    public interface IPagingCriteria
    {
        int TotalItemsPerPage { get; set; }
        int PageIndex { get; set; }
    }
}
