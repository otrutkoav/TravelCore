using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.BusPrices;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

internal sealed class BusTransferPriceRepository : IBusTransferPriceRepository
{
    private readonly PricingDbContext _db;
    public BusTransferPriceRepository(PricingDbContext db) { _db = db; }
    public async Task<IReadOnlyList<BusTransferPrice>> ListAsync(int? blockId, int? pointId, int? transportId, CancellationToken ct)
    {
        var query = _db.BusTransferPrices.AsNoTracking();
        if (blockId.HasValue) query = query.Where(x => x.PriceBlockId == blockId.Value);
        if (pointId.HasValue) query = query.Where(x => x.BusTransferPointId == pointId.Value);
        if (transportId.HasValue) query = query.Where(x => x.TransportId == transportId.Value);
        return await query.OrderByDescending(x => x.Priority).ThenBy(x => x.Id).ToArrayAsync(ct);
    }
    public Task<BusTransferPrice?> GetByIdAsync(int id, CancellationToken ct) => _db.BusTransferPrices.FirstOrDefaultAsync(x => x.Id == id, ct);
    public async Task AddAsync(BusTransferPrice price, CancellationToken ct) => await _db.BusTransferPrices.AddAsync(price, ct);
    public void Remove(BusTransferPrice price) => _db.BusTransferPrices.Remove(price);
    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}
