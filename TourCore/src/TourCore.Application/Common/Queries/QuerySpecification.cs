using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TourCore.Application.Common.Data;

namespace TourCore.Application.Common.Queries
{
    public sealed record QueryOrder(LambdaExpression KeySelector, bool Descending);

    public sealed class QuerySpecification<TEntity> where TEntity : class
    {
        private readonly List<Expression<Func<TEntity, bool>>> _filters = new();
        private readonly List<QueryOrder> _orders = new();
        public IReadOnlyCollection<Expression<Func<TEntity, bool>>> Filters => _filters;
        public IReadOnlyCollection<QueryOrder> Orders => _orders;

        public QuerySpecification<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        { _filters.Add(predicate ?? throw new ArgumentNullException(nameof(predicate))); return this; }
        public QuerySpecification<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> key) => SetFirstOrder(key, false);
        public QuerySpecification<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> key) => SetFirstOrder(key, true);
        public QuerySpecification<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> key) => AddOrder(key, false);
        public QuerySpecification<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> key) => AddOrder(key, true);
        public QuerySpecification<TEntity> ApplyOrder(LambdaExpression key, bool descending) => SetFirstOrder(key, descending);
        public ProjectedQuerySpecification<TEntity, TResult> Select<TResult>(Expression<Func<TEntity, TResult>> projection) => new(this, projection);

        private QuerySpecification<TEntity> SetFirstOrder(LambdaExpression key, bool descending)
        { _orders.Clear(); return AddOrder(key, descending); }
        private QuerySpecification<TEntity> AddOrder(LambdaExpression key, bool descending)
        { _orders.Add(new QueryOrder(key ?? throw new ArgumentNullException(nameof(key)), descending)); return this; }
    }

    public sealed class ProjectedQuerySpecification<TEntity, TResult> where TEntity : class
    {
        public ProjectedQuerySpecification(QuerySpecification<TEntity> source, Expression<Func<TEntity, TResult>> projection)
        { Source = source ?? throw new ArgumentNullException(nameof(source)); Projection = projection ?? throw new ArgumentNullException(nameof(projection)); }
        public QuerySpecification<TEntity> Source { get; }
        public Expression<Func<TEntity, TResult>> Projection { get; }
    }

    public static class QuerySpecification
    {
        public static QuerySpecification<TEntity> For<TEntity>(IRepository<TEntity> repository) where TEntity : class
        { _ = repository ?? throw new ArgumentNullException(nameof(repository)); return new QuerySpecification<TEntity>(); }
    }
}
