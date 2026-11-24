using TourCore.Pricing.Domain.FlightPrices;

namespace TourCore.Pricing.Application.Abstractions;

public interface IFlightPriceRepository
{
    Task<IReadOnlyList<FlightPrice>> ListAsync(int? priceBlockId, int? charterId, int? airClassId, CancellationToken ct);
    Task<FlightPrice?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(FlightPrice price, CancellationToken ct);
    void Remove(FlightPrice price);
    Task SaveChangesAsync(CancellationToken ct);
}
