namespace TourCore.Application.Common.Data
{
    /// <summary>Persistence port marker that does not expose provider-owned IQueryable.</summary>
    public interface IRepository<TEntity> where TEntity : class
    {
    }
}
