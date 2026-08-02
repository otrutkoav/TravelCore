using System.Data.Entity;

namespace TourCore.Infrastructure.SqlServer.Search.Persistence
{
    public sealed class SearchReadDbContext : DbContext
    {
        public SearchReadDbContext() : base("name=TourCoreDb")
        {
            Database.SetInitializer<SearchReadDbContext>(null);
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
    }
}
