using TourCore.Pricing.Domain.PriceBlocks;

namespace TourCore.Pricing.Application.Abstractions;

public interface IPriceBlockRepository
{
    Task<IReadOnlyList<PriceBlock>> ListAsync(int? pricePackageId, CancellationToken cancellationToken);
    Task<PriceBlock?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(PriceBlock priceBlock, CancellationToken cancellationToken);
    void Remove(PriceBlock priceBlock);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
