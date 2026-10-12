namespace TourCore.Application.Common.Queries
{
    public interface IPagedQuery
    {
        int Page { get; }

        int PageSize { get; }
    }
}