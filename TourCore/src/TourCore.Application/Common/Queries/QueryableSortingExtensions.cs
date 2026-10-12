using System;
using System.Linq.Expressions;

namespace TourCore.Application.Common.Queries
{
    public static class QuerySpecificationSortingExtensions
    {
        public static QuerySpecification<T> ApplySorting<T>(
            this QuerySpecification<T> query,
            ISortedQuery sorting,
            SortDefinition<T> sortDefinition)
            where T : class
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (sorting == null)
            {
                throw new ArgumentNullException(nameof(sorting));
            }

            if (sortDefinition == null)
            {
                throw new ArgumentNullException(nameof(sortDefinition));
            }

            var expression = sortDefinition.Resolve(sorting.SortBy);

            return query.ApplyOrder(expression, sorting.SortDirection == SortDirection.Desc);
        }
    }
}
