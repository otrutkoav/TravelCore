using TourCore.Pricing.Domain.BusPrices;

namespace TourCore.Pricing.Application.Abstractions;

public interface IBusTransferPriceRepository
{
    Task<IReadOnlyList<BusTransferPrice>> ListAsync(int? priceBlockId, int? pointId, int? transportId, CancellationToken ct);
    Task<BusTransferPrice?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(BusTransferPrice price, CancellationToken ct);
    void Remove(BusTransferPrice price);
    Task SaveChangesAsync(CancellationToken ct);
}
