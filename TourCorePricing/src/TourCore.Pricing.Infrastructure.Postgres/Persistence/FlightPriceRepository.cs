using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.FlightPrices;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class FlightPriceRepository : IFlightPriceRepository
{
    private readonly PricingDbContext _db;
    public FlightPriceRepository(PricingDbContext db) { _db = db; }

    public async Task<IReadOnlyList<FlightPrice>> ListAsync(int? priceBlockId, int? charterId, int? airClassId, CancellationToken ct)
    {
        var query = _db.FlightPrices.AsNoTracking();
        if (priceBlockId.HasValue) query = query.Where(x => x.PriceBlockId == priceBlockId.Value);
        if (charterId.HasValue) query = query.Where(x => x.CharterId == charterId.Value);
        if (airClassId.HasValue) query = query.Where(x => x.AirClassId == airClassId.Value);
        return await query.OrderByDescending(x => x.Priority).ThenBy(x => x.Id).ToArrayAsync(ct);
    }

    public Task<FlightPrice?> GetByIdAsync(int id, CancellationToken ct) =>
        _db.FlightPrices.FirstOrDefaultAsync(x => x.Id == id, ct);
    public async Task AddAsync(FlightPrice price, CancellationToken ct) => await _db.FlightPrices.AddAsync(price, ct);
    public void Remove(FlightPrice price) => _db.FlightPrices.Remove(price);
    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
