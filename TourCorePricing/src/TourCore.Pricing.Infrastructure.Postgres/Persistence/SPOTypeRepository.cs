using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.SPOTypes;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class SPOTypeRepository : ISPOTypeRepository
{
    private readonly PricingDbContext _dbContext;

    public SPOTypeRepository(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<SPOType>> ListAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SPOTypes
            .AsNoTracking()
            .OrderBy(spoType => spoType.Id)
            .ToArrayAsync(cancellationToken);
    }

    public Task<SPOType?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.SPOTypes.FirstOrDefaultAsync(spoType => spoType.Id == id, cancellationToken);
    }

    public Task<bool> NameExistsAsync(
        string name,
        int? excludedId,
        CancellationToken cancellationToken)
    {
        var normalizedName = name.ToLower();
        return _dbContext.SPOTypes.AnyAsync(
            spoType => spoType.Name.ToLower() == normalizedName
                && (!excludedId.HasValue || spoType.Id != excludedId.Value),
            cancellationToken);
    }

    public async Task AddAsync(SPOType spoType, CancellationToken cancellationToken)
    {
        await _dbContext.SPOTypes.AddAsync(spoType, cancellationToken);
    }

    public void Remove(SPOType spoType)
    {
        _dbContext.SPOTypes.Remove(spoType);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
