using TourCore.Pricing.Domain.RailwayPrices;

namespace TourCore.Pricing.Application.Abstractions;

public interface IRailwayTransferPriceRepository
{
    Task<IReadOnlyList<RailwayTransferPrice>> ListAsync(int? priceBlockId, int? transferId, int? transportId, CancellationToken ct);
    Task<RailwayTransferPrice?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(RailwayTransferPrice price, CancellationToken ct);
    void Remove(RailwayTransferPrice price);
    Task SaveChangesAsync(CancellationToken ct);
}
