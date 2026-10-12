using System;
using System.Linq.Expressions;

namespace TourCore.Application.Common.Queries
{
    public class SortExpression<TEntity>
    {
        public SortExpression(
            string name,
            LambdaExpression expression)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Sort name is required.", nameof(name));
            }

            Name = name;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public string Name { get; }

        public LambdaExpression Expression { get; }
    }

    public sealed class SortExpression<TEntity, TKey> : SortExpression<TEntity>
    {
        public SortExpression(
            string name,
            Expression<Func<TEntity, TKey>> expression)
            : base(name, expression)
        {
        }
    }
}