using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Common.Queries;

namespace TourCore.Application.Common.Data
{
    public interface IQuerySpecificationExecutor
    {
        Task<int> CountAsync<TEntity, TResult>(ProjectedQuerySpecification<TEntity, TResult> query, CancellationToken cancellationToken)
            where TEntity : class;
        Task<IReadOnlyCollection<TResult>> ToArrayAsync<TEntity, TResult>(ProjectedQuerySpecification<TEntity, TResult> query, int? skip, int? take, CancellationToken cancellationToken)
            where TEntity : class;
    }
}
