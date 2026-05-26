using System.Linq;
using TourCore.Application.Common.Data;

namespace TourCore.Infrastructure.SqlServer.Persistence.Repositories
{
    public class QueryableRepository<TEntity>
        : IQueryableRepository<TEntity>
        where TEntity : class
    {
        private readonly TourCoreDbContext _dbContext;

        public QueryableRepository(TourCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>();
        }
    }
}