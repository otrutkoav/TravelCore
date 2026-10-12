using System;

namespace TourCore.Application.Common.Queries;

public static class QuerySpecificationSortingExtensions
{
    public static QuerySpecification<T> ApplySorting<T>(
        this QuerySpecification<T> query,
        ISortedQuery sorting,
        SortDefinition<T> sortDefinition)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(sorting);
        ArgumentNullException.ThrowIfNull(sortDefinition);
        var expression = sortDefinition.Resolve(sorting.SortBy);
        return query.ApplyOrder(expression, sorting.SortDirection == SortDirection.Desc);
    }
}
