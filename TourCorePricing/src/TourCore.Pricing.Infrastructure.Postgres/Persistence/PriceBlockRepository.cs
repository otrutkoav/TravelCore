using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.PriceBlocks;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class PriceBlockRepository : IPriceBlockRepository
{
    private readonly PricingDbContext _dbContext;
    public PriceBlockRepository(PricingDbContext dbContext) { _dbContext = dbContext; }

    public async Task<IReadOnlyList<PriceBlock>> ListAsync(int? pricePackageId, CancellationToken cancellationToken)
    {
        var query = _dbContext.PriceBlocks.AsNoTracking();
        if (pricePackageId.HasValue)
            query = query.Where(item => item.PricePackageId == pricePackageId.Value);
        return await query.OrderByDescending(item => item.CreatedAt).ThenBy(item => item.Id).ToArrayAsync(cancellationToken);
    }

    public Task<PriceBlock?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.PriceBlocks.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public async Task AddAsync(PriceBlock priceBlock, CancellationToken cancellationToken)
    {
        await _dbContext.PriceBlocks.AddAsync(priceBlock, cancellationToken);
    }

    public void Remove(PriceBlock priceBlock) { _dbContext.PriceBlocks.Remove(priceBlock); }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
