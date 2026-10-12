using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TourCore.Application.Common.Data;
using TourCore.Application.Common.Queries;
using TourCore.Infrastructure.Postgres.Persistence;

namespace TourCore.Infrastructure.Postgres.Common;

public sealed class EfCoreQuerySpecificationExecutor : IQuerySpecificationExecutor
{
    private readonly TourCoreDbContext _dbContext;
    public EfCoreQuerySpecificationExecutor(TourCoreDbContext dbContext) => _dbContext = dbContext;

    public Task<int> CountAsync<TEntity, TResult>(ProjectedQuerySpecification<TEntity, TResult> query, CancellationToken cancellationToken)
        where TEntity : class => ApplyFilters(_dbContext.Set<TEntity>().AsNoTracking(), query.Source).CountAsync(cancellationToken);

    public async Task<IReadOnlyCollection<TResult>> ToArrayAsync<TEntity, TResult>(
        ProjectedQuerySpecification<TEntity, TResult> query, int? skip, int? take, CancellationToken cancellationToken)
        where TEntity : class
    {
        var source = ApplyOrdering(ApplyFilters(_dbContext.Set<TEntity>().AsNoTracking(), query.Source), query.Source);
        if (skip.HasValue) source = source.Skip(skip.Value);
        if (take.HasValue) source = source.Take(take.Value);
        return await source.Select(query.Projection).ToArrayAsync(cancellationToken);
    }

    private static IQueryable<TEntity> ApplyFilters<TEntity>(IQueryable<TEntity> source, QuerySpecification<TEntity> specification)
        where TEntity : class
    {
        foreach (var filter in specification.Filters) source = source.Where(filter);
        return source;
    }

    private static IQueryable<TEntity> ApplyOrdering<TEntity>(IQueryable<TEntity> source, QuerySpecification<TEntity> specification)
        where TEntity : class
    {
        IOrderedQueryable<TEntity> ordered = null;
        foreach (var order in specification.Orders)
        {
            var method = ordered == null
                ? order.Descending ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy)
                : order.Descending ? nameof(Queryable.ThenByDescending) : nameof(Queryable.ThenBy);
            var input = (IQueryable<TEntity>)ordered ?? source;
            var call = Expression.Call(typeof(Queryable), method,
                new[] { typeof(TEntity), order.KeySelector.ReturnType },
                input.Expression, Expression.Quote(order.KeySelector));
            ordered = (IOrderedQueryable<TEntity>)input.Provider.CreateQuery<TEntity>(call);
        }
        return ordered ?? source;
    }
}
