using System.Linq;

namespace TourCore.Application.Common.Data
{
    public interface IQueryableRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Query();
    }
}