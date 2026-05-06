namespace TourCore.Application.Common.Queries
{
    public interface ISortedQuery
    {
        string SortBy { get; }

        SortDirection SortDirection { get; }
    }
}