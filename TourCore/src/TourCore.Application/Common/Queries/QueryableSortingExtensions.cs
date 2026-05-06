using System;
using System.Linq;
using System.Linq.Expressions;

namespace TourCore.Application.Common.Queries
{
    public static class QueryableSortingExtensions
    {
        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            ISortedQuery sorting,
            SortDefinition<T> sortDefinition)
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

            return ApplyOrderBy(
                query,
                expression,
                sorting.SortDirection == SortDirection.Desc);
        }

        private static IQueryable<T> ApplyOrderBy<T>(
            IQueryable<T> query,
            LambdaExpression keySelector,
            bool descending)
        {
            var methodName = descending
                ? nameof(Queryable.OrderByDescending)
                : nameof(Queryable.OrderBy);

            var methodCall = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(T), keySelector.ReturnType },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(methodCall);
        }
    }
}