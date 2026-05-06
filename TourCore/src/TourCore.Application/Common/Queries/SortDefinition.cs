using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TourCore.Application.Common.Queries
{
    public sealed class SortDefinition<TEntity>
    {
        private readonly IReadOnlyDictionary<string, LambdaExpression> _map;

        public SortDefinition(
            IEnumerable<SortExpression<TEntity>> expressions,
            LambdaExpression defaultExpression)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException(nameof(expressions));
            }

            DefaultExpression = defaultExpression ?? throw new ArgumentNullException(nameof(defaultExpression));

            _map = expressions.ToDictionary(
                x => x.Name,
                x => x.Expression,
                StringComparer.OrdinalIgnoreCase);
        }

        public LambdaExpression DefaultExpression { get; }

        public LambdaExpression Resolve(string sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                return DefaultExpression;
            }

            LambdaExpression expression;

            return _map.TryGetValue(sortBy, out expression)
                ? expression
                : DefaultExpression;
        }
    }
}