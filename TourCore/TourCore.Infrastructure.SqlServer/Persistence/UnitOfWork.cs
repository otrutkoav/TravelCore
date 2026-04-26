using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions.Persistence;

namespace TourCore.Infrastructure.SqlServer.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TourCoreDbContext _context;

        public UnitOfWork(TourCoreDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}