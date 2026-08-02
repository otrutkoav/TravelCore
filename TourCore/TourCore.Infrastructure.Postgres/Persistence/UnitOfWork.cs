using TourCore.Application.Abstractions.Persistence;

namespace TourCore.Infrastructure.Postgres.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TourCoreDbContext _context;
    public UnitOfWork(TourCoreDbContext context) => _context = context;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        _context.SaveChangesAsync(cancellationToken);
}
